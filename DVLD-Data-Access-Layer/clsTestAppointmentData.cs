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
    public static class clsTestAppointmentData
    {
        public static int AddNewTestApointment(byte testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate,
                                   decimal paidFees, int createdByUserID, bool isLocked, int retakeTestApplicationID)
        {
            int testAppointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"insert into TestAppointments(TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID)
                             values (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, 0, @RetakeTestApplicationID);
                             select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", testTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
            command.Parameters.AddWithValue("@PaidFees", paidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

            if (retakeTestApplicationID != -1)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", retakeTestApplicationID);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", System.DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null & int.TryParse(result.ToString(), out int insertedID))
                {
                    testAppointmentID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return testAppointmentID;
        }

        public static bool UpdateTestAppointment(int testAppointmentID, DateTime newAppointmentDate)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Update  TestAppointments  
                            set AppointmentDate = @NewAppointmentDate
                                where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NewAppointmentDate", newAppointmentDate);
            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);

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

        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            //string query = "select TestAppointmentID, AppointmentDate, PaidFees, IsLocked from TestAppointments";
            string query = "select * from TestAppointments_View order by AppointmentDate Desc";

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

        public static DataTable GetApplicationTestAppointmentsPerTestType(int localApplicationID, byte testTypeID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select TestAppointmentID, AppointmentDate, PaidFees, IsLocked from TestAppointments 
                             where LocalDrivingLicenseApplicationID = @LocalApplicationID and TestTypeID = @TestTypeID
                             order by TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalApplicationID", localApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", testTypeID);


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

        public static bool GetTestAppointmentByID(int testAppointmentID, ref byte testTypeID, ref int localDrivingLicenseApplicationID, 
                                                  ref DateTime appointmentDate, ref decimal paidFees, ref int createdByUserID, ref bool isLocked, 
                                                  ref int retakeTestApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from TestAppointments where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    testTypeID = Convert.ToByte(reader["TestTypeID"]);
                    localDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    appointmentDate = (DateTime)reader["AppointmentDate"];
                    paidFees = Convert.ToDecimal(reader["PaidFees"]);
                    createdByUserID = (int)reader["CreatedByUserID"];
                    isLocked = (bool)reader["IsLocked"];
                    retakeTestApplicationID = reader["RetakeTestApplicationID"] != DBNull.Value ? (int)reader["RetakeTestApplicationID"] : -1;
                }
                else
                {
                    return false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool GetLastTestAppointment(int localDrivingLicenseApplicationID, byte testTypeID, ref int testAppointmentID, 
                                                  ref DateTime appointmentDate, ref decimal paidFees, ref int createdByUserID, ref bool isLocked, 
                                                  ref int retakeTestApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select top 1 * from TestAppointments 
                             where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                             and TestTypeID = @TestTypeID
                             order by TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", testTypeID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    testAppointmentID = (int)reader["TestAppointmentID"];
                    appointmentDate = (DateTime)reader["AppointmentDate"];
                    paidFees = Convert.ToDecimal(reader["PaidFees"]);
                    createdByUserID = (int)reader["CreatedByUserID"];
                    isLocked = (bool)reader["IsLocked"];
                    retakeTestApplicationID = reader["RetakeTestApplicationID"] != DBNull.Value ? (int)reader["RetakeTestApplicationID"] : -1;
                }
                else
                {
                    return false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static int GetTestID(int testAppointmentID)
        {
            int testID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select TestID from Tests
                             where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    testID = ID;
                }

            }
            catch (Exception ex)
            {
            }

            finally
            {
                connection.Close();
            }

            return testID;
        }


        public static string GetLicenseClassNameOfTestAppointmentID(int testAppointmentID)
        {
            string licenseClassName = "";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select ClassName from TestAppointments_View where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    licenseClassName = result.ToString();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return licenseClassName;
        }

        public static bool SetAppointmentLocked(int testAppointmentID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Update TestAppointments  
                             set IsLocked = @IsLocked
                             where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@IsLocked", 1);
            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);

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
