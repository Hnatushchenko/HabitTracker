namespace Helpers.Extensions;

public static class DateTimeOffsetExtensions
{
    public static bool HasUtcDateEqualTo(this DateTimeOffset dateTimeOffset, DateTimeOffset other)
    {
        var result = dateTimeOffset.UtcDateTime.Date == other.UtcDateTime.Date;
        return result;
    }
    
    public static bool HasUtcDateLessThen(this DateTimeOffset dateTimeOffset, DateTimeOffset other)
    {
        var result = dateTimeOffset.UtcDateTime.Date < other.UtcDateTime.Date;
        return result;
    }
    
    public static bool HasUtcDateGreaterThanOrEqualTo(this DateTimeOffset source, DateTimeOffset other)
    {
        return !source.HasUtcDateLessThen(other);
    }
    
    public static DateOnly ToDateOnly(this DateTimeOffset source)
    {
        var result = DateOnly.FromDateTime(source.UtcDateTime);
        return result;
    }
}