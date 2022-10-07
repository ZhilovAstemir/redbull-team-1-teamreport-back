namespace TeamReport.WebAPI.Models;

public class InviteMemberModelRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int CurrentUserId { get; set; }
}
