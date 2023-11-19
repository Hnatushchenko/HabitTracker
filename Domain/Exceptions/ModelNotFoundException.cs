namespace Domain.Exceptions;

public abstract class ModelNotFoundException : Exception
{
    protected ModelNotFoundException()
    {
    }

    protected ModelNotFoundException(string message)
        : base(message)
    {
    }

    protected ModelNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
    
    public required Guid ModelId { get; init; }
}