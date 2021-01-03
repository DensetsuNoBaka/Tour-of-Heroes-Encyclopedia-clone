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
        public HeroHandler()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
        }

        public List<ListItem> List()
        {
            return new List<ListItem>();
        }

        public List<ListItem> List(int? universeId)
        {
            List<ListItem> heroes = new List<ListItem>();

            string json = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Hero_Get", conn);
                    cmd.Parameters.AddWithValue("@Universe_ID", universeId);


                    cmd.CommandType = CommandType.StoredProcedure;

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            heroes.Add(new ListItem
                            {
                                name = dr.GetString(1),
                                value = dr.GetInt32(0)
                            });
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

            return heroes;
        }

        public Hero Get(int heroId)
        {
            Hero hero = new Hero();
            int universeId = 0;

            string json = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Hero_Get", conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Hero_ID", heroId);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            hero.heroId = dr.GetInt32(0);
                            hero.heroName = dr.GetString(1);
                            hero.powerLevel = dr.GetString(2);
                            hero.pictureUrl = dr.GetString(3);

                            universeId = dr.GetInt32(4);
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

                    cmd = new SqlCommand("Universe_Get", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Universe_ID", universeId);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            hero.universe = new Universe{
                                universeId = dr.GetInt32(0),
                                universeName = dr.GetString(1),
                                logoUrl = dr.GetString(2)
                            };
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

                    cmd = new SqlCommand("Hero_Bios_Get", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Hero_ID", heroId);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            hero.heroBio.Add(new HeroBio
                            {
                                heroBioId = dr.GetInt32(0),
                                order = dr.GetInt32(1),
                                header = dr.GetString(2),
                                heroBio = dr.GetString(3)
                            });
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

            return hero;
        }

        public int Insert(Hero newRow)
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
        }

        public void Update(Hero modifiedRow)
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
        }
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
