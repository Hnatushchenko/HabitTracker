using Domain;
using Domain.Habit;
using Domain.ToDoItem;
using MediatR;

namespace Application.Habits.Delete;

public sealed class DeleteHabitCommandHandler : IRequestHandler<DeleteHabitCommand>
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
    
    public async Task Handle(DeleteHabitCommand request, CancellationToken cancellationToken)
    {
        var habit = await _habitRepository.GetByIdAsync(request.HabitId, cancellationToken);
        await _toDoItemRepository.RemoveToDoItemsByTheirHabitAsync(habit.Id, cancellationToken);
        _habitRepository.Remove(habit);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}