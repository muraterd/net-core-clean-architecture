using Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebCMS.Data;

namespace Application.MediatR.Admin.Page.Commands
{
    public class DeletePageCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeletePageCommandHandler : IRequestHandler<DeletePageCommand>
    {
        private readonly AppDbContext dbContext;

        public DeletePageCommandHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeletePageCommand command, CancellationToken cancellationToken)
        {
            var page = await dbContext.Pages.FirstOrDefaultAsync(w => w.Id == command.Id && !w.IsDeleted);

            if (page == null) throw new NotFoundException();

            page.IsDeleted = true;
            await dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
