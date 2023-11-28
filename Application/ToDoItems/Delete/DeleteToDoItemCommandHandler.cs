using Domain;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Delete;

public sealed class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteToDoItemCommandHandler(IToDoItemRepository toDoItemRepository, IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        var toDoItem = await _toDoItemRepository.GetByIdAsync(request.ToDoItemId, cancellationToken);
        await RecursiveDeleteAsync(toDoItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    private async Task RecursiveDeleteAsync(ToDoItem parent, CancellationToken cancellationToken)
    {
        var children = await _toDoItemRepository.GetChildrenByParentToDoItemIdAsync(parent.Id, cancellationToken);
        foreach (var child in children)
        {
            await RecursiveDeleteAsync(child, cancellationToken);
        }
        
        _toDoItemRepository.Remove(parent);
    }
}