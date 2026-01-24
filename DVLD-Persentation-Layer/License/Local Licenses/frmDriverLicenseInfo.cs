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

namespace DVLD.License.Local_Licenses
{
    public partial class frmDriverLicenseInfo : Form
    {
        private int _localDrivingLicenseApplicationID;
        public frmDriverLicenseInfo(int localDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
        }

        private void frmDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            int licenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_localDrivingLicenseApplicationID).GetActiveLicenseID();
            ctrlDriverLicenseInfo1.LoadDriverLicenseInfo(licenseID);
        }
    }
}
