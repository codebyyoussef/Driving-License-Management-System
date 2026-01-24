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
    public partial class frmLicenseHistory : Form
    {
        private int _personID = -1;
        public frmLicenseHistory(int personID)
        {
            InitializeComponent();
            _personID = personID;
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter.LoadPersonInfo(_personID);
            if (ctrlPersonCardWithFilter.PersonID != -1)
            {
                ctrlPersonCardWithFilter.FilterEnabled = false;
                ctrlDriverLicenses1.LoadDriverLocalLicenses(_personID);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
