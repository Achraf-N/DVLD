using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID {  get; set; }
        public int TestAppointmentID {  get; set; }
        public clsTestAppointment TestAppointmentInfo { set; get; }
        public bool TestResult {  get; set; }
        public string Notes {  get; set; }
        public int CreatedByUser {  get; set; }

        public clsTest()
        {
            this.TestID = 0;
            this.TestAppointmentID = 0;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUser = 0;
            Mode = enMode.AddNew;
        }


        public clsTest(int TestID, int TestAppointmentID,
             bool TestResult, string Notes, int CreatedByUserID)

        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUser = CreatedByUserID;

            Mode = enMode.Update;
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public static clsTest Find(int testID)
        {
            int TestAppointmentID=0, CreatedByUser=0;

            bool TestResult = false;
           string Notes = "";
            if (clsTestData.GetTestByID(testID,ref TestAppointmentID, ref TestResult,ref Notes, ref CreatedByUser))
            {
                return new clsTest(testID,TestAppointmentID,TestResult, Notes,CreatedByUser);
            }
            else
            {
                return null;
            }
}


        private bool _AddNewTest()
        {
            //call DataAccess Layer 

            this.TestID = clsTestData.AddNewTest(this.TestAppointmentID,
                this.TestResult, this.Notes, this.CreatedByUser);


            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {
            //call DataAccess Layer 

            return clsTestData.UpdateTest(this.TestID, this.TestAppointmentID,
                this.TestResult, this.Notes, this.CreatedByUser);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTest();

            }

            return false;
        }

        public static clsTest FindLastTestPerPersonAndLicenseClass(int PersonID, int LicenseClassID, clsTestType.enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;
            if (clsTestData.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID, LicenseClassID,(int)TestTypeID,ref TestID,
                ref TestAppointmentID, ref TestResult, ref Notes,ref CreatedByUserID))
            {

                return new clsTest(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            }else return null;

        }


        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }

        }
}
