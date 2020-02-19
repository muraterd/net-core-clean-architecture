using Data;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCMS.Areas.Admin.Filters;

namespace WebCMS.Areas.Admin.Features.Base
{
    [Authorize]
    [ValidateCookie]
    public class AuthorizedController : BaseController
    {
        protected CurrentUser CurrentUser => HttpContext.RequestServices.GetService(typeof(CurrentUser)) as CurrentUser;
    }
}