namespace redbull_team_1_teamreport_back.Domain.Entities;

public class Week
{
    public int Id { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public List<Report> Reports { get; set; }
}