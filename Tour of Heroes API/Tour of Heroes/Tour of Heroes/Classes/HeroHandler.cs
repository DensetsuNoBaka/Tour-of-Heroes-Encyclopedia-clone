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

        public List<Hero> Get(int? heroId)
        {
            List<Hero> heroes = new List<Hero>();

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
                            heroes.Add(new Hero
                            {
                                heroId = dr.GetInt32(0),
                                heroName = dr.GetString(1),
                                powerLevel = dr.GetString(2),
                                pictureUrl = dr.GetString(3)
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
                    cmd.Parameters.AddWithValue("@Universe_ID", newRow.universeId);

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
                    cmd.Parameters.AddWithValue("@Universe_ID", modifiedRow.universeId);

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
