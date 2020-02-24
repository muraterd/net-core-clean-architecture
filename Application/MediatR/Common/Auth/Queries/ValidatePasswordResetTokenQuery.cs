using Data.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using UI.Data;

namespace Application.MediatR.Common.Auth.Queries.IsSuperAdminExist
{
    public class ValidatePasswordResetTokenQuery : IRequest<bool>
    {
        public string Token { get; set; }
    }

    public class ValidatePasswordResetTokenQueryHandler : IRequestHandler<ValidatePasswordResetTokenQuery, bool>
    {
        private readonly AppDbContext dbContext;

        public ValidatePasswordResetTokenQueryHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> Handle(ValidatePasswordResetTokenQuery query, CancellationToken cancellationToken)
        {
            return await dbContext.Users.AnyAsync(w =>
                w.PasswordResetToken == query.Token &&
                w.PasswordResetTokenExpireDate > DateTime.UtcNow
            );
        }
    }
}
