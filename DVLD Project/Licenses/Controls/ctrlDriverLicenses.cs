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

namespace DVLD_Project.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _DriverID;
        private clsDriver _Driver;
        private DataTable _dtDriverLocalLicensesHistory;
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicenseInfo()
        {

            _dtDriverLocalLicensesHistory = clsDriver.GetLicenses(_DriverID);


            dgvLocalLicensesHistory.DataSource = _dtDriverLocalLicensesHistory;
            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 170;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 110;

            }
        }


        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.FindByDriverID(_DriverID);

            _LoadLocalLicenseInfo();

        }

        public void LoadInfoByPersonID(int PersonID)
        {

            _Driver = clsDriver.FindByPersonID(PersonID);
            if (_Driver != null)
            {
                _DriverID = clsDriver.FindByPersonID(PersonID).DriverID;
            }

            _LoadLocalLicenseInfo();
        }

        public void Clear()
        {
            _dtDriverLocalLicensesHistory.Clear();

        }
        private void ctrlDriverLicenses_Load(object sender, EventArgs e)
        {

        }
    }
}
