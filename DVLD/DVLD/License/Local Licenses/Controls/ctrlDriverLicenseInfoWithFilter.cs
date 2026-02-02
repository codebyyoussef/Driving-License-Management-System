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

namespace DVLD.License.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int obj)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(obj);
            }
        }

        private bool _filterEnabled = true;
        public bool FilterEnabled
        {
            get { return _filterEnabled; }
            set
            {
                _filterEnabled = value;
                gbFilter.Enabled = _filterEnabled;
            }
        }

        private int _licenseID = -1;
        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; }
        }

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public void TextBoxLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }

        public void LoadLicenseInfo(int licenseID)
        {
            txtLicenseID.Text = licenseID.ToString();
            if (int.TryParse(txtLicenseID.Text, out _licenseID))
            {
                ctrlDriverLicenseInfo1.LoadDriverLicenseInfo(_licenseID);
                if (ctrlDriverLicenseInfo1.LicenseID != -1)
                {
                    if (OnLicenseSelected != null && _filterEnabled)
                    {
                        LicenseSelected(_licenseID);
                    }
                }
            }
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);

            // if the user press "ENTER"
            if (e.KeyChar == 13) 
            {
                btnFind.PerformClick();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtLicenseID.Text, out _licenseID))
                LoadLicenseInfo(_licenseID);
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "License ID is required. Please enter it to continue.");
            }
            else
            {
                errorProvider1.Clear();
            }

        }
    }
}
