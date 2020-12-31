using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tour_of_Heroes.Entities
{
    public class Hero
    {
        public int heroId { get; set; }
        public string heroName { get; set; }
        public string powerLevel { get; set; }
        public string pictureUrl { get; set; }
        public Universe universe { get; set; }
        public List<HeroBio> heroBio { get; set; }
    }
}
