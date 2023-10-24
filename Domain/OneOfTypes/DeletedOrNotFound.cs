using Domain.Primitives;
using OneOf.Types;

namespace Domain.OneOfTypes;

[GenerateOneOf]
public sealed partial class DeletedOrNotFound : OneOfBase<Deleted, NotFound>
{
}