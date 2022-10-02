using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using redbull_team_1_teamreport_back.Domain.Repositories.Interfaces;
using TeamReport.Domain.Exceptions;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;

namespace TeamReport.Domain.Services;

public class AuthorizationServices: IAuthorizationServices
{
    public readonly IMemberRepository _memberRepository;
    public readonly IMapper _mapper;

    public AuthorizationServices(IMemberRepository memberRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<MemberModel> GetUserForLogin(string email, string password)
    {
        var member = await _memberRepository.GetMemberByEmail(email);

        if(member is null)
        {
            throw new EntityNotFoundException("Member not found");
        }
        if (!PasswordHash.ValidatePassword(password, member.Password))
        {
            throw new EntityNotFoundException("Invalid  password");
        }

        return _mapper.Map<MemberModel>(member);
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
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
