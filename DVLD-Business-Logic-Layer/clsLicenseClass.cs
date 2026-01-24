using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsLicenseClass
    {
        public enum enLicenseClass { SmallMotorcycle = 1, HeavyMotorcycleLicense = 2, OrdinaryDrivingLicense = 3, Commercial = 4, Agricultural = 5, SmallAndMediumBus = 6, TruckAndHeavyVehicle = 7 };

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MimimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }

        private clsLicenseClass(int licenseClassID, string className, string classDescription, byte mimimumAllowedAge, byte defaultValidityLength, decimal classFees)
        {
            LicenseClassID = licenseClassID;
            ClassName = className;
            ClassDescription = classDescription;
            MimimumAllowedAge = mimimumAllowedAge;
            DefaultValidityLength = defaultValidityLength;
            ClassFees = classFees;
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }

        public static clsLicenseClass Find(int licenseClassID)
        {
            string className = ""; 
            string classDescription = "";
            byte minimumAllowedAge = 18; 
            byte defaultValidityLength = 10; 
            decimal classFees = 0;

            if (clsLicenseClassData.GetLicenseClassInfoByID(licenseClassID, ref className, ref classDescription,
                    ref minimumAllowedAge, ref defaultValidityLength, ref classFees))

                return new clsLicenseClass(licenseClassID, className, classDescription,
                    minimumAllowedAge, defaultValidityLength, classFees);
            else
                return null;
        }

        public static clsLicenseClass Find(string className)
        {
            int licenseClassID = -1;
            string classDescription = "";
            byte minimumAllowedAge = 18;
            byte defaultValidityLength = 10;
            decimal classFees = 0;

            if (clsLicenseClassData.GetLicenseClassInfoByClassName(className, ref licenseClassID, ref classDescription,
                    ref minimumAllowedAge, ref defaultValidityLength, ref classFees))

                return new clsLicenseClass(licenseClassID, className, classDescription,
                    minimumAllowedAge, defaultValidityLength, classFees);
            else
                return null;
        }

        public static byte GetDefaultValididyLengthForLicenseClassID(byte licenseClassID)
        {
            return clsLicenseClassData.GetDefaultValididyLengthForLicenseClassID(licenseClassID);
        }
    }
}
