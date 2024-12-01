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

namespace DVLD_Project.Tests
{
    public partial class frmScheduleTest : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private int _AppointmentID = -1;

        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID, int AppointmentID = -1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            _AppointmentID = AppointmentID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrScheduleTest1.TestTypeID = _TestTypeID;
            ctrScheduleTest1.LoadInfo(_LocalDrivingLicenseApplicationID, _AppointmentID);
        }

        private void ctrScheduleTest1_Load(object sender, EventArgs e)
        {

        }
    }
}
