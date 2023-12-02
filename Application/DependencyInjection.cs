﻿using System.Reflection;
using Application.Common.Services.Creators.MissingHabitBasedToDoItemsCreator;
using Application.Common.Services.PipelineBehaviours;
using Application.Common.Services.Updaters;
using Application.Habits.Calculations;
using Application.Habits.Get;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    private static readonly Assembly ApplicationAssembly = typeof(ApplicationAssemblyMarker).Assembly;
    
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOverdueTasksDueDateUpdater, OverdueTasksDueDateUpdater>();
        services.AddSingleton<IGoodHabitStreakCalculator, GoodHabitStreakCalculator>();
        services.AddSingleton<IHabitOccurrencesCalculator, HabitOccurrencesCalculator>();
        services.AddSingleton<IHabitBasedMissingToDoItemCreatorCache, HabitBasedMissingToDoItemCreatorCache>();
        services.AddScoped<MissingHabitBasedToDoItemsCreator>();
        services.AddScoped<IMissingHabitsBasedToDoItemsCreator>(serviceProvider =>
        {
            var cacheService = serviceProvider.GetRequiredService<IHabitBasedMissingToDoItemCreatorCache>();
            var wrappee = serviceProvider.GetRequiredService<MissingHabitBasedToDoItemsCreator>();
            var wrapper = new OptimizedMissingHabitBasedToDoItemCreator(wrappee ,cacheService);
            return wrapper;
        });
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(ApplicationAssembly));
        services.AddValidatorsFromAssembly(ApplicationAssembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    } 
}