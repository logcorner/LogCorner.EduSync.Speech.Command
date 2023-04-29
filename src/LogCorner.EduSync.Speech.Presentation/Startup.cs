using LogCorner.EduSync.Speech.Application.EventSourcing;
using LogCorner.EduSync.Speech.Application.Interfaces;
using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Command.SharedKernel;
using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.Infrastructure;
using LogCorner.EduSync.Speech.Presentation.Exceptions;
using LogCorner.EduSync.Speech.Producer;
using LogCorner.EduSync.Speech.Resiliency;
using LogCorner.EduSync.Speech.Telemetry.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

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
            services.AddTransient<IJsonProvider, JsonDotNetProvider>();

            services.AddProducer("localhost:9092", Configuration);
            services.AddScoped<IEventPublisher, EventPublisher>();
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
            services.AddResiliencyServices();
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

            if (!bool.TryParse(Configuration["isAuthenticationEnabled"], out var isAuthenticationEnabled))
            {
                throw new PresentationException("isAuthenticationEnabled is not configured correctly ");
            }
            string pathBase = Configuration["pathBase"];
            string useHttps = Configuration["useHttps"];
            var protocol = "http";
            if (!string.IsNullOrWhiteSpace(useHttps))
            {
                protocol = "https";
            }
                
                app.UseMiddleware<ExceptionMiddleware>();
            app.UseSwagger(x =>
            {
                if (!string.IsNullOrWhiteSpace(pathBase))
                {
                    x.RouteTemplate = "swagger/{documentName}/swagger.json";
                    x.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{protocol}://{httpReq.Host.Value}{pathBase}" } };
                    });
                }
            })
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "WebApi v1");
                if (isAuthenticationEnabled)
                {
                    var oAuthClientId = Configuration["SwaggerUI:OAuthClientId"];
                    var oAuthClientSecret = Configuration["SwaggerUI:OAuthClientSecret"];
                    c.OAuthClientId(oAuthClientId);
                    c.OAuthClientSecret(oAuthClientSecret);
                    c.OAuthAppName("The Speech Micro Service Command Swagger UI");
                    c.OAuthScopeSeparator(" ");
                    c.OAuthUsePkce();
                }
            });
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                if (isAuthenticationEnabled)
                {
                    endpoints.MapControllers().RequireAuthorization();
                }
                else
                {
                    endpoints.MapControllers();
                }
            });

            if (!string.IsNullOrWhiteSpace(pathBase))
            {
                app.UsePathBase(new PathString(pathBase));
            }
        }
    }
}