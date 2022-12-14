using Microsoft.EntityFrameworkCore;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories.Interfaces;

namespace TeamReport.Data.Repositories;
public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Company> Create(Company entity)
    {
        var company = _context.Companies.Add(entity);
        await _context.SaveChangesAsync();
        return company.Entity;
    }

    public async Task<Company?> Read(int entityId)
    {
        return await _context.Companies.FirstOrDefaultAsync(x => x.Id == entityId);
    }

    public async Task<bool> Update(Company entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int entityId)
    {
        var company = await Read(entityId);
        if (company is null)
        {
            return false;
        }

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Company>> ReadAll()
    {
        return await _context.Companies.ToListAsync();
    }
}

