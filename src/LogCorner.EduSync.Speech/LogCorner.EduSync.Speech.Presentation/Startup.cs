using LogCorner.EduSync.SignalR.Common;
using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.Infrastructure;
using LogCorner.EduSync.Speech.Presentation.Exceptions;
using LogCorner.EduSync.Speech.SharedKernel;
using LogCorner.EduSync.Speech.SharedKernel.Events;
using LogCorner.EduSync.Speech.SharedKernel.Serialyser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace LogCorner.EduSync.Speech.Presentation
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
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

            services.AddSignalRServices(Configuration["HubUrl"]);

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
            /*
                        services
                            .AddAuthentication(options =>
                                {
                                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                                }
                            ).AddJwtBearer(options => Configuration.Bind("AzureAd", options));
            */
            services.AddCustomAuthentication(Configuration);

            services.AddCustomSwagger(Configuration);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1");
                    c.OAuthClientId("ea949966-4b5b-43a5-9917-d0918fb85873");
                    c.OAuthClientSecret("oKK97I2T5rHw_f~N_wHN_Z2-J8H8_-P-9R");
                    c.OAuthAppName("The Speech Micro Service Command Swagger UI");
                    c.OAuthScopeSeparator(" ");

                    c.OAuthUsePkce();
                });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}