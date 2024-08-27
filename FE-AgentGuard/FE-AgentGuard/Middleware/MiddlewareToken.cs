using FE_AgentGuard.Models.ServerModel;

namespace FE_AgentGuard.Middleware
{
    public class MiddlewareToken
    {
        private readonly RequestDelegate _next;
        private readonly TokenService _tokenService;

        public MiddlewareToken(RequestDelegate next, TokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;

            if (path.StartsWithSegments("/Login") || path.Equals("/") || context.Response.Headers.Referer.ToString().Contains("login"))
            {
                await _next(context);
                return;
            }
            var token = _tokenService.Token;

            if (token is null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Redirect("/login/login",false);
                return;
            }
            await _next(context);
        }



    }
}
