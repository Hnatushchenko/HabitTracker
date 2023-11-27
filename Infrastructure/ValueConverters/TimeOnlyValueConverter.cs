using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.ValueConverters;

public sealed class TimeOnlyValueConverter : ValueConverter<TimeOnly, TimeSpan>
{
    public TimeOnlyValueConverter() : base(
        timeOnly => timeOnly.ToTimeSpan(),
        timeSpan => TimeOnly.FromTimeSpan(timeSpan)
        )
    {
        
    }
}