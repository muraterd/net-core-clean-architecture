using Application.Exceptions;
using Data.Entities;
using Data.Enums;
using Data.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebCMS.Data;

namespace Application.MediatR.Admin.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserEntity>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserCommand, UserEntity>
    {
        private readonly AppDbContext dbContext;

        public UpdateUserProfileCommandHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserEntity> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(w => w.Id == command.Id);

            if (user == null) throw new NotFoundException();

            user.FirstName = command.FirstName ?? user.FirstName;
            user.LastName = command.LastName ?? user.LastName;
            user.Email = command.Email ?? user.Email;
            user.IsActive = command.IsActive ?? user.IsActive;
            user.IsDeleted = command.IsDeleted ?? user.IsDeleted;

            await dbContext.SaveChangesAsync();

            return user;
        }
    }
}
