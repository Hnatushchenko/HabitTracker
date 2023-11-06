using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.HideForTheRestOfTheDay;

public sealed record HideToDoItemForTheRestOfTheDayCommand(ToDoItemId ToDoItemId) : IRequest;