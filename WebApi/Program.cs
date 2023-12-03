using System.Text.Json.Serialization;
using Application;
using Carter;
using Infrastructure;
using WebApi.Converters.JsonConverters;
using WebApi.Extensions;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddCarter(configurator: configurator =>
{
    configurator.WithValidatorLifetime(ServiceLifetime.Scoped);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.Converters.Add(new ToDoItemIdConverter());
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.UseCustomExceptionsHandling();
app.UseMiddleware<ValidationExceptionHandlingMiddleware>();

app.UseCors();

app.MapCarter();

app.UseSwaggerUI(); 

app.Run();
