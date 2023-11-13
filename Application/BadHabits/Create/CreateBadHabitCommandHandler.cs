using Domain;
using Domain.BadHabit;
using MediatR;

namespace Application.BadHabits.Create;

public sealed class CreateBadHabitCommandHandler : IRequestHandler<CreateBadHabitCommand>
{
    private readonly IBadHabitRepository _badHabitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBadHabitCommandHandler(IBadHabitRepository badHabitRepository,
        IUnitOfWork unitOfWork)
    {
        _badHabitRepository = badHabitRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(CreateBadHabitCommand request, CancellationToken cancellationToken)
    {
        var badHabit = new BadHabit
        {
            Id = new BadHabitId(Guid.NewGuid()),
            Description = request.Description,
            StartDate = DateTimeOffset.UtcNow,
        };
        _badHabitRepository.AddBadHabit(badHabit);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}