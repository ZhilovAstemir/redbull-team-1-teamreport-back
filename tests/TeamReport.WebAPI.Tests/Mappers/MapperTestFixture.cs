using redbull_team_1_teamreport_back.Data.Entities;
using TeamReport.Domain.Models;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Models.Requests;

namespace TeamReport.Domain.Tests.Mappers;

public class MapperTestFixture
{
    public Member GetMember()
    {
        return new Member()
        {
            Email = "email@email.com",
            Password = PasswordHash.HashPassword("password"),
            FirstName = "FirstName",
            LastName = "LastName",
            Title = "Title"
        };
    }

    public MemberRegistrationRequest GetMemberRegistrationRequest()
    {
        var member = GetMember();
        return new MemberRegistrationRequest()
        {
            Email = member.Email,
            FirstName = member.FirstName,
            LastName = member.FirstName,
            Password = member.Password,
            Title = member.Title
        };
    }
    
}