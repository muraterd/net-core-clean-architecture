using Application.Exceptions;
using Application.Interfaces.Mail;
using Application.Interfaces.Providers;
using Data;
using Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UI.Data;

namespace Application.MediatR.Admin.Auth.Commands
{
    public class ResetPasswordCommand : IRequest<UserEntity>
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, UserEntity>
    {
        private readonly AppDbContext dbContext;
        private readonly IHashProvider hashProvider;

        public ResetPasswordCommandHandler(AppDbContext dbContext, IHashProvider hashProvider)
        {
            this.dbContext = dbContext;
            this.hashProvider = hashProvider;
        }

        public async Task<UserEntity> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            command.NewPassword = command.NewPassword.Trim();

            var user = await dbContext.Users
                .FirstOrDefaultAsync(w =>
                    w.PasswordResetToken == command.Token &&
                    !w.IsDeleted
                );

            if (user == null)
            {
                throw new NotFoundException($"No user found with a PasswordResetToken: {command.Token}");
            }

            if (user.PasswordResetTokenExpireDate < DateTime.UtcNow)
            {
                throw new TokenExpiredException($"PasswordResetToken expired");
            }

            user.Password = hashProvider.Hash(command.NewPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpireDate = null;

            await dbContext.SaveChangesAsync();

            return user;
        }
    }
}
