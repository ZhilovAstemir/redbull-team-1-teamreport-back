using Microsoft.EntityFrameworkCore;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Persistence;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;

namespace redbull_team_1_teamreport_back.Data.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _context;

    public MemberRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Member Create(Member member)
    {
        _context.Members.Add(member);
        _context.SaveChanges();

        return member;
    }

    public Member? Read(int entityId)
    {
        return _context.Members.FirstOrDefault(c => c.Id == entityId);
    }

    public Member? ReadByEmail(string email)
    {
        return _context.Members.FirstOrDefault(c => c.Email == email);
    }

    public List<Member> ReadAll()
    {
        return _context.Members.ToList();
    }


    public bool Update(Member entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
        return true;
    }

    public bool Delete(int entityId)
    {
        var member = Read(entityId);
        if (member == null)
        {
            return false;
        }
        _context.Members.Remove(member);
        _context.SaveChanges();
        return true;
    }
}
