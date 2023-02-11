
using Hubtel.Wallets.Api.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;



namespace Agents.Api.Intallers
{
    /// <summary>
    ///   Class installs most of the controllers, swagger and jwt settings
    /// </summary>
    public class MvcInstaller : IInstaller
    {
        /// <summary>
        /// Methods configures swagger docs, sets up controllers, and adds jwt settings as a singleton
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <returns>void</returns>
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
           

            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(
                    name: "v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Agents API",
                        Description = "An ASP.NET Core Web API for managing Agents",
                    }
                );

                x.AddSecurityDefinition(
                    name: "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description =
                            @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    }
                );
                x.AddSecurityRequirement(
                    new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    }
                );
            });
        }
    }
}
