using Domain.BadHabit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public sealed class BadHabitConfiguration : IEntityTypeConfiguration<BadHabit>
{
    public void Configure(EntityTypeBuilder<BadHabit> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(b => b.Id)
            .HasConversion(badHabitId => badHabitId.Value, value => new BadHabitId(value));
    }
}