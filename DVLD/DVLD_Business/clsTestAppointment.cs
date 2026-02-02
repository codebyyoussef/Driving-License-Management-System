using DataAccessLayer;
using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public clsTestType.enTestType TestTypeID { get; set; }
        public clsTestType TestTypeInfo;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo;
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestApplicationInfo;
        public int TestID
        {
            get { return _GetTestID(); }
        }

        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = clsTestType.enTestType.VisionTest;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            RetakeTestApplicationID = -1;

            Mode = enMode.AddNew;
        }

        private clsTestAppointment(int testAppointmentID, clsTestType.enTestType testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate, 
                                   decimal paidFees, int createdByUserID, bool isLocked, int retakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            TestTypeInfo = clsTestType.Find(TestTypeID);
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID); ;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            IsLocked = isLocked;
            RetakeTestApplicationID = retakeTestApplicationID;
            RetakeTestApplicationInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID);

            Mode = enMode.Update;
        }

        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentData.GetAllTestAppointments();
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int localApplicationID, clsTestType.enTestType testType)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(localApplicationID, (byte)testType);
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestApointment((byte) TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees,
                                                                                  CreatedByUserID, IsLocked, RetakeTestApplicationID);
            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, this.AppointmentDate);
        }

        public bool SetAppointmentLocked()
        {
            return clsTestAppointmentData.SetAppointmentLocked(this.TestAppointmentID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTestAppointment();
            }
            return false;
        }

        public static clsTestAppointment Find(int testAppointmentID)
        {
            byte testTypeID = 0;
            int localApplicationID = -1;
            DateTime appointmentDate = DateTime.Now;
            decimal paidFees = 0;
            int createdByUserID = -1;
            bool isLocked = false;
            int retakeTestApplicationID = -1;


            bool isFound = clsTestAppointmentData.GetTestAppointmentByID(testAppointmentID,ref testTypeID, ref localApplicationID, ref appointmentDate, ref paidFees, 
                                                                         ref createdByUserID, ref isLocked, ref retakeTestApplicationID);

            if (isFound)
                return new clsTestAppointment(testAppointmentID, (clsTestType.enTestType)testTypeID, localApplicationID, appointmentDate, paidFees, createdByUserID, isLocked, 
                                              retakeTestApplicationID);
            else
                return null;
        }

        public static clsTestAppointment GetLastTestAppointment(int localDrivingLicenseApplicationID, clsTestType.enTestType testTypeID)
        {
            int testAppointmentID = -1;
            DateTime appointmentDate = DateTime.Now;
            decimal paidFees = 0;
            int createdByUserID = -1;
            bool isLocked = false;
            int retakeTestApplicationID = -1;


            bool isFound = clsTestAppointmentData.GetLastTestAppointment(localDrivingLicenseApplicationID, (byte)testTypeID, ref testAppointmentID, ref appointmentDate, ref paidFees,
                                                                         ref createdByUserID, ref isLocked, ref retakeTestApplicationID);

            if (isFound)
                return new clsTestAppointment(testAppointmentID, testTypeID, localDrivingLicenseApplicationID, appointmentDate, paidFees, createdByUserID, isLocked,
                                              retakeTestApplicationID);
            else
                return null;
        }

        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(this.TestAppointmentID);
        }
    }
}
