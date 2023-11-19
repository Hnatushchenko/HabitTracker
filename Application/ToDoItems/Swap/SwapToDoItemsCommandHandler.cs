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
        var firstToDoItemTask = _toDoItemRepository.GetById(request.FirstToDoItemId);
        var secondToDoItemTask = _toDoItemRepository.GetById(request.SecondToDoItemId);
        await Task.WhenAll(firstToDoItemTask, secondToDoItemTask);
        var firstToDoItem = firstToDoItemTask.Result;
        var secondToDoItem = secondToDoItemTask.Result;
        
        (firstToDoItem.StartTime, secondToDoItem.StartTime) = (secondToDoItem.StartTime, firstToDoItem.StartTime);
        (firstToDoItem.EndTime, secondToDoItem.EndTime) = (secondToDoItem.EndTime, firstToDoItem.EndTime);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}