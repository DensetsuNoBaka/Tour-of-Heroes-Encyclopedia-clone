using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tour_of_Heroes.Entities;
using Tour_of_Heroes.Interfaces;

namespace Tour_of_Heroes.Classes
{
    public class UniverseHandler : IHandler<Universe>
    {
        private readonly string connectionString;
        private readonly SprocRunner _sprocRunner;

        public UniverseHandler(SprocRunner sprocRunner)
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
            _sprocRunner = sprocRunner;
        }

        public async Task<List<ListItem>> List(int? id)
        {
            List<ListItem> universeList = new List<ListItem>();

            _sprocRunner.sprocName = "Universe_Get";
            if (id != null) _sprocRunner.AddParameter("@Universe_ID", id ?? default(int));
            await _sprocRunner.RunSproc();

            for (int c = 0; c < _sprocRunner.dataOutput.Count; c++)
            {
                universeList.Add(new ListItem
                {
                    name = _sprocRunner.dataOutput[c]["UNIVERSE_NAME"],
                    value = Int32.Parse(_sprocRunner.dataOutput[c]["UNIVERSE_ID"])
                });
            }

            _sprocRunner.Clear();

            return universeList;
        }

        public async Task<Universe> Get(int universeId)
        {
            Universe universe;

            _sprocRunner.sprocName = "Universe_Get";
            _sprocRunner.AddParameter("@Universe_ID", universeId);
            await _sprocRunner.RunSproc();

            universe = new Universe
            {
                universeId = Int32.Parse(_sprocRunner.dataOutput[0]["UNIVERSE_ID"]),
                universeName = _sprocRunner.dataOutput[0]["UNIVERSE_NAME"],
                logoUrl = _sprocRunner.dataOutput[0]["LOGO_URL"]
            };

            _sprocRunner.Clear();

            _sprocRunner.sprocName = "Hero_Get";
            _sprocRunner.AddParameter("@Universe_ID", universeId);
            await _sprocRunner.RunSproc();

            for (int c = 0; c < _sprocRunner.dataOutput.Count; c++)
            {
                universe.heroes.Add(new ListItem
                {
                    name = _sprocRunner.dataOutput[c]["HERO_NAME"],
                    value = Int32.Parse(_sprocRunner.dataOutput[c]["HERO_ID"])
                });
            }

            _sprocRunner.Clear();

            return universe;
        }

        public async Task<int> Insert(Universe newRow)
        {
            int newId = 0;

            _sprocRunner.sprocName = "Universe_Put";
            _sprocRunner.AddParameter("@Universe_Name", newRow.universeName);
            _sprocRunner.AddParameter("@Logo_Url", newRow.logoUrl);
            _sprocRunner.AddOutputParameter("@Universe_ID", 0);
            await _sprocRunner.RunSproc();

            newId = Int32.Parse(_sprocRunner.outValue);

            _sprocRunner.Clear();

            return newId;
        }

        public async Task Update(Universe modifiedRow)
        {
            _sprocRunner.sprocName = "Universe_Put";
            _sprocRunner.AddParameter("@Universe_Name", modifiedRow.universeName);
            _sprocRunner.AddParameter("@Logo_Url", modifiedRow.logoUrl);
            _sprocRunner.AddParameter("@Universe_ID", modifiedRow.universeId);
            await _sprocRunner.RunSproc();

            _sprocRunner.Clear();
        }

        public void Delete(int id)
        {

        }
    }
}
