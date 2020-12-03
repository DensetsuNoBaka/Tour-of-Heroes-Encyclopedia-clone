﻿using System;
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
        public List<Universe> Get(int? universeId)
        {
            return new List<Universe>();
        }
        public int Insert(Universe newRow)
        {
            return 0;
        }
        public void Update(Universe modifiedRow)
        {

        }
        public void Delete(int id)
        {

        }
    }
}