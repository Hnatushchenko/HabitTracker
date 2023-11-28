using Domain;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Update.IsDone;

public sealed class UpdateToDoItemIsDoneCommandHandler : IRequestHandler<UpdateToDoItemIsDoneCommand>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateToDoItemIsDoneCommandHandler(IToDoItemRepository toDoItemRepository,
        IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(UpdateToDoItemIsDoneCommand request,
        CancellationToken cancellationToken)
    {
        var toDoItem = await _toDoItemRepository.GetByIdAsync(request.ToDoItemId, cancellationToken);
        toDoItem.IsDone = request.NewIsDoneValue;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}