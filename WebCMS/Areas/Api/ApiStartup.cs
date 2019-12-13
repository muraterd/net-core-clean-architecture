using System;
using AutoMapper;
using WebCMS.Areas.Api.Features.User.Requests;
using WebCMS.Areas.Api.Models;
using WebCMS.Data.Entities;
using WebCMS.Services.User.Commands;

namespace WebCMS.Areas.Api
{
    public class ApiStartup
    {
        public static void ConfigureAutoMapper(IMapperConfigurationExpression o)
        {
            o.CreateMap<UpdateUserRequest, UpdateUserCommand>();

            o.CreateMap<UserEntity, BaseUserModel>().Include<UserEntity, UserModel>();
            o.CreateMap<UserEntity, UserModel>();
        }
    }
}
