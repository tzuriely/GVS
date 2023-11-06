using GVS.Application.Queries.GetAllLeagues;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GVS.Games.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaguesController : ControllerBase
    {
        private readonly ISender _sender;

        public LeaguesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<GetAllLeaguesResponse>> Get()
        {
            var request = new GetAllLeaguesRequest();
            var leagues = _sender.Send(request);

            return Ok(leagues);
        }
    }
}
