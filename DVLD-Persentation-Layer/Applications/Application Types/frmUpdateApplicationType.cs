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

namespace DVLD.Applications.Application_Types
{
    public partial class frmUpdateApplicationType : Form
    {
        private int _testTypeID = -1;
        private clsApplicationType _testType;

        public frmUpdateApplicationType(int applicationTypeID)
        {
            InitializeComponent();
            _testTypeID = applicationTypeID;
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _testType = clsApplicationType.Find(_testTypeID);

            if (_testType != null)
            {
                lblApplicationTypeID.Text = _testTypeID.ToString();
                txtTitle.Text = _testType.Title;
                txtFees.Text = _testType.Fees.ToString("G29");
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }

            bool isNumber = decimal.TryParse(txtFees.Text, out _);
            if (!isNumber)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            Decimal.TryParse(txtFees.Text, out decimal fees);

            if (title != _testType.Title || fees != _testType.Fees)
            {
                _testType.Title = title;
                _testType.Fees = fees;   

                if (_testType.Save())
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(
                                "No changes detected. Nothing to save.",
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                );
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
