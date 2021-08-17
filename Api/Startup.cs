using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Financas.Application.Persistence;
using Financas.Application.Infrastructure.Autenticacao;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Application.Infrastructure;
using Financas.Api.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using Application.Infrastructure.Email;

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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var tokenConfig = new TokenConfig();
            new ConfigureFromConfigurationOptions<TokenConfig>(
                Configuration.GetSection("TokenConfig"))
                .Configure(tokenConfig);

            services.AddSingleton(tokenConfig);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfig.Issuer,
                    ValidAudience = tokenConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Secret))
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            var environment = HostEnvironment.IsDevelopment() ? "Development" : "Production";
            Console.WriteLine("Environment -> " + environment);
            var connectionString = Configuration.GetConnectionString(environment);
            services.AddDbContext<FinancasContext>(options => options.UseMySql(connectionString));

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ICurrentUser, CurrentUser>();

            var emailConfig = Configuration.GetSection("Email");
            services.AddTransient<IEmailService>(x =>
                new EmailService(
                    emailConfig["Remetente"],
                    emailConfig["SmtpHost"],
                    emailConfig["SmtpPort"],
                    emailConfig["SmtpUser"],
                    emailConfig["SmtpPass"]
            ));

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
                c.CustomSchemaIds(type => type.ToString());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Financas-Api V1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
