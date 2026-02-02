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
using DVLD.Global_Classes;


namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string userName = "", password = "";
            if (clsGlobal.GetStoredCredential(ref userName, ref password))
            {
                txtUserName.Text = userName;
                txtPassword.Text = password;
                chkRememberMe.Checked = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Please enter both username and password before logging in.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            clsUser user = clsUser.FindByUsernameAndPassword(txtUserName.Text, txtPassword.Text);

            if (user != null)
            {
                if (chkRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text, txtPassword.Text);
                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");
                }


                if (user.IsActive == false)
                {
                    MessageBox.Show("Your account is currently inactive. Please contact the system administrator for assistance.", "Account Inactive",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    clsGlobal.currentUser = user;
                    this.Hide();
                    frmMain frm = new frmMain(this);
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Invalid Username/Password.", "Wrong Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
