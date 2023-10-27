using Application.ToDoItems.Update.IsDone;
using Domain;
using Domain.OneOfTypes;
using Domain.Primitives;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Update.Details;

public class UpdateToDoItemDetailsCommandHandler : IRequestHandler<UpdateToDoItemDetailsCommand, UpdatedOrNotFound>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateToDoItemDetailsCommandHandler(IToDoItemRepository toDoItemRepository,
        IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UpdatedOrNotFound> Handle(UpdateToDoItemDetailsCommand request,
        CancellationToken cancellationToken)
    {
        var queryResult = await _toDoItemRepository.GetByIdAsync(request.ToDoItemId);
        if (!queryResult.TryPickT0(out var toDoItem, out var notFound)) return notFound;
        toDoItem.StartTime = request.StartTime;
        toDoItem.EndTime = request.EndTime;
        toDoItem.Description = request.Description;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new Updated();
    }
}