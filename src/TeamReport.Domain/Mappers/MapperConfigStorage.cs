using AutoMapper;
using redbull_team_1_teamreport_back.Domain.Entities;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Mappers;
public class MapperConfigStorage:Profile
{
    public MapperConfigStorage()
    {
        CreateMap<Member, MemberModel>().ReverseMap();
    }
}
