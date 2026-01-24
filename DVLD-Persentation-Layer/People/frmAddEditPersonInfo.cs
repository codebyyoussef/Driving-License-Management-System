using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLD_Business;
using DVLD.Properties;

namespace DVLD
{
    public partial class frmAddEditPersonInfo : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode;

        public enum enGendor { Male = 0, Female = 1 };

        private clsPerson _person;
        private int _personID;

        public delegate void DataBackEventHandler(object sender, int personID);

        public event DataBackEventHandler DataBack;

        public frmAddEditPersonInfo()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }

        public frmAddEditPersonInfo(int personID)
        {
            InitializeComponent();

            Mode = enMode.Update;
            _personID = personID;
        }

        private void _FillComboBoxWithCountries()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        }

        private void _ResetDefaultValues()
        {
            _FillComboBoxWithCountries();

            if (Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            txtNationalNo.Text = "";
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";

            if (rbMale.Checked)
                pbImage.Image = Resources.Male;
            else
                pbImage.Image = Resources.Female;

            llRemoveImage.Visible = pbImage.ImageLocation != null;

            // Restrict the DateTimePicker so the minimum selectable date is 18 years ago (enforcing age requirement)
            dtpDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Today.AddYears(-100);

            cbCountries.SelectedIndex = cbCountries.FindString("Morocco");
        }

        private void _LoadData()
        {
            _person = clsPerson.Find(_personID);

            if (_person == null)
            {
                MessageBox.Show("This form will be closed because the  person with ID = " + _person + " is not found.");
                this.Close();
                return;
            }

            lblPersonID.Text = _personID.ToString();
            txtNationalNo.Text = _person.NationalNo;
            txtFirstName.Text = _person.FirstName;
            txtSecondName.Text = _person.SecondName;
            txtThirdName.Text = _person.ThirdName;
            txtLastName.Text = _person.LastName;
            dtpDateOfBirth.Value = _person.DateOfBirth;
            if (_person.Gendor == 0)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }
            txtEmail.Text = _person.Email;
            txtPhone.Text = _person.Phone;
            txtAddress.Text = _person.Address;
            cbCountries.SelectedIndex = cbCountries.FindString(clsCountry.Find(_person.NationalityCountryID).Name);


            if (_person.ImagePath != "")
            {
                if (File.Exists(_person.ImagePath))
                {
                    pbImage.ImageLocation = _person.ImagePath;
                }
            }

            llRemoveImage.Visible = (_person.ImagePath != "");
        }

        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (Mode == enMode.Update)
                _LoadData();
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(textBox.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider.SetError(textBox, "This field is required.");
            }
            else
            {
                errorProvider.SetError(textBox, null);
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtNationalNo, null);
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider.SetError(txtNationalNo, "Please enter your national number. This field is required.");
                return;
            }

            if (txtNationalNo.Text.Trim() != _person.NationalNo && clsPerson.IsPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider.SetError(txtNationalNo, "National number is used by another person!");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
            {
                errorProvider.SetError(txtEmail, null);
                return;
            }

            if (!clsValidation.IsEmailValid(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtEmail, "Invalid email address format!");
            }
            else
            {
                errorProvider.SetError(txtEmail, null);
            }
        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation == null)
            {
                pbImage.Image = Resources.Male;
            }
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation == null)
            {
                pbImage.Image = Resources.Female;
            }
        }

        private void linkSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog.Title = "Select an Image File";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                pbImage.ImageLocation = selectedFilePath;
                llRemoveImage.Visible = true;
            }
        }

        private void linkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                File.Delete(_person.ImagePath);
            }
            catch (IOException)
            {
                // We could not delete the file.
                // Log it later.
            }

            _person.ImagePath = null;
            pbImage.ImageLocation = null;
            pbImage.Image = null;

            if (rbMale.Checked)
                pbImage.Image = Resources.Male;
            else
                pbImage.Image = Resources.Female;

            llRemoveImage.Visible = false;
        }

        private void frmAddEditPersonInfo_Move(object sender, EventArgs e)
        {
            this.Size = new Size(821, 497);
            int x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        private void frmAddEditPersonInfo_Resize(object sender, EventArgs e)
        {
            this.Size = new Size(821, 497);
            int x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        private bool _HandlePersonImage()
        {
            if (_person.ImagePath != pbImage.ImageLocation)
            {
                if (_person.ImagePath != "") // if _person.ImagePath is "", your code will hit File.Delete("") → exception.
                {
                    try
                    {
                        File.Delete(_person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        // Log it later.
                    }
                }
            }

            if (pbImage.ImageLocation != null)
            {
                string sourceImageFile = pbImage.ImageLocation;

                if (clsUtil.CopyImageToProjectImagesFolder(ref sourceImageFile))
                {
                    _person.ImagePath = sourceImageFile;
                    return true;
                }
                else
                {
                    MessageBox.Show("Error copying image file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // For each control, it raises the Validating event if that control has CausesValidation = true.
            // If any control fails validation (for example, your Validating event handler sets e.Cancel = true), then ValidateChildren() will return false.
            // If all controls pass validation, it returns true.

            if (this.ValidateChildren())
            {
                if (!_HandlePersonImage())
                {
                    return;
                }

                _person.FirstName = txtFirstName.Text.Trim();
                _person.SecondName = txtSecondName.Text.Trim();
                _person.ThirdName = txtThirdName.Text.Trim();
                _person.LastName = txtLastName.Text.Trim();
                _person.NationalNo = txtNationalNo.Text.Trim();

                if (rbMale.Checked)
                    _person.Gendor = (byte)enGendor.Male;
                else
                    _person.Gendor = (byte)enGendor.Female;

                _person.DateOfBirth = dtpDateOfBirth.Value;
                _person.Email = txtEmail.Text.Trim();
                _person.Phone = txtPhone.Text.Trim();
                _person.Address = txtAddress.Text.Trim();
                _person.NationalityCountryID = clsCountry.Find(cbCountries.Text).ID;
                    
                if (_person.Save())
                {
                    lblPersonID.Text = _person.PersonID.ToString();
                    Mode = enMode.Update;
                    lblTitle.Text = "Update Person";

                    MessageBox.Show("The person’s data has been saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //this.DataBack?.Invoke(this, _person.PersonID);
                }
                else
                {
                    MessageBox.Show("The person’s data could not be saved. Please try again.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Some required fields are missing. Please complete all highlighted fields before submitting.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //DataBack?.Invoke(this, _person.PersonID);
            this.Close();
        }

        private void frmAddEditPersonInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataBack?.Invoke(this, _person.PersonID);
        }
    }
}
