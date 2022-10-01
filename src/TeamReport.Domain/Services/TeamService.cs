using AutoMapper;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;

namespace TeamReport.Domain.Services;
public class TeamService: ITeamService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public TeamService(IMemberRepository memberRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public Member Add(Member member)
    {
        return _memberRepository.Create(member);
    }

    public Member? Get(int id)
    {
        return _memberRepository.Read(id);
    }

    public List<Member> GetAll()
    {
        return _memberRepository.ReadAll();
    }
}
