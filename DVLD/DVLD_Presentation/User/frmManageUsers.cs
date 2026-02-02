using DVLD_Business;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD.Users
{
    public partial class frmManageUsers : Form
    {
        private enum enIsActive { All = 0, Yes = 1, No = 2 }

        private int _userID = -1;

        private static DataTable _dtAllUsers;

        public frmManageUsers()
        {
            InitializeComponent();
        } 

        private void _CenterFormWithFixedSize()
        {
            this.Size = new Size(702, 561);
            int x = (this.Parent.Width - this.Width) / 2;
            int y = (this.Parent.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        private void frmManagePeople_Move(object sender, EventArgs e)
        {
            _CenterFormWithFixedSize();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _CenterFormWithFixedSize();

            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            cbFilters.SelectedIndex = 0;
            lblUsersCount.Text = dgvUsers.Rows.Count.ToString();

            if (dgvUsers.Rows.Count > 0 )
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 90;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 90;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 200;

                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[3].Width = 100;

                dgvUsers.Columns[4].Width = 80;

                dgvUsers.Columns[5].HeaderText = "Is Active";
            }
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUserInfo frm = new frmAddEditUserInfo();
            frm.ShowDialog();
            frmManageUsers_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (txtFilterValue.Text.Trim() == "" || cbFilters.SelectedItem.ToString() == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblUsersCount.Text = dgvUsers.Rows.Count.ToString();
            }
            else
            {
                string selectedFilter = cbFilters.SelectedItem.ToString();
                string filterColumn = cbFilters.SelectedItem.ToString().Replace(" ", "");
                string filterValue = txtFilterValue.Text.Trim();

                if (selectedFilter == "User ID" || selectedFilter == "Person ID")
                {
                    _dtAllUsers.DefaultView.RowFilter = string.Format("{0} = {1}", filterColumn, filterValue);
                }
                else
                {
                    _dtAllUsers.DefaultView.RowFilter = string.Format("{0} like '{1}%'", filterColumn, filterValue);
                }

                lblUsersCount.Text = dgvUsers.Rows.Count.ToString();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedFilter = cbFilters.SelectedItem.ToString();
            if (selectedFilter == "User ID" || selectedFilter == "Person ID")
            {
                // Allow control keys (like Backspace)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Block non-numeric input
                }
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterColumn = "IsActive";
            string filterValue = cbIsActive.Text;

            switch (filterValue)
            {
                case "All":
                    break;
                case "Yes":
                    filterValue = "1";
                    break;
                case "No":
                    filterValue = "0";
                    break;
            }

            if (filterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("{0} = {1}", filterColumn, filterValue);

            lblUsersCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilters.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                if (cbIsActive.Visible)
                {
                    cbIsActive.Visible = false;
                }

                txtFilterValue.Visible = cbFilters.Text != "None";
                if (txtFilterValue.Visible)
                {
                    txtFilterValue.Visible = true;
                    txtFilterValue.Clear();
                    txtFilterValue.Focus();
                }
            }
        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void tsmAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditUserInfo frm = new frmAddEditUserInfo();
            frm.ShowDialog();
            frmManageUsers_Load(null, null);
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            frmAddEditUserInfo frm = new frmAddEditUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManageUsers_Load(null, null);
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            if (MessageBox.Show($"Are you sure you want to delete user [{userID}]", "Confirm delete",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsUser.DeleteUser(userID))
                {
                    MessageBox.Show("The user record has been deleted successfully.", "Deletion Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmManageUsers_Load(null, null);
                }
                else
                {
                    MessageBox.Show("User did not delete.");
                }
            }
        }

        private void tsmChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void dgvUsers_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
