﻿using Application.Utils.Extensions.EFCoreExtensions;
using Data.Entities;
using Data.Models.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebCMS.Data;

namespace Application.MediatR.Admin.User.Queries.GetUserById
{
    public class GetAllUsersQuery : IRequest<PageResult<UserEntity>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PageResult<UserEntity>>
    {
        private readonly AppDbContext dbContext;

        public GetAllUsersQueryHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PageResult<UserEntity>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var usersBaseQuery = dbContext.Users
                .Where(w => !w.IsDeleted);

            var usersQuery = usersBaseQuery
                .Include(w => w.Roles)
                .IncludeProfilePhoto();

            return new PageResult<UserEntity>()
            {
                List = await usersQuery.Paginate(query.Page, query.PageSize).ToListAsync(),
                CurrentPage = query.Page,
                TotalPageCount = (await usersBaseQuery.CountAsync()).CalculateTotalPageCount(query.PageSize)
            };
        }
    }
}
