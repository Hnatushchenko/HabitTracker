﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.BadHabit.BadHabit", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("BadHabits");
                });

            modelBuilder.Entity("Domain.BadHabit.BadHabitOccurrence", b =>
                {
                    b.Property<Guid>("BadHabitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("OccurrenceDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("BadHabitId", "OccurrenceDate");

                    b.ToTable("BadHabitOccurrences");
                });

            modelBuilder.Entity("Domain.Habit.Habit", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DayOfWeekFrequency")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("DefaultEndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("DefaultStartTime")
                        .HasColumnType("time");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FrequencyCount")
                        .HasColumnType("int");

                    b.Property<int>("FrequencyTimeUnit")
                        .HasColumnType("int");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ToDoItemDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Habits", t =>
                        {
                            t.HasCheckConstraint("DefaultStartTime", "DefaultStartTime <= DefaultEndTime");
                        });
                });

            modelBuilder.Entity("Domain.HabitArchivedPeriodEntity.HabitArchivedPeriod", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("HabitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("HabitId");

                    b.ToTable("HabitArchivedPeriods", t =>
                        {
                            t.HasCheckConstraint("StartDate", "StartDate <= StartDate");
                        });
                });

            modelBuilder.Entity("Domain.ToDoItem.ToDoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DueDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<Guid?>("HabitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHiddenOnDueDate")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("HabitId");

                    b.HasIndex("ParentId");

                    b.ToTable("ToDoItems", t =>
                        {
                            t.HasCheckConstraint("StartTime", "StartTime <= EndTime");
                        });
                });

            modelBuilder.Entity("Domain.BadHabit.BadHabitOccurrence", b =>
                {
                    b.HasOne("Domain.BadHabit.BadHabit", "BadHabit")
                        .WithMany("Occurrences")
                        .HasForeignKey("BadHabitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BadHabit");
                });

            modelBuilder.Entity("Domain.HabitArchivedPeriodEntity.HabitArchivedPeriod", b =>
                {
                    b.HasOne("Domain.Habit.Habit", "Habit")
                        .WithMany("HabitArchivedPeriods")
                        .HasForeignKey("HabitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Habit");
                });

            modelBuilder.Entity("Domain.ToDoItem.ToDoItem", b =>
                {
                    b.HasOne("Domain.Habit.Habit", "Habit")
                        .WithMany("ToDoItems")
                        .HasForeignKey("HabitId");

                    b.HasOne("Domain.ToDoItem.ToDoItem", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Habit");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Domain.BadHabit.BadHabit", b =>
                {
                    b.Navigation("Occurrences");
                });

            modelBuilder.Entity("Domain.Habit.Habit", b =>
                {
                    b.Navigation("HabitArchivedPeriods");

                    b.Navigation("ToDoItems");
                });

            modelBuilder.Entity("Domain.ToDoItem.ToDoItem", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
