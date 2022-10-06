using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using TeamReport.Data.Entities;
using TeamReport.WebAPI.Helpers;

namespace TeamReport.WebAPI.Tests.Helpers;

public class AuthorizeAttributeTest
{
    [Fact]
    public void ShouldBeAbleToCreateAuthorizeAttribute()
    {
        var attribute = new AuthorizeAttribute();
        attribute.Should().NotBeNull();
    }

    [Fact]
    public void ShouldReturnUnAuthorizedIfNoUserInContext()
    {
        var attribute = new AuthorizeAttribute();
        var context = new AuthorizationFilterContext(new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()),
            new List<IFilterMetadata>());

        attribute.OnAuthorization(context);

        context.Result.Should().BeOfType<JsonResult>().Which.StatusCode.Value.Should().Be(401);
    }

    [Fact]
    public void ShouldWorksCorrectlyWhenUserFoundedInContext()
    {
        var attribute = new AuthorizeAttribute();
        var context = new AuthorizationFilterContext(new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()),
            new List<IFilterMetadata>());

        context.HttpContext.Items["Member"] = new Member();

        attribute.OnAuthorization(context);

        context.Result.Should().BeNull();
    }
}