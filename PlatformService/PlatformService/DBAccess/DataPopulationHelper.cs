using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Linq;

namespace PlatformService.DBAccess
{
    public static class DataPopulationHelper
    {
        public static void PopulationSetUp(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<PlatformServiceDbContext>(), isProduction);
            }
        }

        private static void SeedData(PlatformServiceDbContext platformServiceDbContext, bool isProduction)
        {
            if(isProduction)
            {
                try
                {
                    platformServiceDbContext.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Could not run migrations: {ex.Message}.");
                }
            }

            if(!platformServiceDbContext.Platforms.Any())
            {
                Console.WriteLine("Seeding data....");

                platformServiceDbContext.Platforms.AddRange(
                    new Platform() { Name="Dot Net", Publisher="Microsoft", Cost="Free"},
                    new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                    );

                platformServiceDbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Data already exist.");
            }
        }
    }
}
