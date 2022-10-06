using AutoMapper;
using TeamReport.Domain.Models;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Mappers;

public class MapperAPI : Profile
{
    public MapperAPI()
    {
        CreateMap<MemberRegistrationRequest, MemberModel>().ReverseMap();
        CreateMap<CompanyRegistrationRequest, MemberModel>().ReverseMap();
    }
}
