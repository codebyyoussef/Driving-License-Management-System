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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Users
{
    public partial class frmAddEditUserInfo : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        private int _userID = -1;
        private clsUser _user;

        public frmAddEditUserInfo()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddEditUserInfo(int userID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _userID = userID;
        }

        private void _LoadData()
        {
            _user = clsUser.FindByUserID(_userID);

            if (_user == null)
            {
                MessageBox.Show("This form will be closed because the  user with ID = " + _userID + " is not found.");
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_user.PersonID);

            lblUserID.Text = _user.UserID.ToString();
            txtUserName.Text = _user.UserName;
            txtPassword.Text = _user.Password;
            txtConfirmPassword.Text = txtPassword.Text;
            chkIsActive.Checked = _user.IsActive;
        }

        private void frmAddEditUserInfo_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                this.Text = "Add New User";
                lblTitle.Text = "Add New User";
                _user = new clsUser();

                tbLoginInfo.Enabled = false;
            }
            else
            {
                this.Text = "Update User";
                lblTitle.Text = "Update User";

                ctrlPersonCardWithFilter1.FilterEnabled = false;
                tbLoginInfo.Enabled = true;
                btnSave.Enabled = true;

                _LoadData();
            }
        }

        private void tcPersonalAndLoginInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcUserInfo.SelectedIndex == 1)
            {
                txtUserName.Focus();
            }
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            //_personID = obj;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.IsUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected person already has a user, choose anther one.", "Select anthor person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    tbLoginInfo.Enabled = true;
                    btnSave.Enabled = true;
                    tcUserInfo.SelectedTab = tbLoginInfo;
                }
            }
            else
            {
                MessageBox.Show(
                                "Please select a person before proceeding to the next step.",
                                "Selection Required",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                );
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (txtUserName.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider.SetError(txtUserName, "UserName is required.");
            }
            else
            {
                errorProvider.SetError(txtUserName, null);
            }

            if (_Mode == enMode.AddNew)
            {
                if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtUserName, "This username is already taken. Please choose a different one.");
                }
                else
                {
                    errorProvider.SetError(txtUserName, null);
                }
            }
            else
            {
                if (_user.UserName !=  txtUserName.Text.Trim())
                {
                    if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider.SetError(txtUserName, "This username is already taken. Please choose a different one.");
                    }
                    else
                    {
                        errorProvider.SetError(txtUserName, null);
                    }
                }
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider.SetError(txtPassword, "Password is required.");
            }
            else
            {
                errorProvider.SetError(txtPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider.SetError(txtConfirmPassword, "Password confirmation does not match the password.");
            }
            else
            {
                errorProvider.SetError(txtConfirmPassword, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some required fields are missing. Please complete all highlighted fields before submitting.", 
                                "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _user.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _user.UserName = txtUserName.Text;
            _user.Password = txtPassword.Text;
            _user.IsActive = chkIsActive.Checked;

            if (_user.Save())
            {
                lblUserID.Text = _user.UserID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update User";
                this.Text = lblTitle.Text;

                MessageBox.Show("The user’s data has been saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The user’s data could not be saved. Please try again.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
