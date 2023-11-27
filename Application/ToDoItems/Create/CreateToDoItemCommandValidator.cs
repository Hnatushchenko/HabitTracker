using Domain.ToDoItem;
using FluentValidation;

namespace Application.ToDoItems.Create;

public sealed class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateToDoItemCommandValidator()
    {
        RuleFor(createToDoItemCommand => createToDoItemCommand.ParentToDoItemId!.Value)
            .NotEmpty()
            .When(createToDoItemCommand => createToDoItemCommand.ParentToDoItemId is not null);
        RuleFor(createToDoItemCommand => createToDoItemCommand.StartTime)
            .LessThanOrEqualTo(createToDoItemCommand => createToDoItemCommand.EndTime);
    }
}