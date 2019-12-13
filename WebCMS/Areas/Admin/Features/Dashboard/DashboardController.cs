using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCMS.Areas.Admin.Features.Dashboard
{
    [Authorize]
    [Area("Admin")]
    [Route("admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
