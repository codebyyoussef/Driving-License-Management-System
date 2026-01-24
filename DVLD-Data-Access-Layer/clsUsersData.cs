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
    public class clsUsersData
    {
        public static DataTable GetAllUsers()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"select Users.UserID, Users.PersonID, 
                                    FullName = People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName,
                                    Users.UserName, Users.Password, Users.IsActive
                             from Users
                             inner join People on Users.PersonID = People.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

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

        public static bool GetUsersInfoByUserID(int userID, ref int personID, ref string userName, ref string password, ref bool isActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from Users where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", userID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    personID = (int)reader["PersonID"];
                    userName = (string)reader["UserName"];
                    password = (string)reader["Password"];
                    isActive = (bool)reader["IsActive"];
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

        public static bool GetUsersInfoByPersonID(int personID, ref int userID, ref string userName, ref string password, ref bool isActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from Users where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    userID = (int)reader["UserID"];
                    userName = (string)reader["UserName"];
                    password = (string)reader["Password"];
                    isActive = (bool)reader["IsActive"];
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

        public static bool GetUsersInfoByUsernameAndPassword(string userName, string password, ref int userID, ref int personID, ref bool isActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from Users where UserName = @UserName and Password = @Password";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    userID = (int)reader["UserID"];
                    personID = (int)reader["PersonID"];
                    isActive = (bool)reader["IsActive"];
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

        public static int AddNewUser(int personID, string userName, string password, bool isActive)
        {
            int userID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"insert into Users(PersonID, UserName, Password, IsActive)
                             values (@PersonID, @UserName, @Password, @IsActive);
                             select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@IsActive", isActive);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null & int.TryParse(result.ToString(), out int insertedID))
                {
                    userID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return userID;
        }

        public static bool UpdateUser(int userID, string userName, string password, bool isActive)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Update Users  
                             set UserName = @UserName,
                                 Password = @Password,
                                 IsActive = @IsActive
                                 where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", userID);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@IsActive", isActive);

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

        public static bool DeleteUser(int userID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"delete from Users
                             where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", userID);

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

            return rowsAffected > 0;
        }

        public static bool IsUserExist(int userID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", userID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

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

        public static bool IsUserExist(string userName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", userName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

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

        public static bool IsUserExistForPersonID(int personID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

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

        public static bool ChangePassword(int userID, string newPassword)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Update Users  
                             set Password = @NewPassword
                                 where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", userID);
            command.Parameters.AddWithValue("@NewPassword", newPassword);

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
