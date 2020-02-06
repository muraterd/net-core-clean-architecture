using Application.Exceptions;
using Application.Interfaces.Providers;
using Data.Entities;
using Data.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebCMS.Data;

namespace Application.MediatR.Auth.Commands.CreateSuperAdmin
{
    public class CreateSuperAdminCommand : IRequest<UserEntity>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }

    public class CreateSuperAdminCommandHandler : IRequestHandler<CreateSuperAdminCommand, UserEntity>
    {
        private readonly AppDbContext dbContext;
        private readonly IHashProvider hashProvider;

        public CreateSuperAdminCommandHandler(AppDbContext dbContext, IHashProvider hashProvider)
        {
            this.dbContext = dbContext;
            this.hashProvider = hashProvider;
        }

        public async Task<UserEntity> Handle(CreateSuperAdminCommand request, CancellationToken cancellationToken)
        {
            request.Email = request.Email.Trim();
            request.Password = request.Password.Trim();

            UserEntity user = await dbContext.Users.FirstOrDefaultAsync(w => w.Email == request.Email && !w.IsDeleted);

            if (user != null)
            {
                throw new DuplicateResultException($"Bu email ile daha önce kayıt olunmuş Email: {request.Email}");
            }

            user = new UserEntity()
            {
                Email = request.Email,
                Password = hashProvider.Hash(request.Password),
                Roles = new List<UserRoleEntity>()
                {
                    new UserRoleEntity()
                    {
                        Role = RoleType.SuperAdmin.ToString()
                    }
                }
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }
    }
}
