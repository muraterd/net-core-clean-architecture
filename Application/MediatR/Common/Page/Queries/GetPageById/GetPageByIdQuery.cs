using Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UI.Data;

namespace Application.MediatR.Common.Page.Queries
{
    public class GetPageByIdQuery : IRequest<PageEntity>
    {
        public long Id { get; set; }
    }

    public class GetPageByIdQueryHandler : IRequestHandler<GetPageByIdQuery, PageEntity>
    {
        private readonly AppDbContext dbContext;

        public GetPageByIdQueryHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PageEntity> Handle(GetPageByIdQuery query, CancellationToken cancellationToken)
        {
            var usersQuery = dbContext.Pages
                .Where(w => !w.IsDeleted);

            return await usersQuery.FirstOrDefaultAsync(w => w.Id == query.Id);
        }
    }
}
