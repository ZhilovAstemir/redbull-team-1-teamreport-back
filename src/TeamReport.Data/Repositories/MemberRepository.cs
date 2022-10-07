using Microsoft.EntityFrameworkCore;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories.Interfaces;

namespace TeamReport.Data.Repositories;
public class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _context;

    public MemberRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Member> Create(Member member)
    {
        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        return member;
    }

    public async Task<Member?> Read(int entityId)
    {
        var member = await _context.Members.Include(x => x.Company).FirstOrDefaultAsync(c => c.Id == entityId);
        return member;
    }

    public async Task<Member?> ReadByEmail(string email)
    {
        return await _context.Members.Include(x => x.Company).FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<List<Member>> ReadByCompany(int companyId)
    {
        return await _context.Members.Include(x => x.Company).Where(x => x.Company.Id == companyId).ToListAsync();
    }

    public async Task<List<Member>> ReadAll()
    {
        return await _context.Members.Include(x => x.Company).ToListAsync();
    }


    public async Task<bool> Update(Member entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int entityId)
    {
        var member = await Read(entityId);
        if (member == null)
        {
            return false;
        }
        _context.Members.Remove(member);
        await _context.SaveChangesAsync();
        return true;
    }
}