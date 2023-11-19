using System.Diagnostics;

namespace Helpers.Extensions;

public static class TaskExt
{
    public static async Task<IEnumerable<T>> WhenAll<T>(params Task<T>[] tasks)
    {
        var allTasks = Task.WhenAll(tasks);
        try
        {
            return await allTasks;
        }
        catch (Exception)
        {
            throw allTasks.Exception ?? 
                  throw new UnreachableException("Task.Exception property cannot be null if the exception was thrown when trying to await the task.");
        }
    }
}