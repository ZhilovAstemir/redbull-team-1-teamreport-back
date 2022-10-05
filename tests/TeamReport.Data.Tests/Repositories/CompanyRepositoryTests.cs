using FluentAssertions;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Persistence;
using redbull_team_1_teamreport_back.Data.Repositories;

namespace TeamReport.Data.Tests.Repositories;

public class CompanyRepositoryTests
{
    private readonly RepositoryTestFixture _fixture;
    private readonly ApplicationDbContext _context;


    public CompanyRepositoryTests()
    {
        _fixture = new RepositoryTestFixture();
        _context = _fixture.GetContext();
    }

    
    [Fact]
    public void ShouldBeAbleToCreateCompanyRepository()
    {
        var repository = new CompanyRepository(_context);
        repository.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldBeAbleToCreateCompany()
    {
        _fixture.ClearDatabase(_context);

        var repository = new CompanyRepository(_context);

        var company = _fixture.GetCompany();

        await repository.Create(company);

        _context.Companies.Should().Contain(company);
    }

    [Fact]
    public async Task ShouldBeAbleToReadCompanyById()
    {
        _fixture.ClearDatabase(_context);

        var repository = new CompanyRepository(_context);

        var company = _fixture.GetCompany();

        var addedCompany = await repository.Create(company);

        var gotCompany = await repository.Read(addedCompany.Id);

        gotCompany?.Name.Should().Be(company.Name);
    }

    [Fact]
    public async Task ShouldBeAbleToGetAllCompanies()
    {
        _fixture.ClearDatabase(_context);

        var repository = new CompanyRepository(_context);

        var company = _fixture.GetCompany();

        await repository.Create(company);

        var companies = await repository.ReadAll();

        companies.Should().Contain(company);
    }

    [Fact]
    public async Task ShouldBeAbleToUpdateCompany()
    {
        _fixture.ClearDatabase(_context);

        var repository = new CompanyRepository(_context);
        var company = _fixture.GetCompany();

        await repository.Create(company);

        company.Name = "NewCompanyName";

        (await repository.Update(company)).Should().BeTrue();

        var updatedCompany = await repository.Read(company.Id);
        updatedCompany?.Name.Should().Be(company.Name);
    }

    [Fact]
    public async Task ShouldBeAbleToDeleteCompany()
    {
        _fixture.ClearDatabase(_context);

        var repository = new CompanyRepository(_context);
        var company = _fixture.GetCompany();

        await repository.Create(company);
        (await repository.Read(company.Id)).Should().NotBeNull();

        (await repository.Delete(company.Id)).Should().BeTrue();

        (await repository.Read(company.Id)).Should().BeNull();
    }

    [Fact]
    public async Task ShouldDeleteReturnFalseIfNothingToDelete()
    {
        _fixture.ClearDatabase(_context);

        var repository = new CompanyRepository(_context);

        (await repository.Delete(0)).Should().BeFalse();
    }
}