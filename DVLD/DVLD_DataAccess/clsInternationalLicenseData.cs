using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class clsInternationalLicenseData
    {
        public static bool GetInternationalLicenseInfoByID(int internationalLicenseID, ref int applicationID, ref int driverID, ref int issuedUsingLocalLicenseID, ref DateTime issueDate, ref DateTime expirationDate,
                                                           ref bool isActive, ref int createdByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from InternationalLicenses where InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    applicationID = (int)reader["ApplicationID"];
                    driverID = (int)reader["DriverID"];
                    issuedUsingLocalLicenseID =(int)reader["IssuedUsingLocalLicenseID"];
                    issueDate = (DateTime)reader["IssueDate"];
                    expirationDate = (DateTime)reader["ExpirationDate"];
                    isActive = (bool)reader["IsActive"];
                    createdByUserID = (int)reader["CreatedByUserID"];
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

        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive 
                             from InternationalLicenses
                             order by ExpirationDate desc";

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

        public static DataTable GetDriverInternationalLicenses(int driverID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select InternationalLicenseID, ApplicationID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive  from InternationalLicenses 
                             where DriverID = @DriverID
                             order by ExpirationDate desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", driverID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dataTable.Load(reader);
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

            return dataTable;
        }

        public static int AddNewInternationalLicense(int applicationID, int driverID, int issuedUsingLocalLicenseID, DateTime issueDate, DateTime expirationDate, bool isActive, 
                                                     int createdByUserID)
        {
            int internationalicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
                             update InternationalLicenses
                             set IsActive = 0
                             where DriverID = @DriverID;

                             insert into InternationalLicenses(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID)
                             values (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID);

                             update Applications 
                             set ApplicationStatus = 3,
                                 LastStatusDate = GETDATE()
                             where ApplicationID = @ApplicationID;

                             select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", applicationID);
            command.Parameters.AddWithValue("@DriverID", driverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", issuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", issueDate);
            command.Parameters.AddWithValue("@ExpirationDate", expirationDate);
            command.Parameters.AddWithValue("@IsActive", isActive);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null & int.TryParse(result.ToString(), out int insertedID))
                {
                    internationalicenseID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return internationalicenseID;
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int driverID)
        {
            int activeInternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select top 1 InternationalLicenseID from InternationalLicenses
                             where DriverID = @DriverID 
                             and IsActive = 1";

            //string query = @"select top 1 InternationalLicenseID from InternationalLicenses
            //                 where DriverID = @DriverID 
            //                 and GetDate() between IssueDate and ExpirationDate";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", driverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null & int.TryParse(result.ToString(), out int licenseID))
                {
                    activeInternationalLicenseID = licenseID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return activeInternationalLicenseID;
        }

    }

}
