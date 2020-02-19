using Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace UI.Areas.Admin.Filters
{
    public class ValidateCookieAttribute : TypeFilterAttribute
    {
        public ValidateCookieAttribute()
            : base(typeof(AuthorizeActionFilter))
        {

        }
    }

    public class AuthorizeActionFilter : IAsyncAuthorizationFilter
    {
        private readonly CurrentUser user;
        private bool IsUserExist
        {
            get
            {
                return user.Id > 0;
            }
        }

        public AuthorizeActionFilter(CurrentUser user)
        {
            this.user = user;
        }


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Sometimes our cookie and token still exists in the client but user cant be found in database.
            // For example if we delete and recreate the database but the user has the cookie or token from the 
            // previous login.
            if (!IsUserExist)
            {
                await context.HttpContext.SignOutAsync();
                context.HttpContext.Response.Redirect("/admin/auth/login");
            }
        }
    }
}
