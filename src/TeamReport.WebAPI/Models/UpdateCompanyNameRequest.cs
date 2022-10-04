namespace TeamReport.WebAPI.Models;

public class UpdateCompanyNameRequest
{
    public int MemberId { get; set; }

    public string NewCompanyName { get; set; }
}