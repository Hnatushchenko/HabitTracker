using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.DueTomorrow;

public sealed record DueToDoItemTomorrowCommand(ToDoItemId ToDoItemId) : IRequest;