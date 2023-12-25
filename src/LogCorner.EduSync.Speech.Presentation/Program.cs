using LogCorner.EduSync.Speech.Application.EventSourcing;
using LogCorner.EduSync.Speech.Application.Interfaces;
using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.Infrastructure;
using LogCorner.EduSync.Speech.Presentation.Configuration;
using LogCorner.EduSync.Speech.Producer;
using LogCorner.EduSync.Speech.Resiliency;
using LogCorner.EduSync.Speech.Telemetry.Configuration;
using Microsoft.EntityFrameworkCore;
using LogCorner.EduSync.Speech.Command.SharedKernel;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
//
builder.Services.AddScoped<ICreateSpeechUseCase, SpeechUseCase>();
builder.Services.AddScoped<IUpdateSpeechUseCase, SpeechUseCase>();
builder.Services.AddScoped<IDeleteSpeechUseCase, SpeechUseCase>();

var connectionString = Configuration["ConnectionStrings:SpeechDB"];

builder.Services.AddDbContext<DataBaseContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

builder.Services.AddScoped<ISpeechRepository, SpeechRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IEventSourcingSubscriber, EventSourcingSubscriber>();

builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository<AggregateRoot<Guid>>>();
builder.Services.AddTransient<IEventSerializer, JsonEventSerializer>();
builder.Services.AddTransient<IEventSourcingHandler<Event>, EventSourcingHandler>();
builder.Services.AddScoped(typeof(IInvoker<>), typeof(Invoker<>));
builder.Services.AddTransient<IDomainEventRebuilder, DomainEventRebuilder>();
builder.Services.AddTransient<IJsonProvider, JsonDotNetProvider>();

builder.Services.AddProducer("localhost:9092", Configuration);
builder.Services.AddScoped<IEventPublisher, EventPublisher>();
builder.Services.AddSharedKernel();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "CorsPolicy",
        builder => builder.WithOrigins(Configuration["allowedOrigins"].Split(","))
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddCustomAuthentication(Configuration);

builder.Services.AddCustomSwagger(Configuration);
builder.Services.AddResiliencyServices();
builder.Services.AddOpenTelemetryServices(Configuration);

//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
});

app.MapControllers();
// Configure the Prometheus scraping endpoint
app.MapPrometheusScrapingEndpoint();
app.Run();