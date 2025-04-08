using InterRapidisimoApi.BackgroundServices;
using InterRapidisimoApi.Utilities;
using InterRapidisimoApplication.EventHandlers;
using InterRapidisimoData.Context;
using InterRapidisimoEventBus.Abstractions;
using InterRapidisimoEventBus.Events;
using InterRapidisimoEventBus.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        );
    })
);



builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("localhost", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IEventBus, RabbitMQEventBus>(sp =>
    new RabbitMQEventBus(
        sp.GetRequiredService<RabbitMQConnection>(),
        sp.GetRequiredService<ILogger<RabbitMQEventBus>>(),
        sp,
        builder.Configuration["RabbitMQ:QueueName"] // Lee el nombre de la cola específica para este servicio
    ));
builder.Services.AddTransient<SubjectCreatedEventHandler>();

builder.Services.AddHostedService<EventBusSubscriber>();
builder.Services.RegisterBusinessServices();

var app = builder.Build();
app.UseCors("localhost");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "interRapidisimoApi V1");
});

app.UseRouting();
app.UseAuthorization();

app.MapControllers(); // <---------------------- ¡Asegúrate de agregar esta línea aquí!

app.Run();