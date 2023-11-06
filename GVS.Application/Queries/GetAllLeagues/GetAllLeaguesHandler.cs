using GVS.Domain.Entities;
using GVS.Domain.Repositories;
using MediatR;

namespace GVS.Application.Queries.GetAllLeagues
{
    public class GetAllLeaguesHandler
        : IRequestHandler<GetAllLeaguesRequest, GetAllLeaguesResponse>
    {
        private readonly ILeaguesRepository _repo;

        public GetAllLeaguesHandler(ILeaguesRepository repo)
        {
            _repo = repo;
        }

        public async Task<GetAllLeaguesResponse> Handle(GetAllLeaguesRequest request, CancellationToken cancellationToken)
        {
            var response = await _repo.GetAllLeagues();
            var leagues = MapLeagues(response);

            return leagues;
        }

        private GetAllLeaguesResponse MapLeagues(List<League> response) => new GetAllLeaguesResponse() { Leagues = response };
    }
}
