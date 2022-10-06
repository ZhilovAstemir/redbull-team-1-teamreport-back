using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Moq;
using TeamReport.Data.Entities;
using TeamReport.Data.Exceptions;
using TeamReport.Data.Repositories;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services;
using TeamReport.WebAPI.Helpers;

namespace TeamReport.WebAPI.Tests.Helpers;

public class JwtMiddlewareTest
{
    private readonly HelpersTestFixture _fixture;
    public JwtMiddlewareTest()
    {
        _fixture = new HelpersTestFixture();
    }

    [Fact]
    public void ShouldBeAbleToCreateJwtMiddleware()
    {
        var middleware = new JwtMiddleware(null, WebApplication.CreateBuilder().Configuration);
        middleware.Should().NotBeNull();
    }

    [Fact]
    public void ShouldJwtMiddlewareDoNothingIfNoToken()
    {
        var middleware = new JwtMiddleware(null, WebApplication.CreateBuilder().Configuration);
        var context = _fixture.GetContext();
        var memberRepository = new MemberRepository(context);
        var task = middleware.Invoke(new DefaultHttpContext(), memberRepository);
        task.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldJwtMiddlewareValidateToken()
    {
        var httpContext = new DefaultHttpContext();
        var middleware = new JwtMiddleware(It.IsAny<RequestDelegate>(), WebApplication.CreateBuilder().Configuration);
        var memberRepository = new MemberRepository(_fixture.GetContext());
        var member = await memberRepository.Create(_fixture.GetMember());
        var mapper = _fixture.GetMapper();
        var authService = new MemberService(memberRepository, mapper);


        httpContext.Request.Headers.Add("Authorization", await authService.GetToken(mapper.Map<Member, MemberModel>(member)));
        var task = middleware.Invoke(httpContext, memberRepository);

        task.IsCompleted.Should().BeTrue();

        httpContext.Items["Member"].Should().BeOfType<Member>();
    }

    [Fact]
    public void ShouldJwtMiddlewareThrowTokenValidationException()
    {
        var httpContext = new DefaultHttpContext();
        var middleware = new JwtMiddleware(It.IsAny<RequestDelegate>(), WebApplication.CreateBuilder().Configuration);
        var memberRepository = new MemberRepository(_fixture.GetContext());
        var member = memberRepository.Create(_fixture.GetMember());
        var mapper = _fixture.GetMapper();
        var authService = new MemberService(memberRepository, mapper);


        httpContext.Request.Headers.Add("Authorization", "2012347192740912oaisfhoiahsdfoi");
        var task = () => middleware.Invoke(httpContext, memberRepository);

        task.Should().ThrowAsync<TokenValidationException>();
    }
}