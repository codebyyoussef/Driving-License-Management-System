using DataAccessLayer;
using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Business.clsLicense;

namespace DVLD_Business
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enIssueReason { FirstTime = 1, Renew = 2, ReplacementForDamaged = 3, ReplacementForLost = 4 };

        private int _licenseID;
        public int LicenseID { 
            get { return _licenseID; }
        }
        public int ApplicationID { get; set; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo;
        public int DriverID { get; set; }
        public clsDriver DriverInfo;
        public byte LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }

        public string IssueReasonText
        {
            get { return GetIssueReasonText(this.IssueReason); }}
        public int CreatedByUserID { get; set; }

        //public clsDetainedLicense DetainedInfo { get; set; }
        //public bool IsDetained
        //{
        //    get { return clsDetainedLicense.IsLicenseDetained(this.LicenseClassID); }
        //}

        public clsLicense()
        {
            _licenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            LicenseClassID = 0;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = string.Empty;
            PaidFees = 0;
            IsActive = true;
            IssueReason = enIssueReason.FirstTime;
            CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsLicense(int licenseID, int applicationID, int driverID, byte licenseClassID, DateTime issueDate, DateTime expirationDate,
                           decimal paidFees, bool isActive, enIssueReason issueReason, int createdByUserID, string notes = "")
        {
            _licenseID = licenseID;
            ApplicationID = applicationID;
            LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByApplicationID(applicationID);
            DriverID = driverID;
            DriverInfo = clsDriver.FindDriverByDriverID(DriverID);
            LicenseClassID = licenseClassID;
            LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;

            //DetainedLicense = clsDetainedLicense.FindByLicenseID(LicenseID);

            Mode = enMode.Update;
        }

        public static string GetIssueReasonText(enIssueReason issueReason)
        {
            switch (issueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";

                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.ReplacementForDamaged:
                    return "Replacement for damaged";

                case enIssueReason.ReplacementForLost:
                    return "Replacement for lost";
            }
            return "";
        }

        private bool _AddNewLicense()
        {
            _licenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClassID, this.IssueDate,
                            this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
            return (_licenseID != -1);
        }

        //private bool _UpdateLicense()
        //{
        //}

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                //case enMode.Update:
                //    return _UpdateLicense();
            }
            return false;
        }

        public static DataTable GetDriverLicenses(int driverID)
        {
            return clsLicenseData.GetDriverLicenses(driverID);
        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();
        }

        public static clsLicense Find(int licenseID)
        {
            int applicationID = -1;
            int driverID = -1;
            byte licenseClassID = 0;
            DateTime issueDate = DateTime.Now;
            DateTime expirationDate = DateTime.Now;
            decimal paidFees = 0;
            bool isActive = false;
            byte issueReason = 0;
            int createdByUserID = -1;
            string notes = "";

            bool isFound = clsLicenseData.GetLicenseInfoByLicenseID(licenseID, ref applicationID, ref driverID, ref licenseClassID, ref issueDate,
                                                                    ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, 
                                                                    ref createdByUserID);

            if (isFound)
                return new clsLicense(licenseID, applicationID, driverID, licenseClassID, issueDate, expirationDate, paidFees, isActive, 
                                      (enIssueReason)issueReason, createdByUserID, notes);
            else
                return null;
        }

        public static bool IsLicenseExistByPersonID(int personID, byte licenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(personID, licenseClassID) != -1;
        }

        public static int GetActiveLicenseIDByPersonID(int personID, byte licenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(personID, licenseClassID);
        }

        public bool IsLicenseExpired()
        {
            return this.ExpirationDate <= DateTime.Now;
        }

        public bool DeactiveCurrentLicense()
        {
            return clsLicenseData.DeactiveLicense(_licenseID);
        }

        public static bool DeactiveCurrentLicense(int licenseID)
        {
            return clsLicenseData.DeactiveLicense(licenseID);
        }



    }
}
