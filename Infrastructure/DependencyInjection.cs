using Application.Interfaces.Providers;
using Infrastructure.Providers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IHashProvider, HashProvider>();
            services.AddScoped<ITokenProvider, TokenProvider>();

            return services;
        }
    }
}
