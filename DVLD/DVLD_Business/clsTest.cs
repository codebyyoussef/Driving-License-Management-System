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
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        private int _testID = -1;
        public int TestID 
        { 
            get { return _testID; } 
        }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo;
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTest()
        {
            _testID = -1;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = "";
            CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsTest(int testID, int testAppointmentID, bool testResult, string notes, int createdByUserID)
        {
            _testID = testID;
            TestAppointmentID = testAppointmentID;
            TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            TestResult = testResult;
            Notes = notes;
            CreatedByUserID = createdByUserID;

            Mode = enMode.Update;
        }

        private bool _AddNewTest()
        {
            _testID = clsTestData.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
                                                              
            return (_testID != -1);
        }

        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.TestID, this.TestAppointmentID,
                this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTest();

            }

            return false;
        }

        public static clsTest Find(int testID)
        {
            int testAppointmentID = -1;
            bool testResult = false;
            string notes = "";
            int createdByUserID = -1;

            bool isFound = clsTestData.GetTestInfoByID(testID, ref testAppointmentID, ref testResult, ref notes, ref createdByUserID);

            if (isFound)
                return new clsTest(testID, testAppointmentID, testResult, notes, createdByUserID);

            else
                return null;

        }

        public static clsTest FindLastTestPerPersonAndLicenseClassAndTestType(int personID, int licenseClassID, clsTestType.enTestType testTypeID)
        {
            int testID = -1;
            int testAppointmentID = -1;
            bool testResult = false;
            string notes = "";
            int createdByUserID = -1;

            bool isFound = clsTestData.GetLastTestByPersonAndLicenseClassAndTestType(personID, licenseClassID, (byte)testTypeID, ref testID,
                           ref testAppointmentID, ref testResult, ref notes, ref createdByUserID);

            if (isFound)
                return new clsTest(testID, testAppointmentID, testResult, notes, createdByUserID);

            else
                return null;
        }

        public static DataTable GetAllTests()
        {
            return clsTestData.GetAllTests();
        }

        public static byte GetPassedTestCount(int localDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(localDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int localDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(localDrivingLicenseApplicationID) == 3;
        }
    }
}
