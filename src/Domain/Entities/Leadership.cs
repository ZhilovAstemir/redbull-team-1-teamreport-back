namespace redbull_team_1_teamreport_back.Domain.Entities;

public class Leadership
{
    public int Id { get; set; }
    public Member? Leader { get; set; }
    public Member? Member { get; set; }
}