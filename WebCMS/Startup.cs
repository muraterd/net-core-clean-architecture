using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Serilog;
using WebCMS.Areas.Admin;
using WebCMS.Areas.Api;
using WebCMS.Areas.Web;
using WebCMS.Data;
using WebCMS.Services.Page;
using Application;
using Infrastructure;
using Application.Services.User;
using FluentValidation.AspNetCore;
using Application.MediatR.Auth.Commands.CreateSuperAdmin;
using Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace WebCMS
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
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateSuperAdminCommandValidator>())
                .AddRazorRuntimeCompilation()
                .AddFeatureFolders()
                .AddAreaFeatureFolders(new OdeToCode.AddFeatureFolders.AreaFeatureFolderOptions()
                {
                    DefaultAreaViewLocation = "Areas/{2}/Features/{1}/{0}/{0}.cshtml"
                });

            // Configure DI
            services.AddHttpContextAccessor();
            //services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("MsSql")));
            services.AddDbContext<AppDbContext>(opts => opts.UseInMemoryDatabase("WebCMS"));
            services.AddSingleton(appConfig);
            services.AddScoped<UserService>();
            services.AddScoped<PageService>();

            services.AddApplication();
            services.AddInfrastructure();

            // Configure Authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(
                options =>
                {
                    options.LoginPath = new PathString("/admin/auth/login");
                    options.AccessDeniedPath = new PathString("/admin/auth/denied");
                    options.Cookie.Name = "WebCMS.Cookie";
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            // Configure static files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Areas", "Admin", "wwwroot")),
                RequestPath = "/admin/public"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Areas", "Web", "wwwroot")),
                RequestPath = "/public"
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

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

            // Create and Seed Database
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                Log.Information("Checking if database exists");
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                var created = dbContext.Database.EnsureCreated();

                if (created)
                    Log.Information("Database created!");
                else
                    Log.Information("Database is already created");
            }

            Log.Information($"Running in environment: {env.EnvironmentName}");
        }
    }
}
