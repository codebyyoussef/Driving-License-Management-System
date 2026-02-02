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
    static public class clsLicenseClassData
    {
        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable("LicenseClasses");

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from LicenseClasses";

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

        public static bool GetLicenseClassInfoByID(byte licenseClassID, ref string className, ref string classDescription, ref byte minimumAllowedAge,
                                                   ref byte defaultValidityLength, ref decimal classFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from LicenseClasses where LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", licenseClassID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    className = reader["ClassName"].ToString();
                    classDescription = reader["ClassDescription"].ToString();
                    minimumAllowedAge = Convert.ToByte(reader["MinimumAllowedAge"]);
                    defaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    classFees = Convert.ToDecimal(reader["ClassFees"]);
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

        public static bool GetLicenseClassInfoByClassName(string className, ref byte licenseClassID, ref string classDescription,
                                                          ref byte minimumAllowedAge, ref byte defaultValidityLength, ref decimal classFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from LicenseClasses where ClassName = @ClassName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", className);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    licenseClassID = Convert.ToByte(reader["LicenseClassID"]);
                    classDescription = reader["ClassDescription"].ToString();
                    minimumAllowedAge = Convert.ToByte(reader["MinimumAllowedAge"]);
                    defaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    classFees = Convert.ToByte(reader["ClassFees"]);
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

        public static byte GetDefaultValididyLengthForLicenseClassID(byte licenseClassID)
        {
            byte defaultValidityLength = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select DefaultValidityLength from LicenseClasses where LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", licenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte number))
                {
                    defaultValidityLength = number;
                }

            }
            catch (Exception ex)
            {
            }

            finally
            {
                connection.Close();
            }

            return defaultValidityLength;
        }
    }
}
