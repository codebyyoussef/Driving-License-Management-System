using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsPerson
    {
        public enum enMode { AddNew, Update };
        enMode Mode = enMode.AddNew;

        public enum enGendor { Male = 0, Female = 1 };

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + SecondName + " " + (ThirdName != "" ? ThirdName + " " : "") + LastName;
            }
        }
        public DateTime DateOfBirth { get; set; }
        public byte Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        
        public clsCountry CountryInfo;

        private string _ImagePath;
        public string ImagePath
        {
            get {  return _ImagePath; }
            set { _ImagePath = value; }
        }

        public clsPerson()
        {

            PersonID = -1;
            NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            Gendor = 0;
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = -1;
            ImagePath = "";

            Mode = enMode.AddNew;
        }

        private clsPerson(int personID, string notionalID, string firstName, string secondName, string thirdName, 
                         string lastName, DateTime dateOfBirth, byte gendor, string address, string phone, string email, 
                         int nationalityCountryID, string imagePath)
        {
            this.PersonID = personID;
            this.NationalNo = notionalID;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gendor = gendor;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = nationalityCountryID;
            this.CountryInfo = clsCountry.Find(nationalityCountryID);
            this.ImagePath = imagePath;

            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth,
                            this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, 
                                 this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }

        static public bool DeletePerson(int ID)
        {
            return clsPersonData.DeletePerson(ID);
        }

        public static DataTable GetAllPersons()
        {
            return clsPersonData.GetAllPersons();
        }

        public static clsPerson Find(int personID)
        {
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "", email = "", phone = "", address = "", imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            byte gendor = 0;
            int nationalityCountryID = -1;

            bool isFound = clsPersonData.GetPersonInfoByID(personID, ref nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName,
                           ref dateOfBirth, ref gendor, ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath);

            if (isFound)
                return new clsPerson(personID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gendor, address, phone,
                                    email, nationalityCountryID, imagePath);
            else
                return null;
        }

        public static clsPerson Find(string nationalNo)
        {
            int personID = -1;
            string firstName = "", secondName = "", thirdName = "", lastName = "", email = "", phone = "", address = "", imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            byte gendor = 0;
            int nationalityCountryID = -1;

            bool isFound = clsPersonData.GetPersonInfoByNationalNo(nationalNo, ref personID, ref firstName, ref secondName, ref thirdName, ref lastName,
                           ref dateOfBirth, ref gendor, ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath);

            if (isFound)
            {
                return new clsPerson(personID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gendor, address, phone,
                                    email, nationalityCountryID, imagePath);
            }
            else
            {
                return null;
            }
        }

        public static bool IsPersonExist(int personID)
        {
            return clsPersonData.IsPersonExist(personID);
        }

        public static bool IsPersonExist(string nationalNo)
        {
            return clsPersonData.IsPersonExist(nationalNo);
        }
    }
}
