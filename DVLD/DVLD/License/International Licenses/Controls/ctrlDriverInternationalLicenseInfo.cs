using BusinessLogicLayer;
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

namespace DVLD.License.International_internationalLicenses.Controls
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        private int _internationalLicenseID = -1;
        private clsInternationalLicense _internationalLicense;

        public int InternationalLicenseID
        {
            get { return _internationalLicenseID; }
        }
        public clsInternationalLicense SelectedInternationalLicenseInfo
        {
            get { return _internationalLicense; }
        }
        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void _FillDriverInternationalLicenseInfo()
        {
            lblApplicationID.Text = _internationalLicense.ApplicationID.ToString();

            // Person Info
            lblName.Text = _internationalLicense.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _internationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblDateOfBirth.Text = _internationalLicense.DriverInfo.PersonInfo.DateOfBirth.ToString("dd/MMM/yyyy");
            lblGendor.Text = _internationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";

            // License Info
            lblLicenseID.Text = _internationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblInternationalLicenseID.Text = _internationalLicenseID.ToString();
            lblDriverID.Text = _internationalLicense.DriverID.ToString();
            lblIssueDate.Text = _internationalLicense.IssueDate.ToString("dd/MMM/yyyy");
            lblExpirationDate.Text = _internationalLicense.ExpirationDate.ToString("dd/MMM/yyyy");
            lblIsActive.Text = _internationalLicense.IsActive == true ? "Yes" : "No";

            _LoadPersonImage();
        }

        public void _LoadPersonImage()
        {
            clsPerson.enGendor gender = (clsPerson.enGendor)_internationalLicense.DriverInfo.PersonInfo.Gendor;
            if (gender == clsPerson.enGendor.Male)
            {
                pbGendor.Image = Properties.Resources.Male;
            }
            else
            {
                pbGendor.Image = Properties.Resources.Female;
            }

            string imagePath = _internationalLicense.DriverInfo.PersonInfo.ImagePath;

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

        public void LoadDriverInternationalLicenseInfo(int internationalLicenseID)
        {
            _internationalLicenseID = internationalLicenseID;
            _internationalLicense = clsInternationalLicense.Find(internationalLicenseID);

            if (_internationalLicense == null)
            {
                MessageBox.Show("Could not find international license with ID = " + internationalLicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _internationalLicenseID = -1;
                return;
            }

            _FillDriverInternationalLicenseInfo();
        }
    }
}
