using DVLD_Project.Applications.Internationl_License;
using DVLD_Project.Applications.Local_Driving_License;
using DVLD_Project.Applications.Renew_Local_License;
using DVLD_Project.Applications.ReplaceLostOrDamagedLicense;
using DVLD_Project.Applications.Rlease_Detained_License;
using DVLD_Project.Drivers;
using DVLD_Project.Global_Classes;
using DVLD_Project.Licenses.Detain_License;
using DVLD_Project.Login;
using DVLD_Project.Tests.Test_Types;
using DVLD_Project.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class Form1 : Form
    {
        frmLogin _frmLogin;
        public Form1(frmLogin Login)
        {
            InitializeComponent();
            _frmLogin = Login;
        }

        private void localeLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication managePeople = new frmAddUpdateLocalDrivingLicesnseApplication();
            managePeople.Show();

        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePeople managePeople = new ManagePeople();
            managePeople.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void applicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void localDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
            Form1_Load(null,null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //frmManageApplicationTypes frm = new frmManageApplicationTypes();
           // frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicesnseApplications frm = new frmListInternationalLicesnseApplications();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }
    }
}
