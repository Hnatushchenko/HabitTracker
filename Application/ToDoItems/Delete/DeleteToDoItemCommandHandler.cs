using Domain;
using Domain.OneOfTypes;
using Domain.Primitives;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Delete;

public sealed class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand, DeletedOrNotFound>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteToDoItemCommandHandler(IToDoItemRepository toDoItemRepository, IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<DeletedOrNotFound> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        var queryResult = await _toDoItemRepository.GetByIdDeprecatedAsync(request.ToDoItemId);
        if (!queryResult.TryPickT0(out var habit, out var notFound)) return notFound;
        _toDoItemRepository.Remove(habit);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new Deleted();
    }
}