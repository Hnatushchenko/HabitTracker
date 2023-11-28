using Domain;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.DueTomorrow;

public sealed class DueToDoItemTomorrowCommandHandler : IRequestHandler<DueToDoItemTomorrowCommand>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DueToDoItemTomorrowCommandHandler(IToDoItemRepository toDoItemRepository,
        IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DueToDoItemTomorrowCommand request, CancellationToken cancellationToken)
    {
        var toDoItem = await _toDoItemRepository.GetByIdAsync(request.ToDoItemId, cancellationToken);
        toDoItem.DueDate = toDoItem.DueDate.AddDays(1);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}