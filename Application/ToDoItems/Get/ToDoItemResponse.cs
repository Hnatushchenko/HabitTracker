namespace Application.ToDoItems.Get;

public sealed record ToDoItemResponse
{
    public required Guid Id { get; init; }
    public required TimeOnly StartTime { get; init; }
    public required TimeOnly EndTime { get; init; }
    public required string Description { get; init; }
    public required bool IsDone { get; init; }
    public required bool IsBasedOnHabit { get; init; }
    public IEnumerable<ToDoItemResponse> Children { get; set; } = Array.Empty<ToDoItemResponse>();
}