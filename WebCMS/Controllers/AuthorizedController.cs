using System;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Data.Entities;

namespace WebCMS.Controllers
{
    public class AuthorizedController : ControllerBase
    {
        protected UserEntity currentUser
        {
            get
            {
                var userPayload = HttpContext.Items["user"];
                return userPayload != null ? (UserEntity)HttpContext.Items["user"] : null;
            }
        }

        public AuthorizedController()
        {
        }
    }
}
