﻿using Application.Utils.Extensions.EFCoreExtensions;
using Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UI.Data;

namespace Application.MediatR.Admin.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserEntity>
    {
        public long Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserEntity>
    {
        private readonly AppDbContext dbContext;

        public GetUserByIdQueryHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserEntity> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var usersQuery = dbContext.Users
                .Where(w => !w.IsDeleted)
                .Include(w => w.Roles)
                .IncludeProfilePhoto();

            return await usersQuery.FirstOrDefaultAsync(w => w.Id == query.Id);
        }
    }
}
