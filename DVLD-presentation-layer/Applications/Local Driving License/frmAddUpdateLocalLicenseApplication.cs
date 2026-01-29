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
using DVLD.Global_Classes;
using System.Runtime.InteropServices;

namespace DVLD.Applications.Local_Driving_License_Applications
{
    public partial class frmAddUpdateLocalLicenseApplication : Form
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;


        private int _localDrivingLicenseApplicationID = -1;
        private int _selectedPersonID = -1;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;

        public frmAddUpdateLocalLicenseApplication()
        {
            InitializeComponent();
        }

        public frmAddUpdateLocalLicenseApplication(int localDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _mode = enMode.Update;
        }

        private void frmNewLocalLicenseApplication_Load(object sender, EventArgs e)
        {
            if (_mode == enMode.AddNew)
            {
                this.Text = "New Local Driving License Application";
                lblTitle.Text = "New Local Driving License Application";
                tbApplicationInfo.Enabled = false;
                _localDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
            }
            else
            {
                this.Text = "Update Local Driving License Application";
                lblTitle.Text = "Update Local Driving License Application";
                ctrlPersonCardWithFilter1.FilterEnabled = false;

                _LoadData();

                tbApplicationInfo.Enabled = true;
            }
        }

        private void _LoadData()
        {
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_localDrivingLicenseApplicationID);

            if (_localDrivingLicenseApplication == null )
            {
                MessageBox.Show("No application with ID = " + _localDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_localDrivingLicenseApplication.ApplicantPersonID);

            lblApplicationID.Text = _localDrivingLicenseApplication.ApplicationID.ToString();
            lblApplicationDate.Text = _localDrivingLicenseApplication.ApplicationDate.ToShortDateString();
            cbLicenseClasses.SelectedIndex = _localDrivingLicenseApplication.LicenseClassID - 1;
            lblApplicationFees.Text = _localDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedBy.Text = _localDrivingLicenseApplication.CreatedByUserInfo.UserName;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                tbApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
                tcApplicationInfo.SelectedTab = tbApplicationInfo;

                _SetInitialApplicationInfo();
            }
            else
            {
                MessageBox.Show("Please select a person before proceeding to the next step.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void _SetInitialApplicationInfo()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            _FillComboBoxWithLicenseClasses();
            cbLicenseClasses.SelectedIndex = 2;
            lblApplicationFees.Text = Convert.ToDecimal(clsApplicationType.Find((byte)clsApplication.enApplicationType.NewLocalDrivingLicense).Fees).ToString();
            lblCreatedBy.Text = clsGlobal.currentUser.UserName;
        }

        private void _FillComboBoxWithLicenseClasses()
        {
            DataTable _dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in _dtLicenseClasses.Rows)
            {
                cbLicenseClasses.Items.Add(row["ClassName"]);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int selectedPersonID = ctrlPersonCardWithFilter1.PersonID;
            byte selectedLicenseClassID = (byte)clsLicenseClass.Find(cbLicenseClasses.Text).LicenseClassID;

            int activeApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(selectedPersonID, clsApplication.enApplicationType.NewLocalDrivingLicense, selectedLicenseClassID);

            if (activeApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected person already have an active application for the selected class with ID = " + activeApplicationID,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClasses.Focus();
                return;
            }

            //check if user already have issued license of the same driving  class.
            if (clsLicense.IsLicenseExistByPersonID(ctrlPersonCardWithFilter1.PersonID, selectedLicenseClassID))
            {

                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Set base application info

            _localDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _localDrivingLicenseApplication.ApplicantPersonInfo = clsPerson.Find(_localDrivingLicenseApplication.ApplicantPersonID);
            _localDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _localDrivingLicenseApplication.ApplicationTypeID = (byte)clsApplication.enApplicationType.NewLocalDrivingLicense;
            _localDrivingLicenseApplication.ApplicationTypeInfo = clsApplicationType.Find(_localDrivingLicenseApplication.ApplicationTypeID);
            _localDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _localDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _localDrivingLicenseApplication.PaidFees = Convert.ToDecimal(lblApplicationFees.Text);
            _localDrivingLicenseApplication.CreatedByUserID = clsGlobal.currentUser.UserID;
            _localDrivingLicenseApplication.CreatedByUserInfo = clsUser.FindByUserID(_localDrivingLicenseApplication.CreatedByUserID);

            // Set local application info
            _localDrivingLicenseApplication.LicenseClassID = (byte)(cbLicenseClasses.SelectedIndex + 1);


            if (_localDrivingLicenseApplication.Save())
            {
                lblApplicationID.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                _mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("The data could not be saved. Please try again.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _selectedPersonID = obj;
        }

        private void frmAddUpdateLocalLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}



