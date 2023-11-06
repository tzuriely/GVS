using GVS.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Security;

namespace GVS.Domain
{
    public class Game
    {
        public int GameId { get; set; }
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