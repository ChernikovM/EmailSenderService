using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmailSenderService.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {

        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task GoogleResponse()
        {
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Redirect("/api/emailsender/graphql");
        }
    }

}
