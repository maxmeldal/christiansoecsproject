using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Repositories;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChristiansOeCsProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //opretter en instance scope for hver http request
            services.AddScoped<FacilityService>();
            services.AddScoped<RestaurantService>();
            services.AddScoped<TripService>();
            services.AddScoped<AttractionService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}