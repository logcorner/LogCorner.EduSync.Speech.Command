using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Presentation
{
    public static class ServicesConfiguration
    {
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(options =>
                    {
                        configuration.Bind("AzureAdB2C", options);

                        options.TokenValidationParameters.NameClaimType = "name";
                    },
                    options => { configuration.Bind("AzureAdB2C", options); });
        }

        public static void AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var tenantName = configuration["SwaggerUI:TenantName"];
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LogCorner Micro Service Event Driven Architecture - Command HTTP API",
                    Version = "v1",
                    Description = "The Speech Micro Service Command HTTP API"
                });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,

                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"https://{tenantName}.b2clogin.com/{tenantName}.onmicrosoft.com/B2C_1_SignUpIn/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri($"https://{tenantName}.b2clogin.com/{tenantName}.onmicrosoft.com/B2C_1_SignUpIn/oauth2/v2.0/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {$"https://{tenantName}.onmicrosoft.com/command/api/Speech.Create","Create a new Speech"},
                                {$"https://{tenantName}.onmicrosoft.com/command/api/Speech.Edit", "Edit and Update a  Speech" },
                                {$"https://{tenantName}.onmicrosoft.com/command/api/Speech.Delete","Delete a Speech"}
                            }
                        }
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        },
                        new[] {
                                $"https://{tenantName}.onmicrosoft.com/command/api/Speech.Create",
                                $"https://{tenantName}.onmicrosoft.com/command/api/Speech.Edit",
                                $"https://{tenantName}.onmicrosoft.com/command/api/Speech.Delete"
                              }
                    }
                });
            });
        }
     
    }
}