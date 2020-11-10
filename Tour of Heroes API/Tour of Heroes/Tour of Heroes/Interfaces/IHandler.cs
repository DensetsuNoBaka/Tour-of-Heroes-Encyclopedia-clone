using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tour_of_Heroes.Interfaces
{
    public interface IHandler<T>
    {
        List<T> Get(int? heroId);
        int Insert(T newRow);
        void Update(T modifiedRow);
        void Delete(int id);
    }
}
