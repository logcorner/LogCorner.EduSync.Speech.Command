using LogCorner.EduSync.Speech.Application.Interfaces;
using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.Infrastructure;
using LogCorner.EduSync.Speech.Presentation.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using LogCorner.EduSync.Notification.Common;
using LogCorner.EduSync.Speech.Command.SharedKernel;
using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
namespace LogCorner.EduSync.Speech.Presentation
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICreateSpeechUseCase, SpeechUseCase>();
            services.AddScoped<IUpdateSpeechUseCase, SpeechUseCase>();
            services.AddScoped<IDeleteSpeechUseCase, SpeechUseCase>();

            var connectionString = Configuration["ConnectionStrings:SpeechDB"];

            services.AddDbContext<DataBaseContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddScoped<ISpeechRepository, SpeechRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IEventSourcingSubscriber, EventSourcingSubscriber>();

            services.AddScoped<IEventStoreRepository, EventStoreRepository<AggregateRoot<Guid>>>();
            services.AddTransient<IEventSerializer, JsonEventSerializer>();
            services.AddTransient<IEventSourcingHandler<Event>, EventSourcingHandler>();
            services.AddScoped(typeof(IInvoker<>), typeof(Invoker<>));
            services.AddTransient<IDomainEventRebuilder, DomainEventRebuilder>();
            services.AddTransient<IJsonProvider, JsonProvider>();

            services.AddSignalRServices($"{Configuration["HubUrl"]}?clientName=speech-http-command-api", Configuration);

            services.AddSharedKernel();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.WithOrigins(Configuration["allowedOrigins"].Split(","))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddCustomAuthentication(Configuration);

            services.AddCustomSwagger(Configuration);

            services.AddOpenTelemetry(Configuration);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            string pathBase = Configuration["pathBase"];
           //app.UseMiddleware<ExceptionMiddleware>();
            app.UseSwagger(x =>
            {
                if (!string.IsNullOrWhiteSpace(pathBase))
                {
                    x.RouteTemplate = "swagger/{documentName}/swagger.json";
                    x.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"https://{httpReq.Host.Value}{pathBase}" } };
                    });
                }
            })
            .UseSwaggerUI(c =>
            {
                var oAuthClientId = Configuration["SwaggerUI:OAuthClientId"];
                var oAuthClientSecret = Configuration["SwaggerUI:OAuthClientSecret"];

                c.SwaggerEndpoint("../swagger/v1/swagger.json", "WebApi v1");
                c.OAuthClientId(oAuthClientId);
                c.OAuthClientSecret(oAuthClientSecret);
                c.OAuthAppName("The Speech Micro Service Command Swagger UI");
                c.OAuthScopeSeparator(" ");

                c.OAuthUsePkce();
            });
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });

            if (!string.IsNullOrWhiteSpace(pathBase))
            {
                app.UsePathBase(new Microsoft.AspNetCore.Http.PathString(pathBase));
            }
        }
    }
}