using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsCountry
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public clsCountry()
        {
            ID = -1;
            Name = "";
        }

        private clsCountry(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        public static clsCountry Find(int ID)
        {
            string countryName = "";

            if (clsCountryData.GetCountryInfoByID(ID, ref countryName))
            {
                return new clsCountry(ID, countryName);
            }
            else
            {
                return null;
            }
        }

        public static clsCountry Find(string countryName)
        {
            int ID = -1;
            if (clsCountryData.GetCountryInfoByName(countryName, ref ID))
            {
                return new clsCountry(ID, countryName);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }
    }
}
