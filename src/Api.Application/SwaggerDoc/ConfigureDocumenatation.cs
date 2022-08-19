using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.Application.SwaggerDoc
{
    public class ConfigureDocumenatation
    {
        public static void SwaggerGenDoc(IServiceCollection services)
        {
            services.AddSwaggerGen(s=>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Api AspNetCore 3.1",
                    Description = " Api Rest com conceito de arquitetura em DDD",
                    TermsOfService = new Uri("https://github.com/ThiAlc-Dev/netcoreApi"),
                    Contact = new OpenApiContact
                    {
                        Name = "Thiago Alcantara",
                        Email = "thiago.prog@outlook.com"
                    }
                });

                 s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
                { 
                    Name = "Authorization", 
                    Type = SecuritySchemeType.ApiKey, 
                    In = ParameterLocation.Header, 
                    Description = "JWT Authorization header using the Bearer scheme"
                }); 

                s.AddSecurityRequirement(new OpenApiSecurityRequirement 
                { 
                    { 
                          new OpenApiSecurityScheme 
                          { 
                              Reference = new OpenApiReference 
                              { 
                                  Type = ReferenceType.SecurityScheme, 
                                  Id = "Bearer" 
                              } 
                          }, 
                         new List<string> {} 
                    } 
                }); 
            });
        }

        public static void SwaggerUIDoc(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Api em .Net Core 3.1");
                s.RoutePrefix = string.Empty;
            });
        }
    }
}