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

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        private clsUser _user;
        private int _userID = -1;

        public frmChangePassword(int userID)
        {
            InitializeComponent();
            _userID = userID;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            this.ActiveControl = btnClose;
            _user = clsUser.FindByUserID(_userID);

            if (_user == null)
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Could not Find User with ID = " + _userID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlUserCard1.LoadUserInfo(_userID);
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current password cannot be blank.");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }

            if (txtCurrentPassword.Text != _user.Password)
            {
                errorProvider1.SetError(txtCurrentPassword, "Current password is wrong!");
                txtCurrentPassword.SelectAll();
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "New password cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }

            if (txtNewPassword.Text == txtCurrentPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "Your new password cannot be the same as your current password. Please choose a different one.");
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }

        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirmation Password cannot be blank.");
                return;
            }
            else
                errorProvider1.SetError(txtConfirmPassword, null);

            if (txtConfirmPassword.Text != txtNewPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password confirmation does not match the password.");
            }
            else
                errorProvider1.SetError(txtConfirmPassword, null);
        }

        private void _ResetDefaultValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _user.Password = txtNewPassword.Text;
            if (_user.Save())
            {
                MessageBox.Show("Password changed successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefaultValues();
            }
            else
            {
                MessageBox.Show("An Erro Occured, Password did not change.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmChangePassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.AutoValidate = AutoValidate.Disable;
        }

    }
}
