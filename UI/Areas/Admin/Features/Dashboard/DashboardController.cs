using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI.Areas.Admin.Features.Base;

namespace UI.Areas.Admin.Features.Dashboard
{
    [Area("Admin")]
    [Route("admin")]
    public class DashboardController : AuthorizedController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
