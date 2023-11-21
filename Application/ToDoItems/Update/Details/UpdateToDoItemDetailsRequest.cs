namespace Application.ToDoItems.Update.Details;

public sealed record UpdateToDoItemDetailsRequest(TimeOnly StartTime,
    TimeOnly EndTime,
    string Description);