using DVLD_Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Global_Classes
{
    internal static class clsGlobal
    {

        public static clsUser currentUser;

        public static bool RememberUsernameAndPassword(string userName, string password)
        {
            try
            {
                string dataToSave = userName + "|" + password;
                using (StreamWriter file = new StreamWriter("LoginInfo.txt"))
                {
                    file.WriteLine(dataToSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static bool GetStoredCredential(ref string userName, ref string password)
        {
            if (File.Exists("LoginInfo.txt"))
            {
                StreamReader file = new StreamReader("LoginInfo.txt");
                string line = file.ReadToEnd().Trim();
                file.Close();


                string[] userLoginInfo = line.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (userLoginInfo.Length != 0)
                {
                    
                    userName = userLoginInfo[0];
                    password = userLoginInfo[1];
                    return true;
                }
            }
            return false;
        }
    }
}
