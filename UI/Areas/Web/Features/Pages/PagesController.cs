using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UI.Areas.Web.Models.Page;

namespace UI.Areas.Web.Features.Page
{
    [Area("Web")]
    [Route("/[controller]")]
    public class PagesController: Controller
    {
        [HttpGet("{slug}")]
        public IActionResult Index(String slug)
        {
            //var page = pageService.GetPage(slug);
            //var pageModel = Mapper.Map<PageModel>(page);

            //return View(pageModel);

            // TODO: Mediatra taşınacak

            return View();
        }
    }
}
