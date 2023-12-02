﻿using Application.Data;
using Domain;
using Domain.BadHabit;
using Domain.Habit;
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
        var connectionString = configuration.GetConnectionString("default");
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<IApplicationContext>(serviceProvider => 
            serviceProvider.GetRequiredService<ApplicationContext>());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IHabitRepository, HabitRepository>();
        services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
        services.AddScoped<IBadHabitRepository, BadHabitRepository>();
    }
}