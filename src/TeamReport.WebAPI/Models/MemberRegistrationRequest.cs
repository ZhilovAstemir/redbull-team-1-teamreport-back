namespace TeamReport.Domain.Models.Requests;

public class MemberRegistrationRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Title { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
