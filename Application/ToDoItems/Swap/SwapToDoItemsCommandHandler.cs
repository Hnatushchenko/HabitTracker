using Domain;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Swap;

public sealed class SwapToDoItemsCommandHandler : IRequestHandler<SwapToDoItemsCommand>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SwapToDoItemsCommandHandler(IToDoItemRepository toDoItemRepository, IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(SwapToDoItemsCommand request, CancellationToken cancellationToken)
    {
        var firstToDoItem = await _toDoItemRepository.GetByIdAsync(request.FirstToDoItemId, cancellationToken);
        var secondToDoItem = await _toDoItemRepository.GetByIdAsync(request.SecondToDoItemId, cancellationToken);
        (firstToDoItem.StartTime, secondToDoItem.StartTime) = (secondToDoItem.StartTime, firstToDoItem.StartTime);
        (firstToDoItem.EndTime, secondToDoItem.EndTime) = (secondToDoItem.EndTime, firstToDoItem.EndTime);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}