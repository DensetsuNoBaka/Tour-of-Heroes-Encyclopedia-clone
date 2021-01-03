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

        public UniverseHandler()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
        }

        public List<ListItem> List(int? id)
        {
            List<ListItem> universe = new List<ListItem>();

            string json = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Universe_Get", conn);
                    //cmd.Parameters.AddWithValue("@Universe_ID", universeId);


                    cmd.CommandType = CommandType.StoredProcedure;

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            universe.Add(new ListItem
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

            return universe;
        }
        public Universe Get(int universeId)
        {
            Universe universe = new Universe();

            string json = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Universe_Get", conn);
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
                            universe.universeId = universeId;
                            universe.universeName = dr.GetString(1);
                            universe.logoUrl = dr.GetString(2);
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

                    cmd = new SqlCommand("Hero_Get", conn);

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
                            universe.heroes.Add(new ListItem
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

            //return universe;
            return universe;
        }
        public int Insert(Universe newRow)
        {
            int newId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Universe_put", conn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Universe_Name", newRow.universeName);
                    cmd.Parameters.AddWithValue("@Logo_Url", newRow.logoUrl);

                    SqlParameter outputParam = new SqlParameter("@Universe_ID", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

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

                    newId = (int)outputParam.Value;

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
        public void Update(Universe modifiedRow)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Universe_put", conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Universe_ID", modifiedRow.universeId);
                    cmd.Parameters.AddWithValue("@Universe_Name", modifiedRow.universeName);
                    cmd.Parameters.AddWithValue("@Logo_Url", modifiedRow.logoUrl);

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

        }
    }
}
