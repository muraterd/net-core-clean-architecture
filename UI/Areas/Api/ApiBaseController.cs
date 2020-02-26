using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Api
{
    [ApiExceptionFilter]
    public class ApiBaseController : ControllerBase
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;

        public ApiBaseController()
        {

        }
    }
}
