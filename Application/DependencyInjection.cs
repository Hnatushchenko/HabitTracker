using Application.Common.Interfaces.Creators;
using Application.Common.PipelineBehaviours;
using Application.Common.Services.Creators;
using Application.Habits.Calculations;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IHabitOccurrencesCalculator, HabitOccurrencesCalculator>();
        services.AddScoped<IHabitsBasedToDoItemsCreator, HabitsBasedToDoItemsCreator>();
        var assembly = typeof(ApplicationAssemblyMarker).Assembly;
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });
        services.AddValidatorsFromAssembly(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    } 
}