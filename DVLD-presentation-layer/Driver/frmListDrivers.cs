using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Driver
{
    public partial class frmListDrivers : Form
    {
        private DataTable _dtDrivers;
        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _dtDrivers = clsDriver.GetAllDrivers();
            cbFilters.SelectedIndex = 0;
            dgvDrivers.DataSource = _dtDrivers;
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();

            if (_dtDrivers.Rows.Count > 0 )
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[2].HeaderText = "National No";
                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
            }
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilters.Text != "None";
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Focus();
                txtFilterValue.SelectAll();
            }
            else
            {
                _dtDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtDrivers.Rows.Count.ToString();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedFilter = cbFilters.SelectedItem.ToString();
            if (selectedFilter == "Driver ID" || selectedFilter == "Person ID")
            {
                // Allow control keys (like Backspace)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Block non-numeric input
                }
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (txtFilterValue.Text.Trim() == "")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
            }
            else
            {
                string selectedFilter = cbFilters.SelectedItem.ToString();
                string filterValue = txtFilterValue.Text.Trim();
                string filterColumn = "";

                switch (selectedFilter)
                {
                    case "Driver ID":
                        filterColumn = "DriverID";
                        break;
                    case "Person ID":
                        filterColumn = "PersonID";
                        break;
                    case "National No":
                        filterColumn = "NationalNo";
                        break;
                    case "Full Name":
                        filterColumn = "FullName";
                        break;
                }

                if (selectedFilter == "Driver ID" || selectedFilter == "Person ID")
                {
                    _dtDrivers.DefaultView.RowFilter = string.Format("{0} = {1}", filterColumn, filterValue);
                }
                else
                {
                    _dtDrivers.DefaultView.RowFilter = string.Format("{0} like '{1}%'", filterColumn, filterValue);
                }

                lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
            }
        }


    }
}
