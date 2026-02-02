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

namespace DVLD.Applications.Renew_Driving_license
{
    public partial class frmRenewLocalDrivingLicenseApplication : Form
    {
        private decimal _applicationFees = 0;
        private int _renewedLicenseID = -1;
        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            _SetRenewLicenseApplicationInfoDefaults();
            linkShowLicensesHistory.Enabled = false;
            linkShowNewLicenseInfo.Enabled = false;
            btnRenewLicense.Enabled = false;
        }

        private void _SetRenewLicenseApplicationInfoDefaults()
        {
            lblRenewLicenseApplicationID.Text = "[???]";
            lblRenewedLicenseID.Text = "[???]";
            lblOldLicenseID.Text = "[???]";

            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblExpirationDate.Text = "[???]";

            _applicationFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees;
            lblApplicationFees.Text = _applicationFees.ToString("0");
            lblLicenceFees.Text = "[$$$]";
            lblTotalFees.Text = "[$$$]";

            lblCreatedByUser.Text = clsGlobal.currentUser.UserName;

            txtNotes.Text = "";
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int selectedLicenseID = obj;

            if (selectedLicenseID == -1)
                return;

            linkShowLicensesHistory.Enabled = (selectedLicenseID != -1);

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive == false)
            {
                MessageBox.Show("Selected license is not active, choose an active license.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show($"Selected license is not yet expired, it will expire on: {ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate.ToString("dd/MMM/yyyy")}", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                lblOldLicenseID.Text = selectedLicenseID.ToString();
                lblLicenceFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.ClassFees.ToString("0");
                lblTotalFees.Text = (_applicationFees + ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.ClassFees).ToString("0");

                lblExpirationDate.Text = DateTime.Now.AddYears(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength).ToString("dd/MMM/yyyy");

                btnRenewLicense.Enabled = true;
            }
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to renew this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                clsLicense renewedLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(), clsGlobal.currentUser.UserID);
                if (renewedLicense != null)
                {
                    MessageBox.Show($"License renewed successfully with ID = {renewedLicense.LicenseID}", "License Renewed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _renewedLicenseID = renewedLicense.LicenseID;
                    lblRenewLicenseApplicationID.Text = renewedLicense.ApplicationID.ToString();
                    lblRenewedLicenseID.Text = renewedLicense.LicenseID.ToString();

                    ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                    linkShowNewLicenseInfo.Enabled = true;
                    btnRenewLicense.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Failed to renew license!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void linkShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseInfo frm = new frmDriverLicenseInfo(_renewedLicenseID);
            frm.ShowDialog();
        }
    }
}
