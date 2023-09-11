using GVS.Application.Queries.GamesByText;
using GVS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GVS.Games.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly GamesByTextHandler _handler;
        public GamesController(GamesByTextHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public async Task<ActionResult<GamesByTextResponse>> Get([FromQuery]GamesByTextReuest reuest)
        {
            var games = await _handler.Handle(reuest);

            if (games is null)
            {
                return Ok(Array.Empty<object>());
            }

            return Ok(games);
        }
    }
}
