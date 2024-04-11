namespace Application.Exceptions;

public class InvalidQualityActionException : Exception
{
    public InvalidQualityActionException() : base() { }
    public InvalidQualityActionException(string message) : base(message) { }
    public InvalidQualityActionException(string message, Exception innerException) : base(message, innerException) { }
}