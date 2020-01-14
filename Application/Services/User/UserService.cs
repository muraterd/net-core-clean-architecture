using Application.Interfaces.Providers;
using Application.Services.User.Commands;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCMS.Data;

namespace Application.Services.User
{
    public class UserService
    {
        private readonly IHashProvider hashProvider;
        private readonly AppDbContext dbContext;

        public UserService(AppDbContext dbContext, IHashProvider hashProvider)
        {
            this.dbContext = dbContext;
            this.hashProvider = hashProvider;
        }

        public List<UserEntity> GetList()
        {
            return dbContext.Users.ToList();
        }

        public async Task<UserEntity> GetByEmail(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(w => w.Email == email);
        }

        public async Task<UserEntity> CreateUser(CreateUserCommand command)
        {
            var userEntry = await dbContext.Users.AddAsync(new UserEntity()
            {
                Email = command.Email,
                Password = hashProvider.Hash(command.Password.Trim())
            });

            return userEntry.Entity;
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
