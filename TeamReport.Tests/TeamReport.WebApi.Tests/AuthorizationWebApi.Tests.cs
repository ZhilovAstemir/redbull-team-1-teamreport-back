using AutoMapper;
using Moq;
using TeamReport.Domain.Mappers;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Controllers;
using TeamReport.WebAPI.Models.Requests;

namespace TeamReport.Tests.TeamReport.WebApi.Tests;
public class AuthorizationControllerTests
{
    private AuthorizationController _sut;
    private Mock<IAuthorizationServices> _memberServiceMock;
    private IMapper _mapper;

    public AuthorizationControllerTests()
    {
        _memberServiceMock = new Mock<IAuthorizationServices>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfigStorage>()));
        _sut = new AuthorizationController(_memberServiceMock.Object);
    }

    [Fact]
    public async Task ShouldBeAbleToReturnToken()
    {
        var login = new LoginRequest()
        {
            Email = "lana@mail.com",
            Password = "Ahinef09"
        };

        var actual = await _sut.Login(login);

        Assert.NotNull(actual);
    }
}
