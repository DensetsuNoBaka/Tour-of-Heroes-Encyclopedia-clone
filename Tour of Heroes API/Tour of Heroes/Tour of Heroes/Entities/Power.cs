using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tour_of_Heroes.Entities
{
    public class Power
    {
        public int PowerId { get; set; }
        public string PowerName { get; set; }
        public string PowerDescription { get; set; }

        public List<ListItem> heroes { get; set; }

        public Power()
        {
            heroes = new List<ListItem>();
        }
    }
}
