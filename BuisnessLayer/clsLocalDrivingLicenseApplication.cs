﻿using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public class clsLocalDrivingLicenseApplication:clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode;

        
        public int LocalDrivingLicenseApplicationID {  get; set; }
        public int LicenseClassID { get; set; }

        public clsLicenseClass LicenseClass;
        public string PersonFullName
        {
            get {
                return clsPeopleManagement.Find(ApplicantPersonID).FullName;
                }

        }
        public clsLocalDrivingLicenseApplication()
        {
            this.LicenseClassID = 0;
            this.LocalDrivingLicenseApplicationID = 0;
            Mode = enMode.AddNew;
        }
        
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID, int licenseClassID)
        {
             this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (int)ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassID = licenseClassID;
            this.LicenseClass = clsLicenseClass.Find(licenseClassID);
            Mode = enMode.Update;
        }
   

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID,this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID!=-1);

        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID,this.ApplicationID, this.LicenseClassID);
            

        }


        public bool Save()
        {

            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            
            if (!base.Save())
                return false;
            

            //After we save the main application now we save the sub application.
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseApplication();

            }

            return false;
        }



        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            // 
            int ApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID
                (LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                //now we find the base application
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;


        }


        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            // 
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID
                (ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                //now we find the base application
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;


        }

        public bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsApplicationBaseDeleted = false;
            IsLocalDrivingApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!IsLocalDrivingApplicationDeleted)
                return false;
            IsApplicationBaseDeleted = base.Delete();
            return IsApplicationBaseDeleted;
        }


        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID ,this.LicenseClassID);
        }
        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(ApplicationID,2);
        }

        public int TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.TotalTrialsPerTest((int)TestTypeID,this.LocalDrivingLicenseApplicationID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID,(int) TestTypeID);
        }
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID ,TestTypeID);
        }
        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID()!=-1);
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;
            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDriver();
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {

                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            //now we diver is there, so we add new licesnse

            clsLicense License = new clsLicense();
            License.DriverID = DriverID;   
            License.CreatedByUserID = CreatedByUserID;
            License.ApplicationID = this.ApplicationID;
            License.LicenseClass = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClass.DefaultValidityLength);
            License.Notes = Notes;

            License.PaidFees = this.LicenseClass.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            if (License.Save())
            {
                this.SetComplete();
                return License.LicenseID;
            }
            else
            {
                return -1;
            }
        }


        public bool PassedAllTest()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }














    }
}
