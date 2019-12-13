using System;
using AutoMapper;
using WebCMS.Areas.Web.Models;
using WebCMS.Areas.Web.Models.Page;
using WebCMS.Data.Entities;

namespace WebCMS.Areas.Web
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
