using Application.Exceptions;
using Application.Interfaces.Providers;
using Application.Services.User;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth
{
    public class SigninManager
    {
        private readonly UserService userService;
        private readonly IHashProvider hashProvider;

        public SigninManager(UserService userService, IHashProvider hashProvider)
        {
            this.userService = userService;
            this.hashProvider = hashProvider;
        }

        public async Task<UserEntity> Signin(string email, string password)
        {
            email = email.Trim();
            password = password.Trim();

            var user = await userService.GetByEmail(email);

            if (user == null || user.IsDeleted)
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı");
            }

            if (!user.IsActive)
            {
                throw new AccessForbiddenException($"Kullanıcı aktif değil. Email: {user.Email}");
            }

            if (!hashProvider.Verify(password, user.Password))
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı");
            }

            return user;
        }


    }
}
