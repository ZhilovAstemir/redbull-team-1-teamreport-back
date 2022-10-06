using FluentAssertions;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services;

namespace TeamReport.Domain.Tests.Services;
public class EmailServiceTest
{
    private readonly ServiceTestFixture _fixture;

    public EmailServiceTest()
    {
        _fixture = new ServiceTestFixture();
    }

    [Fact]
    public void ShouldBeAbleToCreateAuthorizationService()
    {
        var service = new EmailService(_fixture.GetNewOptions());
        service.Should().NotBeNull();
    }
}
