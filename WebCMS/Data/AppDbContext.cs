using System;
using Microsoft.EntityFrameworkCore;
using WebCMS.Data.Entities;

namespace WebCMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PageEntity> Pages { get; set; }
    }
}
