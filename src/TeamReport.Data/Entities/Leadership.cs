namespace redbull_team_1_teamreport_back.Domain.Entities;

public class Leadership
{
    public int Id { get; set; }
    public List<Member> Leaders { get; set; }
    public List<Member> Members { get; set; }
}