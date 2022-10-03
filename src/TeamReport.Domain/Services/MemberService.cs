using AutoMapper;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;

namespace TeamReport.Domain.Services;
public class MemberService: IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public MemberService(IMemberRepository memberRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<Member> AddMember(MemberModel member)
    { 
        member.Password = PasswordHash.HashPassword(member.Password);
        return await _memberRepository.Create(_mapper.Map<MemberModel,Member>(member));
    }

    public async Task<List<Member>> GetAll()
    {
        return await _memberRepository.ReadAll();
    }
}

