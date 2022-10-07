namespace TeamReport.Domain.Exceptions;

public class InvalidCreditalsException : Exception
{
    public InvalidCreditalsException(string message) : base(message) { }
}