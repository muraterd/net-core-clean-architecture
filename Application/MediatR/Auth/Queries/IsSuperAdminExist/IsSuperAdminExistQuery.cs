using Data.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebCMS.Data;

namespace Application.MediatR.Auth.Queries.IsSuperAdminExist
{
    public class IsSuperAdminExistQuery : IRequest<bool>
    {}

    public class IsSuperAdminExistQueryHandler : IRequestHandler<IsSuperAdminExistQuery, bool>
    {
        private readonly AppDbContext dbContext;

        public IsSuperAdminExistQueryHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> Handle(IsSuperAdminExistQuery query, CancellationToken cancellationToken)
        {
            return await dbContext.UserRoles.AnyAsync(w => w.Role == RoleType.SuperAdmin.ToString());
        }
    }
}
