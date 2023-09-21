namespace CarReferenceGuide.Application.Domain.Exceptions;

public sealed record UserFriendlyExceptionResponse(IEnumerable<string?> Errors)
{
    public UserFriendlyExceptionResponse(string error) : this(new List<string?> {error})
    {
    }
}