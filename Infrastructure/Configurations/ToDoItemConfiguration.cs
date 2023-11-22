using Domain.ToDoItem;
using Infrastructure.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public sealed class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasConversion(toDoItemId => toDoItemId.Value, value => new ToDoItemId(value));
        builder.Property(t => t.EndTime).HasConversion<TimeOnlyValueConverter>().HasColumnType("time");
        builder.Property(t => t.StartTime).HasConversion<TimeOnlyValueConverter>().HasColumnType("time");
        builder.ToTable(t => t.HasCheckConstraint(nameof(ToDoItem.StartTime),
            $"{nameof(ToDoItem.StartTime)} <= {nameof(ToDoItem.EndTime)}"));
    }
}
