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
using DVLD_Business;
using DVLD_Business;
using DVLD.Tests.TestTypes;

namespace DVLD.Tests
{
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            DataTable dtTestTypes = clsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = dtTestTypes;
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();

            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 120;

                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 300;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 88;
            }
        }

        private void mnuEditTestType_Click(object sender, EventArgs e)
        {
            frmUpdateTestType frm = new frmUpdateTestType((clsTestType.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManageTestTypes_Load(null, null);
        }
    }
}
