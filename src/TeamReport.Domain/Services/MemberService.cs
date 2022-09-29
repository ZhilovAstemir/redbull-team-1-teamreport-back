using AutoMapper;
using redbull_team_1_teamreport_back.Domain.Entities;
using redbull_team_1_teamreport_back.Domain.Repositories.Interfaces;
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

    public int AddMember(MemberModel member)
    {

        member.Password = PasswordHash.HashPassword(member.Password);
        return _memberRepository.AddMember(_mapper.Map<Member>(member));
    }
}
