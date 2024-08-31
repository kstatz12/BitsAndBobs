namespace BitsAndBobs;

[Serializable]
public class DomainException : Exception
{
    public DomainExceptionType Type { get; }

    public DomainException(DomainExceptionType type, string message) : base(message)
    {
        this.Type = type;
    }

    public DomainException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

public enum DomainExceptionType
{
    InvalidInput,
    NotFound,
    Unauthorized,
    Forbidden,
    Conflict,
    InternalError
}
