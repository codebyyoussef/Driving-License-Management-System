using DVLD.Controls;
using DVLD.Global_Classes;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static DVLD.Global_Classes.clsGlobal;

namespace DVLD.License.Local_Licenses
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private int _licenseID = -1;
        private clsLicense _license;

        public int LicenseID
        {
            get { return _licenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return _license; }
        }

        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        private void _FillDriverLicenseInfo()
        {
            // Person Info
            pbImage.ImageLocation = _license.LocalDrivingLicenseApplicationInfo.ApplicantPersonInfo.ImagePath;
            lblName.Text = _license.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _license.DriverInfo.PersonInfo.NationalNo;
            lblDateOfBirth.Text = _license.DriverInfo.PersonInfo.DateOfBirth.ToString("dd/MMM/yyyy");
            lblGendor.Text = _license.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
           


            // License Info
            clsLicenseClass.enLicenseClass licenseClassType = (clsLicenseClass.enLicenseClass)_license.LocalDrivingLicenseApplicationInfo.LicenseClassID;
            switch (licenseClassType)
            {
                case clsLicenseClass.enLicenseClass.SmallMotorcycle:
                    pbLicenseClass.Image = Properties.Resources.SmallMotor;
                    break;

                case clsLicenseClass.enLicenseClass.HeavyMotorcycleLicense:
                    pbLicenseClass.Image = Properties.Resources.BigMotor;
                    break;

                case clsLicenseClass.enLicenseClass.OrdinaryDrivingLicense:
                    pbLicenseClass.Image = Properties.Resources.Car;
                    break;

                case clsLicenseClass.enLicenseClass.Commercial:
                    pbLicenseClass.Image = Properties.Resources.Taxi;
                    break;

                case clsLicenseClass.enLicenseClass.Agricultural:
                    pbLicenseClass.Image = Properties.Resources.Tractor;
                    break;

                case clsLicenseClass.enLicenseClass.SmallAndMediumBus:
                    pbLicenseClass.Image = Properties.Resources.Bus;
                    break;

                case clsLicenseClass.enLicenseClass.TruckAndHeavyVehicle:
                    pbLicenseClass.Image = Properties.Resources.Truck;
                    break;
            }
            lblClassName.Text = _license.LicenseClassInfo.ClassName;
            lblLicenseID.Text = _licenseID.ToString();
            lblDriverID.Text = _license.DriverID.ToString();
            lblIssueDate.Text = _license.IssueDate.ToString("dd/MMM/yyyy");
            lblExpirationDate.Text = _license.ExpirationDate.ToString("dd/MMM/yyyy");
            lblNotes.Text = String.IsNullOrEmpty(_license.Notes) ? "Empty" : _license.Notes;
            lblIsActive.Text = _license.IsActive == true ? "Yes" : "No";
            switch (_license.IssueReason)
            {
                case clsLicense.enIssueReason.FirstTime:
                    pbIssueReason.Image = Properties.Resources.New;
                    break;

                case clsLicense.enIssueReason.Renew:
                    pbIssueReason.Image = Properties.Resources.Renew;
                    break;

                case clsLicense.enIssueReason.ReplacementForDamaged:
                    pbIssueReason.Image = Properties.Resources.Damaged;
                    break;

                case clsLicense.enIssueReason.ReplacementForLost:
                    pbIssueReason.Image = Properties.Resources.Lost;
                    break;
            }
            lblIssueReason.Text = _license.IssueReasonText;
            //lblIsDetained.Text = _license.IsDetained == true ? "Yes" : "No";
            _LoadPersonImage();
        }

        public void _LoadPersonImage()
        {
            clsPerson.enGendor gender = (clsPerson.enGendor)_license.DriverInfo.PersonInfo.Gendor;
            if (gender == clsPerson.enGendor.Male)
            {
                pbGendor.Image = Properties.Resources.Male;
            }
            else
            {
                pbGendor.Image = Properties.Resources.Female;
            }

            string imagePath = _license.DriverInfo.PersonInfo.ImagePath;

            if (imagePath != "")
            {
                if (File.Exists(imagePath))
                {
                    pbImage.Load(imagePath);
                }
                else
                {
                    MessageBox.Show("Could not find this image: " + imagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void LoadDriverLicenseInfo(int licenseID)
        {
            _licenseID = licenseID;
            _license = clsLicense.Find(licenseID);

            if (_license == null)
            {
                MessageBox.Show("Could not find license with ID = " + licenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _licenseID = -1;
                return;
            }

            _FillDriverLicenseInfo();
        }
    }
}
