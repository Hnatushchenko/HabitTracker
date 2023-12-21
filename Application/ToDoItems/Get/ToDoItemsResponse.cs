namespace Application.ToDoItems.Get;

public sealed record ToDoItemsResponse(IEnumerable<ToDoItemResponse> ToDoItems);
