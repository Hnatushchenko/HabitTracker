using Domain;
using Domain.Habit;
using MediatR;

namespace Application.Habits.Update;

public sealed class UpdateHabitDetailsCommandHandler : IRequestHandler<UpdateHabitDetailsCommand>
{
    private readonly IHabitRepository _habitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateHabitDetailsCommandHandler(IHabitRepository habitRepository,
        IUnitOfWork unitOfWork)
    {
        _habitRepository = habitRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(UpdateHabitDetailsCommand request, CancellationToken cancellationToken)
    {
        var queryResult = await _habitRepository.GetByIdAsync(request.HabitId);
        if (!queryResult.TryPickT0(out var habit, out var notFound)) return;
        habit.ToDoItemDescription = request.ToDoItemDescription;
        habit.DefaultStartTime = request.DefaultStartTime;
        habit.DefaultEndTime = request.DefaultEndTime;
        habit.Description = request.Description;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}