using System;
using AutoMapper;
using WebCMS.Areas.Api.Features.User.Requests;
using WebCMS.Areas.Api.Models;
using Data.Entities;
using Application.Services.User.Commands;

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
