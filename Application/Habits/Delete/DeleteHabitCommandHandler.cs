using Domain;
using Domain.Habit;
using Domain.OneOfTypes;
using Domain.Primitives;
using Domain.ToDoItem;
using MediatR;

namespace Application.Habits.Delete;

public sealed class DeleteHabitCommandHandler : IRequestHandler<DeleteHabitCommand, DeletedOrNotFound>
{
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IHabitRepository _habitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteHabitCommandHandler(IToDoItemRepository toDoItemRepository,
        IHabitRepository habitRepository,
        IUnitOfWork unitOfWork)
    {
        _toDoItemRepository = toDoItemRepository;
        _habitRepository = habitRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<DeletedOrNotFound> Handle(DeleteHabitCommand request, CancellationToken cancellationToken)
    {
        var queryResult = await _habitRepository.GetByIdAsync(request.HabitId);
        if (!queryResult.TryPickT0(out var habit, out var notFound)) return notFound;
        await _toDoItemRepository.RemoveToDoItemsByTheirHabitAsync(habit.Id);
        _habitRepository.Remove(habit);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new Deleted();
    }
}