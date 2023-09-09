using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVS.Domain.Repositories
{
    public interface IGamesRepository
    {
        Task<List<Game>> GetGamesByText(string text);
    }
}
