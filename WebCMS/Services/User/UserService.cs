using System;
using System.Collections.Generic;
using System.Linq;
using WebCMS.Data;
using WebCMS.Data.Entities;
using WebCMS.Services.User.Commands;

namespace WebCMS.Services.User
{
    public class UserService
    {
        private readonly AppDbContext dbContext;

        public UserService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<UserEntity> GetList()
        {
            return dbContext.Users.ToList();
        }

        public void UpdateUser(long Id, UpdateUserCommand command)
        {
            UserEntity user = dbContext.Users.FirstOrDefault(w => w.Id == Id);
            user.Email = command.Email ?? user.Email;
            user.IsActive = command.IsActive ?? user.IsActive;
            user.IsDeleted = command.IsDeleted ?? user.IsDeleted;

            dbContext.SaveChanges();
        }
    }
}
