namespace Application.Common.Services.Updaters;

public interface IOverdueTasksDueDateUpdater
{
    Task SetDueDateForTodayForOverdueTasks(CancellationToken cancellationToken);
}
