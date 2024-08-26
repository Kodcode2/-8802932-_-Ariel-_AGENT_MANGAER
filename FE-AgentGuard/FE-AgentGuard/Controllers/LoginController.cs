using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace FE_AgentGuard.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly string UrlBase;

        public LoginController(HttpClient httpClient)
        {
            UrlBase = "http://localhost:5149/login";
            httpClient = httpClient;
        }

        public IActionResult Login(Login login)
        {
            httpClient.PostAsync(login);
            return View();
        }
    }
}
