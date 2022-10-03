using AutoMapper;
using TeamReport.Domain.Models;
using TeamReport.Domain.Models.Requests;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Mappers;

public class MapperAPI: Profile
{
    public MapperAPI()
    {
        CreateMap<MemberRegistrationRequest, MemberModel>();
        CreateMap<CompanyRegistrationRequest, MemberModel>();
    }
}
