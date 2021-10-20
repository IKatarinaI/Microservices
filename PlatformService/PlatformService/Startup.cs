using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PlatformService.DBAccess;
using PlatformService.Repositories;
using PlatformService.Repositories.Interfaces;
using PlatformService.SyncDataServices.Http;
using PlatformService.SyncDataServices.Http.Interfaces;
using System;

namespace PlatformService
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environemnt = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environemnt { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if(Environemnt.IsProduction())
            {
                services.AddDbContext<PlatformServiceDbContext>
                        (opt => opt.UseSqlServer(Configuration.GetConnectionString("PlatformsConn")));
            }
            else
            {
                // local environment
                services.AddDbContext<PlatformServiceDbContext>
                        (opt => opt.UseInMemoryDatabase("database"));
            }

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlatformService", Version = "v1" });
            });

            // automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // httpclient
            services.AddHttpClient<IHttpCommandDataClient, HttpCommandDataClient>();

            // repositories
            services.AddScoped<IPlatformServiceRepository, PlatformServiceRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // set up DataPopulationHelper
            DataPopulationHelper.PopulationSetUp(app, env.IsProduction());
        }
    }
}
