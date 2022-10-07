using FluentAssertions;
using MimeKit;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Tests.Models;

public class MessageTest
{
    [Fact]
    public void ShouldBeAbleToCreateMessage()
    {
        var message = new Message(new List<string>(), "", "");
        message.Should().NotBeNull().And.BeOfType<Message>();
    }

    [Fact]
    public void ShouldMessageHaveProps()
    {
        var message = new Message(new List<string>(), "", "");
        message.To.Should().BeOfType<List<MailboxAddress>>();
        message.Subject.Should().BeOfType<string>();
        message.Content.Should().BeOfType<string>();
    }
}