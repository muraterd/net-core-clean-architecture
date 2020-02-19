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

namespace Application.MediatR.Admin.User.Commands
{
    public class UpdateAvatarCommand : IRequest<PhotoEntity>
    {
        public long UserId { get; set; }
        public IFormFile Image { get; set; }
    }

    public class UpdateAvatarCommandHandler : IRequestHandler<UpdateAvatarCommand, PhotoEntity>
    {
        private readonly AppDbContext dbContext;

        public UpdateAvatarCommandHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PhotoEntity> Handle(UpdateAvatarCommand command, CancellationToken cancellationToken)
        {
            using (var image = Image.Load(command.Image.OpenReadStream()))
            {
                var filename = FileUploadHelper.GetUniqueFileName(command.Image.FileName);

                image.Save($"{FileUploadSize.ORIGINAL}/{filename}");

                image.CropSquareCentered();

                image.Clone(i => i.Resize(700, 700)).Save($"{FileUploadSize.LARGE}/{filename}");
                image.Clone(i => i.Resize(500, 500)).Save($"{FileUploadSize.MEDIUM}/{filename}");
                image.Clone(i => i.Resize(300, 300)).Save($"{FileUploadSize.SMALL}/{filename}");

                var user = await dbContext.Users.Include(w => w.Photos).FirstOrDefaultAsync(w => w.Id == command.UserId);

                var newPhoto = new PhotoEntity()
                {
                    FileName = filename,
                    Width = image.Width,
                    Height = image.Height,
                    IsProfilePhoto = true
                };
                user.Photos.Add(newPhoto);

                await dbContext.SaveChangesAsync();

                return newPhoto;
            }
        }
    }
}
