namespace TeamReport.Domain.Exceptions;

public class UsedEmailException : Exception
{
    public UsedEmailException(string message) : base(message) { }
}