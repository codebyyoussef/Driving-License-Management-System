using BusinessLogicLayer;
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

namespace DVLD.License.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _localDrinvingLicenseApplicationID = -1;
        private int _driverID = -1;
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        public void LoadDriverLicenses(int personID)
        {
            _driverID = clsDriver.FindDriverByPersonID(personID).DriverID;

            if (_driverID != -1)
            {
                _LoadDriverLocalLicenses();
                _LoadDriverInternationalLicenses();
            }
        }

        private void _LoadDriverLocalLicenses()
        {
            dgvLocalLicenses.DataSource = clsLicense.GetDriverLicenses(_driverID);

            if (dgvLocalLicenses.Rows.Count > 0)
            {
                lblLocalLicenseRecordsCount.Text = dgvLocalLicenses.Rows.Count.ToString();

                dgvLocalLicenses.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicenses.Columns[0].Width = 100;
                dgvLocalLicenses.Columns[1].HeaderText = "App.ID";
                dgvLocalLicenses.Columns[1].Width = 100;
                dgvLocalLicenses.Columns[2].HeaderText = "Class Name";
                dgvLocalLicenses.Columns[2].Width = 200;
                dgvLocalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicenses.Columns[3].Width = 110;
                dgvLocalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicenses.Columns[4].Width = 110;
                dgvLocalLicenses.Columns[5].HeaderText = "Is Active";
                dgvLocalLicenses.Columns[5].Width = 90;
            }
        }

        private void _LoadDriverInternationalLicenses()
        {

            dgvInternationalLicenses.DataSource = clsInternationalLicense.GetDriverInternationalLicenses(_driverID);

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                lblInternationalLicenseRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();

                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 100;
                dgvInternationalLicenses.Columns[1].HeaderText = "Appllication.ID";
                dgvInternationalLicenses.Columns[1].Width = 100;
                dgvInternationalLicenses.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[2].Width = 100;
                dgvInternationalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[3].Width = 120;
                dgvInternationalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[4].Width = 120;
                dgvInternationalLicenses.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[5].Width = 90;
            }
        }

        private void mnuShowLicenseInfo_Click(object sender, EventArgs e)
        {
            frmDriverLicenseInfo frm = new frmDriverLicenseInfo((int)dgvLocalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
