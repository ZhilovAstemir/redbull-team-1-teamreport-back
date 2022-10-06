using AutoMapper;
using TeamReport.Data.Entities;
using TeamReport.Data.Repositories.Interfaces;
using TeamReport.Domain.Services.Interfaces;

namespace TeamReport.Domain.Services;
public class TeamService : ITeamService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public TeamService(IMemberRepository memberRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<Member> Add(Member member)
    {
        return await _memberRepository.Create(member);
    }

    public async Task<Member?> Get(int id)
    {
        return await _memberRepository.Read(id);
    }

    public async Task<List<Member>> GetAll()
    {
        return await _memberRepository.ReadAll();
    }
}
