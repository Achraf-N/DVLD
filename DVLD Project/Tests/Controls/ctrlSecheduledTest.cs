using BuisnessLayer;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Tests.Controls
{
    public partial class ctrlSecheduledTest : UserControl
    {
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;
        private int _TestID = -1;
        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
        }
        public int TestID
        {
            get
            {
                return _TestID;
            }
        }

        private clsTestType.enTestType _TestTypeID;
        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
        }


        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        
        private int _LocalDrivingLicenseApplicationID;
        public ctrlSecheduledTest()
        {
            InitializeComponent();
        }
        public void LoadInfo(int AppointmentID)
        {
            _TestAppointmentID = AppointmentID;
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }

            _TestID = _TestAppointment.TestID;

            lblLocalDrivingLicenseAppID.Text = AppointmentID.ToString();
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            _LocalDrivingLicenseApplicationID = _TestAppointment.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName.ToString();
            lblDate.Text = _TestAppointment.AppointmentDate.ToString();
            //NOO
            lblTrial.Text =  _LocalDrivingLicenseApplication.TotalTrialsPerTest(clsTestType.enTestType.VisionTest).ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClass.ClassName.ToString();
            lblTestID.Text = "Not Taken YET";

        }
        private void gbTestType_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlSecheduledTest_Load(object sender, EventArgs e)
        {

        }
    }
}
