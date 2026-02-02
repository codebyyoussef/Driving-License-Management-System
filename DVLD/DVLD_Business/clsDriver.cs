using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsDriver
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        private int _driverID;
        public int DriverID { get { return _driverID; } }
        public int PersonID { set; get; }
        public clsPerson PersonInfo;
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate { get; }


        public clsDriver()
        {
            _driverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;

            Mode = enMode.AddNew;
        }

        private clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            _driverID = driverID;
            PersonID = personID;
            PersonInfo = clsPerson.Find(PersonID);
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;

            Mode = enMode.Update;
        }

        private bool _AddNewDriver()
        {
            _driverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID);
            return (_driverID != -1);
        }

        private bool _UpdateDriver()
        {
            return clsDriverData.UpdateDriver(this.DriverID, this.PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDriver();
            }
            return false;
        }

        public static clsDriver FindDriverByDriverID(int driverID)
        {
            int personID = -1;
            int createdByUserID = -1;
            DateTime createdDate = DateTime.Now;


            bool isFound = clsDriverData.GetDriverInfoByDriverID(driverID, ref personID, ref createdByUserID, ref createdDate);

            if (isFound)
                return new clsDriver(driverID, personID, createdByUserID, createdDate);
            else
                return null;
        }

        public static clsDriver FindDriverByPersonID(int personID)
        {
            int driverID = -1;
            int createdByUserID = -1;
            DateTime createdDate = DateTime.Now;


            bool isFound = clsDriverData.GetDriverInfoByPersonID(personID, ref driverID, ref createdByUserID, ref createdDate);

            if (isFound)
                return new clsDriver(driverID, personID, createdByUserID, createdDate);
            else
                return null;
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }

        public static DataTable GetLocalLicenses(int driverID)
        {
            return clsLicense.GetDriverLocalLicenses(driverID);
        }

        public static DataTable GetInternationalLicenses(int driverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(driverID);
        }
    }
}
