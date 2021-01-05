using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tour_of_Heroes.Entities;
using Tour_of_Heroes.Interfaces;

namespace Tour_of_Heroes.Classes
{
    public class HeroHandler : IHandler<Hero>
    {
        private readonly string connectionString;
        public SprocRunner _sprocRunner;
        public HeroHandler(SprocRunner sprocRunner)
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
            _sprocRunner = sprocRunner;
        }

        public List<ListItem> List()
        {
            return new List<ListItem>();
        }

        public async Task<List<ListItem>> List(int? universeId)
        {
            List<ListItem> heroes = new List<ListItem>();

            _sprocRunner.sprocName = "Hero_Get";
            if(universeId != null) _sprocRunner.AddParameter("@Universe_ID", universeId ?? default(int));
            await _sprocRunner.RunSproc();

            for(int c = 0; c < _sprocRunner.dataOutput.Count; c++)
            {
                heroes.Add(new ListItem
                {
                    name = _sprocRunner.dataOutput[c]["HERO_NAME"],
                    value = Int32.Parse(_sprocRunner.dataOutput[c]["HERO_ID"])
                });
            }

            _sprocRunner.Clear();

            return heroes;
        }

        public async Task<Hero> Get(int heroId)
        {
            Hero hero = new Hero();
            int universeId = 0;

            //This first call will get basic Hero table data
            _sprocRunner.sprocName = "Hero_Get";
            _sprocRunner.AddParameter("@Hero_ID", heroId);
            await _sprocRunner.RunSproc();

            hero.heroName = _sprocRunner.dataOutput[0]["HERO_NAME"];
            hero.heroId = Int32.Parse(_sprocRunner.dataOutput[0]["HERO_ID"]);
            hero.powerLevel = _sprocRunner.dataOutput[0]["POWER_LEVEL"];
            hero.pictureUrl = _sprocRunner.dataOutput[0]["PICTURE_URL"];
            universeId = Int32.Parse(_sprocRunner.dataOutput[0]["UNIVERSE_ID"]);

            _sprocRunner.Clear();

            //This second call will get related Universe table data
            _sprocRunner.sprocName = "Universe_Get";
            _sprocRunner.AddParameter("@Universe_ID", universeId);
            await _sprocRunner.RunSproc();

            hero.universe = new Universe
            {
                universeId = Int32.Parse(_sprocRunner.dataOutput[0]["UNIVERSE_ID"]),
                universeName = _sprocRunner.dataOutput[0]["UNIVERSE_NAME"],
                logoUrl = _sprocRunner.dataOutput[0]["LOGO_URL"]
            };

            _sprocRunner.Clear();

            //This third call will get related Hero Bio table data
            _sprocRunner.sprocName = "Hero_Bios_Get";
            _sprocRunner.AddParameter("@Hero_ID", heroId);
            await _sprocRunner.RunSproc();

            for (int c = 0; c < _sprocRunner.dataOutput.Count; c++)
            {
                hero.heroBio.Add(new HeroBio
                {
                    heroBioId = Int32.Parse(_sprocRunner.dataOutput[c]["HERO_BIO_ID"]),
                    order = Int32.Parse(_sprocRunner.dataOutput[c]["ORDER"]),
                    header = _sprocRunner.dataOutput[c]["HEADER"],
                    heroBio = _sprocRunner.dataOutput[c]["HERO_BIO"]
                });
            }

            _sprocRunner.Clear();

            return hero;
        }

        public async Task<int> Insert(Hero newRow)
        {
            return 0;
        }

        /*public int Insert(Hero newRow)
        {
            int newId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Hero_put", conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Hero_Name", newRow.heroName);
                    cmd.Parameters.AddWithValue("@Power_Level", newRow.powerLevel);
                    cmd.Parameters.AddWithValue("@Picture_Url", newRow.pictureUrl);
                    cmd.Parameters.AddWithValue("@Universe_ID", newRow.universe.universeId);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            newId = dr.GetInt32(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
                throw ex;
            }

            return newId;
        }*/

        public async Task Update(Hero modifiedRow)
        {

        }

        /*public void Update(Hero modifiedRow)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Hero_put", conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Hero_ID", modifiedRow.heroId);
                    cmd.Parameters.AddWithValue("@Hero_Name", modifiedRow.heroName);
                    cmd.Parameters.AddWithValue("@Power_Level", modifiedRow.powerLevel);
                    cmd.Parameters.AddWithValue("@Picture_Url", modifiedRow.pictureUrl);
                    //cmd.Parameters.AddWithValue("@Universe_ID", modifiedRow.universeId);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
                throw ex;
            }
        }*/

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Hero_put", conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Hero_ID", id);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
                throw ex;
            }
        }
    }
}
