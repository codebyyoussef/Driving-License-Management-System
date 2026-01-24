using DVLD.Controls;
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

namespace DVLD.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private int _applicationID = -1;
        private clsApplication _application;
        public int BaseApplicationID
        {
            get { return _applicationID; }
        }
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        private void _FillBaseApplicationInfo()
        {
            switch(_application.StatusText)
            {
                case "New":
                    pbStatus.Image = Properties.Resources.StatusCompeted; 
                    break;
                case "Cancelled":
                    pbStatus.Image = Properties.Resources.Cancel;
                    break;
                case "Completed":
                    pbStatus.Image = Properties.Resources.StatusCompeted;
                    break;
            }

            lblApplicationID.Text = _application.ApplicationID.ToString();
            lblStatus.Text = _application.StatusText;
            lblFees.Text = _application.PaidFees.ToString();
            lblType.Text = _application.ApplicationTypeInfo.Title;
            lblApplicant.Text = _application.ApplicantPersonInfo.FullName;
            lblDate.Text = _application.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = _application.LastStatusDate.ToShortDateString();
            lblCreatedBy.Text = _application.CreatedByUserInfo.UserName;
        }

        public void LoadBaseApplicationInfo(int baseApplicationID)
        {
            _application = clsApplication.FindBaseApplication(baseApplicationID);

            if (_application == null)
            {
                MessageBox.Show("No Base Application with ApplicationID = " + baseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _applicationID = baseApplicationID;
            _FillBaseApplicationInfo();
        }

        private void linkViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(_application.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
