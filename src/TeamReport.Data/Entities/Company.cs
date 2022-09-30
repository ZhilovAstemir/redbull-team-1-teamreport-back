namespace redbull_team_1_teamreport_back.Data.Entities;

public class Company
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Member> Member { get; set; }
}