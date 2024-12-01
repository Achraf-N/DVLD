using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;

namespace DataAccessLayer
{
    public class clsDataContry
    {
        public static DataTable GetAllDataCountry()
        {

            DataTable dt = new DataTable();
            bool isFound = false;

            string Datastring = clsDataControlConnection.ConnectionString;
            SqlConnection connection = new SqlConnection(Datastring);
            string query = "select * from Countries";
            SqlCommand command = new SqlCommand(query, connection);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);

                }
                connection.Close();

            }
            catch (Exception ex)
            {


            }
            return dt;
        }

        public static bool GetCountryInfoByName(string CountryName, ref int CountryID)
        {
            bool isFound = false;
            string Datastring = clsDataControlConnection.ConnectionString;
            SqlConnection connection = new SqlConnection(Datastring);
            string query = "select * from Countries where CountryName = @CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("CountryName", CountryName);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    CountryID = (int)reader["CountryID"];
                }
                connection.Close();

            }
            catch (Exception ex)
            {


            }

            return isFound;
        }


        public static bool GetCountryInfoByID(int CountryID, ref string CountryName)
        {
            bool isFound = false;
            string Datastring = clsDataControlConnection.ConnectionString;
            SqlConnection connection = new SqlConnection(Datastring);
            string query = "select * from Countries where CountryID = @CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("CountryID", CountryID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    CountryName = (string)reader["CountryName"];
                }
                connection.Close();

            }
            catch (Exception ex)
            {


            }

            return isFound;
        }
    }
    
}
