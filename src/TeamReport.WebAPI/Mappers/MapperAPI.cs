using AutoMapper;
using redbull_team_1_teamreport_back.Data.Entities;
using TeamReport.Domain.Models;
using TeamReport.Domain.Models.Requests;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.MapperStorage;

public class MapperAPI: Profile
{
   public MapperAPI()
    {
        CreateMap<MemberRegistrationRequest, MemberModel>();
        CreateMap<InviteMemberModelRequest, InviteMemberRequest>();
    }
}
