using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
using TeamReport.Domain.Exceptions;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;

namespace TeamReport.Domain.Services;

public class CompanyService:ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;
    public CompanyService(ICompanyRepository companyRepository,IMemberRepository memberRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _memberRepository = memberRepository;
        _mapper = mapper;
    }
    

    public async Task<CompanyModel?> GetCompany(int memberId)
    {
        var member = await _memberRepository.Read(memberId);
        if (member?.Company != null)
        {
            return _mapper.Map<Company, CompanyModel>(member.Company);
        }
        return null;
    }

    public async Task<CompanyModel?> SetName(int memberId, string newCompanyName)
    {
        var member = await _memberRepository.Read(memberId);
        if (member?.Company != null)
        {
            member.Company.Name = newCompanyName;
            await _memberRepository.Update(member);
            return _mapper.Map<Company, CompanyModel>(member.Company);
        }
        return null;
    }

    public async Task<MemberModel?> Register(MemberModel memberModel)
    {
        var member = _mapper.Map<MemberModel,Member>(memberModel);
        if (member.Company?.Name != null)
        {
           member.Password = PasswordHash.HashPassword(member.Password);
           var createdMember =await _memberRepository.Create(member);

           return _mapper.Map<Member,MemberModel>(createdMember);
        }

        return null;
    }

}