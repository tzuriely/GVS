using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMining.Robots.Sport5
{
    public class Sport5GameModel
    {
        public int LeagueId { get; set; }
        public int ProviderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Location { get; set; }
        public string Language { get; set; }
        public DateTime UploadTime { get; set; }
    }
}
