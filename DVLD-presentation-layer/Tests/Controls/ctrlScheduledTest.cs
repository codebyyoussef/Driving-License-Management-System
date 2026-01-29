using DVLD.Properties;
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
using static DVLD_Business.clsTestType;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
        private clsTestType.enTestType _testTypeID;
        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _testTypeID;
            }
            set
            {
                _testTypeID = value;

                switch (_testTypeID)
                {

                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestInfo.Text = "Vision Test";
                            pbTest.Image = Resources.VisionTest;
                            break;
                        }

                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestInfo.Text = "Written Test";
                            pbTest.Image = Resources.WrittenTest;
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestInfo.Text = "Street Test";
                            pbTest.Image = Resources.StreetTest;
                            break;
                        }
                }
            }
        }

        private int _testID = -1;
        public int TestID
        {
            get
            {
                return _testID;
            }
        }

        private clsTestAppointment _testAppointment;
        private int _testAppointmentID = -1;
        public int TestAppointmentID
        {
            get
            {
                return _testAppointmentID;
            }
        }
       
        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int testAppointmentID)
        {
            _testAppointmentID = testAppointmentID;
            _testAppointment = clsTestAppointment.Find(_testAppointmentID);

            if (_testAppointment != null)
            {
                _testTypeID = _testAppointment.TestTypeID;
                _testID = _testAppointment.TestID;

                lblLocalDrivingLicenseApplicationID.Text = _testAppointment.LocalDrivingLicenseApplicationID.ToString();
                lblLicenseClassName.Text = _testAppointment.LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
                lblName.Text = _testAppointment.LocalDrivingLicenseApplicationInfo.ApplicantPersonInfo.FullName;
                lblTrial.Text = _testAppointment.LocalDrivingLicenseApplicationInfo.TotalTrialsPerTest(_testTypeID).ToString();
                lblDate.Text = _testAppointment.AppointmentDate.ToShortDateString();
                lblFees.Text = _testAppointment.PaidFees.ToString("0");
                lblTestID.Text = (_testAppointment.TestID == -1) ? "Not Taken Yet" : _testAppointment.TestID.ToString();
            }
            else
            {
                MessageBox.Show("Error: No  Appointment ID = " + _testAppointmentID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _testAppointmentID = -1;
            }
        }
    }
}
