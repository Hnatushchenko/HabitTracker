using Domain.OneOfTypes;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Update.IsDone;

public sealed record UpdateToDoItemIsDoneCommand(ToDoItemId ToDoItemId, bool NewIsDoneValue) : IRequest<UpdatedOrNotFound>;