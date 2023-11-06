namespace Domain.ToDoItem;

public sealed record ToDoItemId(Guid Value) : IParsable<ToDoItemId>
{
    public static ToDoItemId Parse(string s, IFormatProvider? provider)
    {
        var guid = Guid.Parse(s, provider);
        return new ToDoItemId(guid);
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out ToDoItemId result)
    {
        var isParsedSuccessfully = Guid.TryParse(s, provider, out var parsedGuid);
        result = new ToDoItemId(parsedGuid);
        return isParsedSuccessfully;
    }
}