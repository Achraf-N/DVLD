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

namespace DVLD_Project.Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        private int _PersonID;
        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrPersonCardWithFilter1.FilterEnabled = false;
                ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);
            }
            else
            {
                ctrPersonCardWithFilter1.Enabled = true;
                ctrPersonCardWithFilter1.FilterFocus();
            }
        }

        private void ctrPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;
            if (_PersonID == -1)
            {
                ctrlDriverLicenses1.Clear();
            }
            else
                ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);
        }
    
    
    
    }
}
