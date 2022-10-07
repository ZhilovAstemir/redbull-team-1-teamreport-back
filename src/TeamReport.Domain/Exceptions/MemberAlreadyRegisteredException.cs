namespace TeamReport.Domain.Exceptions;

public class MemberAlreadyRegisteredException : Exception
{
    public MemberAlreadyRegisteredException(string message) : base(message) { }
}