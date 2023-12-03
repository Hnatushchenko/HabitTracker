using Domain.ToDoItem;
using FluentValidation;

namespace Application.ToDoItems.Create;

public sealed class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateToDoItemCommandValidator(IToDoItemRepository toDoItemRepository)
    {
        RuleFor(createToDoItemCommand => createToDoItemCommand.ParentToDoItemId!.Value)
            .NotEmpty()
            .When(createToDoItemCommand => createToDoItemCommand.ParentToDoItemId is not null);
        RuleFor(createToDoItemCommand => createToDoItemCommand.StartTime)
            .LessThanOrEqualTo(createToDoItemCommand => createToDoItemCommand.EndTime);
        RuleFor(createToDoItemCommand => createToDoItemCommand.ParentToDoItemId)
            .MustAsync(async (parentToDoItemId, cancellationToken) =>
            {
                if (parentToDoItemId is null)
                {
                    return true;
                }

                if (parentToDoItemId.Value != Guid.Empty)
                {
                    var exists = await toDoItemRepository.ExistsAsync(parentToDoItemId, cancellationToken);
                    return exists;
                }

                return false;
            }).WithMessage("Parent to-do item with the specified ID does not exist. Please provide an ID of an existing to-do item.");
    }
}