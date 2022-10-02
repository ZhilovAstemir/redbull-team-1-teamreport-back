using AutoMapper;
using redbull_team_1_teamreport_back.Data.Entities;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Mappers;

public class MapperDomain: Profile
{
    public MapperDomain()
    {
        CreateMap<Member, MemberModel>().ReverseMap();
    }
}

