using BE_AgentGuard.Models;
using BE_AgentGuard.RouteModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Text;

public class MiddlewareAccess
{
    private readonly RequestDelegate _next;

    public MiddlewareAccess(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await DisFromJson<MissionAssigned>(context);
        List<string> pathsToSimulation = new List<string> { "/mission/update", "/pin", "/move", };
        List<string> pathsToMVC = new List<string> { "/mission/assigned", "/mission/get" };
        List<string> pathsToAll = new List<string> { "/agents", "/targets","/missions" };

        if (pathsToSimulation.Any(path => context.Request.Path.ToString().ToLower().ToLower().Contains(path)))
        {
            Tokens token = await DisFromJson<Tokens>(context);
            string id = FindToken(token.Token);
            if (id is null)
            {
                context.Response.Redirect("/login");
                return;
            }
            await _next(context);
            return;
        }

        if (pathsToMVC.Any(path => context.Request.Path.ToString().ToLower().Contains(path)))
        {
            Tokens token = await DisFromJson<Tokens>(context);
            string id = FindToken(token.Token);
            if (id is null)
            {
                context.Response.Redirect("/login");
                return;
            }
            await _next(context);
            return;
        }

        if (pathsToAll.Any(path => context.Request.Path.ToString().ToLower().ToLower().Contains(path)))
        {
            if(context.Request.Method == "GET")
            {
                await _next(context);
                return;
            }
            Tokens token = await DisFromJson<Tokens>(context);
            string id = FindToken(token.Token);
            if (id == null)
            {
                context.Response.Redirect("/login");
                return;
            }

            if ((context.Request.Method == "POST" && id == "SimulationServer") )
            {
                await _next(context);
                return;
            }
                 await context.Response.WriteAsync("Access denied");
        }

        if (context.Request.Path.StartsWithSegments("/login"))
        {
            LoginModel loginModel = await DisFromJson<LoginModel>(context);
            if (loginModel.id == "SimulationServer" || loginModel.id == "MVCServer")
            {
                CreateToken(loginModel.id);
                await _next(context);
                return;
            }
            else
            {
                await context.Response.WriteAsync("Access denied.");
            }
        }
    }

    public void CreateToken(string id)
    {
        foreach (var item in SaveTokens.tokenAndID)
        {
            if (item.id == id && item.token == null)
            {
                string a = Guid.NewGuid().ToString();
                item.token = a;
            }
        }
    }

    public async Task<T> DisFromJson<T>(HttpContext context) where T : class
    {
        context.Request.EnableBuffering();
        string json;
        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
        {
            json = await reader.ReadToEndAsync();
        }
        context.Request.Body.Position = 0;
        T model = JsonConvert.DeserializeObject<T>(json);
        return model;
    }

    public string FindToken(string token)
    {
        foreach (var item in SaveTokens.tokenAndID)
        {
            if (item.token == token)
            {
                return item.id;
            }
        }
        return null;
    }
}
