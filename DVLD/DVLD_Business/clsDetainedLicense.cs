using DataAccessLayer;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business.clsLicense;

namespace DVLD_Business
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        private int _detainID;
        public int DetainID
        {
            get { return _detainID; }
        }
        public int LicenseID { set; get; }
        public DateTime DetainDate { set; get; }
        public decimal FineFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo;
        public bool IsReleased { set; get; }
        public DateTime ReleaseDate { set; get; }
        public int ReleasedByUserID { set; get; }
        public clsUser ReleasedByUserInfo;
        public int ReleaseApplicationID { set; get; }

        public clsDetainedLicense()
        {
            _detainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.Now;
            FineFees = 0;
            CreatedByUserID = -1;
            IsReleased = false;
            ReleaseDate = DateTime.Now;
            ReleasedByUserID = -1;
            ReleaseApplicationID = -1;

            Mode = enMode.AddNew;
        }

        private clsDetainedLicense(int detainID, int licenseID,  DateTime detainDate, decimal fineFees, int createdByUserID, bool isReleased, DateTime releaseDate, 
                                   int releasedByUserID, int releaseApplicationID)
        {
            _detainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;

            CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            ReleasedByUserInfo = clsUser.FindByUserID(ReleasedByUserID);

            Mode = enMode.Update;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDetainedLicense();
            }
            return false;
        }

        private bool _AddNewDetainedLicense()
        {
            _detainID = clsDetainedLicenseData.AddNewDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
            return (_detainID != -1);
        }

        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicenseData.UpdateDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID); ;
        }

        public bool ReleaseDetainedLicense(int releasedByUserID, int releaseApplicationID)
        {
            return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID, releasedByUserID, releaseApplicationID);
        }

        public static clsDetainedLicense FindByDetainID(int detainID)
        {
            int licenseID = -1;
            DateTime detainDate = DateTime.Now;
            decimal fineFees = 0;
            int createdByUserID = -1;
            bool isReleased = false;
            DateTime releaseDate = DateTime.Now;
            int releasedByUserID = -1;
            int releaseApplicationID = -1;


            bool isFound = clsDetainedLicenseData.GetDetainedLicenseInfoByDetainID(detainID, ref licenseID, ref detainDate, ref fineFees, ref createdByUserID, ref isReleased, ref releaseDate,
                                                                                    ref releasedByUserID, ref releaseApplicationID);

            if (isFound)
                return new clsDetainedLicense(detainID, licenseID, detainDate, fineFees, createdByUserID, isReleased, releaseDate, releasedByUserID, releaseApplicationID);
            else
                return null;
        }

        public static clsDetainedLicense FindByLicenseID(int licenseID)
        {
            int detainID = -1;
            DateTime detainDate = DateTime.Now;
            decimal fineFees = 0;
            int createdByUserID = -1;
            bool isReleased = false;
            DateTime releaseDate = DateTime.Now;
            int releasedByUserID = -1;
            int releaseApplicationID = -1;


            bool isFound = clsDetainedLicenseData.GetDetainedLicenseInfoByLicenseID(licenseID, ref detainID, ref detainDate, ref fineFees, ref createdByUserID, ref isReleased, ref releaseDate,
                                                                                    ref releasedByUserID, ref releaseApplicationID);

            if (isFound)
                return new clsDetainedLicense(detainID, licenseID, detainDate, fineFees, createdByUserID, isReleased, releaseDate, releasedByUserID, releaseApplicationID);
            else
                return null;
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();
        }

        public static bool IsLicenseDetained(int licenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(licenseID);
        }
    }
}
