using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class clsDetainedLicenseData
    {
        public static bool GetDetainedLicenseInfoByDetainID(int detainID, ref int licenseID, ref DateTime detainDate, ref decimal fineFees, ref int createdByUserID, ref bool isReleased, ref DateTime releaseDate,
                                                            ref int releasedByUserID, ref int releaseApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from DetainedLicenses where DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", detainID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    licenseID = (int)reader["LicenseID"];
                    detainDate = (DateTime)reader["DetainDate"];
                    fineFees = Convert.ToDecimal(reader["FineFees"]);
                    createdByUserID = (int)reader["CreatedByUserID"];
                    isReleased = (bool)reader["IsReleased"];
                    releasedByUserID = (int)reader["ReleaseApplicationID"];

                    if (string.IsNullOrEmpty(reader["ReleaseDate"].ToString()))
                        releaseDate = DateTime.MinValue;
                    else
                        releaseDate = (DateTime)reader["ReleaseDate"];

                    if (string.IsNullOrEmpty(reader["ReleasedByUserID"].ToString()))
                        releasedByUserID = -1;
                    else
                        releasedByUserID = (int)reader["ReleasedByUserID"];

                    if (string.IsNullOrEmpty(reader["ReleaseApplicationID"].ToString()))
                        releaseApplicationID = -1;
                    else
                        releaseApplicationID = (int)reader["ReleaseApplicationID"];
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

        public static bool GetDetainedLicenseInfoByLicenseID(int licenseID, ref int detainID, ref DateTime detainDate, ref decimal fineFees, ref int createdByUserID, ref bool isReleased, ref DateTime releaseDate,
                                                           ref int releasedByUserID, ref int releaseApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from DetainedLicenses where LicenseID = @LicenseID and IsReleased = 0";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", licenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    detainID = (int)reader["DetainID"];
                    detainDate = (DateTime)reader["DetainDate"];
                    fineFees = Convert.ToDecimal(reader["FineFees"]);
                    createdByUserID = (int)reader["CreatedByUserID"];
                    isReleased = (bool)reader["IsReleased"];

                    if (string.IsNullOrEmpty(reader["ReleaseDate"].ToString()))
                        releaseDate = DateTime.MinValue;
                    else
                        releaseDate = (DateTime)reader["ReleaseDate"];

                    if (string.IsNullOrEmpty(reader["ReleasedByUserID"].ToString()))
                        releasedByUserID = -1;
                    else
                        releasedByUserID = (int)reader["ReleasedByUserID"];

                    if (string.IsNullOrEmpty(reader["ReleaseApplicationID"].ToString()))
                        releaseApplicationID = -1;
                    else
                        releaseApplicationID = (int)reader["ReleaseApplicationID"];
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

        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from DetainedLicenses_View";

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

        public static int AddNewDetainedLicense(int licenseID, DateTime detainDate, decimal fineFees, int createdByUserID)
        {
            int detainID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"insert into DetainedLicenses(LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased)
                             values (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, 0);

                             select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", licenseID);
            command.Parameters.AddWithValue("@DetainDate", detainDate);
            command.Parameters.AddWithValue("@FineFees", fineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null & int.TryParse(result.ToString(), out int insertedID))
                {
                    detainID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return detainID;
        }

        public static bool UpdateDetainedLicense(int detainID, int licenseID, DateTime detainDate, decimal fineFees, int createdByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"update dbo.DetainedLicenses
                              set LicenseID = @LicenseID, 
                              DetainDate = @DetainDate, 
                              FineFees = @FineFees,
                              CreatedByUserID = @CreatedByUserID,   
                              where DetainID = @DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", detainID);
            command.Parameters.AddWithValue("@LicenseID", licenseID);
            command.Parameters.AddWithValue("@DetainDate", detainDate);
            command.Parameters.AddWithValue("@FineFees", fineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool ReleaseDetainedLicense(int detainID, int releasedByUserID, int releaseApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Update DetainedLicenses  
                            set IsReleased = 1,
                                ReleaseDate = GETDATE(), 
                                ReleasedByUserID = @ReleasedByUserID,
                                ReleaseApplicationID = @ReleaseApplicationID
                            where DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ReleasedByUserID", releasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", releaseApplicationID);
            command.Parameters.AddWithValue("@DetainID", detainID);

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

        public static bool IsLicenseDetained(int licenseID)
        {
            bool isDetained = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select result = 1 from DetainedLicenses 
                             where LicenseID = @LicenseID 
                             and IsReleased = 0";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", licenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isDetained = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return isDetained;
        }

    }
}
