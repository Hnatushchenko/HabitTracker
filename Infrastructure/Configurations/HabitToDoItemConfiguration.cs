using Domain.Habit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class HabitToDoItemConfiguration : IEntityTypeConfiguration<HabitToDoItem>
{
    public void Configure(EntityTypeBuilder<HabitToDoItem> builder)
    {
        builder.HasKey(habitToDoItem => new { habitToDoItem.HabitId, habitToDoItem.ToDoItemId });
    }
}