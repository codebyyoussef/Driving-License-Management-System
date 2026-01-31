using DVLD.Applications.Release_License;
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

namespace DVLD.Applications.Detain_License
{
    public partial class frmListDetainedLicenses : Form
    {
        private DataTable _dtDetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;
            cbFilters.SelectedIndex = 0;
            lblRecordsCount.Text = dgvDetainedLicenses.Rows.Count.ToString();

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[6].HeaderText = "N.No";
                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[8].HeaderText = "Release App.ID";
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            mnuReleaseDetainedLicense.Enabled = (bool)dgvDetainedLicenses.CurrentRow.Cells[3].Value == false;
        }

        private void mnuShowPersonDetails_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(dgvDetainedLicenses.CurrentRow.Cells[6].Value.ToString());
            frm.ShowDialog();
        }

        private void mnuShowLicenseDetails_Click(object sender, EventArgs e)
        {
            frmDriverLicenseInfo frm = new frmDriverLicenseInfo((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void mnuShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int personID = clsPerson.Find(dgvDetainedLicenses.CurrentRow.Cells[6].Value.ToString()).PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(personID);
            frm.ShowDialog();
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dtDetainedLicenses.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvDetainedLicenses.Rows.Count.ToString();

            if (cbFilters.Text == "None")
            {
                if (txtFilterValue.Visible)
                    txtFilterValue.Visible = false;
                else
                    cbIsReleased.Visible = false;
            }
            else
            {
                if (cbFilters.Text == "Is Released")
                {
                    if (txtFilterValue.Visible)
                    {
                        txtFilterValue.Visible = false;
                    }
                    cbIsReleased.Visible = true;
                    cbIsReleased.SelectedIndex = 0;
                }
                else
                {
                    if (cbIsReleased.Visible)
                    {
                        cbIsReleased.Visible = false;
                    }
                    txtFilterValue.Visible = true;
                    txtFilterValue.Clear();
                    txtFilterValue.Focus();
                }
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string selectedFilter = cbFilters.SelectedItem.ToString();
            string filterColumn = "";
            string filterValue = txtFilterValue.Text.Trim();

            switch (selectedFilter)
            {
                case "Detain ID":
                    filterColumn = "DetainID";
                    break;
                case "National No":
                    filterColumn = "NationalNo";
                    break;
                case "Full Name":
                    filterColumn = "FullName";
                    break;
                case "Release Application ID":
                    filterColumn = "ReleaseApplicationID";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDetainedLicenses.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "DetainID" || filterColumn == "ReleaseApplicationID")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("{0} = {1}", filterColumn, filterValue);
            }
            else
            {
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("{0} like '{1}%'", filterColumn, filterValue);
            }

            lblRecordsCount.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.Text == "Detain ID" || cbFilters.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterColumn = "IsReleased";
            string filterValue = cbIsReleased.Text;

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
                _dtDetainedLicenses.DefaultView.RowFilter = "";
            else
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("{0} = {1}", filterColumn, filterValue);

            lblRecordsCount.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void mnuReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }
    }
}
