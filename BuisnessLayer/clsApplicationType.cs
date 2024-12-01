using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int ID { set; get; }
        public string Title { set; get; }
        public float Fees { set; get; }

        public clsApplicationType() { 
            this.ID = -1;
            this.Title = "";
            this.Fees = 0;
            Mode = enMode.AddNew;
        }
        public clsApplicationType(int iD, string title, float fees)
        {
            ID = iD;
            Title = title;
            Fees = fees;
            Mode = enMode.Update;
        }



        private bool _AddNewApplicationType()
        {
            //call DataAccess Layer 

            this.ID = clsApplicationTypeData.AddNewApplicationType(this.Title, this.Fees);


            return (this.ID != -1);
        }

        private bool _UpdateApplicationType()
        {
            //call DataAccess Layer 

            return clsApplicationTypeData.UpdateApplicationType(this.ID, this.Title, this.Fees);
        }


        public static clsApplicationType Find(string Title)
        {
            int iD = -1;
            float fees = 0;
            if (clsApplicationTypeData.GetApplicationTypeInfoByName(Title, ref iD, ref fees))
            {
                return new clsApplicationType(iD,Title,fees);
            }
            else
            {
                return null;
            }
        }


        public static clsApplicationType Find(int ID)
        {
            string Title = "";
            float fees = 0;
            if (clsApplicationTypeData.GetApplicationTypeInfoByID(ID, ref Title, ref fees))
            {
                return new clsApplicationType(ID, Title, fees);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplicationType();

            }

            return false;
        }

    }
}
