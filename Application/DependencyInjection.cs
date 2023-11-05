using Application.Habits.Calculations;
using Application.Interfaces.Creators;
using Application.Services.Creators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IHabitOccurrencesCalculator, HabitOccurrencesCalculator>();
        services.AddScoped<IHabitsBasedToDoItemsCreator, HabitsBasedToDoItemsCreator>();
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });
        services.AddValidatorsFromAssembly(assembly);
    } 
}