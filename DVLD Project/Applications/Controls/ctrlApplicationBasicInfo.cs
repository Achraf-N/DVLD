using BuisnessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private clsApplication _Application;
        public clsApplication Application
        {
            get
            {
                return _Application;
            }
        }
        
        private int _ApplicantID = -1;
        public int ApplicantID
        {
            get {
                return _ApplicantID;
                   }
        }
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        public void ResetApplicationInfo()
        {
            _ApplicantID = -1;
            lblApplicationID.Text = "";
            lblStatus.Text = "";
            lblFees.Text = "";
            lblType.Text = "";
            lblApplicant.Text = "";
            lblDate.Text = "";
            lblStatusDate.Text = "";
            lblCreatedByUser.Text = "";


        }

        private void _FillApplicationInfo()
        {
            llViewPersonInfo.Enabled = true;
            _ApplicantID = _Application.ApplicationID;
            lblApplicationID.Text = _ApplicantID.ToString();
            lblStatus.Text = _Application.ApplicationStatus.ToString();
            lblFees.Text = _Application.PaidFees.ToString();
            lblType.Text = _Application.ApplicationTypeInfo.Title.ToString();
            clsPeopleManagement Perople = clsPeopleManagement.Find(_Application.ApplicantPersonID);
            lblApplicant.Text = Perople.FullName.ToString();
            lblDate.Text = _Application.ApplicationDate.ToString();
            lblStatusDate.Text = _Application.LastStatusDate.ToString();
            lblCreatedByUser.Text = clsUser.FindByUserID(_Application.CreatedByUserID).UserName.ToString();



        }
       
        public void LoadApplicationInfo(int ID)
        {
            _Application = clsApplication.FindBaseApplication(ID);
            if (_Application == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + ApplicantID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillApplicationInfo();
        }
        
        private void ctrlApplicationBasicInfo_Load(object sender, EventArgs e)
        {

        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_Application.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
