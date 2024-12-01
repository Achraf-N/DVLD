using BuisnessLayer;
using DVLD_Project.Global_Classes;
using DVLD_Project.People.Controls;
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
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _SelectedPersonID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;

        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdateLocalDrivingLicesnseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;   
        }
        private void _FillLicenseClassesInComoboBox()
        {
            DataTable dt = clsLicenseClass.GetAllLicenseClasses();
            foreach (DataRow row in dt.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }
        private void _ResetDefualtValues()
        {
            _FillLicenseClassesInComoboBox();
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                ctrPersonCardWithFilter1.FilterFocus();
                cbLicenseClass.SelectedIndex = 2;

                lblApplicationDate.Text = DateTime.Now.ToString();
                lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();
                tpApplicationInfo.Enabled = false;
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;


            }
        }
        private void tpApplicationInfo_Click(object sender, EventArgs e)
        {

        }
        private void _LoadData()
        {
            ctrPersonCardWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }
            ctrPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblApplicationDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToString();
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);

            lblCreatedByUser.Text = clsUser.FindByUserID(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;
        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            // Handle the data received
            _SelectedPersonID = PersonID;
            ctrPersonCardWithFilter1.LoadPersonInfo(PersonID);


        }
        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {

            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                return;
            }

            //incase of add new mode.
            if (ctrPersonCardWithFilter1.PersonID != -1)
            {

                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];

            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
               // ctrPersonCardWithFilter1.FilterFocus();
            }
        }

        private void ctrPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            int LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;


            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }


            //check if user already have issued license of the same driving  class.
            if (clsLicense.IsLicenseExistByPersonID(ctrPersonCardWithFilter1.PersonID, LicenseClassID))
            {

                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;
            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrPersonCardWithFilter1.PersonID;

            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = 1;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            if (_LocalDrivingLicenseApplication.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();

                _Mode = enMode.Update;
                
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void ctrPersonCardWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Activated(object sender, EventArgs e)
        {
            ctrPersonCardWithFilter1.FilterFocus();
        }
    }
}
