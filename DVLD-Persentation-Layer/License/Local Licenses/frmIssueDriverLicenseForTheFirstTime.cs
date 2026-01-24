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

namespace DVLD.License
{
    public partial class frmIssueDriverLicenseForTheFirstTime : Form
    {
        private int _localDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        public frmIssueDriverLicenseForTheFirstTime(int localDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
        }

        private void frmIssueDriverLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_localDrivingLicenseApplicationID);

            if (_localDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Applicaiton with ID=" + _localDrivingLicenseApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!_localDrivingLicenseApplication.PassedAllTest())
            {
                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int licenseID = _localDrivingLicenseApplication.GetActiveLicenseID();
            if (licenseID != -1)
            {
                MessageBox.Show("Person already has License before with License ID=" + licenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingApplicationID(_localDrivingLicenseApplicationID);
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int licenseID = _localDrivingLicenseApplication.IssueLicenseForTheFirstTime(txtNotes.Text, clsGlobal.currentUser.UserID);

            if (licenseID != -1)
            {
                MessageBox.Show($"License issued successfully with license ID = {licenseID}", "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show($"Failed to issued the license", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }             
        }
    }
}
