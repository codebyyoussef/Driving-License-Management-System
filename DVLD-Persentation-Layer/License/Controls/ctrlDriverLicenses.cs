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
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        public void LoadDriverLocalLicenses(int personID)
        {
            int driverID = clsDriver.FindDriverByPersonID(personID).DriverID;

            if (driverID != -1)
            {
                dgvLocalLicenses.DataSource = clsLicense.GetDriverLicenses(driverID);

                if (dgvLocalLicenses.Rows.Count > 0)
                {
                    lblRecordsCount.Text = dgvLocalLicenses.Rows.Count.ToString();

                    dgvLocalLicenses.Columns[0].HeaderText = "Lic.ID";
                    dgvLocalLicenses.Columns[1].HeaderText = "App.ID";
                    dgvLocalLicenses.Columns[2].HeaderText = "Class Name";
                    dgvLocalLicenses.Columns[3].HeaderText = "Issue Date";
                    dgvLocalLicenses.Columns[4].HeaderText = "Expiration Date";
                    dgvLocalLicenses.Columns[5].HeaderText = "Is Active";
                }
            }
        }
    }
}
