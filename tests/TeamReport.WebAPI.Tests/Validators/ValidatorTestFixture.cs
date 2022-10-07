using TeamReport.Data.Entities;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Tests.Validators;

public class ValidatorTestFixture
{
    public CompanyRegistrationRequest GetCompanyRegistrationRequest()
    {
        var member = GetMember();

        return new CompanyRegistrationRequest()
        {
            Email = member.Email,
            Company = member.Company,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Password = member.Password,
            Title = member.Title
        };
    }

    public UpdateCompanyNameRequest GetUpdateCompanyNameRequest()
    {
        return new UpdateCompanyNameRequest() { NewCompanyName = "New Comapny Name" };
    }

    public ContinueRegistrationRequest GetContinueRegistrationRequest()
    {
        return new ContinueRegistrationRequest() { Password = "Password!", Title = "Title" };
    }

    public Member GetMember()
    {
        return new Member()
        {
            Email = "email@email.com",
            Password = "password1",
            FirstName = "FirstName",
            LastName = "LastName",
            Title = "Title",
            Company = GetCompany()
        };
    }

    public Company GetCompany()
    {
        return new Company()
        {
            Name = "CompanyName"
        };
    }
}