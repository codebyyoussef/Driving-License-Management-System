using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Resolvers;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _mode = enMode.AddNew;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public byte LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = 0;
            
            _mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID, int applicationID, int applicantPersonID,
          DateTime applicationDate, byte applicationTypeID,
           enApplicationStatus applicationStatus, DateTime lastStatusDate,
           decimal paidFees, int createdByUserID, byte licenseClassID)

        {
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.ApplicationID = applicationID;
            this.ApplicantPersonID = applicantPersonID;
            this.ApplicantPersonInfo = clsPerson.Find(ApplicantPersonID);
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = (byte)applicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.LicenseClassID = licenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(licenseClassID);
            _mode = enMode.Update;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID(int localDrivingLicenseApplicationID)
        {
            int applicationID = -1;
            byte licenseClassID = 0;

            bool isFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID(localDrivingLicenseApplicationID, ref applicationID, ref licenseClassID);
            if (isFound)
            {
                clsApplication application = clsApplication.FindBaseApplication(applicationID);
                return new clsLocalDrivingLicenseApplication(localDrivingLicenseApplicationID, applicationID, application.ApplicantPersonID,
                           application.ApplicationDate, application.ApplicationTypeID, application.ApplicationStatus, application.LastStatusDate,
                           application.PaidFees, application.CreatedByUserID, licenseClassID);
            }
            else
                return null;
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int applicationID)
        {
            int localDrivingLicenseApplicationID = -1;
            byte licenseClassID = 0;

            bool isFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID(applicationID, ref localDrivingLicenseApplicationID, ref licenseClassID);

            if (isFound)
            {
                clsApplication application = clsApplication.FindBaseApplication(applicationID);
                return new clsLocalDrivingLicenseApplication(localDrivingLicenseApplicationID, applicationID, application.ApplicantPersonID,
                           application.ApplicationDate, application.ApplicationTypeID, application.ApplicationStatus, application.LastStatusDate,
                           application.PaidFees, application.CreatedByUserID, licenseClassID);
            }
            else
                return null;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        public bool Delete()
        {
            bool isLocalDrivingApplicationDeleted = false;
            bool isBaseApplicationDeleted = false;

            //First we delete the Local Driving License Application
            isLocalDrivingApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!isLocalDrivingApplicationDeleted)
                return false;

            //Then we delete the base Application
            isBaseApplicationDeleted = base.Delete();
            return isBaseApplicationDeleted;
        }

        public bool Save()
        {
            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)_mode;
            if (!base.Save())
                return false;


            //After we save the main application now we save the sub application.
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {

                        _mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                //case enMode.Update:

                //    return _UpdateLocalDrivingLicenseApplication();
            }

            return false;
        }

        public bool DoesPassTestType(clsTestType.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (byte)testTypeID);
        }

        public static bool DoesPassTestType(int localDrivingLicenseApplicationID, clsTestType.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(localDrivingLicenseApplicationID, (byte)testTypeID);
        }

        public bool DoesPassPreviousTest(clsTestType.enTestType currentTestType)
        {
            switch(currentTestType)
            {
                //Written Test, you cannot sechdule it before person passes the vision test.
                //we check if pass visiontest 1.
                case clsTestType.enTestType.WrittenTest:
                    return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (byte)clsTestType.enTestType.VisionTest);
                //Street Test, you cannot sechdule it before person passes the written test.
                //we check if pass Written 2.
                case clsTestType.enTestType.StreetTest:
                    return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (byte)clsTestType.enTestType.StreetTest);
                default:
                    return false;
            }
        }

        public bool DoesAttendTestType(clsTestType.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (byte)testTypeID);
        }

        public static bool DoesAttendTestType(int localDrivingLicenseApplicationID, clsTestType.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(localDrivingLicenseApplicationID, (byte)testTypeID);
        }

        public byte TotalTrialsPerTest(clsTestType.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (byte)testTypeID);
        }

        public static byte TotalTrialsPerTest(int localDrivingLicenseApplicationID, clsTestType.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(localDrivingLicenseApplicationID, (byte)testTypeID);
        }

        public bool AttendedTest(clsTestType.enTestType testTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (byte)testTypeID) > 0;
        }

        public static bool AttendedTest(int localDrivingLicenseApplicationID, clsTestType.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(localDrivingLicenseApplicationID, (byte)testTypeID) > 0;
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (byte)testTypeID);
        }

        public static bool IsThereAnActiveScheduledTest(int localDrivingLicenseApplicationID, clsTestType.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(localDrivingLicenseApplicationID, (byte)testTypeID);
        }

        public clsTest GetLastTestPerTestType(clsTestType.enTestType testTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClassAndTestType(this.ApplicantPersonID, this.LicenseClassID, testTypeID);
        }

        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        public static byte GetPassedTestCount(int localDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(localDrivingLicenseApplicationID);
        }

        public bool PassedAllTest()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTest(int localDrivingLicenseApplicationID)
        {
            return clsTest.PassedAllTests(localDrivingLicenseApplicationID);
        }

        public int IssueLicenseForTheFirstTime(string notes, int createdByUserID)
        {
            int driverID = -1;
            clsDriver driver = clsDriver.FindDriverByPersonID(this.ApplicantPersonID);
            if (driver == null)
            {
                driver = new clsDriver();
                driver.PersonID = this.ApplicantPersonID;
                driver.CreatedByUserID = createdByUserID;

                if (driver.Save())
                {
                    driverID = driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                driverID = driver.DriverID;
            }

            clsLicense license = new clsLicense();
            license.ApplicationID = this.ApplicationID;
            license.DriverID = driverID;
            license.LicenseClassID = this.LicenseClassID;
            license.IssueDate = DateTime.Now;
            license.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            license.Notes = notes;
            license.PaidFees = this.LicenseClassInfo.ClassFees;
            license.IsActive = true;
            license.IssueReason = clsLicense.enIssueReason.FirstTime;
            license.CreatedByUserID = createdByUserID;
            
            if (license.Save())
            {
                this.SetComplete();
                return license.LicenseID;
            }
            else
            {
                return -1;
            }
        }

        public bool IsLicenseIssued()
        {
            return GetActiveLicenseID() != -1;
        }

        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

    }
}
