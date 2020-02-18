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
using WebCMS.Data;

namespace Application.MediatR.Admin.User.Commands
{
    public class DeleteAvatarCommand : IRequest<bool>
    {
        public long UserId { get; set; }
    }

    public class DeleteAvatarCommandHandler : IRequestHandler<DeleteAvatarCommand, bool>
    {
        private readonly AppDbContext dbContext;

        public DeleteAvatarCommandHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteAvatarCommand command, CancellationToken cancellationToken)
        {
            var oldProfilePhotos = await dbContext.Photos.Where(w=>w.UserEntityId == command.UserId && w.IsProfilePhoto).ToListAsync();

            dbContext.Photos.RemoveRange(oldProfilePhotos);

            foreach (var item in oldProfilePhotos)
            {
                File.Delete($"{FileUploadSize.ORIGINAL}/{item.FileName}");
                File.Delete($"{FileUploadSize.LARGE}/{item.FileName}");
                File.Delete($"{FileUploadSize.MEDIUM}/{item.FileName}");
                File.Delete($"{FileUploadSize.SMALL}/{item.FileName}");
            }

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
