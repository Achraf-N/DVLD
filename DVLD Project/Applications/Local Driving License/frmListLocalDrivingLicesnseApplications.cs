using BuisnessLayer;
using DVLD_Project.Licenses.Local_Licenses;
using DVLD_Project.Tests;
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
    public partial class frmListLocalDrivingLicesnseApplications : Form
    {
        private DataTable _dtAllLocalDrivingLicenseApplications;
        public frmListLocalDrivingLicesnseApplications()
        {
            InitializeComponent();
        }

        private void frmListLocalDrivingLicesnseApplications_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;

            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {

                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;
            }

            cbFilterBy.SelectedIndex = 0;
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
     

            frmLocalDrivingLicenseApplicationInfo frm =
                        new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm =
                        new frmAddUpdateLocalDrivingLicesnseApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if(LocalDrivingLicenseApplication!=null){
                if (LocalDrivingLicenseApplication.Delete())
                {

                    MessageBox.Show("Application Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListLocalDrivingLicesnseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete application, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmListLocalDrivingLicesnseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvLocalDrivingLicenseApplications_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();

            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void vistionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsTestAppointment testAppointment = clsTestAppointment.GetLastTestAppointment(LocalDrivingLicenseApplicationID,clsTestType.enTestType.VisionTest);

            if(testAppointment == null)
            {

                MessageBox.Show("No Vision Test Appointment Found!", "Set Appointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmTakeTest frm = new frmTakeTest(testAppointment.TestAppointmentID, clsTestType.enTestType.VisionTest);
            frm.ShowDialog();
        }
        private void WrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            if (!clsLocalDrivingLicenseApplication.DoesPassTestType(LocalDrivingLicenseApplicationID,clsTestType.enTestType.VisionTest))
            {
                MessageBox.Show("Person Should Pass the Vision Test First!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            clsTestAppointment testAppointment = clsTestAppointment.GetLastTestAppointment(LocalDrivingLicenseApplicationID, clsTestType.enTestType.WrittenTest);

            if (testAppointment == null)
            {

                MessageBox.Show("No Written Test Appointment Found!", "Set Appointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmTakeTest frm = new frmTakeTest(testAppointment.TestAppointmentID, clsTestType.enTestType.WrittenTest);
            frm.ShowDialog();
        }
        private void StreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            if (!clsLocalDrivingLicenseApplication.DoesPassTestType(LocalDrivingLicenseApplicationID, clsTestType.enTestType.WrittenTest))
            {
                MessageBox.Show("Person Should Pass the Written Test First!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            clsTestAppointment testAppointment = clsTestAppointment.GetLastTestAppointment(LocalDrivingLicenseApplicationID, clsTestType.enTestType.StreetTest);

            if (testAppointment == null)
            {

                MessageBox.Show("No Street Test Appointment Found!", "Set Appointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmTakeTest frm = new frmTakeTest(testAppointment.TestAppointmentID, clsTestType.enTestType.StreetTest);
            frm.ShowDialog();
        }

        private void _ScheduleTest(clsTestType.enTestType TestType)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }
        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
        }

        private void ScheduleTestsMenue_Click(object sender, EventArgs e)
        {

        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.WrittenTest);

        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.StreetTest);
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                    clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID
                                                    (LocalDrivingLicenseApplicationID);
            int TotalPassedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;

            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;

            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editToolStripMenuItem.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
            ScheduleTestsMenue.Enabled = !LicenseExists;

            //Enable/Disable Cancel Menue Item
            //We only canel the applications with status=new.
            CancelApplicaitonToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            //Enable/Disable Delete Menue Item
            //We only allow delete incase the application status is new not complete or Cancelled.
            DeleteApplicationToolStripMenuItem.Enabled =
                (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);



            //Enable Disable Schedule menue and it's sub menue
            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest); ;
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            ScheduleTestsMenue.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (ScheduleTestsMenue.Enabled)
            {
                //To Allow Schdule vision test, Person must not passed the same test before.
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;

            }

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {


            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
            //refresh
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;


            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(
               LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    
    
    
    
    }
}
