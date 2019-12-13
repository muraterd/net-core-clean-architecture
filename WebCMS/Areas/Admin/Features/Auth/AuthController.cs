using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace WebCMS.Areas.Admin.Features.Auth
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class AuthController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            //HttpContext.User
            var claims = new List<Claim>();
            claims.Add(new Claim("Id", "3"));
            claims.Add(new Claim(ClaimTypes.Name, "muraterd"));
            claims.Add(new Claim("Email", "muraterd@fdsfds.com"));

            var identity = new ClaimsIdentity(claims, "Jwt");
            var principal = new ClaimsPrincipal(identity);

            HttpContext.User = principal;

            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);


            return Redirect("/admin");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/admin");
        }
    }
}
