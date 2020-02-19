﻿using Application.Exceptions;
using Application.Utils.Extensions;
using Data.Entities;
using Data.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UI.Data;

namespace Application.MediatR.Admin.Page.Commands
{
    public class CreatePageCommand : IRequest<PageEntity>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class CreatePageCommandHandler : IRequestHandler<CreatePageCommand, PageEntity>
    {
        private readonly AppDbContext dbContext;

        public CreatePageCommandHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PageEntity> Handle(CreatePageCommand command, CancellationToken cancellationToken)
        {
            PageEntity page = new PageEntity();
            page.Slug = command.Slug ?? page.Slug;
            page.Title = command.Title ?? page.Title;
            page.Content = command.Content ?? page.Content;
            page.IsActive = command.IsActive ?? page.IsActive;
            page.IsDeleted = command.IsDeleted ?? page.IsDeleted;

            dbContext.Pages.Add(page);
            await dbContext.SaveChangesAsync();
            return page;
        }
    }
}
