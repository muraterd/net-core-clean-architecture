using Application.Exceptions;
using Application.Interfaces.Providers;
using Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebCMS.Data;

namespace Application.MediatR.Api.Auth.Commands.Login
{
    public class LoginCommand : IRequest<UserEntity>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserEntity>
    {
        private readonly AppDbContext dbContext;
        private readonly IHashProvider hashProvider;

        public LoginCommandHandler(AppDbContext dbContext, IHashProvider hashProvider)
        {
            this.dbContext = dbContext;
            this.hashProvider = hashProvider;
        }

        public async Task<UserEntity> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            request.Email = request.Email.Trim();
            request.Password = request.Password.Trim();

            var user = await dbContext.Users.FirstOrDefaultAsync(w => w.Email == request.Email);

            if (user == null || user.IsDeleted)
            {
                throw new AccessDeniedException($"Kullanıcı adı veya şifre hatalı.");
            }

            if (!user.IsActive)
            {
                throw new AccessForbiddenException($"Kullanıcı aktif değil. Email: {user.Email}");
            }

            if (!hashProvider.Verify(request.Password, user.Password))
            {
                throw new AccessDeniedException($"Kullanıcı adı veya şifre hatalı. Email: {user.Email}");
            }

            return user;
        }
    }
}
