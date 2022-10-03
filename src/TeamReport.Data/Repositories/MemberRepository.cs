using Microsoft.EntityFrameworkCore;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Persistence;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;

namespace redbull_team_1_teamreport_back.Data.Repositories;
public class MemberRepository: IMemberRepository
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
        return await _context.Members.FirstOrDefaultAsync(c => c.Id == entityId);
    }

    public async Task<Member?> ReadByEmail(string email)
    {
        return await _context.Members.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<List<Member>> ReadAll()
    {
        return await _context.Members.ToListAsync();
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