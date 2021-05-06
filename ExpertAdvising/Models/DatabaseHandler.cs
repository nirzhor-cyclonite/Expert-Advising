using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace trial7.Models
{
    public class DatabaseHandler
    {
        string connectionString = @"Data Source = XPS-CYCLONITE\SQLEXPRESS; Initial Catalog = ExpertAdvisingTrial2; Integrated Security=True";
        DataSet dataset { get; set; }
        public DataSet selectFunction(string command)
        {
            dataset = new DataSet();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, sqlConnection);
                sqlDataAdapter.Fill(dataset);
                return dataset;


            }
        }

        public void update(string query)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }

        }
    }
}