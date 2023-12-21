using Application.Habits.Unarchive;
using Domain;
using Domain.Habit;
using Domain.Habit.ValueObjects;
using Domain.HabitArchivedPeriodEntity;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace Application.UnitTests.Habits.Handlers;

public class UnarchiveHabitCommandHandlerTests
{
    private readonly IHabitArchivedPeriodRepository _habitArchivedPeriodRepository;
    private readonly UnarchiveHabitCommandHandler _sut;
    private readonly IHabitRepository _habitRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public UnarchiveHabitCommandHandlerTests()
    {
        _habitArchivedPeriodRepository = Substitute.For<IHabitArchivedPeriodRepository>();
        _habitRepository = Substitute.For<IHabitRepository>();
        _timeProvider = Substitute.For<TimeProvider>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _publisher = Substitute.For<IPublisher>();
        _sut = new UnarchiveHabitCommandHandler(_habitArchivedPeriodRepository, 
            _habitRepository, _timeProvider, _unitOfWork, _publisher);
    }

    [Fact]
    public async Task Handle_GivenUnarchivedHabit_ReturnsErrorAndDoesNotPublishNotifications()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var request = new UnarchiveHabitCommand(new HabitId(Guid.NewGuid()));
        var unarchivedHabit = Substitute.For<IHabit>();
        unarchivedHabit.IsArchived = false;
        _habitRepository.GetByIdAsync(Arg.Is(request.HabitId), cancellationToken).Returns(unarchivedHabit);
        
        // Act
        var result = await _sut.Handle(request, CancellationToken.None);
        
        // Assert
        result.IsT1.Should().BeTrue();
        unarchivedHabit.IsArchived.Should().BeFalse();
        await _unitOfWork.DidNotReceiveWithAnyArgs().SaveChangesAsync(default);
        await _publisher.DidNotReceiveWithAnyArgs().Publish(default!, cancellationToken);
    }

    [Fact]
    public async Task Handle_GivenValidInput_ShouldReturnSuccessAndWorkCorrectly()
    {
        // Arrange
        var utcNow = DateTimeOffset.UtcNow;
        _timeProvider.GetUtcNow().Returns(utcNow);
        var cancellationToken = CancellationToken.None;
        var request = new UnarchiveHabitCommand(new HabitId(Guid.NewGuid()));
        var unarchivedHabit = Substitute.For<IHabit>();
        unarchivedHabit.IsArchived = true;
        var habitArchivedPeriod = Substitute.For<HabitArchivedPeriod>();
        _habitRepository.GetByIdAsync(Arg.Is(request.HabitId), cancellationToken).Returns(unarchivedHabit);
        _habitArchivedPeriodRepository
            .GetUnfinishedPeriodByHabitIdAsync(Arg.Is(unarchivedHabit.Id), Arg.Any<CancellationToken>())
            .Returns(habitArchivedPeriod);
        
        // Act
        var result = await _sut.Handle(request, CancellationToken.None);
        
        // Assert
        unarchivedHabit.IsArchived.Should().BeFalse();
        result.IsT0.Should().BeTrue();
        habitArchivedPeriod.EndDate.Should().BeExactly(utcNow);
        await _unitOfWork.ReceivedWithAnyArgs().SaveChangesAsync(default);
        await _publisher.Received().Publish(Arg.Any<HabitUnarchivedNotification>(), Arg.Is(default(CancellationToken)));
    }
}
