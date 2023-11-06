using GVS.Domain.Entities;

namespace GVS.Application.Queries.GamesByText
{
    public record GamesByTextResponse
    {
        public List<GameResponse> Games { get; set; }
    }

    public record GameResponse
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