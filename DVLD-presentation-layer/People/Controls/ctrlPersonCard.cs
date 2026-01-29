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
using System.IO;

namespace DVLD.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private clsPerson _person;
        private int _personID = -1;

        public int PersonID
        {
            get { return _personID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _person; } 
        }

        private void _LoadPersonImage()
        {
            if (_person.ImagePath == "")
            {
                if (_person.Gendor == 0)
                    pbImage.Image = Properties.Resources.Male;
                else
                    pbImage.Image = Properties.Resources.Female;
            }
            else
            {
                string imagePath = _person.ImagePath;
                if (File.Exists(imagePath))
                {
                    pbImage.ImageLocation = imagePath;
                }
                else
                {
                    MessageBox.Show("Could not find this image " + imagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _FillPersonInfo()
        {
            llEditPersonInfo.Enabled = true;
            _personID = _person.PersonID;
            lblPersonID.Text = _person.PersonID.ToString();
            lblName.Text = _person.FullName;
            lblNationalNo.Text = _person.NationalNo;
            lblGendor.Text = _person.Gendor == 0 ? "Male" : "Female";
            lblEmail.Text = _person.Email;
            lblAddress.Text = _person.Address;
            lblDateOfBirth.Text = _person.DateOfBirth.ToShortDateString();
            lblPhone.Text = _person.Phone;
            lblCountry.Text = _person.CountryInfo.Name;

            _LoadPersonImage();
        }

        public void ResetPersonInfo()
        {
            _personID = -1;
            lblPersonID.Text = "[????]";
            lblName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblAddress.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblPhone.Text = "[????]";
            lblCountry.Text = "[????]";
            pbImage.ImageLocation = null;
            pbImage.Image = Properties.Resources.Male;
            llEditPersonInfo.Enabled = false;
        }

        public void LoadPersonInfo(int personID)
        {
            _person = clsPerson.Find(personID);

            if (_person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No person with PersonID = " + personID, "Person not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _FillPersonInfo();
            }
        }

        public void LoadPersonInfo(string nationalNo)
        {
            _person = clsPerson.Find(nationalNo);

            if (_person == null)
            {
                MessageBox.Show("No person with NationalNo = " + nationalNo, "Person not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _FillPersonInfo();
            }
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(_personID);
            frm.ShowDialog();

            // Refresh
            LoadPersonInfo(_personID);
        }
    }
}
