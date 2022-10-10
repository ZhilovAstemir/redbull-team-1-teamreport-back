using AutoMapper;
using TeamReport.Domain.Models;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Mappers;

public class MapperAPI : Profile
{
    public MapperAPI()
    {
        CreateMap<InviteMemberModelRequest, MemberModel>().ReverseMap();
        CreateMap<MemberRegistrationRequest, MemberModel>().ReverseMap();
        CreateMap<CompanyRegistrationRequest, MemberModel>().ReverseMap();
        CreateMap<ReportRequest, ReportModel>();
        CreateMap<EditMemberInformationRequest, MemberModel>().ReverseMap();
    }
}
