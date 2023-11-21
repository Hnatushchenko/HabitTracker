using FluentValidation;

namespace Application.ToDoItems.Create;

public sealed class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateToDoItemCommandValidator()
    {
        RuleFor(createToDoItemCommand => createToDoItemCommand.StartTime)
            .LessThanOrEqualTo(createToDoItemCommand => createToDoItemCommand.EndTime);
    }
}