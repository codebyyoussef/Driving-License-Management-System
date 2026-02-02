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

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        private enum enFilter { PersonID = 0, NationalNo = 1 };

        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int personID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
                handler(personID);
        }

        public delegate void DataBackEventHandler(int personID);
        public event DataBackEventHandler DataBack;

        private bool _showAddPerson = true;
        public bool ShowAddPerson
        {
            get { return _showAddPerson; } 
            set 
            { 
                _showAddPerson = value;
                btnAddNewPerson.Visible = _showAddPerson;
            }
        }

        private bool _filterEnabled = true;
        public bool FilterEnabled
        {
            get { return _filterEnabled; }
            set
            {
                _filterEnabled = value;
                gbFilters.Enabled = _filterEnabled;
            }
        }

        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        private clsPerson _selectedPersonInfo;

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        public void SelectAll()
        {
            txtFilterValue.SelectAll();
        }

        private void FindNow()
        {
            switch(cbFilterBy.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
                    break;
                case "National No":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;
                default:
                    break;
            }

            if (OnPersonSelected != null && FilterEnabled)
                OnPersonSelected(ctrlPersonCard1.PersonID);

        }

        public void LoadPersonInfo(int personID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = personID.ToString();
            FindNow();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FindNow();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void DataBackEvent(object sender, int personID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = personID.ToString();
            ctrlPersonCard1.LoadPersonInfo(personID);
            PersonSelected(ctrlPersonCard1.PersonID);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }

            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
