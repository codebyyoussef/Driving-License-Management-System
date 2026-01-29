using DVLD.Global_Classes;
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

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {
        private int _testAppointmentID = -1;
        private clsTestAppointment _testAppointment;
        private clsTestType.enTestType _testTypeID;

        private int _testID = -1;
        private clsTest _test;
        public frmTakeTest(int testAppointmentID, clsTestType.enTestType testTypeID)
        {
            InitializeComponent();
            _testAppointmentID = testAppointmentID;
            _testTypeID = testTypeID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestTypeID = _testTypeID;
            ctrlScheduledTest1.LoadInfo(_testAppointmentID);

            if (ctrlScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

                _testID = ctrlScheduledTest1.TestID;
            if (_testID != -1)
            {
                _test = clsTest.Find(_testID);

                if (_test.TestResult == true)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = false;

                txtNotes.Text = _test.Notes;

                lblUserMessage.Visible = true;
                rbPass.Enabled = false;
                rbFail.Enabled = false;
            }
            else
                _test = new clsTest();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!rbPass.Checked && !rbFail.Checked)
            {
                MessageBox.Show("Please select Pass or Fail before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmation = MessageBox.Show("Note: You cannot change the Pass/Fail results after you save.\nAre you sure you want to save? \r\n", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                _test.TestAppointmentID = _testAppointmentID;
                _test.TestResult = rbPass.Checked;
                _test.Notes = txtNotes.Text.Trim();
                _test.CreatedByUserID = clsGlobal.currentUser.UserID;

                if (_test.Save())
                {
                    MessageBox.Show("Data saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("Save failed. Please try again", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
