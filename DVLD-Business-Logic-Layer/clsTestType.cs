using DVLD_DataAccess;
using DVLD_Business;
using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestType
    {
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }

        public enTestType ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Fees { get; set; }

        private clsTestType(enTestType ID, string title, string description, decimal fees)
        {
            this.ID = ID;
            Title = title;
            Description = description;
            Fees = fees;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }

        public static clsTestType Find(enTestType ID)
        {
            string title = "", description = "";
            decimal fees = 0;

            bool isFound = clsTestTypeData.GetTestTypeInfoByID((int)ID, ref title, ref description, ref fees);   

            if (isFound)
                return new clsTestType(ID, title, description, fees);
            else
                return null;
        }

        private bool _UpdateTestTypeInfo()
        {
            return clsTestTypeData.UpdateTestTypeInfo((int)ID, Title, Description, Fees);
        }

        public bool Save()
        {
            return _UpdateTestTypeInfo();
        }
    }
}
