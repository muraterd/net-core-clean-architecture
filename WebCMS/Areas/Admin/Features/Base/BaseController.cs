using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCMS.Areas.Admin.Features.Base
{
    public class BaseController : Controller
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;
    }
}
