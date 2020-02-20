using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UI.Areas.Admin;
using UI.Areas.Api;
using UI.Areas.Web;
using UI.Data;
using Application;
using Infrastructure;
using FluentValidation.AspNetCore;
using Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Application.MediatR.Admin.Auth.Commands.CreateSuperAdmin;
using UI.Areas.Admin.Features.Users.Profile;

namespace UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appConfig = new AppConfig();
            Configuration.Bind("AppConfig", appConfig);

            services
                .AddControllersWithViews()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<CreateSuperAdminCommandValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<ProfileViewModelValidator>();
                })
                .AddRazorRuntimeCompilation()
                .AddFeatureFolders()
                .AddAreaFeatureFolders(new OdeToCode.AddFeatureFolders.AreaFeatureFolderOptions()
                {
                    DefaultAreaViewLocation = "Areas/{2}/Features/{1}/{0}/{0}.cshtml"
                });

            // Configure DI
            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("MsSql")));
            //services.AddDbContext<AppDbContext>(opts => opts.UseInMemoryDatabase("UI.));
            services.AddSingleton(appConfig);
            services.AddScoped<CurrentUser>();
            services.AddHttpContextAccessor();

            services.AddApplication();
            services.AddInfrastructure(Configuration);

            // Configure Authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(
                options =>
                {
                    options.LoginPath = new PathString("/admin/auth/login");
                    options.AccessDeniedPath = new PathString("/admin/auth/denied");
                    options.Cookie.Name = "CleanArchitecture.Cookie";
                })
                .AddJwtBearer("JWT", options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appConfig.Auth.JwtSignKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.EnsureDbCreated();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseDeveloperExceptionPage();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("tr-TR"),
                new CultureInfo("en-US"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("tr-TR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            // Configure static files
            app.UseStaticFiles();
            app.AddUploadsFolder();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCurrentUserMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Configure AutoMapper
            Mapper.Initialize(o =>
            {
                ApiStartup.ConfigureAutoMapper(o);
                AdminStartup.ConfigureAutoMapper(o);
                WebStartup.ConfigureAutoMapper(o);
            });
        }
    }
}
