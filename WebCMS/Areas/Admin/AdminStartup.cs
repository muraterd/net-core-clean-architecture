using System;
using AutoMapper;
using WebCMS.Areas.Admin.Features.Users.Requests;
using WebCMS.Areas.Admin.Models;
using WebCMS.Areas.Admin.Models.Page;
using Data.Entities;
using WebCMS.Areas.Admin.Features.Auth;
using Application.Services.User.Commands;

namespace WebCMS.Areas.Admin
{
    public class AdminStartup
    {
        public static void ConfigureAutoMapper(IMapperConfigurationExpression o)
        {
            o.CreateMap<UpdateUserRequest, UpdateUserCommand>();
            o.CreateMap<RegisterViewModel, CreateUserCommand>();

            o.CreateMap<BaseEntity, BaseModel>();

            o.CreateMap<PageEntity, BasePageModel>();
            o.CreateMap<PageEntity, PageModel>();
        }
    }
}
