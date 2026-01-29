using DataAccessLayer;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business.clsApplication;
using static DVLD_Business.clsLicense;

namespace BusinessLogicLayer
{
    public class clsInternationalLicense : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        private int _internationalLicenseID = -1;
        public int InternationalLicenceID
        {
            get { return _internationalLicenseID; }
        }
        public int DriverID { set; get; }
        public clsDriver DriverInfo;
        public int IssuedUsingLocalLicenseID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }

        public clsInternationalLicense()
        {
            base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;

            _internationalLicenseID = -1;
            DriverID = -1;
            IssuedUsingLocalLicenseID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            IsActive = true;

            Mode = enMode.AddNew;
        }

        private clsInternationalLicense(int applicationID, int applicationPersonID, DateTime applicationDate, enApplicationStatus applicationStatus, DateTime lastStatusDatee, decimal paidFess,
                                        int createdByUserID, int internationLicenseID, int driverID, int issuedUsingLocalLicenseID, DateTime issueDate, DateTime expirationDate, bool isActive)
        {
            
            base.ApplicationID = applicationID;
            base.ApplicantPersonID = applicationPersonID;
            base.ApplicationDate = applicationDate;
            base.ApplicationTypeID = (byte)enApplicationType.NewInternationalLicense;
            base.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = lastStatusDatee;
            base.PaidFees = paidFess;
            base.CreatedByUserID = createdByUserID;
            base.CreatedByUserInfo = clsUser.FindByUserID(createdByUserID);

            _internationalLicenseID = internationLicenseID;
            DriverID = driverID;
            DriverInfo = clsDriver.FindDriverByDriverID(DriverID);
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;

            Mode = enMode.Update;
        }       

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _AddNewInternationalLicense();
            }
            return false;
        }

        private bool _AddNewInternationalLicense()
        {
            _internationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate,
                                                                                             this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return (_internationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            // Not implemented yet.
            return false;
        }

        public static clsInternationalLicense Find(int internationLicenseID)
        {
            int applicationID = -1;
            int driverID = -1;
            int issuedUsingLocalLicenseID = -1;
            DateTime issueDate = DateTime.Now;
            DateTime expirationDate = DateTime.Now;
            bool isActive = false;
            int createdByUserID = -1;

            bool isFound = clsInternationalLicenseData.GetInternationalLicenseInfoByID(internationLicenseID, ref applicationID, ref driverID, ref issuedUsingLocalLicenseID, ref issueDate,
                                                                                       ref expirationDate, ref isActive, ref createdByUserID);

            clsApplication application = clsApplication.FindBaseApplication(applicationID);

            if (isFound)
                return new clsInternationalLicense(applicationID, application.ApplicantPersonID, application.ApplicationDate, application.ApplicationStatus, application.LastStatusDate, application.PaidFees,
                                                   createdByUserID, internationLicenseID, driverID, issuedUsingLocalLicenseID, issueDate, expirationDate, isActive);
            else
                return null;
        }

        public static DataTable GetDriverInternationalLicenses(int driverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(driverID);
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int driverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(driverID);
        }

        public static bool IsInternationalLicenseExistByDriverID(int driverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(driverID) != -1;
        }
        
        public bool IsInternationalLicenseExpired()
        {
            return this.ExpirationDate <= DateTime.Now;
        }
    }
}
