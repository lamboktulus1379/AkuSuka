using AkuSuka.Filters;
using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Repository;
using System;
using System.Linq;
using System.Text;
using TokenManager;

namespace AkuSuka.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureTokenService(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
        }

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["mysqlconnection:connectionString"]; services.AddDbContextPool<RepositoryContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        // Replace with your connection string.
                        connectionString,
                        // Replace with your server version and type.
                        // For common usages, see pull request #1233.
                        new MySqlServerVersion(new Version(10, 4, 11)), // use MariaDbServerVersion for MariaDB
                        mySqlOptions => mySqlOptions
                            .CharSetBehavior(CharSetBehavior.NeverAppend))
                    // Everything from this point on is optional but helps with debugging.
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<ISortHelper<Owner>, SortHelper<Owner>>();
            services.AddScoped<ISortHelper<Account>, SortHelper<Account>>();
            services.AddScoped<ISortHelper<Product>, SortHelper<Product>>();

            services.AddScoped<IDataShaper<Owner>, DataShaper<Owner>>();
            services.AddScoped<IDataShaper<Account>, DataShaper<Account>>();
            services.AddScoped<IDataShaper<Product>, DataShaper<Product>>();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<ValidateMediaTypeAttribute>();
        }

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var newtonSoftJsonFormatter = config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();

                if (newtonSoftJsonFormatter != null)
                {
                    newtonSoftJsonFormatter.SupportedMediaTypes.Add("application/vnd.codemaze.hateoas+json");
                }

                var xmlOutputFormatter = config.OutputFormatters.OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

                if (xmlOutputFormatter != null)
                {
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.codemaze.hateoas+xml");
                }
            });
        }

        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "http://localhost:5000",
                    ValidAudience = "http://localhost:5000",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("I was wandering what if the best I could do for may day to day"))
                };
            });
        }
    }
}
