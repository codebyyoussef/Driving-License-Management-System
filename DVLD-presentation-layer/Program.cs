using DVLD.Applications.International_Driving_License;
using DVLD.Applications.Local_Driving_License_Applications;
using DVLD.Applications.Renew_Driving_license;
using DVLD.Login;
using DVLD.People;
using DVLD.Tests;
using DVLD.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
