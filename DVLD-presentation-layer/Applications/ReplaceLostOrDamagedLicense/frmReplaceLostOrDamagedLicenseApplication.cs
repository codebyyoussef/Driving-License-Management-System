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

namespace DVLD.Applications.ReplaceLostOrDamagedLicense
{
    public partial class frmReplaceLostOrDamagedLicenseApplication : Form
    {
        private clsLicense.enIssueReason _issueReason;
        private int _newLicenseID = -1;
        public frmReplaceLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmReplaceLostOrDamagedLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.TextBoxLicenseIDFocus();
        }

        private void frmReplaceLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Checked = true;
            _SetFormDefaults();
        }

        private void _SetFormDefaults()
        {
            if (rbDamagedLicense.Checked)
            {
                this.Text = "Repalcement For Damaged License";
                lblTitle.Text = "Repalcement For Damaged License";
                lblApplicationFees.Text = clsApplicationType.Find((byte)clsApplication.enApplicationType.ReplacementForADamagedDrivingLicense).Fees.ToString("0");
            }
            else
            {
                this.Text = "Repalcement For Lost License";
                lblTitle.Text = "Repalcement For Lost License";
                lblApplicationFees.Text = clsApplicationType.Find((byte)clsApplication.enApplicationType.ReplacementForALostDrivingLicense).Fees.ToString("0");
            }
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedByUser.Text = clsGlobal.currentUser.UserName;
            linkShowLicensesHistory.Enabled = false;
            linkShowNewLicenseInfo.Enabled = false;
            btnIssueReplacement.Enabled = false;    
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Repalcement For Damaged License";
            lblTitle.Text = "Repalcement For Damaged License";
            lblApplicationFees.Text = clsApplicationType.Find((byte)clsApplication.enApplicationType.ReplacementForADamagedDrivingLicense).Fees.ToString("0");
            _issueReason = clsLicense.enIssueReason.ReplacementForDamaged;
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Repalcement For Lost License";
            lblTitle.Text = "Repalcement For Lost License";
            lblApplicationFees.Text = clsApplicationType.Find((byte)clsApplication.enApplicationType.ReplacementForALostDrivingLicense).Fees.ToString("0");
            _issueReason = clsLicense.enIssueReason.ReplacementForLost;
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

            lblOldLicenseID.Text = selectedLicenseID.ToString();
            btnIssueReplacement.Enabled = true;
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to issue a replacement for this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                clsLicense newLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Replace(_issueReason, clsGlobal.currentUser.UserID);
                if (newLicense != null)
                {
                    MessageBox.Show($"License replaced successfully with ID = {newLicense.LicenseID}", "License issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _newLicenseID = newLicense.LicenseID;
                    lblReplacementLicenseApplicationID.Text = newLicense.ApplicationID.ToString();
                    lblReplacedLicenseID.Text = newLicense.LicenseID.ToString();

                    ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                    linkShowNewLicenseInfo.Enabled = true;
                    btnIssueReplacement.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Failed to Issue the license!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseInfo frm = new frmDriverLicenseInfo(_newLicenseID);
            frm.ShowDialog();
        }

        private void linkShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
