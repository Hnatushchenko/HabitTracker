using Domain;
using Domain.OneOfTypes;
using Domain.Primitives;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Update.Details;

public sealed class UpdateToDoItemDetailsCommandHandler : IRequestHandler<UpdateToDoItemDetailsCommand>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateToDoItemDetailsCommandHandler(IToDoItemRepository toDoItemRepository,
        IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(UpdateToDoItemDetailsCommand request,
        CancellationToken cancellationToken)
    {
        var toDoItem = await _toDoItemRepository.GetByIdAsync(request.ToDoItemId, cancellationToken);
        toDoItem.StartTime = request.StartTime;
        toDoItem.EndTime = request.EndTime;
        toDoItem.Description = request.Description;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}