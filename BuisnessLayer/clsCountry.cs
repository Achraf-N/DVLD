using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BuisnessLayer
{
    public class clsCountry
    {

        public int ID { set; get; }
        public string CountryName { set; get; }

        public clsCountry() {
            this.ID = -1;
            this.CountryName = "";
        }

        private clsCountry(int ID, string CountryName)

        {
            this.ID = ID;
            this.CountryName = CountryName;
        }
        public static clsCountry Find(string CountryName)
        {
            int ID = -1;

            if (clsDataContry.GetCountryInfoByName(CountryName, ref ID))

                return new clsCountry(ID, CountryName);
            else
                return null;

        }

        public static clsCountry Find(int  ID)
        {
            string countryName = "";

            if (clsDataContry.GetCountryInfoByID(ID, ref countryName))

                return new clsCountry(ID, countryName);
            else
                return null;

        }


        public static DataTable GetAllCountries()
        {
            return clsDataContry.GetAllDataCountry();
        }
    }
}
