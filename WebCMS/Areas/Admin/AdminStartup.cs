using AutoMapper;
using WebCMS.Areas.Admin.Models;
using WebCMS.Areas.Admin.Models.Page;
using Data.Entities;
using Application.MediatR.Admin.User.Commands.UpdateUser;
using WebCMS.Areas.Admin.Features.Users.Profile;
using Data.Models.Common;
using WebCMS.Areas.Admin.Models.Base;
using System.Reflection;

namespace WebCMS.Areas.Admin
{
    public class AdminStartup
    {
        public static void ConfigureAutoMapper(IMapperConfigurationExpression o)
        {
            o.CreateMap<BaseEntity, BaseModel>();

            o.CreateMap<PageEntity, BasePageModel>();
            o.CreateMap<PageEntity, PageModel>();

            // Entity <-> Command <-> Query <-> ViewModel Mappings
            o.CreateMap<UserEntity, ProfileViewModel>();
            o.CreateMap<ProfileViewModel, UpdateUserCommand>();


            o.CreateMap(typeof(PageResult<>), typeof(ListPageViewModel<>), MemberList.Source);

        }
    }
}
