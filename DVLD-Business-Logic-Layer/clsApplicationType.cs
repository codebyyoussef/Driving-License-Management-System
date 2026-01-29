using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsApplicationType
    {
        public byte ID { get; set; }
        public string Title { get; set; }
        public decimal Fees { get; set; }

        private clsApplicationType(byte ID,  string title, decimal fees)
        {
            this.ID = ID;
            this.Title = title; 
            this.Fees = fees;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }

        public static clsApplicationType Find(byte ID)
        {
            string title = "";
            decimal fees = 0;

            bool isFound = clsApplicationTypeData.GetApplicationTypeInfoByID(ID, ref title, ref fees);

            if (isFound)
                return new clsApplicationType(ID, title, fees);
            else
                return null;
        }

        private bool _UpdateApplicationTypeInfo()
        {
            return clsApplicationTypeData.UpdateApplicationTypeInfo(this.ID, this.Title, this.Fees);
        }

        public bool Save()
        {
            return _UpdateApplicationTypeInfo();
        }
    }
}
