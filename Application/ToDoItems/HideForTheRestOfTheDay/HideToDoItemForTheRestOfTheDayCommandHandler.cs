using Domain;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.HideForTheRestOfTheDay;

public sealed class HideToDoItemForTheRestOfTheDayCommandHandler : IRequestHandler<HideToDoItemForTheRestOfTheDayCommand>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HideToDoItemForTheRestOfTheDayCommandHandler(IToDoItemRepository toDoItemRepository,
        IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(HideToDoItemForTheRestOfTheDayCommand request, CancellationToken cancellationToken)
    {
        var toDoItem = await _toDoItemRepository.GetByIdAsync(request.ToDoItemId, cancellationToken);
        toDoItem.IsHiddenOnDueDate = true;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}