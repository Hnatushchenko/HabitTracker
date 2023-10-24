﻿using Domain.Habit;
using Domain.ToDoItem;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class ApplicationContext : DbContext, IApplicationContext
{
    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
    public DbSet<Habit> Habits => Set<Habit>();
    
    public ApplicationContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\mssqllocaldb;Database=UltimateSelfimprovementDb;Trusted_Connection=True;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}