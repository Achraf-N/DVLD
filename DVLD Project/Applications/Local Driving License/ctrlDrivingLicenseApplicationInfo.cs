using BuisnessLayer;
using DVLD_Project.Licenses.Local_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.Local_Driving_License
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _LicenseID = -1;
        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            ctrlApplicationBasicInfo1.ResetApplicationInfo();
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblAppliedFor.Text = "[????]";


        }

        private void _FillLocalDrivingLicenseApplicationInfo()
        {

            _LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();

            llShowLicenceInfo.Enabled = (_LicenseID != -1);
            int Passed = _LocalDrivingLicenseApplication.GetPassedTestCount();
            lblPassedTests.Text =_LocalDrivingLicenseApplication.GetPassedTestCount().ToString()+"/3";
            int LicenceID = _LocalDrivingLicenseApplication.LicenseClassID;
            lblAppliedFor.Text = clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);


        }
        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID)
        {

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();


                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();


        }

        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(ApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();


                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();


        }

        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            //frmShowLicenseInfo frm = new frmShowLicenseInfo(_LocalDrivingLicenseApplication.GetActiveLicenseID());
            //frm.ShowDialog();
        }
    }
}
