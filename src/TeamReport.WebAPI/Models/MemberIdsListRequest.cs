namespace TeamReport.WebAPI.Models;

public class MemberIdsListRequest
{
    public int MemberId { get; set; }
    public List<int> MembersIds { get; set; }
}