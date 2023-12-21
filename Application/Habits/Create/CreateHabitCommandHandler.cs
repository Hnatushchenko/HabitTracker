using Application.Common.Services.Creators.InitialHabitToDoItemsCreator;
using Domain;
using Domain.Habit;
using Domain.Habit.ValueObjects;
using MediatR;

namespace Application.Habits.Create;

public sealed class CreateHabitCommandHandler : IRequestHandler<CreateHabitCommand>
{
    private readonly IInitialHabitToDoItemsCreator _initialHabitToDoItemsCreator;
    private readonly IHabitRepository _habitRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public CreateHabitCommandHandler(IInitialHabitToDoItemsCreator initialHabitToDoItemsCreator,
        IHabitRepository habitRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _initialHabitToDoItemsCreator = initialHabitToDoItemsCreator;
        _habitRepository = habitRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }
        
    public async Task Handle(CreateHabitCommand request, CancellationToken cancellationToken)
    {
        var habit = CreateHabit(request);
        _habitRepository.Add(habit);
        await _initialHabitToDoItemsCreator.CreateInitialToDoItemsAsync(habit, cancellationToken);
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        await PublishHabitCreatedNotificationAsync(habit);
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
    
    private async Task PublishHabitCreatedNotificationAsync(Habit habit)
    {
        var habitCreatedNotification = new HabitCreatedNotification
        {
            Habit = habit
        };
        await _publisher.Publish(habitCreatedNotification);
    }
}