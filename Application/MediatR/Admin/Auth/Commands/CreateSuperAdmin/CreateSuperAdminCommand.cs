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
using UI.Data;

namespace Application.MediatR.Admin.Auth.Commands.CreateSuperAdmin
{
    public class CreateSuperAdminCommand : IRequest<UserEntity>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
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

        public async Task<UserEntity> Handle(CreateSuperAdminCommand command, CancellationToken cancellationToken)
        {
            command.Email = command.Email.Trim();
            command.Password = command.Password.Trim();

            UserEntity user = await dbContext.Users.FirstOrDefaultAsync(w => w.Email == command.Email && !w.IsDeleted);

            if (user != null)
            {
                throw new DuplicateResultException($"Bu email ile daha önce kayıt olunmuş Email: {command.Email}");
            }

            user = new UserEntity()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = hashProvider.Hash(command.Password),
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
