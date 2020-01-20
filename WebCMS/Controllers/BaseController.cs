using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebCMS.Controllers
{
    public class BaseController : ControllerBase
    {

        protected IMediator Mediator => HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;

        public BaseController()
        {
            
        }
    }
}
