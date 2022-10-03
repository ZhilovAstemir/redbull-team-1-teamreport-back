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

    public async Task<Member> GetMemberByEmail(string email) => await _context.Members.FirstOrDefaultAsync(c => c.Email == email);

    public async Task<int> AddMember(Member member)
    {
        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        return member.Id;
    }

    public List<Member> GetAll()
    {
        return _context.Members.ToList();
    }
}
