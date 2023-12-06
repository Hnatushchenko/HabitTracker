using Domain.HabitArchivedPeriodEntity;
using Domain.HabitArchivedPeriodEntity.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class HabitArchivedPeriodConfiguration : IEntityTypeConfiguration<HabitArchivedPeriod>
{
    public void Configure(EntityTypeBuilder<HabitArchivedPeriod> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id)
            .HasConversion(id => id.Value, value => new HabitArchivedPeriodId(value));
        builder.ToTable(t => t.HasCheckConstraint(nameof(HabitArchivedPeriod.StartDate),
            $"{nameof(HabitArchivedPeriod.StartDate)} <= {nameof(HabitArchivedPeriod.StartDate)}"));
    }
}
