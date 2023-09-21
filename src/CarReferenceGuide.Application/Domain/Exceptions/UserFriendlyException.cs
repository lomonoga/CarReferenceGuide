namespace CarReferenceGuide.Application.Domain.Exceptions;

public class UserFriendlyException : Exception
{
    public UserFriendlyException(string message = "Ошибка", Exception? innerException = null) 
        : base(message, innerException)
    {
    }
}