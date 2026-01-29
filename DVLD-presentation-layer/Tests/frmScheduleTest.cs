using DVLD.Global_Classes;
using DVLD.Tests.Controls;
using DVLD_Business;
using Microsoft.SqlServer.Server;
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
    public partial class frmScheduleTest : Form
    {
        private int _localDrivingLicenseApplicationID = -1;
        private clsTestType.enTestType _testTypeID = clsTestType.enTestType.VisionTest;
        private int _testAppointmentID = -1;
        public frmScheduleTest(int localDrivingLicenseApplicationID, clsTestType.enTestType testTypeID, int testAppointmentID = -1)
        {
            InitializeComponent();
            _localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _testTypeID = testTypeID;
            _testAppointmentID = testAppointmentID;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestTypeID = _testTypeID;
            ctrlScheduleTest1.LoadInfo(_localDrivingLicenseApplicationID, _testAppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
