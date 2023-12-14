using Application.Data;
using Domain;
using Domain.BadHabit;
using Domain.Habit;
using Domain.HabitArchivedPeriodEntity;
using Domain.ToDoItem;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringFromEnvironment = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        var connectionStringFromConfiguration = configuration.GetConnectionString("default");
        var connectionString = connectionStringFromEnvironment ?? connectionStringFromConfiguration;
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<IApplicationContext>(serviceProvider => 
            serviceProvider.GetRequiredService<ApplicationContext>());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddRepositories();
    }
    
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IHabitArchivedPeriodRepository, HabitArchivedPeriodRepository>();
        services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
        services.AddScoped<IBadHabitRepository, BadHabitRepository>();
        services.AddScoped<IHabitRepository, HabitRepository>();
    }
}