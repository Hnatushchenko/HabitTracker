namespace Application.Extensions;

public static class DateTimeOffsetExtensions
{
    public static bool HasSameUtcDateAs(this DateTimeOffset @this, DateTimeOffset other)
    {
        var result = @this.UtcDateTime.Date == other.UtcDateTime.Date;
        return result;
    }
}