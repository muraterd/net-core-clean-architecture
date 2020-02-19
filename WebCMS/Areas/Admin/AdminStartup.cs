﻿using AutoMapper;
using WebCMS.Areas.Admin.Models;
using WebCMS.Areas.Admin.Models.Page;
using Data.Entities;
using Application.MediatR.Admin.User.Commands.UpdateUser;
using WebCMS.Areas.Admin.Features.Users.Profile;
using Data.Models.Common;
using WebCMS.Areas.Admin.Models.Base;
using WebCMS.Areas.Admin.Features.Pages.Update;
using Application.MediatR.Admin.Page.Commands;
using WebCMS.Areas.Admin.Features.Pages.Create;

namespace WebCMS.Areas.Admin
{
    public class AdminStartup
    {
        public static void ConfigureAutoMapper(IMapperConfigurationExpression o)
        {
            o.CreateMap<BaseEntity, BaseModel>();

            o.CreateMap<PageEntity, BasePageModel>();
            o.CreateMap<PageEntity, PageModel>();

            // Common
            o.CreateMap(typeof(PageResult<>), typeof(ListPageViewModel<>), MemberList.Source);

            // User
            o.CreateMap<UserEntity, ProfileViewModel>();
            o.CreateMap<ProfileViewModel, UpdateUserCommand>();

            // Page
            o.CreateMap<PageEntity, UpdatePageViewModel>();
            o.CreateMap<UpdatePageViewModel, UpdatePageCommand>();
            o.CreateMap<PageEntity, CreatePageViewModel>();
            o.CreateMap<CreatePageViewModel, CreatePageCommand>();
        }
    }
}
