using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tour_of_Heroes.Entities
{
    public class Universe
    {
        public int universeId { get; set; }
        public string universeName { get; set; }
        public string logoUrl { get; set; }
        public List<ListItem> heroes { get; set; }

        public Universe()
        {
            heroes = new List<ListItem>();
        }
    }
}
