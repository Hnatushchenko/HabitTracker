using FluentValidation;

namespace Application.ToDoItems.Update.Details;

public sealed class UpdateToDoItemDetailsCommandValidator : AbstractValidator<UpdateToDoItemDetailsCommand>
{
    public UpdateToDoItemDetailsCommandValidator()
    {
        RuleFor(updateToDoItemDetailsCommand => updateToDoItemDetailsCommand.ToDoItemId.Value).NotEmpty();
        RuleFor(updateToDoItemDetailsCommand => updateToDoItemDetailsCommand.StartTime)
            .LessThanOrEqualTo(updateToDoItemDetailsCommand => updateToDoItemDetailsCommand.EndTime);
    }
}