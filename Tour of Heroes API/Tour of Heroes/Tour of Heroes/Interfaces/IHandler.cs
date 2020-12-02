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
        List<ListItem> List(int? id);
        List<T> Get(int? heroId);
        int Insert(T newRow);
        void Update(T modifiedRow);
        void Delete(int id);
    }
}
