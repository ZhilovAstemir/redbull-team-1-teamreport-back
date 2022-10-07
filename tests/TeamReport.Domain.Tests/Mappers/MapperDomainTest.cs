using AutoMapper;
using FluentAssertions;
using TeamReport.Data.Entities;
using TeamReport.Domain.Mappers;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Tests.Mappers;

public class MapperDomainTest
{
    private readonly MapperTestFixture _fixture;
    public MapperDomainTest()
    {
        _fixture = new MapperTestFixture();
    }

    [Fact]
    public void ShouldMapMemberToMemberModel()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperDomain>());
        var mapper = config.CreateMapper();

        var member = _fixture.GetMember();
        var memberModel = mapper.Map<Member, MemberModel>(member);

        memberModel.Id.Should().Be(member.Id);
        memberModel.Email.Should().Be(member.Email);
        memberModel.FirstName.Should().Be(member.FirstName);
        memberModel.LastName.Should().Be(member.LastName);
        memberModel.Title.Should().Be(member.Title);
    }

    [Fact]
    public void ShouldMapMemberModelToMember()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperDomain>());
        var mapper = config.CreateMapper();

        var memberModel = _fixture.GetMemberModel();
        var member = mapper.Map<MemberModel, Member>(memberModel);

        memberModel.Id.Should().Be(member.Id);
        memberModel.Email.Should().Be(member.Email);
        memberModel.FirstName.Should().Be(member.FirstName);
        memberModel.LastName.Should().Be(member.LastName);
        memberModel.Title.Should().Be(member.Title);
        memberModel.Password.Should().Be(member.Password);
    }


}