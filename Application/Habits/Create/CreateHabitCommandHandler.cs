using Domain;
using Domain.Habit;
using MediatR;

namespace Application.Habits.Create;

public sealed class CreateHabitCommandHandler : IRequestHandler<CreateHabitCommand>
{
    private readonly IHabitRepository _habitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateHabitCommandHandler(IHabitRepository habitRepository,
        IUnitOfWork unitOfWork)
    {
        _habitRepository = habitRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(CreateHabitCommand request, CancellationToken cancellationToken)
    {
        var habit = new Habit
        {
            Id = new HabitId(Guid.NewGuid()),
            Description = request.Description,
            FrequencyTimeUnit = request.TimeUnit,
            FrequencyCount = FrequencyCount.From(request.FrequencyCount),
            StartDate = request.StartDate,
            ToDoItemDescription = request.ToDoItemDescription
        };
        _habitRepository.Add(habit);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}