using MediatR;

namespace GVS.Application.Queries.GamesByText
{
    public record GamesByTextRequest : IRequest<GamesByTextResponse>
    {
        public string Text { get; set; }
    }
}