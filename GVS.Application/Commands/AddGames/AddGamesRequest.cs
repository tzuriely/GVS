using GVS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVS.Application.Commands.AddGames
{
    public record AddGamesRequest : IRequest
    {
        public List<GameRequest> Games { get; set; }
    }

    public record GameRequest 
    {
        public int LeagueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Location { get; set; }
        public int ProviderId { get; set; }
        public string Language { get; set; }
        public DateTime UploadTime { get; set; }
    }
}
