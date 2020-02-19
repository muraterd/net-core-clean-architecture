using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.MediatR.Admin.Page.Queries;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Areas.Admin.Features.Base;
using WebCMS.Areas.Admin.Features.Pages.Create;
using WebCMS.Areas.Admin.Features.Pages.Update;
using WebCMS.Areas.Admin.Features.Users.Requests;
using WebCMS.Areas.Admin.Models.Base;
using WebCMS.Areas.Admin.Models.Page;
using WebCMS.Controllers;
using WebCMS.Data;
using WebCMS.Services.Page;
using WebCMS.Services.Page.Commands;

namespace WebCMS.Areas.Admin.Features.Users
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class PagesController : AuthorizedController
    {
        private readonly PageService pageService;

        public PagesController(PageService pageService)
        {
            this.pageService = pageService;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] GetAllPagesQuery query)
        {
            var result = await Mediator.Send(query);
            var viewModel = Mapper.Map<ListPageViewModel<PageEntity>>(result);

            if (TempData["SuccessMessage"] != null)
            {
                viewModel.SuccessMessage = TempData["SuccessMessage"].ToString();
            }

            return View(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Update(long id)
        {
            PageEntity page = await Mediator.Send(new GetPageByIdQuery() { Id = id });

            return View(page.ToUpdatePageViewModel());
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update(UpdatePageViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await Mediator.Send(viewModel.ToUpdatePageCommand());

            return View(viewModel);
        }

        [HttpGet("new")]
        public IActionResult Create()
        {
            return View(new CreatePageViewModel());
        }

        [HttpPost("new")]
        public async Task<IActionResult> Create(CreatePageViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await Mediator.Send(viewModel.ToCreatePageCommand());

            TempData["SuccessMessage"] = $"{viewModel.Title} sayfası başarıyla oluşturuldu";

            return RedirectToAction("List");
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(long id)
        {
            pageService.DeletePage(id);
            return RedirectToAction("List");
        }
    }
}
