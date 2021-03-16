using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Financas.Application.Persistence;

namespace Financas.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            HostEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var environment = HostEnvironment.IsDevelopment() ? "Development" : "Production";
            var connectionString = Configuration.GetConnectionString(environment);
            services.AddDbContext<FinancasContext>(options => options.UseSqlServer(connectionString));

            services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.Where(type => type.Name.Equals("QueryHandler")))
                    .AsSelf()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.Where(type => type.Name.Equals("CommandHandler")))
                    .AsSelf()               
                    .WithTransientLifetime());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Financas-Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("./swagger/v1/swagger.json", "Financas-Api V1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
