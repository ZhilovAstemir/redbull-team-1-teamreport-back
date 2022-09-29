using AutoMapper;
using TeamReport.Domain.Models;
using TeamReport.WebAPI.Models.Requests;

namespace TeamReport.WebAPI.MapperStorage;

public class MapperAPI: Profile
{
   public MapperAPI()
    {
        CreateMap<MemberRegistrationRequest, MemberModel>();
    }
}
