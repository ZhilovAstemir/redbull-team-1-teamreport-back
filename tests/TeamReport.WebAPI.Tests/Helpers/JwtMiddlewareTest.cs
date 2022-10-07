using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Moq;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories;
using TeamReport.Data.Repositories.Interfaces;
using TeamReport.Domain.Exceptions;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services;
using TeamReport.WebAPI.Helpers;

namespace TeamReport.WebAPI.Tests.Helpers;

public class JwtMiddlewareTest
{
    private readonly HelpersTestFixture _fixture;
    private readonly IMemberRepository _memberRepository;
    private readonly ILeadershipRepository _leadershipRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ApplicationDbContext _context;
    public JwtMiddlewareTest()
    {
        _fixture = new HelpersTestFixture();
        _context = _fixture.GetContext();
        _memberRepository = new MemberRepository(_context);
        _leadershipRepository = new LeadershipRepository(_context);
        _companyRepository = new CompanyRepository(_context);
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
        var task = middleware.Invoke(new DefaultHttpContext(), _memberRepository);
        task.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldJwtMiddlewareValidateToken()
    {
        var httpContext = new DefaultHttpContext();
        var middleware = new JwtMiddleware(It.IsAny<RequestDelegate>(), WebApplication.CreateBuilder().Configuration);
        var memberRepository = new MemberRepository(_fixture.GetContext());
        var companyRepository = new CompanyRepository(_fixture.GetContext());
        var member = await memberRepository.Create(_fixture.GetMember());
        var mapper = _fixture.GetMapper();
        var authService = new MemberService(memberRepository, companyRepository, mapper);


        httpContext.Request.Headers.Add("Authorization", await authService.GetToken(mapper.Map<Member, MemberModel>(member)));
        var task = middleware.Invoke(httpContext, _memberRepository);

        task.IsCompleted.Should().BeTrue();

        httpContext.Items["Member"].Should().BeOfType<Member>();
    }

    [Fact]
    public void ShouldJwtMiddlewareThrowTokenValidationException()
    {
        var httpContext = new DefaultHttpContext();
        var middleware = new JwtMiddleware(It.IsAny<RequestDelegate>(), WebApplication.CreateBuilder().Configuration);
        var memberRepository = new MemberRepository(_fixture.GetContext());
        var companyRepository = new CompanyRepository(_fixture.GetContext());
        var member = memberRepository.Create(_fixture.GetMember());
        var mapper = _fixture.GetMapper();
        var authService = new MemberService(memberRepository, companyRepository, mapper);


        httpContext.Request.Headers.Add("Authorization", "2012347192740912oaisfhoiahsdfoi");
        var task = () => middleware.Invoke(httpContext, _memberRepository);

        task.Should().ThrowAsync<TokenValidationException>();
    }
}