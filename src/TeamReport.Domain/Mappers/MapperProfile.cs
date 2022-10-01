using AutoMapper;
using redbull_team_1_teamreport_back.Data.Entities;
using TeamReport.Domain.Models;
using TeamReport.Domain.Models.Requests;

namespace TeamReport.Domain.Mappers;

public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<Member, MemberModel>().ReverseMap();
        CreateMap<MemberRegistrationRequest, MemberModel>();
    }
}

