using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer
{
    public static class clsTestData
    {
        public static bool GetTestInfoByID(int testID, ref int testAppointmentID, ref bool testResult, ref string notes, ref int createdByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from Tests where TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", testID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    testAppointmentID = (int)reader["TestAppointmentID"];
                    testResult = (bool)reader["TestResult"];
                    notes = (string)reader["Notes"];
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
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool GetLastTestByPersonAndLicenseClassAndTestType(int personID, int licenseClassID, byte testTypeID, ref int testID, 
                      ref int testAppointmentID, ref bool testResult, ref string notes, ref int createdByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select top 1 Tests.TestID, Tests.TestAppointmentID, Tests.TestResult, Tests.Notes, Tests.CreatedByUserID 
                             from Tests
                             inner join TestAppointments
                             on TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                             inner join LocalDrivingLicenseApplications
                             on LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                             inner join Applications
                             on Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                             where Applications.ApplicantPersonID = @PersonID
                             and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                             and TestAppointments.TestTypeID = @TestTypeID
                             order by Tests.TestAppointmentID desc;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@LicenseClassID", licenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", testTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    testID = (int)reader["TestID"];
                    testAppointmentID = (int)reader["TestAppointmentID"];
                    testResult = (bool)reader["TestResult"];
                    notes = (string)reader["Notes"];
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
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static DataTable GetAllTests()
        {
            DataTable allTests = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from Tests";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    allTests.Load(reader);
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

            return allTests;
        }

        public static int AddNewTest(int testAppointmentID, bool testResult, string notes, int createdByUserID)
        {
            int testID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"insert into Tests(TestAppointmentID, TestResult, Notes, CreatedByUserID)
                             values (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);
                             
                             update TestAppointments
                             set IsLocked = 1 
                             where TestAppointmentID = @TestAppointmentID;

                             select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);
            command.Parameters.AddWithValue("@TestResult", testResult);
            if (notes != "")
                command.Parameters.AddWithValue("@Notes", notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null & int.TryParse(result.ToString(), out int insertedID))
                {
                    testID = insertedID;
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

        public static bool UpdateTest(int testID, int testAppointmentID, bool testResult,
            string notes, int createdByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Update Tests  
                            set TestAppointmentID = @TestAppointmentID,
                                TestResult=@TestResult,
                                Notes = @Notes,
                                CreatedByUserID=@CreatedByUserID
                                where TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", testID);
            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);
            command.Parameters.AddWithValue("@TestResult", testResult);
            command.Parameters.AddWithValue("@Notes", notes);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

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

        public static byte GetPassedTestCount(int localDrivingLicenseApplicationID)
        {
            byte passedTestCount = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select PassedTestCount from LocalDrivingLicenseApplications_View
                             where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            // a query without using a view
            //string query = @"select PassedTestsCount = Count(*)
            //                from Tests
            //                inner join TestAppointments 
            //                on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
            //                where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and Tests.TestResult = 1;";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null & byte.TryParse(result.ToString(), out byte number))
                {
                    passedTestCount = number;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return passedTestCount;
        }
    }
}
