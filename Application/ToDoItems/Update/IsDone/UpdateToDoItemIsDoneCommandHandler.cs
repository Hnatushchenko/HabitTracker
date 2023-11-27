using Domain;
using Domain.OneOfTypes;
using Domain.Primitives;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Update.IsDone;

public sealed class UpdateToDoItemIsDoneCommandHandler : IRequestHandler<UpdateToDoItemIsDoneCommand, UpdatedOrNotFound>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateToDoItemIsDoneCommandHandler(IToDoItemRepository toDoItemRepository,
        IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UpdatedOrNotFound> Handle(UpdateToDoItemIsDoneCommand request,
        CancellationToken cancellationToken)
    {
        var queryResult = await _toDoItemRepository.GetByIdDeprecatedAsync(request.ToDoItemId);
        if (!queryResult.TryPickT0(out var toDoItem, out var notFound)) return notFound;
        toDoItem.IsDone = request.NewIsDoneValue;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new Updated();
    }
}