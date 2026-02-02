using DVLD_Business;
using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }   
        public bool IsActive { get; set; }
        

        public clsUser()
        {
            UserID = -1;
            UserName = "";
            Password = "";
            IsActive = false;

            Mode = enMode.AddNew;
        }

        private clsUser(int userID,  int personID, string userName, string password, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.PersonInfo = clsPerson.Find(personID);
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;

            Mode = enMode.Update;
        }

        public static DataTable GetAllUsers()
        {
            return clsUsersData.GetAllUsers();
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUsersData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUsersData.UpdateUser(this.UserID, this.UserName, this.Password, this.IsActive);
        }

        static public bool DeleteUser(int userID)
        {
            return clsUsersData.DeleteUser(userID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public static clsUser FindByUserID(int userID)
        {
            int personID = -1;
            string userName = "", password = "";
            bool isActive = false;

            bool isFound = clsUsersData.GetUsersInfoByUserID(userID, ref personID, ref userName, ref password, ref isActive);

            if (isFound)
                return new clsUser(userID, personID, userName, password, isActive);
            else
                return null;
        }

        public static clsUser FindByPersonID(int personID)
        {
            int userID = -1;
            string userName = "", password = "";
            bool isActive = false;

            bool isFound = clsUsersData.GetUsersInfoByPersonID(personID, ref userID, ref userName, ref password, ref isActive);

            if (isFound)
                return new clsUser(userID, personID, userName, password, isActive);
            else
                return null;
        }

        public static clsUser FindByUsernameAndPassword(string userName, string password)
        {
            int userID = -1, personID = -1;
            bool isActive = false;

            bool isFound = clsUsersData.GetUsersInfoByUsernameAndPassword(userName, password, ref userID, ref personID, ref isActive);

            if (isFound)
                return new clsUser(userID, personID, userName, password, isActive);
            else
                return null;
        }

        public static bool IsUserExist(int userID)
        {
            return clsUsersData.IsUserExist(userID);
        }

        public static bool IsUserExist(string userName)
        {
            return clsUsersData.IsUserExist(userName);
        }

        public static bool IsUserExistForPersonID(int personID)
        {
            return clsUsersData.IsUserExistForPersonID(personID);
        }


    }
}
