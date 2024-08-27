using FE_AgentGuard.InterFaces;
using FE_AgentGuard.Models.Models;
using FE_AgentGuard.Models.ServerModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Net.Http;

namespace FE_AgentGuard.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string UrlBase;
        private TokenService TokenService;

        public LoginController(HttpClient httpClient,TokenService tokenService)
        {
            UrlBase = "http://localhost:5149/login";
            _httpClient = httpClient;
            TokenService = tokenService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Login(LoginId login)
        {
            var response = await _httpClient.PostAsJsonAsync(UrlBase, login);
            response.EnsureSuccessStatusCode();
            TokenService.SetToken(await response.Content.ReadFromJsonAsync<Tokens>());
            return RedirectToAction("GeneralView","General");
        }
    }
}
