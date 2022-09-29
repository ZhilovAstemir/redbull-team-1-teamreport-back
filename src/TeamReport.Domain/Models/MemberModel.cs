namespace TeamReport.Domain.Models;

public class MemberModel
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Title { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
    public CompanyModel? Company { get; set; }
}
