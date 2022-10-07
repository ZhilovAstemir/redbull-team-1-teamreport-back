using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TeamReport.Data.Entities;
using TeamReport.Data.Repositories.Interfaces;
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
            throw new InvalidCreditalsException("Member not found");
        }
        if (!PasswordHash.ValidatePassword(password, member.Password))
        {
            throw new InvalidCreditalsException("Invalid creditals");
        }
        return _mapper.Map<Member, MemberModel>(member);
    }

    public async Task<MemberModel> ContinueRegistration(MemberModel memberModel)
    {
        var member = await _memberRepository.Read(memberModel.Id);

        if (member is null)
            throw new EntityNotFoundException("Can't find user to continue registration");

        member.Title = memberModel.Title;
        member.Password = PasswordHash.HashPassword(memberModel.Password);

        if (!(await _memberRepository.Update(member)))
            throw new EntityNotFoundException("Can't update user to continue registration");

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
        if (member.Password is not null)
        {
            member.Password = PasswordHash.HashPassword(member.Password);
        }

        member.Company = await _companyRepository.Read(memberModel.Company.Id);

        if (await _memberRepository.ReadByEmail(member.Email) != null)
        {
            throw new UsedEmailException("User with the same email is already exists");
        }

        var addedMember = await _memberRepository.Create(member);

        return _mapper.Map<Member, MemberModel>(addedMember);
    }

    public async Task<MemberModel> GetMemberByEmail(string email)
    {
        var member = await _memberRepository.ReadByEmail(email);
        if (member != null)
        {
            return _mapper.Map<Member, MemberModel>(member);
        }
        return null;
    }


    public async Task<MemberModel> UpdateMemberInformation(MemberModel model)
    {
        var member = await _memberRepository.Read(model.Id);
        member.Company = await _companyRepository.Read(model.Company.Id);
        member.FirstName = model.FirstName;
        member.LastName = model.LastName;

        if (await _memberRepository.Update(member))
        {
            return _mapper.Map<Member, MemberModel>(await _memberRepository.Read(model.Id));
        }
        return null;
    }
}

