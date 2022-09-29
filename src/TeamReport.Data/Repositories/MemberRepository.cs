using redbull_team_1_teamreport_back.Domain.Entities;
using redbull_team_1_teamreport_back.Domain.Persistence;
using redbull_team_1_teamreport_back.Domain.Repositories.Interfaces;

namespace redbull_team_1_teamreport_back.Domain.Repositories;
public class MemberRepository: IMemberRepository
{
    private readonly ApplicationDbContext _context;

    public MemberRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Member GetMemberByEmail(string email) => _context.Members.FirstOrDefault(c => c.Email == email);

    public int AddMember(Member member)
    {
        _context.Members.Add(member);
        _context.SaveChanges();

        return member.Id;
    }
}
