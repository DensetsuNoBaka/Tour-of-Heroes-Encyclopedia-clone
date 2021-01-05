using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tour_of_Heroes.Entities;

namespace Tour_of_Heroes.Classes
{
    public class SprocRunner
    {
        private readonly string connectionString;
        public string sprocName { get; set; }
        public List<SqlParameter> parameterCollection { get; set; }
        public SqlParameter outputParam { get; set; }
        public List<Dictionary<string, string>> dataOutput { get; set; }
        public string outValue { get; set; }

        public SprocRunner()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
            dataOutput = new List<Dictionary<string, string>>();
            parameterCollection = new List<SqlParameter>();
        }

        public void AddParameter(string field, int value)
        {
            parameterCollection.Add(new SqlParameter(field, value));
        }

        public void AddParameter(string field, string value)
        {
            parameterCollection.Add(new SqlParameter(field, value));
        }

        public void AddOutputParameter(string field, string value)
        {
            outputParam = new SqlParameter(field, SqlDbType.VarChar);
            outputParam.Direction = ParameterDirection.Output;
            parameterCollection.Add(outputParam);
        }

        public void AddOutputParameter(string field, int value)
        {
            outputParam = new SqlParameter(field, SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;
            parameterCollection.Add(outputParam);
        }

        public async Task RunSproc()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(sprocName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    if(this.parameterCollection != null)
                    {
                        foreach (SqlParameter paramater in this.parameterCollection)
                        {
                            cmd.Parameters.Add(paramater);
                        }
                    }

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            dataOutput.Add(new Dictionary<string, string>());

                            for (int c = 0; c < dr.FieldCount; c++)
                            {
                                dataOutput[dataOutput.Count-1].Add(dr.GetName(c).ToUpper(), dr.GetValue(c).ToString());
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }

                    //close data reader
                    dr.Close();

                    if (this.outputParam != null) this.outValue = this.outputParam.Value.ToString();

                    //close connection
                    conn.Close();
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Clear()
        {
            //parameterCollection = new List<SqlParameter>();
            parameterCollection.Clear();
            outputParam = null;// = new SqlParameter();
            dataOutput.Clear();// = new List<Dictionary<string, string>>();
            sprocName = String.Empty;
            outValue = String.Empty;
        }
    }
}
