using System.Text.Json.Serialization;
using Application;
using Carter;
using Infrastructure;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPresentation();
builder.Services.AddDbContext<ApplicationContext>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
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

app.UseCors();

app.MapCarter();

// Console.ForegroundColor = ConsoleColor.Green;
// var currentDir = Directory.GetCurrentDirectory();
// // Get all the file names in the directory and subdirectories
// string pattern = "*.*";
// string[] files = Directory.GetFiles(currentDir, pattern, SearchOption.AllDirectories);
//
// for (int i = 0; i < 150; i++)
// {
//     var random = new Random();
// // Loop through the array and print each file name to the console
//     foreach (string file in files)
//     {
//         Console.Write(file);
//         if (random.Next() % 10 == 0)
//         {
//             Console.WriteLine();
//         }
//     }
// }

app.Run();