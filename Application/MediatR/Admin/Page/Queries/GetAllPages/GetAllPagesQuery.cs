using Application.MediatR.Base.Queries;
using Application.Utils.Extensions.EFCoreExtensions;
using Data.Entities;
using Data.Models.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UI.Data;

namespace Application.MediatR.Admin.Page.Queries
{
    public class GetAllPagesQuery : PageQuery<PageResult<PageEntity>>
    { 
    
    }

    public class GetAllPagesQueryHandler : IRequestHandler<GetAllPagesQuery, PageResult<PageEntity>>
    {
        private readonly AppDbContext dbContext;

        public GetAllPagesQueryHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PageResult<PageEntity>> Handle(GetAllPagesQuery query, CancellationToken cancellationToken)
        {
            var pagesQuery = dbContext.Pages
                .Where(w => !w.IsDeleted);

            return new PageResult<PageEntity>()
            {
                List = await pagesQuery.Paginate(query.Page, query.PageSize).ToListAsync(),
                CurrentPage = query.Page,
                TotalPageCount = (await pagesQuery.CountAsync()).CalculateTotalPageCount(query.PageSize)
            };
        }
    }
}
