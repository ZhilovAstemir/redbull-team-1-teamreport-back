namespace redbull_team_1_teamreport_back.Data.Entities;

public class Member
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Title { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Company? Company { get; set; }
    public List<Leadership> Leaderships { get; set; }
    public List<Leadership> Memberships { get; set; }
    public List<Report> Reports { get; set; }
}