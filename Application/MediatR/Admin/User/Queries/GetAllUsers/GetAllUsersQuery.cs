using Data.Entities;
using Data.Enums;
using Data.Models.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebCMS.Data;

namespace Application.MediatR.Admin.User.Queries.GetAllUsers
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
            var usersQuery = dbContext.Users
                .Include(w => w.Roles)
                .Where(w => !w.IsDeleted && w.IsActive);

            return new PageResult<UserEntity>() {
                List = await usersQuery.Paginate(query.Page, query.PageSize).ToListAsync(),
                TotalPageCount = (await usersQuery.CountAsync()).CalculateTotalPageCount(query.PageSize)
            };
        }
    }
}
