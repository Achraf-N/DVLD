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

namespace DVLD_Project.Applications.Local_Driving_License
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        public frmLocalDrivingLicenseApplicationInfo(int LocalID)
        {
            InitializeComponent();
            int ApplicationID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalID).ApplicationID;
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(LocalID);
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
        }
    
    
    }
}
