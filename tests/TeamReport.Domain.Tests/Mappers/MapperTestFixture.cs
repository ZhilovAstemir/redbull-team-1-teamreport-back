using TeamReport.Data.Entities;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Models;

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

    public MemberModel GetMemberModel()
    {
        return new MemberModel()
        {
            Email = "email@email.com",
            Password = "password",
        };
    }

}