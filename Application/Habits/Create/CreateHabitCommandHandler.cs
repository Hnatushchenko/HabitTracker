using Domain;
using Domain.Habit;
using MediatR;

namespace Application.Habits.Create;

public sealed class CreateHabitCommandHandler : IRequestHandler<CreateHabitCommand>
{
    private readonly IHabitRepository _habitRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public CreateHabitCommandHandler(IHabitRepository habitRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _habitRepository = habitRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }
        
    public async Task Handle(CreateHabitCommand request, CancellationToken cancellationToken)
    {
        var habit = CreateHabit(request);
        await AddHabitAsync(habit, cancellationToken);
        await PublishHabitCreatedNotificationAsync(habit, cancellationToken);
    }

    private static Habit CreateHabit(CreateHabitCommand request)
    {
        return new Habit
        {
            Id = new HabitId(Guid.NewGuid()),
            Description = request.Description,
            FrequencyTimeUnit = request.TimeUnit,
            FrequencyCount = FrequencyCount.From(request.FrequencyCount),
            StartDate = request.StartDate,
            DayOfWeekFrequency = request.DayOfWeekFrequency,
            DefaultStartTime = request.DefaultStartTime,
            DefaultEndTime = request.DefaultEndTime,
            ToDoItemDescription = request.ToDoItemDescription
        };
    }

    private async Task AddHabitAsync(IHabit habit, CancellationToken cancellationToken)
    {
        _habitRepository.Add(habit);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
        
    private async Task PublishHabitCreatedNotificationAsync(Habit habit, CancellationToken cancellationToken)
    {
        var habitCreatedNotification = new HabitCreatedNotification
        {
            Habit = habit
        };
        await _publisher.Publish(habitCreatedNotification, cancellationToken);
    }
}