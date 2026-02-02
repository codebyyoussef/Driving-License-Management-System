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

namespace DVLD.Users.Controls
{
    public partial class ctrlUserCard : UserControl
    {
        private clsUser _user;

        private int _userID = -1;
        public int UserID
        {
            get { return _userID; }
        }

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_user.PersonID);

            lblUserID.Text = _user.UserID.ToString();
            lblUsename.Text = _user.UserName;
            lblIsActive.Text = _user.IsActive == true ? "Yes" : "No";
        }

        public void LoadUserInfo(int userID)
        {
            _user = clsUser.FindByUserID(userID);

            if (_user == null)
            {
                MessageBox.Show("No User with UserID = " + userID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _userID = userID;
            _FillUserInfo();
        }
    }
}
