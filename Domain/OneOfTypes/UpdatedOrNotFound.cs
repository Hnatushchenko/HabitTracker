using Domain.Primitives;
using OneOf.Types;

namespace Domain.OneOfTypes;

[GenerateOneOf]
public sealed partial class UpdatedOrNotFound : OneOfBase<Updated, NotFound>
{
}