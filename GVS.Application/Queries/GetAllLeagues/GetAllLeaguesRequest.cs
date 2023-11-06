using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVS.Application.Queries.GetAllLeagues
{
    public record GetAllLeaguesRequest : IRequest<GetAllLeaguesResponse>
    {
    }
}
