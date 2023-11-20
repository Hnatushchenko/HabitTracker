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
        if (firstToDoItem.StartTime == secondToDoItem.StartTime)
        {
            firstToDoItem.StartTime = firstToDoItem.StartTime.AddMinutes(1);
        }
        else
        {
            (firstToDoItem.StartTime, secondToDoItem.StartTime) = (secondToDoItem.StartTime, firstToDoItem.StartTime);
            (firstToDoItem.EndTime, secondToDoItem.EndTime) = (secondToDoItem.EndTime, firstToDoItem.EndTime);
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}