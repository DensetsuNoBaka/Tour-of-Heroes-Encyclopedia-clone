using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tour_of_Heroes.Entities;
using Tour_of_Heroes.Interfaces;

namespace Tour_of_Heroes.Classes
{
    public class PowerHandler : IHandler<Power>
    {
        public async Task<List<ListItem>> List(int? id)
        {
            return new List<ListItem>();
        }
        public async Task<Power> Get(int id)
        {
            return new Power();
        }
        public async Task<int> Insert(Power newRow)
        {
            return 0;
        }
        public async Task Update(Power modifiedRow)
        {
            return;
        }
        public async void Delete(int id)
        {

        }
    }
}
