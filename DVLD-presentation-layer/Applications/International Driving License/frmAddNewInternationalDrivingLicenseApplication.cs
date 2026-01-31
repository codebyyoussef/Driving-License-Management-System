using BusinessLogicLayer;
using DVLD.Global_Classes;
using DVLD.License;
using DVLD.License.International_Licenses;
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

namespace DVLD.Applications.International_Driving_License
{
    public partial class frmAddNewInternationalDrivingLicenseApplication : Form
    {
        private int _driverID = -1;
        private clsApplication.enApplicationType _applicationTypeID = clsApplication.enApplicationType.NewInternationalLicense;
        private int _internationalLicenseID;
        private clsInternationalLicense _internationalLicense;
        public frmAddNewInternationalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmAddNewInternationalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _SetApplicationInfoDefaults();
            linkShowLicensesHistory.Enabled = false;
            linkShowLicenseInfo.Enabled = false;
            btnIssueLicense.Enabled = false;
        }

        private void _SetApplicationInfoDefaults()
        {
            lblInternationalLicenseApplicationID.Text = "[???]";
            lblInternationalLicenseID.Text = "[???]";
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString("0");
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");
            lblCreatedByUser.Text = clsGlobal.currentUser.UserName;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int selectedLicenseID = obj;

            lblLocalLicenseID.Text = selectedLicenseID.ToString();

            linkShowLicensesHistory.Enabled = selectedLicenseID != -1;

            if (selectedLicenseID == -1)
                return;


            
            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassID != 3)
            {
                MessageBox.Show("Selected license should be class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _SetApplicationInfoDefaults();
                return;
            }

            _driverID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            _internationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(_driverID); 
            if (_internationalLicenseID != -1)
            {
                MessageBox.Show($"Person already have an active international license with ID = {_internationalLicenseID}", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                
                _internationalLicense = clsInternationalLicense.Find(_internationalLicenseID);
                lblInternationalLicenseApplicationID.Text = _internationalLicense.ApplicationID.ToString();
                lblInternationalLicenseID.Text = _internationalLicenseID.ToString();
                lblApplicationDate.Text = _internationalLicense.ApplicationDate.ToString("dd/MMM/yyyy");
                lblIssueDate.Text = _internationalLicense.IssueDate.ToString("dd/MMM/yyyy");
                lblExpirationDate.Text = _internationalLicense.ExpirationDate.ToString("dd/MMM/yyyy");
                lblCreatedByUser.Text = _internationalLicense.CreatedByUserInfo.UserName;

                linkShowLicenseInfo.Enabled = true;
            }
            else
            {
                _SetApplicationInfoDefaults();
                linkShowLicensesHistory.Enabled = true;
                linkShowLicenseInfo.Enabled = false;
                btnIssueLicense.Enabled = true;
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                clsInternationalLicense internationalLicense = new clsInternationalLicense();

                internationalLicense.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
                internationalLicense.ApplicationDate = DateTime.Now;
                internationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                internationalLicense.LastStatusDate = DateTime.Now;
                internationalLicense.PaidFees = clsApplicationType.Find((byte)clsApplication.enApplicationType.NewInternationalLicense).Fees;

                internationalLicense.DriverID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
                internationalLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.LicenseID;
                internationalLicense.IssueDate = DateTime.Now;
                internationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

                internationalLicense.CreatedByUserID = clsGlobal.currentUser.UserID;

                if (internationalLicense.Save())
                {
                    MessageBox.Show($"International license issued successfully with ID = {internationalLicense.InternationalLicenceID}", "License issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _internationalLicenseID = internationalLicense.InternationalLicenceID;
                    lblInternationalLicenseApplicationID.Text = internationalLicense.ApplicationID.ToString();
                    lblInternationalLicenseID.Text = internationalLicense.InternationalLicenceID.ToString();

                    ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                    linkShowLicenseInfo.Enabled = true;
                    btnIssueLicense.Enabled = false;
                }
                else
                {
                    MessageBox.Show($"Failed to issue international license!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalDriverInfo frm = new frmShowInternationalDriverInfo(_internationalLicenseID);
            frm.ShowDialog();
        }

        private void linkShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        
    }
}
