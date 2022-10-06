using AutoMapper;
using TeamReport.Data.Entities;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Mappers;

public class MapperDomain : Profile
{
    public MapperDomain()
    {
        CreateMap<Member, MemberModel>().ReverseMap();
        CreateMap<Company, CompanyModel>().ReverseMap();
    }
}

