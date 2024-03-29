﻿using System.Threading.Tasks;
using Application.MediatR.Admin.Page.Commands;
using Application.MediatR.Admin.Page.Queries;
using Application.MediatR.Common.Page.Queries;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using UI.Areas.Admin.Features.Base;
using UI.Areas.Admin.Features.Pages.Create;
using UI.Areas.Admin.Features.Pages.Update;
using UI.Areas.Admin.Models.Base;

namespace UI.Areas.Admin.Features.Users
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class PagesController : AuthorizedController
    {
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
        public async Task<IActionResult> Delete(long id)
        {
            await Mediator.Send(new DeletePageCommand() { Id = id });

            return RedirectToAction("List");
        }
    }
}
