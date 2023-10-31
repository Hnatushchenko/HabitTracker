using Domain;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Create;

public sealed class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateToDoItemCommandHandler(IToDoItemRepository toDoItemRepository,
        IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
    {
        var toDoItem = new ToDoItem()
        {
            Id = new ToDoItemId(Guid.NewGuid()),
            Description = request.Description,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            DueDate = request.DueDate
        };
        _toDoItemRepository.Add(toDoItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}