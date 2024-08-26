using BE_AgentGuard.Models;
using BE_AgentGuard.RouteModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace BE_AgentGuard.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            foreach (var item in SaveTokens.tokenAndID)
            {
                if (item.id == loginModel.id) 
                {
                    return Ok(new Tokens { Token = item.token });
                }
            }
            return null;
        }
    }
}
