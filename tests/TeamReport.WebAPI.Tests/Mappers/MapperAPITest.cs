using AutoMapper;
using FluentAssertions;
using TeamReport.Domain.Models;
using TeamReport.WebAPI.Mappers;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Tests.Mappers;

public class MapperAPITest
{
    private readonly MapperTestFixture _fixture;
    public MapperAPITest()
    {
        _fixture = new MapperTestFixture();
    }

    [Fact]
    public void ShouldMapMemberRegistrationRequestToMemberModel()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperAPI>());
        var mapper = config.CreateMapper();

        var registrationRequest = _fixture.GetMemberRegistrationRequest();
        var memberModel = mapper.Map<MemberRegistrationRequest, MemberModel>(registrationRequest);

        memberModel.Password.Should().Be(registrationRequest.Password);
        memberModel.Email.Should().Be(registrationRequest.Email);
        memberModel.FirstName.Should().Be(registrationRequest.FirstName);
        memberModel.LastName.Should().Be(registrationRequest.LastName);
        memberModel.Title.Should().Be(registrationRequest.Title);
    }
}