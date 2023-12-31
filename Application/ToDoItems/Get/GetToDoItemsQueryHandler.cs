﻿using Application.Common.Services.Creators.MissingHabitBasedToDoItemsCreator;
using Application.Common.Services.Updaters;
using Domain.ToDoItem;
using MediatR;

namespace Application.ToDoItems.Get;

public sealed class GetToDoItemsQueryHandler : IRequestHandler<GetToDoItemsQuery, ToDoItemsResponse>
{
    private readonly IMissingHabitsBasedToDoItemsCreator _missingHabitsBasedToDoItemsCreator;
    private readonly IOverdueTasksDueDateUpdater _overdueTasksDueDateUpdater;
    private readonly IToDoItemRepository _toDoItemRepository;

    public GetToDoItemsQueryHandler(IMissingHabitsBasedToDoItemsCreator missingHabitsBasedToDoItemsCreator,
        IOverdueTasksDueDateUpdater overdueTasksDueDateUpdater,
        IToDoItemRepository toDoItemRepository)
    {
        _missingHabitsBasedToDoItemsCreator = missingHabitsBasedToDoItemsCreator;
        _overdueTasksDueDateUpdater = overdueTasksDueDateUpdater;
        _toDoItemRepository = toDoItemRepository;
    }
    public async Task<ToDoItemsResponse> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken)
    {
        var targetDate = request.TargetDate;
        await _overdueTasksDueDateUpdater.SetDueDateForTodayForOverdueTasks(cancellationToken);
        await _missingHabitsBasedToDoItemsCreator.CreateMissingToDoItemsAsync(targetDate, cancellationToken);
        var toDoItems = await _toDoItemRepository.GetByDueDateAndNotHiddenAsync(targetDate, cancellationToken);
        var toDoItemResponseList = ConvertAllToDoItemsToTree(toDoItems);
        var toDoItemsResponse = new ToDoItemsResponse(toDoItemResponseList);
        return toDoItemsResponse;
    }
    
    private static ToDoItemResponse MapToDoItemToToDoItemResponse(ToDoItem toDoItem)
    {
        var isToDoItemAssociatedWithHabit = toDoItem.HabitId is not null;
        var children = toDoItem.Children.Select(MapToDoItemToToDoItemResponse);
        var toDoItemResponse = new ToDoItemResponse
        {
            IsBasedOnHabit = isToDoItemAssociatedWithHabit,
            Description = toDoItem.Description,
            StartTime = toDoItem.StartTime,
            EndTime = toDoItem.EndTime,
            IsDone = toDoItem.IsDone,
            Id = toDoItem.Id.Value,
            Children = children
        };
        
        return toDoItemResponse;
    }
    
    private static List<ToDoItemResponse> ConvertAllToDoItemsToTree(IReadOnlyCollection<ToDoItem> toDoItems)
    {
        List<ToDoItemResponse> rootToDoItemResponseList = new();
        var parents = toDoItems.Where(t => t.ParentId is null);
        BuildToDoItemsTreeRecursively(parents);
        return rootToDoItemResponseList;
        
        void BuildToDoItemsTreeRecursively(IEnumerable<ToDoItem> parentToDoItems)
        {
            foreach (var parentToDoItem in parentToDoItems)
            {
                var children = toDoItems.Where(t => t.ParentId == parentToDoItem.Id).ToList();
                BuildToDoItemsTreeRecursively(children);
                var toDoItemResponse = MapToDoItemToToDoItemResponse(parentToDoItem);
                toDoItemResponse.Children = children.Select(MapToDoItemToToDoItemResponse);
                if (parentToDoItem.ParentId is null)
                {
                    rootToDoItemResponseList.Add(toDoItemResponse);
                }
            }
        }
    }
}