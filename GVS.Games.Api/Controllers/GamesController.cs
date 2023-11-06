using GVS.Application.Commands.AddGames;
using GVS.Application.Queries.GamesByText;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GVS.Games.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly ISender _sender;

        public GamesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<GamesByTextResponse>> Get([FromQuery]GamesByTextRequest reuest)
        {
            var games = await _sender.Send(reuest);

            if (games is null)
            {
                return Ok(Array.Empty<object>());
            }

            return Ok(games);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddGamesRequest request)
        {
            await _sender.Send(request);
            return Ok();
        }
    }
}
