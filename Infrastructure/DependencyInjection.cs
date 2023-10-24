using Domain;
using Domain.Habit;
using Domain.ToDoItem;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IApplicationContext, ApplicationContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IHabitRepository, HabitRepository>();
        services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
    }
}