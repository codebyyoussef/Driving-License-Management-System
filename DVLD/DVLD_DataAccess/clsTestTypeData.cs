using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public static class clsTestTypeData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from TestTypes";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool GetTestTypeInfoByID(int ID, ref string title, ref string description, ref decimal fees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from TestTypes where TestTypeID = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    title = reader["TestTypeTitle"].ToString();
                    description = reader["TestTypeDescription"].ToString();
                    fees = (decimal)reader["TestTypeFees"];
                }
                else
                {
                    return false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // You put code of logging errors
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool UpdateTestTypeInfo(int ID, string title, string description, decimal fees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Update TestTypes  
                             set TestTypeTitle = @Title,
                                 TestTypeDescription = @Description,
                                 TestTypeFees = @Fees
                             where TestTypeID = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@Description", description);
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@Fees", fees);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
    }
}
