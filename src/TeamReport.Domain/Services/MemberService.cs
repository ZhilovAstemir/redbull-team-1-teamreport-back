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
public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public MemberService(IMemberRepository memberRepository, ICompanyRepository companyRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<List<MemberModel>> GetAll()
    {
        return _mapper.Map<List<Member>, List<MemberModel>>(await _memberRepository.ReadAll());
    }

    public async Task<MemberModel> Login(string email, string password)
    {
        var member = await _memberRepository.ReadByEmail(email);
        if (member == null)
        {
            throw new EntityNotFoundException("Member not found");
        }
        if (!PasswordHash.ValidatePassword(password, member.Password))
        {
            throw new EntityNotFoundException("Invalid creditals");
        }
        return _mapper.Map<Member, MemberModel>(member);
    }


    public async Task<string> GetToken(MemberModel member)
    {
        if (member is null || member.Email is null)
        {
            throw new DataException("Object or part of it is empty");
        }

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256),
            claims: new List<Claim>() { new Claim("user", member.Id.ToString()) });

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public async Task<MemberModel> Register(MemberModel memberModel)
    {
        var member = _mapper.Map<MemberModel, Member>(memberModel);
        member.Password = PasswordHash.HashPassword(member.Password);
        member.Company = await _companyRepository.Read(memberModel.Company.Id);

        if (await _memberRepository.ReadByEmail(member.Email) != null)
        {
            throw new UsedEmailException("User with the same email is already exists");
        }

        var addedMember = await _memberRepository.Create(member);

        return _mapper.Map<Member, MemberModel>(addedMember);
    }
}

