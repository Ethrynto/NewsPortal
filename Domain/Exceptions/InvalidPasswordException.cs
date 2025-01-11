namespace Domain.Exceptions;

public class InvalidPasswordException : Exception
{
    private const string DefaultMessage = "Wrong password.";
    
    public InvalidPasswordException() 
        : base(DefaultMessage)
    {
    }

    public InvalidPasswordException(string message) 
        : base(message)
    {
    }

    public InvalidPasswordException(string message, Exception inner) 
        : base(message, inner)
    {
    }
}