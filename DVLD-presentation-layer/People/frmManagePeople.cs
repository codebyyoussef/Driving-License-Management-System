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
using DVLD_Business;

namespace DVLD
{
    public partial class frmManagePeople : Form
    {
        private static DataTable _dtAllPeople = clsPerson.GetAllPersons();

        // Only select the columns that you want to show in the grid
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName", 
                                                                              "GendorCaption", "DateOfBirth", "CountryName", "Phone", "Email");
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void _CenterFormWithFixedSize()
        {
            this.Size = new Size(1062, 585);
            int x = (this.Parent.Width - this.Width) / 2;
            int y = (this.Parent.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        private void frmManagePeople_Move(object sender, EventArgs e)
        {
            _CenterFormWithFixedSize();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _CenterFormWithFixedSize();

            cbFilters.SelectedIndex = 0;
            dgvPeopleList.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();

            if (dgvPeopleList.Rows.Count > 0)
            {
                dgvPeopleList.Columns[0].HeaderText = "Person ID";
                dgvPeopleList.Columns[0].Width = 60;

                dgvPeopleList.Columns[1].HeaderText = "National No";
                dgvPeopleList.Columns[1].Width = 70;

                dgvPeopleList.Columns[2].HeaderText = "First Name";
                dgvPeopleList.Columns[2].Width = 90;

                dgvPeopleList.Columns[3].HeaderText = "Second Name";
                dgvPeopleList.Columns[3].Width = 100;

                dgvPeopleList.Columns[4].HeaderText = "Third Name";
                dgvPeopleList.Columns[4].Width = 90;

                dgvPeopleList.Columns[5].HeaderText = "Last Name";
                dgvPeopleList.Columns[5].Width = 90;

                dgvPeopleList.Columns[6].HeaderText = "Gendor";
                dgvPeopleList.Columns[6].Width = 50;

                dgvPeopleList.Columns[7].HeaderText = "Date Of Birth";
                dgvPeopleList.Columns[7].Width = 95;

                dgvPeopleList.Columns[8].HeaderText = "Country";
                dgvPeopleList.Columns[8].Width = 60;

                dgvPeopleList.Columns[9].HeaderText = "Phone";
                dgvPeopleList.Columns[9].Width = 100;

                dgvPeopleList.Columns[10].HeaderText = "Email";
                dgvPeopleList.Columns[10].Width = 100;
            }

            //_RefreshPeopleList();
        }

        private void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetAllPersons();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName",
                                                                              "GendorCaption", "DateOfBirth", "CountryName", "Phone", "Email");
            dgvPeopleList.DataSource = _dtPeople;
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilters.Text != "None";
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Visible = true;
                txtFilterValue.Clear();
                txtFilterValue.Focus();
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilterValue.Text.Trim() == "" || cbFilters.SelectedItem.ToString() == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();
            }
            else
            {
                string selectedFilter = cbFilters.SelectedItem.ToString();
                string filterColumn = "";
                string filterValue = txtFilterValue.Text.Trim();

                if (selectedFilter == "Gendor")
                {
                    filterColumn = "GendorCaption";
                }
                else
                {
                    filterColumn = cbFilters.SelectedItem.ToString().Replace(" ", "");
                }
                
                if (selectedFilter == "Person ID")
                {
                    _dtPeople.DefaultView.RowFilter = string.Format("{0} = {1}", filterColumn, filterValue);
                }
                else
                {
                    _dtPeople.DefaultView.RowFilter = string.Format("{0} like '{1}%'", filterColumn, filterValue);
                }

                lblRecordsCount.Text = dgvPeopleList.Rows.Count.ToString();
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.SelectedIndex == 1)
            {
                // Allow control keys (like Backspace)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Block non-numeric input
                }
            }
        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dgvPeopleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void tsmAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(-1);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo((int)dgvPeopleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvPeopleList.CurrentRow.Cells[0].Value;
            if (MessageBox.Show($"Are you sure you want to delete person [{ID}]", "Confirm delete",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson(ID))
                {
                    MessageBox.Show("The person record has been deleted successfully.", "Deletion Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();
                }
                else
                {
                    MessageBox.Show("Person did not delete.");
                }
            }
        }

        private void tsmSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet!", "Not ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsmPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet!", "Not ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
