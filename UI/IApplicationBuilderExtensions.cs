using Data.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UI.Data;

public static class IApplicationBuilderExtensions
{
    public static void AddUploadsFolder(this IApplicationBuilder app)
    {
        try
        {
            Log.Information("Checking if upload paths exists");
            FileUploadHelper.EnsureAllUploadPathsCreated();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/uploads"
            });
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Upload paths could not be created!");
        }
    }

    public static void EnsureDbCreated(this IApplicationBuilder app)
    {
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
    }
}