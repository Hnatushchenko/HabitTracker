using ValueOf;

namespace Domain.Habit;

public class FrequencyCount : ValueOf<int, FrequencyCount>
{
    protected override void Validate()
    {
        if (Value < 1)
        {
            throw new ArgumentException($"Frequency count cannot be less then one. Current value: {Value}");
        }
    }
}