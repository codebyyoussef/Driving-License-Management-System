using DVLD.Global_Classes;
using DVLD.License;
using DVLD.License.Local_Licenses;
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

namespace DVLD.Applications.Release_License
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        private int _detainedLicenseID = -1;
        private clsDetainedLicense _detainedLicense;
        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicenseApplication(int detainedLicenseID)
        {
            InitializeComponent();
            _detainedLicenseID = detainedLicenseID;
        }

        private void frmReleaseDetainedLicenseApplication_Load(object sender, EventArgs e)
        {
            if (_detainedLicenseID == -1)
            {
                linkShowLicensesHistory.Enabled = false;
                linkShowLicenseInfo.Enabled = false;
                btnRelease.Enabled = false;
            }
            else
            {
                ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_detainedLicenseID);
                ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                _DisplayDetainedLicenseReleaseInfo();
            }
        }

        private void frmReleaseDetainedLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.TextBoxLicenseIDFocus();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _detainedLicenseID = obj;

            if (_detainedLicenseID == -1)
                return;

            linkShowLicensesHistory.Enabled = (_detainedLicenseID != -1);

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected license is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _DisplayDetainedLicenseReleaseInfo();

            btnRelease.Enabled = true;
        }

        private void _DisplayDetainedLicenseReleaseInfo()
        {
            _detainedLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedLicenseInfo;

            lblLicenseID.Text = _detainedLicense.LicenseID.ToString();

            lblDetainID.Text = _detainedLicense.DetainID.ToString();
            lblDetainDate.Text = _detainedLicense.DetainDate.ToString("dd/MMM/yyyy");
            lblCreatedByUser.Text = _detainedLicense.CreatedByUserID.ToString();

            decimal releaseApplicationFees = clsApplicationType.Find((byte)clsApplication.enApplicationType.ReleaseDetainedDrivingLicense).Fees;
            lblApplicationFees.Text = releaseApplicationFees.ToString("0");
            lblFineFees.Text = _detainedLicense.FineFees.ToString("0");
            lblTotalFees.Text = (releaseApplicationFees + _detainedLicense.FineFees).ToString("0");
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to release this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int releaseApplicationID = -1;
                if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.currentUser.UserID, ref releaseApplicationID))
                {
                    lblApplicationID.Text = releaseApplicationID.ToString();
                    MessageBox.Show($"License released successfully.", "License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                    btnRelease.Enabled = false;
                    linkShowLicenseInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Failed to release the license!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseInfo frm = new frmDriverLicenseInfo(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID);
            frm.ShowDialog();
        }

        private void linkShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        
    }
}
