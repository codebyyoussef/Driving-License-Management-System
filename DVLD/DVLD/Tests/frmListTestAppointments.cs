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

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {
        private int _localDrivingLicenseApplicationID = -1;
        private clsTestType.enTestType _testTypeID;
        private DataTable _dtTestAppointments;

        public frmListTestAppointments(int localDrivingLicenseApplicationID, clsTestType.enTestType testTypeID)
        {
            InitializeComponent();
            _localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _testTypeID = testTypeID;
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _SetFormTitleAndImage();

            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingApplicationID(_localDrivingLicenseApplicationID);
            _dtTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_localDrivingLicenseApplicationID, _testTypeID);
            dgvAppointments.DataSource = _dtTestAppointments;
            lblRecordsCount.Text = dgvAppointments.Rows.Count.ToString();

            if (dgvAppointments.Rows.Count > 0)
            {
                dgvAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvAppointments.Columns[0].Width = 120;

                dgvAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvAppointments.Columns[1].Width = 120;

                dgvAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvAppointments.Columns[2].Width = 80;

                dgvAppointments.Columns[3].HeaderText = "Is Locked";
                dgvAppointments.Columns[3].Width = 80;
            }
        }

        private void _SetFormTitleAndImage()
        {
            switch (_testTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblTitle.Text = "Vision Test Appointments";
                    pbTest.Image = Properties.Resources.VisionTest;
                    break;
                case clsTestType.enTestType.WrittenTest:
                    lblTitle.Text = "Written Test Appointments";
                    pbTest.Image = Properties.Resources.WrittenTest;
                    break;
                case clsTestType.enTestType.StreetTest:
                    lblTitle.Text = "Street Test Appointments";
                    pbTest.Image = Properties.Resources.StreetTest;
                    break;
            }
        }

        private void btnAddTestAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_localDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_testTypeID))
            {
                MessageBox.Show("Person already have an active appointment for this test, you cannot add new appointment.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            clsTest lastTest = localDrivingLicenseApplication.GetLastTestPerTestType(_testTypeID);

            if (lastTest == null)
            {
                frmScheduleTest frm1 = new frmScheduleTest(_localDrivingLicenseApplicationID, _testTypeID);
                frm1.ShowDialog();
                frmListTestAppointments_Load(null, null);
                return;
            }

            if (lastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm2 = new frmScheduleTest(_localDrivingLicenseApplicationID, _testTypeID);
            frm2.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void mnuEdit_Click(object sender, EventArgs e)
        {
            int testAppointmentID = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            //if ()
            //{
            //    MessageBox.Show("This test application cannot be edited because the test has already been taken.", "Edit Not Allowed",
            //                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            frmScheduleTest frm = new frmScheduleTest(_localDrivingLicenseApplicationID, _testTypeID, testAppointmentID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void mnuTakeTest_Click(object sender, EventArgs e)
        {
            int testAppointmentID = (int)dgvAppointments.CurrentRow.Cells[0].Value;

            if (clsTest.PassedAllTests(_localDrivingLicenseApplicationID))
            {
                MessageBox.Show("You can not take this test because the test has already been taken.", "Take Test Not Allowed",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmTakeTest frm = new frmTakeTest(testAppointmentID, _testTypeID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
