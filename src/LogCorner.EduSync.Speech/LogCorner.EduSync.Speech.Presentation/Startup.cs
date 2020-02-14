using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Domain.Events;
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
using System;

namespace LogCorner.EduSync.Speech.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRegisterSpeechUseCase, RegisterSpeechUseCase>();
            services.AddScoped<IUpdateSpeechUseCase, RegisterSpeechUseCase>();

            var connectionString = Configuration["ConnectionStrings:SpeechDB"];

            services.AddDbContext<DataBaseContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddScoped<ISpeechRepository, SpeechRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IRegisterSpeechUseCase, RegisterSpeechUseCase>();

            services.AddTransient<IEventSourcingSubscriber, EventSourcingSubscriber>();

            services.AddScoped(typeof(IEventStoreRepository<>), typeof(EventStoreRepository<>));
            services.AddTransient<IEventSerializer, JsonEventSerializer>();
            services.AddTransient<IEventSourcingHandler<Event>, EventSourcingHandler<AggregateRoot<Guid>>>();
            services.AddScoped(typeof(IInvoker<>), typeof(Invoker<>));
            services.AddTransient<IDomainEventRebuilder, DomainEventRebuilder>();
            services.AddTransient<IJsonProvider, JsonProvider>();

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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}