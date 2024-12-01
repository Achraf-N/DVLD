using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataAccessLayer;
namespace BuisnessLayer
{
    public class clsPeopleManagement
    {
        enum enMode { AddNew=0, Update=1 }
        private enMode _Mode = enMode.AddNew;
        public int PersonID { get; set; }
        public string NationaleNo { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            }
        }
        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }


        public int NationalityCo { get; set; }

        public clsCountry CountryInfo;
        public string ImagePath { get; set; }

        public DateTime DateofBirth { get; set; }

        public short Gendor { get; set; }

        private clsPeopleManagement(int ID, string NationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateofBirth, short gendor, string address, string phone, string email, int nationaliteCo, string imagePath)
        {
            PersonID = ID;
            NationaleNo = NationalNo;
            FirstName = firstName;

            LastName = lastName;
            ThirdName = thirdName;
            Address = address;
            Phone = phone;
            Email = email;
            ImagePath = imagePath;
            SecondName = secondName;
            Gendor = gendor;
            NationalityCo = nationaliteCo;

            DateofBirth = dateofBirth;
            _Mode = enMode.Update;
        }
        public clsPeopleManagement()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.DateofBirth = DateTime.Now;
            this.ThirdName = "";
            this.ImagePath = "";
            this.Address = "";
            this.Email = "";
            this.Gendor = 0;
            this.Phone = "";
            this.NationaleNo = "";
            this.NationalityCo = -1;
            this.SecondName = "";
            _Mode = enMode.AddNew;
        }

        public static clsPeopleManagement Find(int PersonID)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            short Gendor = 0;

            bool IsFound = clsDataPeople.GetPeopleByID
                                (
                                    PersonID, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref NationalNo, ref DateOfBirth,
                                    ref Gendor, ref Address, ref Phone, ref Email,
                                    ref NationalityCountryID, ref ImagePath
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsPeopleManagement(PersonID, FirstName, SecondName, ThirdName, LastName,
                          NationalNo, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            else
                return null;
        }

        public static clsPeopleManagement Find(string NationalNo)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1, PersonID=-1;
            short Gendor = 0;

            bool IsFound = clsDataPeople.GetPeopleByNationalNO
                                (
                                    NationalNo, ref PersonID, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref DateOfBirth,
                                    ref Gendor, ref Address, ref Phone, ref Email,
                                    ref NationalityCountryID, ref ImagePath
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsPeopleManagement(PersonID, FirstName, SecondName, ThirdName, LastName,
                          NationalNo, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            else
                return null;
        }


        public static clsPeopleManagement FindPeopleByID(int ID)
        {
            string ThirdName = "", FirstName = "", LastName = "", Address = "", Email = "", Phone = "", SecondName = "", NationaleNo = "", ImagePath = "";

            int NationalityCo = -1;
            short Gendor = 0;
            DateTime DateofBirth = DateTime.Now;

            if (clsDataPeople.GetPeopleByID(ID,ref NationaleNo, ref FirstName, ref SecondName,ref ThirdName, ref LastName,ref DateofBirth,ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCo, ref ImagePath))
            {
                return new clsPeopleManagement(ID, NationaleNo, FirstName, SecondName, ThirdName, LastName, DateofBirth, Gendor, Address, Phone, Email, NationalityCo, ImagePath);
            }
            else
            {
                return null;
            }
        }
        private bool _AddNewPerson()
        {
            this.PersonID = clsDataPeople.AddNewPerson(
               this.FirstName, this.SecondName, this.ThirdName,
               this.LastName, this.NationaleNo,
               this.DateofBirth, this.Gendor, this.Address, this.Phone, this.Email,
               this.NationalityCo, this.ImagePath);

            return (this.PersonID != -1);

        }

        private bool _UpdatePerson()
        {

            return clsDataPeople.UpdatePerson(this.PersonID,
               this.FirstName, this.SecondName, this.ThirdName,
               this.LastName, this.NationaleNo,
               this.DateofBirth, this.Gendor, this.Address, this.Phone, this.Email,
               this.NationalityCo, this.ImagePath);

        }

        public bool Save()
        {
            switch (_Mode)
            {

                case enMode.AddNew:

                    if (_AddNewPerson())
                    {

                        _Mode = enMode.Update;
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
      
        public static DataTable GetAllPeople()
        {
          return clsDataPeople.GetDataAllPeople();
        }

        public static int GetTotalNumberRecord()
        {
            return clsDataPeople.GetAllPeopleNumber();
        }

        public static bool isPersonExist(string NationalNo)
        {
            return clsDataPeople.IsPersonExist(NationalNo);
        }

        public static bool Delete(int PersonID)
        {
            return clsDataPeople.DeletePerson(PersonID);
        }
    }
}
