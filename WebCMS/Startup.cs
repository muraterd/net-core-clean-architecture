using System.IO;
using Application.Auth;
using Application.Interfaces.Providers;
using Application.Services.User;
using AutoMapper;
using Infrastructure.Providers;
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
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddFeatureFolders()
                .AddAreaFeatureFolders(new OdeToCode.AddFeatureFolders.AreaFeatureFolderOptions()
                {
                    DefaultAreaViewLocation = "Areas/{2}/Features/{1}/{0}/{0}.cshtml"
                });

            // Configure DI
            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("MsSql")));
            services.AddScoped<UserService>();
            services.AddScoped<PageService>();
            services.AddScoped<IHashProvider, HashProvider>();
            services.AddScoped<SigninManager>();

            // Configure Authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.LoginPath = new PathString("/admin/auth/login");
                    options.AccessDeniedPath = new PathString("/admin/auth/denied");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApiErrorHandlerMiddleware();

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
