using BusinessLogicLayer;
using DVLD.License.International_Licenses;
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
        private clsDriver _driver;
        private DataTable _dtDriverLocalLisenseHistory;
        private DataTable _dtDriverInternationalLicenseHistory;
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        public void LoadDriverLicensesByDriverID(int driverID)
        {
            _driverID = driverID;
            _driver = clsDriver.FindDriverByDriverID(driverID);

            if (_driver == null)
            {
                MessageBox.Show("There is no driver with ID = " + _driverID, "Driver not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadDriverLocalLicenses();
            _LoadDriverInternationalLicenses();
        }

        public void LoadDriverLicensesByPersonID(int personID)
        {
            _driver = clsDriver.FindDriverByPersonID(personID);

            if (_driver == null)
            {
                MessageBox.Show("There is no driver with person ID = " + personID, "Person is not a driver", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _driverID = _driver.DriverID;

            _LoadDriverLocalLicenses();
            _LoadDriverInternationalLicenses();
        }

        private void _LoadDriverLocalLicenses()
        {
            _dtDriverLocalLisenseHistory = clsDriver.GetLocalLicenses(_driverID);
            dgvLocalLicensesHistory.DataSource = _dtDriverLocalLisenseHistory;
            lblLocalLicenseRecordsCount.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 100;
                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 100;
                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 200;
                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 110;
                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 110;
                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 90;
            }
        }

        private void _LoadDriverInternationalLicenses()
        {
            _dtDriverInternationalLicenseHistory = clsDriver.GetInternationalLicenses(_driverID);
            dgvInternationalLicenses.DataSource = _dtDriverInternationalLicenseHistory;
            lblInternationalLicenseRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
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
            frmDriverLicenseInfo frm = new frmDriverLicenseInfo((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void mnuShowInternationalLicenseInfo_Click(object sender, EventArgs e)
        {
            frmShowInternationalDriverInfo frm = new frmShowInternationalDriverInfo((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        public void Clear()
        {
            _dtDriverLocalLisenseHistory.Clear();
            lblLocalLicenseRecordsCount.Text = "??";
            _dtDriverInternationalLicenseHistory.Clear();
            lblInternationalLicenseRecordsCount.Text = "??";
        }


    }
}
