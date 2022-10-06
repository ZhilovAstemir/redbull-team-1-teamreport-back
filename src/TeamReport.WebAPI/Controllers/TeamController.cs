using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Helpers;

namespace TeamReport.WebAPI.Controllers;

[Authorize]
[ApiController]
[Produces("application/json")]
[Route("api/team")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly IMapper _mapper;

    public TeamController(ITeamService teamService, IMapper mapper)
    {
        _teamService = teamService;
        _mapper = mapper;
    }


}
