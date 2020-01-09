using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using WebCMS.Data;
using WebCMS.Data.Entities;
using WebCMS.Helpers;

namespace WebCMS.Filters
{
    public class JwtAuthorizeAttribute : TypeFilterAttribute
    {
        public JwtAuthorizeAttribute(string Roles = "") : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { Roles };
        }

        private class ClaimRequirementFilter : IAuthorizationFilter
        {
            private readonly string _roles;
            private readonly AppDbContext dbContext;

            public ClaimRequirementFilter(AppDbContext dbContext, string Roles)
            {
                _roles = Roles;
                this.dbContext = dbContext;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                // Allow Anonymous skips all authorization
                if (context.Filters.Any(item => item is IAllowAnonymousFilter))
                {
                    return;
                }

                var authHeader = context.HttpContext.Request.Headers["Authorization"].ToString().Trim();

                if (String.IsNullOrEmpty(authHeader))
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new ObjectResult(new { error = "Not Authorized" });
                }

                var token = authHeader.Replace("Bearer ", string.Empty);

                try
                {
                    JwtModel jwt = JwtHelper.Decode(token);
                    UserEntity user = dbContext.Users.FirstOrDefault(w => w.Id == jwt.Id &&
                    w.IsActive &&
                    !w.IsDeleted);

                    if (user == null)
                    {
                        context.HttpContext.Response.StatusCode = 401;
                        context.Result = new ObjectResult(new { error = "Not Authorized" });
                        return;
                    }

                    context.HttpContext.Items["user"] = user;

                    return;
                }
                catch
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new ObjectResult(new { error = "Not Authorized" });
                }
            }
        }
    }
}
