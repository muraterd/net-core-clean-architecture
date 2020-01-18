using Application.Exceptions;
using Application.Interfaces.Providers;
using Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebCMS.Data;

namespace Application.MediatR.Auth.Commands
{
    public class RegisterCommand : IRequest<UserEntity>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserEntity>
    {
        private readonly AppDbContext dbContext;
        private readonly IHashProvider hashProvider;

        public RegisterCommandHandler(AppDbContext dbContext, IHashProvider hashProvider)
        {
            this.dbContext = dbContext;
            this.hashProvider = hashProvider;
        }

        public async Task<UserEntity> Handle(RegisterCommand request, CancellationToken cancellationToken)
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
                Password = hashProvider.Hash(request.Password)
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }
    }
}
