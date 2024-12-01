using BuisnessLayer;
using DVLD_Project.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Licenses.Local_Licenses
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplications;
        public frmIssueDriverLicenseFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            int LicenseID = _LocalDrivingLicenseApplications.IssueLicenseForTheFirtTime(txtNotes.Text.Trim(),1);

            //int LicenseID = _LocalDrivingLicenseApplications.IssueLicenseForTheFirtTime(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);


            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ctrlDrivingLicenseApplicationInfo1_Load(object sender, EventArgs e)
        {

        }

        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {

            txtNotes.Focus();
            _LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplications == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + _LocalDrivingLicenseApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!_LocalDrivingLicenseApplications.PassedAllTest())
            {
                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _LocalDrivingLicenseApplications.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}
