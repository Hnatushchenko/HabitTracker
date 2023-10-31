using Domain.ToDoItem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Configurations;

public sealed class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasConversion(toDoItemId => toDoItemId.Value, value => new ToDoItemId(value));
        builder.Property(t => t.EndTime).HasConversion<MyTimeOnlyConverter>().HasColumnType("time");
        builder.Property(t => t.StartTime).HasConversion<MyTimeOnlyConverter>().HasColumnType("time");
        builder.ToTable(t => t.HasCheckConstraint(nameof(ToDoItem.StartTime),
            $"{nameof(ToDoItem.StartTime)} <= {nameof(ToDoItem.EndTime)}"));
    }
}

public class MyTimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
{
    public MyTimeOnlyConverter() : base(timeOnly => 
            timeOnly.ToTimeSpan(), 
        timeSpan => TimeOnly.FromTimeSpan(timeSpan)) { }
}