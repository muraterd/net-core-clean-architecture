using System;
using System.Linq;
using WebCMS.Data;
using Data.Entities;
using WebCMS.Services.Page.Commands;

namespace WebCMS.Services.Page
{
    public class PageService
    {
        private readonly AppDbContext dbContext;

        public PageService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<PageEntity> GetPages(int currentPage, int itemsPerPage)
        {  
            return dbContext.Pages.Where(w => w.IsActive && !w.IsDeleted).Paginate(currentPage, itemsPerPage);
        }

        public PageEntity GetPage(long Id)
        {
            return dbContext.Pages.FirstOrDefault(w => w.Id == Id && w.IsActive && !w.IsDeleted);
        }

        public PageEntity GetPage(String slug)
        {
            return dbContext.Pages.FirstOrDefault(w => w.Slug == slug && w.IsActive && !w.IsDeleted);
        }

        public PageEntity UpdatePage(long id, UpdatePageCommand command)
        {
            PageEntity page = GetPage(id);
            page.Slug = command.Slug ?? page.Slug;
            page.Title = command.Title ?? page.Title;
            page.Content = command.Content ?? page.Content;
            page.IsActive = command.IsActive ?? page.IsActive;
            page.IsDeleted = command.IsDeleted ?? page.IsDeleted;

            dbContext.SaveChanges();
            return page;
        }

        public PageEntity CreatePage(CreatePageCommand command)
        {
            PageEntity page = new PageEntity();
            page.Slug = command.Slug ?? page.Slug;
            page.Title = command.Title ?? page.Title;
            page.Content = command.Content ?? page.Content;
            page.IsActive = command.IsActive ?? page.IsActive;
            page.IsDeleted = command.IsDeleted ?? page.IsDeleted;

            dbContext.Pages.Add(page);
            dbContext.SaveChanges();
            return page;
        }

        public PageEntity DeletePage(long id)
        {
            PageEntity page = GetPage(id);
            page.IsDeleted = true;

            dbContext.SaveChanges();
            return page;
        }
    }
}
