using Application.Utils.Extensions.EFCoreExtensions;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UI.Data;

/// <summary>
/// Populate user object from identity
/// </summary>
public class CurrentUserMiddleware
{
    private readonly RequestDelegate next;

    public CurrentUserMiddleware(RequestDelegate next)
    {
        this.next = next;
    }



    public async Task Invoke(HttpContext httpContext, CurrentUser user, AppDbContext dbContext)
    {
        var identity = httpContext.User.Identity;

        if (identity.IsAuthenticated)
        {
            var userId = Convert.ToInt64(((ClaimsIdentity)identity).Claims
                .FirstOrDefault(c => c.Type == "Id").Value ?? "0");

            var dbUser = await dbContext.Users
                .IncludeProfilePhoto()
                .FirstOrDefaultAsync(w => w.Id == userId);

            var profilePhoto = dbUser.Photos.FirstOrDefault(w => w.IsProfilePhoto);

            if (dbUser != null)
            {
                user.Id = dbUser.Id;
                user.FirstName = dbUser.FirstName;
                user.LastName = dbUser.LastName;
                user.Email = dbUser.Email;
                user.ProfilePhoto = profilePhoto ?? new PhotoEntity();
                user.Roles = dbUser.Roles.Select(c => c.Role).ToList();
            }
        }

        await next(httpContext);
    }
}

public static class CurrentUserMiddlewareExtensions
{
    public static IApplicationBuilder UseCurrentUserMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CurrentUserMiddleware>();
    }
}