﻿// <auto-generated />

#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231106114052_AddIsHiddenOnDueDatePropertyToTheToDoItemModel")]
    partial class AddIsHiddenOnDueDatePropertyToTheToDoItemModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Habit.Habit", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

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

                    b.ToTable("Habits");
                });

            modelBuilder.Entity("Domain.Habit.HabitToDoItem", b =>
                {
                    b.Property<Guid>("HabitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ToDoItemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("HabitId", "ToDoItemId");

                    b.HasIndex("ToDoItemId");

                    b.ToTable("HabitToDoItems");
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

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("HabitId");

                    b.ToTable("ToDoItems", t =>
                        {
                            t.HasCheckConstraint("StartTime", "StartTime <= EndTime");
                        });
                });

            modelBuilder.Entity("Domain.Habit.HabitToDoItem", b =>
                {
                    b.HasOne("Domain.Habit.Habit", "Habit")
                        .WithMany()
                        .HasForeignKey("HabitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.ToDoItem.ToDoItem", "ToDoItem")
                        .WithMany()
                        .HasForeignKey("ToDoItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Habit");

                    b.Navigation("ToDoItem");
                });

            modelBuilder.Entity("Domain.ToDoItem.ToDoItem", b =>
                {
                    b.HasOne("Domain.Habit.Habit", null)
                        .WithMany("ToDoItems")
                        .HasForeignKey("HabitId");
                });

            modelBuilder.Entity("Domain.Habit.Habit", b =>
                {
                    b.Navigation("ToDoItems");
                });
#pragma warning restore 612, 618
        }
    }
}
