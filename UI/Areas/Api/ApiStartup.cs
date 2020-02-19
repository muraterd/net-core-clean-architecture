using System;
using AutoMapper;
using UI.Areas.Api.Models;
using Data.Entities;

namespace UI.Areas.Api
{
    public class ApiStartup
    {
        public static void ConfigureAutoMapper(IMapperConfigurationExpression o)
        {
            o.CreateMap<UserEntity, BaseUserModel>().Include<UserEntity, UserModel>();
            o.CreateMap<UserEntity, UserModel>();
        }
    }
}
