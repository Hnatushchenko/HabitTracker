using FluentValidation;
using FluentValidation.Results;

namespace Domain.Habit;

public sealed class FrequencyCount : ValueOf<int, FrequencyCount>
{
    protected override void Validate()
    {
        if (Value < 1)
        {
            var failure = new ValidationFailure(nameof(FrequencyCount),
                $"'{nameof(FrequencyCount)}' must be greater than 0.",
                Value);
            throw new ValidationException(new [] {failure});
        }
    }
}