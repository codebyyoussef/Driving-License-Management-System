using DVLD.Controls;
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

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _mode = enMode.AddNew;

        private enum enCreationMode { FristTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _creationMode = enCreationMode.FristTimeSchedule;

        private clsTestType.enTestType _testTypeID = clsTestType.enTestType.VisionTest;

        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        private int _localDrivingLicenseApplicationID = -1;

        private clsTestAppointment _testAppointment;
        private int _testAppointmentID = -1;

        public clsTestType.enTestType TestTypeID
        {
            set
            {
                _testTypeID = value;
                switch (_testTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                        gbScheduleTestInfo.Text = "Vision Test";
                        pbTest.Image = Properties.Resources.VisionTest;
                        break;
                    case clsTestType.enTestType.WrittenTest:
                        gbScheduleTestInfo.Text = "Written Test";
                        pbTest.Image = Properties.Resources.WrittenTest;
                        break;
                    case clsTestType.enTestType.StreetTest:
                        gbScheduleTestInfo.Text = "Street Test";
                        pbTest.Image= Properties.Resources.StreetTest;
                        break;
                }
            }
            get
            {
                return _testTypeID;
            }
        }
        public clsTestType TestTypeInfo;

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int localDrivingLicenseApplicationID, int testAppointmentID = -1)
        {
            if (testAppointmentID == -1)
                _mode = enMode.AddNew;
            else
                _mode = enMode.Update;

            _localDrivingLicenseApplicationID = localDrivingLicenseApplicationID; ;
            _testAppointmentID = testAppointmentID;

            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_localDrivingLicenseApplicationID);

            if (_localDrivingLicenseApplication == null)
            {
                MessageBox.Show($"Error: no local driving license application with ID {_localDrivingLicenseApplicationID}", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }


            if (_localDrivingLicenseApplication.DoesAttendTestType(_testTypeID))
                _creationMode = enCreationMode.RetakeTestSchedule;
            else
                _creationMode = enCreationMode.FristTimeSchedule;


            if (_creationMode == enCreationMode.RetakeTestSchedule)
            {
                lblTitle.Text = "Schedule Retake Test";
                gbRetakeTestInfo.Enabled = true;
                decimal retakeTestApplicationFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees;
                lblRetakeApplicationFees.Text = retakeTestApplicationFees.ToString("0");
                decimal testTypeFees = clsTestType.Find(_testTypeID).Fees;
                lblTotalFees.Text = (testTypeFees + retakeTestApplicationFees).ToString("0");
            }
            else
            {
                lblTitle.Text = "Schedule Test";
                gbRetakeTestInfo.Enabled = false;
            }

            lblLocalDrivingLicenseApplicationID.Text = _localDrivingLicenseApplicationID.ToString();
            lblLicenseClassName.Text = _localDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblName.Text = _localDrivingLicenseApplication.ApplicantPersonInfo.FullName;

            lblTrial.Text = _localDrivingLicenseApplication.TotalTrialsPerTest(_testTypeID).ToString();

            if (_mode == enMode.AddNew)
            {
                dtpDate.MinDate = DateTime.Now;
                dtpDate.Value = DateTime.Now;
                lblFees.Text = clsTestType.Find(_testTypeID).Fees.ToString("0");
                _testAppointment = new clsTestAppointment();
            }
            else
            {
                if (!_LoadTestAppointmentIData())
                    return;
            }


            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandleAppointmentLockedConstraint())
                return;

            if (!_HandlePreviousTestConstraint())
                return;
        }

        private bool _LoadTestAppointmentIData()
        {
            _testAppointment = clsTestAppointment.Find(_testAppointmentID);

            if (_testAppointment == null)
            {
                MessageBox.Show($"Error: no test appointment with ID {_testAppointmentID}", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            if (DateTime.Compare(DateTime.Now, _testAppointment.AppointmentDate) < 0)
                dtpDate.MinDate = DateTime.Now;
            else
                dtpDate.MinDate = _testAppointment.AppointmentDate;

            dtpDate.Value = _testAppointment.AppointmentDate;
            lblFees.Text = _testAppointment.PaidFees.ToString("0");

            if (_testAppointment.RetakeTestApplicationID != -1)
            {
                decimal retakeTestApplicationFees = _testAppointment.RetakeTestApplicationInfo.PaidFees;
                decimal testTypeFees = _testAppointment.TestTypeInfo.Fees;

                gbRetakeTestInfo.Enabled = true;

                lblRetakeApplicationFees.Text = retakeTestApplicationFees.ToString("0");
                lblTotalFees.Text = testTypeFees.ToString("0");
                lblRetakeTestApplicationID.Text = _testAppointment.RetakeTestApplicationID.ToString();
            }

            return true;
        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (_mode == enMode.AddNew && _localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_testTypeID))
            {
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;
            }
            return true;
        }

        private bool _HandleAppointmentLockedConstraint()
        {
            if (_testAppointment.IsLocked)
            {
                lblUserMessage.Text = "Person already sat for this test, the appointment is locked";
                lblUserMessage.Enabled = true;
                dtpDate.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }
            else
            {
                lblUserMessage.Enabled = false;
            }
            return true;
        }

        private bool _HandlePreviousTestConstraint()
        {
            switch (_testTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblUserMessage.Visible = false;
                    return true;
                case clsTestType.enTestType.WrittenTest:
                    if (!_localDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMessage.Enabled = true;
                        dtpDate.Enabled = false;
                        btnSave.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        return true;
                    }
                case clsTestType.enTestType.StreetTest:
                    if (!_localDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMessage.Enabled = true;
                        dtpDate.Enabled = false;
                        btnSave.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        return true;
                    }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _testAppointment.TestTypeID = _testTypeID;
            _testAppointment.LocalDrivingLicenseApplicationID = _localDrivingLicenseApplicationID;
            _testAppointment.AppointmentDate = dtpDate.Value;
            _testAppointment.PaidFees = Convert.ToDecimal(lblFees.Text);
            _testAppointment.CreatedByUserID = clsGlobal.currentUser.UserID;

            if (_testAppointment.Save())
            {
                _mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private bool _HandleRetakeApplication()
        {
            //this will decide to create a seperate application for retake test or not.
            // and will create it if needed , then it will linkit to the appoinment.
            if (_mode == enMode.AddNew && _creationMode == enCreationMode.RetakeTestSchedule)
            {
                //First Create Applicaiton 
                clsApplication application = new clsApplication();
                application.ApplicantPersonID = _localDrivingLicenseApplication.ApplicantPersonID;
                application.ApplicationDate = DateTime.Now;
                application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                application.LastStatusDate = DateTime.Now;
                application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees;
                application.CreatedByUserID = clsGlobal.currentUser.UserID;

                if (!application.Save())
                {
                    _testAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _testAppointment.RetakeTestApplicationID = application.ApplicationID;
            }
            return true;
        }
    }
}
