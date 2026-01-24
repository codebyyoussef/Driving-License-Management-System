using DVLD.Applications.Local_Driving_License;
using DVLD.License;
using DVLD.License.Local_Licenses;
using DVLD.Tests;
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

namespace DVLD.Applications.Local_Driving_License_Applications
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        private DataTable _dtAllLocalDrivingLicenseApplications;
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;

            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {

                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;
            }

            cbFilters.SelectedIndex = 0;

        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilters.Text != "None";
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Focus();
                txtFilterValue.SelectAll();
            }
            else
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtAllLocalDrivingLicenseApplications.Rows.Count.ToString();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedFilter = cbFilters.SelectedItem.ToString();
            if (selectedFilter == "L.D.L.AppID")
            {
                // Allow control keys (like Backspace)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Block non-numeric input
                }
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (txtFilterValue.Text.Trim() == "")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            }
            else
            {
                string selectedFilter = cbFilters.SelectedItem.ToString();
                string filterValue = txtFilterValue.Text.Trim();
                string filterColumn = "";

                switch(selectedFilter)
                {
                    case "L.D.L.AppID":
                        filterColumn = "LocalDrivingLicenseApplicationID";
                        break;
                    case "National No":
                        filterColumn = "NationalNo";
                        break;
                    case "Full Name":
                        filterColumn = "FullName";
                        break;
                    case "Status":
                        filterColumn = "Status";
                        break;
                }

                if (selectedFilter == "L.D.L.AppID")
                {
                    _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("{0} = {1}", filterColumn, filterValue);
                }
                else
                {
                    _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("{0} like '{1}%'", filterColumn, filterValue);
                }

                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            }
        }

        private void mnuCancelApplication_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this application?", "Confirm Cancellation",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
                clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID);

                if (localDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application cancelled successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmLocalDrivingLicenseApplications_Load(null, null);
                }
                else
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalLicenseApplication frm = new frmAddUpdateLocalLicenseApplication();
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void mnuDeleteApplication_Click(object sender, EventArgs e)
        {
            DialogResult deleteConfirmation = MessageBox.Show("Are you sure you want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (deleteConfirmation == DialogResult.Yes)
            {
                int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

                clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID);

                if (localDrivingLicenseApplication != null)
                {
                    if (localDrivingLicenseApplication.Delete())
                    {
                        MessageBox.Show("Application deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmLocalDrivingLicenseApplications_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void mnuShowApplicationDetails_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void mnuScheduleVisionTest_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, clsTestType.enTestType.VisionTest);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void mnuSechduleWrittenTest_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, clsTestType.enTestType.WrittenTest);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void mnuSechduleStreetTest_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, clsTestType.enTestType.StreetTest);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmsLocalApplications_Opening(object sender, CancelEventArgs e)
        {
            int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID);

            //Enable/Disable Cancel Menue Item
            //We only cancel the applications with status=new.
            mnuCancelApplication.Enabled = localDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;


            //Enable/Disable Delete Menue Item
            //We only allow delete incase the application status is New or Cancelled.
            mnuDeleteApplication.Enabled = localDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New || localDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.Cancelled;

            bool passedVisionTest = localDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest);
            bool passedWrittenTest = localDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool passedStreetTest = localDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            mnuScheduleTests.Enabled = (!passedVisionTest || !passedWrittenTest || !passedStreetTest) 
                                        && 
                                        localDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;

            if (mnuScheduleTests.Enabled)
            {
                //To Allow Schdule vision test, Person must not passed the same test before.
                mnuScheduleVisionTest.Enabled = !passedVisionTest;

                //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                mnuSechduleWrittenTest.Enabled = passedVisionTest && !passedWrittenTest;

                //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.

                mnuSechduleStreetTest.Enabled = passedVisionTest && passedWrittenTest && !passedStreetTest;
            }

            string status = dgvLocalDrivingLicenseApplications.CurrentRow.Cells[6].Value.ToString();
            if (status == "New")
            {
                byte passedTests = Convert.ToByte(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value);
                mnuIssueDrivingLicenseFirstTime.Enabled = (passedTests == 3);
            }
            else if(status == "Completed")
            {
                mnuIssueDrivingLicenseFirstTime.Enabled = false;
                mnuShowLicense.Enabled = true;
            }
            else
            {
                mnuIssueDrivingLicenseFirstTime.Enabled = false;
                mnuShowLicense.Enabled = false;
            }
        }

        private void mnuIssueDrivingLicenseFirstTime_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicenseForTheFirstTime frm = new frmIssueDriverLicenseForTheFirstTime((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);

        }

        private void mnuShowLicense_Click(object sender, EventArgs e)
        {
            frmDriverLicenseInfo frm = new frmDriverLicenseInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void mnuShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID);
            frmLicenseHistory frm = new frmLicenseHistory(localDrivingLicenseApplication.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
