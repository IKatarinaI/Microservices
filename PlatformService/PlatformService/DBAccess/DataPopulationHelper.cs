using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Linq;

namespace PlatformService.DBAccess
{
    public static class DataPopulationHelper
    {
        public static void PopulationSetUp(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<PlatformServiceDbContext>());
            }
        }

        private static void SeedData(PlatformServiceDbContext platformServiceDbContext)
        {
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
