using Application.Interfaces.Mail;
using Application.Interfaces.Providers;
using Infrastructure.Mail;
using Infrastructure.Providers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration Configuration)
        {
            // IUrlHelper
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            // Infrastructure
            services.AddScoped<IHashProvider, HashProvider>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IMailSender, MailSender>();
            services.AddScoped<IUrlProvider, UrlProvider>();

            return services;
        }
    }
}
