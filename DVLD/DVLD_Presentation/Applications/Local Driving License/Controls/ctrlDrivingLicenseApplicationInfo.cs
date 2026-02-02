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

namespace DVLD.Applications.Local_Driving_License.Controls
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private int _localDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        //private int _licenseID;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _localDrivingLicenseApplicationID; }
        }
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadApplicationInfoByLocalDrivingApplicationID(int localDrivingLicenseApplicationID)
        {
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID);

            if (_localDrivingLicenseApplication == null)
            {
                MessageBox.Show("No local Driving License Application with ID = " + localDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _FillLocalDrivingLicenseApplicationInfo();
        }

        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            //_licenseID = _localDrivingLicenseApplication.GetActiveLicenseID();

            //incase there is license enable the show link.
            //linkShowLicenseInfo.Enabled = (_licenseID != -1);

            lblDrivingLicenseApplicationID.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClassName.Text = _localDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblPassedTests.Text = _localDrivingLicenseApplication.GetPassedTestCount().ToString() + "/3";

            // Fill base application Info
            ctrlApplicationBasicInfo1.LoadBaseApplicationInfo(_localDrivingLicenseApplication.ApplicationID);
        }
    }
}
