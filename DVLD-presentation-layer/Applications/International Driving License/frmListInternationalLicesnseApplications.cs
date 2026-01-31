using BusinessLogicLayer;
using DVLD.Applications.International_Driving_License;
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

namespace DVLD.License.International_Licenses
{
    public partial class frmListInternationalLicesnseApplications : Form
    {
        private DataTable _dtAllInternationalDrivingLicenseApplications;
        public frmListInternationalLicesnseApplications()
        {
            InitializeComponent();
        }

        private void frmListInternationalLicesnseApplications_Load(object sender, EventArgs e)
        {
            _dtAllInternationalDrivingLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            cbFilters.SelectedIndex = 0;

            dgvInternationalDrivingLicenseApplications.DataSource = _dtAllInternationalDrivingLicenseApplications;
            lblRecordsCount.Text = dgvInternationalDrivingLicenseApplications.Rows.Count.ToString();

            if (dgvInternationalDrivingLicenseApplications.Rows.Count > 0)
            {

                dgvInternationalDrivingLicenseApplications.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalDrivingLicenseApplications.Columns[0].Width = 100;

                dgvInternationalDrivingLicenseApplications.Columns[1].HeaderText = "Application ID";
                dgvInternationalDrivingLicenseApplications.Columns[1].Width = 100;

                dgvInternationalDrivingLicenseApplications.Columns[2].HeaderText = "Driver ID";
                dgvInternationalDrivingLicenseApplications.Columns[2].Width = 100;

                dgvInternationalDrivingLicenseApplications.Columns[3].HeaderText = "L.License ID";
                dgvInternationalDrivingLicenseApplications.Columns[3].Width = 120;

                dgvInternationalDrivingLicenseApplications.Columns[4].HeaderText = "Issue Date";
                dgvInternationalDrivingLicenseApplications.Columns[4].Width = 150;

                dgvInternationalDrivingLicenseApplications.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalDrivingLicenseApplications.Columns[5].Width = 150;

                dgvInternationalDrivingLicenseApplications.Columns[6].HeaderText = "Is Active";
                dgvInternationalDrivingLicenseApplications.Columns[5].Width = 150;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string selectedFilter = cbFilters.SelectedItem.ToString();
            string filterColumn = "";
            string filterValue = txtFilterValue.Text.Trim();

            switch (selectedFilter)
            {
                case "International License ID":
                    filterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    filterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    filterColumn = "DriverID";
                    break;
                case "Local License ID":
                    filterColumn = "IssuedUsingLocalLicenseID";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "")
            {
                _dtAllInternationalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvInternationalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }

            _dtAllInternationalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("{0} = {1}", filterColumn, filterValue);
            lblRecordsCount.Text = dgvInternationalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dtAllInternationalDrivingLicenseApplications.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvInternationalDrivingLicenseApplications.Rows.Count.ToString();

            if (cbFilters.Text == "None")
            {
                if (txtFilterValue.Visible)
                    txtFilterValue.Visible = false;
                else
                    cbIsActive.Visible = false;
            }
            else
            {
                if (cbFilters.Text == "Is Active")
                {
                    if (txtFilterValue.Visible)
                    {
                        txtFilterValue.Visible = false;
                    }
                    cbIsActive.Visible = true;
                    cbIsActive.SelectedIndex = 0;
                }
                else
                {
                    if (cbIsActive.Visible)
                    {
                        cbIsActive.Visible = false;
                    }
                    txtFilterValue.Visible = true;
                    txtFilterValue.Clear();
                    txtFilterValue.Focus();
                }
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterColumn = "IsActive";
            string filterValue = cbIsActive.Text;

            switch (filterValue)
            {
                case "All":
                    break;
                case "Yes":
                    filterValue = "1";
                    break;
                case "No":
                    filterValue = "0";
                    break;
            }

            if (filterValue == "All")
                _dtAllInternationalDrivingLicenseApplications.DefaultView.RowFilter = "";
            else
                _dtAllInternationalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("{0} = {1}", filterColumn, filterValue);

            lblRecordsCount.Text = dgvInternationalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalDrivingLicenseApplication frm = new frmAddNewInternationalDrivingLicenseApplication(); 
            frm .ShowDialog();
            frmListInternationalLicesnseApplications_Load(null, null);
        }

        private void mnuShowPersonDetails_Click(object sender, EventArgs e)
        {
            int driverID = (int)dgvInternationalDrivingLicenseApplications.CurrentRow.Cells[2].Value;
            int personID = clsDriver.FindDriverByDriverID(driverID).PersonID;

            frmPersonDetails frm = new frmPersonDetails(personID);
            frm .ShowDialog();
        }

        private void mnuShowLicenseDetails_Click(object sender, EventArgs e)
        {
            int internationalLicenseID = (int)dgvInternationalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmShowInternationalDriverInfo frm = new frmShowInternationalDriverInfo(internationalLicenseID);
            frm.ShowDialog();
        }

        private void mnuShowPesonLicenseHistory_Click(object sender, EventArgs e)
        {
            int driverID = (int)dgvInternationalDrivingLicenseApplications.CurrentRow.Cells[2].Value;
            int personID = clsDriver.FindDriverByDriverID(driverID).PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(personID);
            frm .ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
