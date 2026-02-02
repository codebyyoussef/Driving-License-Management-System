using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.License
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        private int _personID = -1;

        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
        }

        public frmShowPersonLicenseHistory(int personID)
        {
            InitializeComponent();
            _personID = personID;
        }

        private void frmShowPersonLicenseHistory_Activated(object sender, EventArgs e)
        {
            if (_personID == -1)
            {
                ctrlPersonCardWithFilter.FilterFocus();
            }
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_personID != -1)
            {
                ctrlPersonCardWithFilter.LoadPersonInfo(_personID);

                if (ctrlPersonCardWithFilter.PersonID != -1)
                {
                    ctrlPersonCardWithFilter.FilterEnabled = false;
                    ctrlDriverLicenses1.LoadDriverLicensesByPersonID(_personID);
                }
            }
        }

        private void ctrlPersonCardWithFilter_OnPersonSelected(int obj)
        {
            _personID = obj;
            if (_personID != -1)
            {
                ctrlDriverLicenses1.LoadDriverLicensesByPersonID(_personID);
            }
            else
            {
                ctrlDriverLicenses1.Clear();
                ctrlPersonCardWithFilter.SelectAll();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
