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
using static DVLD_Business.clsLicense;

namespace DVLD.Applications.Detain_License
{
    public partial class frmDetainLicenseApplication : Form
    {
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmDetainLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.TextBoxLicenseIDFocus();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedByUser.Text = clsGlobal.currentUser.UserName;

            linkShowLicensesHistory.Enabled = false;
            linkShowLicenseInfo.Enabled = false;

            btnDetain.Enabled = false;
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

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected license is already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            lblLicenseID.Text = selectedLicenseID.ToString();
            btnDetain.Enabled = true;
            txtFineFees.Focus();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (txtFineFees.Text.Trim() == "")
            {
                errorProvider1.SetError(txtFineFees, "Fees can not be empty!");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            DialogResult result = MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int detainedLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Detain(Convert.ToDecimal(txtFineFees.Text), clsGlobal.currentUser.UserID);
                if (detainedLicenseID != -1)
                {
                    MessageBox.Show($"License detained successfully with ID = {detainedLicenseID}", "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblDetainID.Text = detainedLicenseID.ToString();

                    ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                    txtFineFees.Enabled = false;
                    btnDetain.Enabled = false;
                    linkShowLicenseInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Failed to detain the license!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
