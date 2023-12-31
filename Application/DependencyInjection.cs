﻿using System.Reflection;
using Application.Common.Services.Creators.InitialHabitToDoItemsCreator;
using Application.Common.Services.Creators.MissingHabitBasedToDoItemsCreator;
using Application.Common.Services.PipelineBehaviours;
using Application.Common.Services.Updaters;
using Application.Frequency.DayOfWeekToDayOfWeekFrequencyMapper;
using Application.Habits.Calculations;
using Application.Habits.Calculations.HabitOccurrencesCalculator;
using Application.Habits.Create;
using Application.Habits.Get;
using Application.Habits.Unarchive;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application;

public static class DependencyInjection
{
    private static readonly Assembly ApplicationAssembly = typeof(ApplicationAssemblyMarker).Assembly;
    
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IGoodHabitStreakCalculator, GoodHabitStreakCalculator>();
        services.AddSingleton<IHabitOccurrencesCalculator, HabitOccurrencesCalculator>();
        services.AddSingleton<IDayOfWeekToDayOfWeekFrequencyMapper, DayOfWeekToDayOfWeekFrequencyMapper>();
        services.AddSingleton<IDateTimeOffsetIncrementer, DateTimeOffsetIncrementer>();
        services.AddScoped<IInitialHabitToDoItemsCreator, InitialHabitToDoItemsCreator>();
        services.AddScoped<IOverdueTasksDueDateUpdater, OverdueTasksDueDateUpdater>();
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
        services.AddHabitBasedMissingToDoItemCreatorCache();
    }

    private static void AddHabitBasedMissingToDoItemCreatorCache(this IServiceCollection services)
    {
        services.RemoveAll<INotificationHandler<HabitCreatedNotification>>();
        services.RemoveAll<INotificationHandler<HabitUnarchivedNotification>>();
        services.AddSingleton<HabitBasedMissingToDoItemCreatorCache>();
        var getHabitBasedMissingToDoItemCreatorCache = (IServiceProvider serviceProvider) =>
            serviceProvider.GetRequiredService<HabitBasedMissingToDoItemCreatorCache>();
        services.AddSingleton<IHabitBasedMissingToDoItemCreatorCache>(getHabitBasedMissingToDoItemCreatorCache);
        services.AddSingleton<INotificationHandler<HabitCreatedNotification>>(getHabitBasedMissingToDoItemCreatorCache);
        services.AddSingleton<INotificationHandler<HabitUnarchivedNotification>>(getHabitBasedMissingToDoItemCreatorCache);
    }
}