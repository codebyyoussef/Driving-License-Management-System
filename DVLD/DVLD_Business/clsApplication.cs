using DataAccessLayer;
using DVLD_Business;
using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business.clsApplication;

namespace DVLD_Business
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1}
        public enMode Mode = enMode.AddNew;

        public enum enApplicationType { NewLocalDrivingLicense = 1, RenewDrivingLicense = 2, ReplacementForALostDrivingLicense = 3,
                                        ReplacementForADamagedDrivingLicense = 4, ReleaseDetainedDrivingLicense = 5, NewInternationalLicense = 6,
                                        RetakeTest = 7}

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 }

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson ApplicantPersonInfo;
        public DateTime ApplicationDate { get; set; }
        public byte ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationTypeInfo;
        public enApplicationStatus ApplicationStatus {  get; set; }
        public string StatusText
        {
            get
            {
                switch(ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
                
            }
        }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;

        public clsApplication()
        {
            ApplicationID = -1;
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = 0;
            ApplicationStatus = enApplicationStatus.New;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsApplication(int applicationID, int applicantPersonID, DateTime applicationDate, byte applicationTypeID, 
                               enApplicationStatus applicationStatus, DateTime lastStatusDate, decimal paidFees, int createdByUserID)
        {
            this.ApplicationID = applicationID;
            this.ApplicantPersonID = applicantPersonID;
            this.ApplicantPersonInfo = clsPerson.Find(ApplicantPersonID);
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            Mode = enMode.Update;
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, (byte)this.ApplicationTypeID, (byte)this.ApplicationStatus,
                                                                      this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return (this.ApplicationID != -1);
        }

        //private bool _UpdateApplicationStatus()
        //{
        //    return clsApplicationData.UpdateApplicationStatus(this.ApplicationID, (byte)this.ApplicationStatus, this.LastStatusDate);
        //}

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                //case enMode.Update:
                //    return _UpdateApplicationStatus();
            }
            return false;
        }

        public static bool Delete(int applicationID)
        {
            return clsApplicationData.DeleteApplication(applicationID);
        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        public static clsApplication FindBaseApplication(int applicationID)
        {
            int applicantPersonID = -1;
            DateTime applicationDate = DateTime.Now;
            byte applicationTypeID = 0;
            byte applicationStatus = 0;
            DateTime lastStatusDate = DateTime.Now;
            decimal paidFees = 0;
            int createdByUserID = -1;

            bool isFound = clsApplicationData.GetApplicationInfoByID(applicationID, ref applicantPersonID, ref applicationDate, ref applicationTypeID,
                           ref applicationStatus, ref lastStatusDate, ref paidFees, ref createdByUserID);

            if (isFound)
                return new clsApplication(applicationID, applicantPersonID, applicationDate, applicationTypeID, (enApplicationStatus)applicationStatus, 
                                          lastStatusDate, paidFees, createdByUserID);
            else
                return null;
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateApplicationStatus(ApplicationID, 2);
        }

        public bool SetComplete()
        {
            return clsApplicationData.UpdateApplicationStatus(this.ApplicationID, (byte)enApplicationStatus.Completed);
        }

        public static bool IsApplicationExist(int applicationID)
        {
            return clsApplicationData.IsApplicationExist(applicationID);
        }

        public static bool DoesPersonHaveActiveApplication(int personID, byte applicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(personID, applicationTypeID);
        }

        public bool DoesPersonHaveActiveApplication(byte applicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(this.ApplicantPersonID, applicationTypeID);
        }

        public static int GetActiveApplicationID(int personID, enApplicationType applicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(personID, (byte)applicationTypeID);
        }

        public int GetActiveApplicationID(enApplicationType applicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(this.ApplicantPersonID, (byte)applicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int personID, enApplicationType applicationTypeID, byte licenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(personID, (byte)applicationTypeID, licenseClassID);
        }
    }
}
