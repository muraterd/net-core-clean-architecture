using System;
using Microsoft.EntityFrameworkCore;
using Data.Entities;
using System.Collections.Generic;

namespace WebCMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<SettingsEntity> Settings { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<PageEntity> Pages { get; set; }
        public DbSet<PhotoEntity> Photos { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    string hashedAdminPassword = BCrypt.Net.BCrypt.HashPassword("asd123!");

        //    modelBuilder.Entity<SettingsEntity>().HasData(
        //        new SettingsEntity()
        //        {
        //            Id = 1,
        //            Title = "WebCMS Project"
        //        });

        //    modelBuilder.Entity<UserEntity>().HasData(
        //        new UserEntity()
        //        {
        //            Id = 1,
        //            Email = "admin@admin.com",
        //            Password = hashedAdminPassword,
        //            Roles = new List<UserRoleEntity>()
        //            {
        //                new UserRoleEntity()
        //                {
        //                    Role = "Admin"
        //                }
        //            }
        //        });
        //}
    }
}
