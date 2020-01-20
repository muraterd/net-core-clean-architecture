using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

public static class AuthExtensions
{
    public static async Task LoginWithCookie(this UserEntity user, Microsoft.AspNetCore.Http.HttpContext context)
    {
        var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("Email", user.Email)
            };

        var identity = new ClaimsIdentity(claims, "Jwt");
        var principal = new ClaimsPrincipal(identity);

        context.User = principal;

        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}