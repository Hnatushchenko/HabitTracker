using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.ToDoItem;

namespace WebApi.Converters.JsonConverters;

public sealed class ToDoItemIdConverter : JsonConverter<ToDoItemId>
{
    public override ToDoItemId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        var value = reader.GetGuid();
        return new ToDoItemId(value);
    }

    public override void Write(Utf8JsonWriter writer, ToDoItemId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
