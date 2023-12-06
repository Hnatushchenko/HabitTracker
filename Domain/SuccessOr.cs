using OneOf.Types;

namespace Domain;

[GenerateOneOf]
public partial class SuccessOr<TError> : OneOfBase<Success, TError>;
