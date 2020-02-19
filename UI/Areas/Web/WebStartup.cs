using System;
using AutoMapper;
using UI.Areas.Web.Models;
using UI.Areas.Web.Models.Page;
using Data.Entities;

namespace UI.Areas.Web
{
    public class WebStartup
    {
        public static void ConfigureAutoMapper(IMapperConfigurationExpression o)
        {
            o.CreateMap<BaseEntity, BaseModel>();

            o.CreateMap<PageEntity, PageModel>();
        }
    }
}
