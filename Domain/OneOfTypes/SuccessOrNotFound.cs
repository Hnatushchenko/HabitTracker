using OneOf.Types;

namespace Domain.OneOfTypes;

[GenerateOneOf]
public sealed partial class SuccessOrNotFound : OneOfBase<Success, NotFound>
{
}