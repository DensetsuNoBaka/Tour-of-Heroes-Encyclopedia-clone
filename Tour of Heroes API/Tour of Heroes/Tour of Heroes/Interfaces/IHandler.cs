using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tour_of_Heroes.Entities;

namespace Tour_of_Heroes.Interfaces
{
    public interface IHandler<T>
    {
        Task<List<ListItem>> List(int? id);
        Task<T> Get(int id);
        Task<int> Insert(T newRow);
        Task Update(T modifiedRow);
        void Delete(int id);
    }
}
