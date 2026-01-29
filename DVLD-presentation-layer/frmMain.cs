using DVLD_Business;
using DVLD.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD.Login;
using DVLD.Tests;
using DVLD.Applications.Local_Driving_License_Applications;
using DVLD.Driver;
using DVLD.Applications.International_Driving_License;
using DVLD.License.International_Licenses;
using DVLD.Applications.Renew_Driving_license;
using DVLD.Applications.ReplaceLostOrDamagedLicense;

namespace DVLD
{
    public partial class frmMain : Form
    {
        private frmLogin _frmLogin;
        public frmMain(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;
        }

        private frmManagePeople _frmManagePeople;
        private frmManageUsers _frmManageUsers;

        private void mnuPeople_Click(object sender, EventArgs e)
        {
            if (_frmManagePeople == null || _frmManagePeople.IsDisposed)
            {
                _frmManagePeople = new frmManagePeople();
            }
            _frmManagePeople.MdiParent = this;
            _frmManagePeople.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MdiClient client = this.Controls.OfType<MdiClient>().FirstOrDefault();
            // this.Controls → Refers to all the controls contained inside your current form(the MDI parent form).
            // .OfType<MdiClient>() → Filters those controls and only returns the ones of type MdiClient.
            // MdiClient is the special hidden control that represents the background area of an MDI parent form where child forms are displayed.
            // .FirstOrDefault() → Takes the first MdiClient it finds, or returns null if none exist.
            // Result: You now have a reference(client) to the MDI client area of your parent form.

            if (client != null)
            {
                client.BackColor = ColorTranslator.FromHtml("#F5F7FA"); ; // sets the MDI client background
            }

        }

        private void mnuUsers_Click(object sender, EventArgs e)
        {
            if (_frmManageUsers == null || _frmManageUsers.IsDisposed)
            {
                _frmManageUsers = new frmManageUsers();
            }
            _frmManageUsers.MdiParent = this;
            _frmManageUsers.Show();
        }

        private void mnuSignOut_Click(object sender, EventArgs e)
        {
            clsGlobal.currentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void mnuCurrentUserInfo_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.currentUser.UserID);
            frm.ShowDialog();
        }

        private void mnuChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.currentUser.UserID);
            frm.Show();
        }

        private void mnuManageApplicationTypes_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frm = new frmManageApplicationTypes();
            frm.ShowDialog();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _frmLogin.Close();
        }

        private void mnuManageTestTypes_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frm = new frmManageTestTypes();
            frm.ShowDialog();
        }

        private void mnuLocalLicense_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalLicenseApplication frm = new frmAddUpdateLocalLicenseApplication();
            frm.ShowDialog();
        }

        private void mnuLocalDrivingLicenseApplications_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frm = new frmLocalDrivingLicenseApplications();
            frm.Show();
        }

        private void mnuRetakeTest_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frm = new frmLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void mnuDrivers_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void mnuInternationalLicense_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalDrivingLicenseApplication frm = new frmAddNewInternationalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void mnuInternationalDrivingLicenseApplications_Click(object sender, EventArgs e)
        {
            frmListInternationalLicesnseApplications frm = new frmListInternationalLicesnseApplications();
            frm.ShowDialog();
        }

        private void mnuRenewDrivingLicense_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void mnuReplacementForLostOrDamagedLicense_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }
    }
}
