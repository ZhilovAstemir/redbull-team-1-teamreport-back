using AutoMapper;
using TeamReport.Data.Repositories.Interfaces;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;
using Member = TeamReport.Data.Entities.Member;

namespace TeamReport.Domain.Services;
public class TeamService : ITeamService
{
    private readonly IMemberRepository _memberRepository;
    private readonly ILeadershipRepository _leadershipRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public TeamService(IMemberRepository memberRepository, ILeadershipRepository leadershipRepository, ICompanyRepository companyRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _leadershipRepository = leadershipRepository;
        _companyRepository = companyRepository;
        _mapper = mapper;
    }


    public async Task<List<MemberModel>> GetMemberLeaders(int memberId)
    {
        var leadersOfMember = await _leadershipRepository.ReadLeaders(memberId);
        var memberModels = _mapper.Map<List<Member>, List<MemberModel>>(leadersOfMember);
        if (memberModels is null)
        {
            throw new AutoMapperMappingException();
        }
        return memberModels.ToList();
    }

    public async Task<List<MemberModel>> GetMemberReporters(int memberId)
    {
        var reportersOfMember = await _leadershipRepository.ReadReporters(memberId);
        var memberModels = _mapper.Map<List<Member>, List<MemberModel>>(reportersOfMember);
        if (memberModels is null)
        {
            throw new AutoMapperMappingException();
        }
        return memberModels.ToList();
    }

    public async Task<List<MemberModel>> UpdateMemberLeaders(int memberId, List<int> newLeadersIds)
    {
        var leadersOfMember = await _leadershipRepository.ReadLeaders(memberId);
        var leadersModels = _mapper.Map<List<Member>, List<MemberModel>>(leadersOfMember);
        if (leadersModels is null)
        {
            throw new AutoMapperMappingException();
        }

        var newLeaders = await _leadershipRepository.UpdateLeaders(memberId, newLeadersIds);

        return _mapper.Map<List<Member>, List<MemberModel>>(newLeaders);
    }

    public async Task<List<MemberModel>> UpdateMemberReporters(int memberId, List<int> newReportersIds)
    {
        var reportersOOfMember = await _leadershipRepository.ReadReporters(memberId);
        var reportersModel = _mapper.Map<List<Member>, List<MemberModel>>(reportersOOfMember);
        if (reportersModel is null)
        {
            throw new AutoMapperMappingException();
        }

        var newReporters = await _leadershipRepository.
            UpdateReporters(memberId, newReportersIds);

        return _mapper.Map<List<Member>, List<MemberModel>>(newReporters);
    }

    public async Task<List<MemberModel>> GetAllTeamMembers(int companyId)
    {
        var teamMembers = await _memberRepository.ReadAll();
        return _mapper.Map<List<Member>, List<MemberModel>>(teamMembers);
    }
}
