using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
using TeamReport.Domain.Auth;
using TeamReport.Domain.Exceptions;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;

namespace TeamReport.Domain.Services;

public class AuthorizationService: IAuthorizationService
{
    public readonly IMemberRepository _memberRepository;
    public readonly IMapper _mapper;

    public AuthorizationService(IMemberRepository memberRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<MemberModel> Login(string email, string password)
    {
        var member = await _memberRepository.ReadByEmail(email);
        if (member == null)
        {
            throw new InvalidCreditalsException();
        }
        if (!PasswordHash.ValidatePassword(password,member.Password))
        {
            throw new InvalidCreditalsException();
        }
        return _mapper.Map<Member,MemberModel>(member);
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
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(7)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256),
            claims:new List<Claim>(){new Claim("user",member.Id.ToString())}
            );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public async Task<int> Register(MemberModel memberModel)
    {
        memberModel.Password = PasswordHash.HashPassword(memberModel.Password);

        var member = _mapper.Map<MemberModel, Member>(memberModel);

        var addedMember= await _memberRepository.Create(member);

        return addedMember.Id;
    }
}
