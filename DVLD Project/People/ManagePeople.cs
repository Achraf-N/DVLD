using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuisnessLayer;
namespace DVLD_Project
{
    public partial class ManagePeople : Form
    {

        private static DataTable _dtAllPeople = clsPeopleManagement.GetAllPeople();

        //only select the columns that you want to show in the grid
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "Gendor", "DateOfBirth", "NationalityCountryID",
                                                         "Phone", "Email");

        public ManagePeople()
        {
            InitializeComponent();
        }

        private void _ref()
        {
            _dtAllPeople = clsPeopleManagement.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "Gendor", "DateOfBirth", "NationalityCountryID",
                                                         "Phone", "Email");

            dgvShowallrecord.DataSource = _dtPeople;
            lRecord.Text = dgvShowallrecord.Rows.Count.ToString();

        }
        private void ManagePeople_Load(object sender, EventArgs e)
        {
            //_ref();

            dgvShowallrecord.DataSource = _dtPeople;
            cbFilter.SelectedIndex = 0;
            lRecord.Text = dgvShowallrecord.Rows.Count.ToString();
            if (dgvShowallrecord.Rows.Count > 0)
            {

                dgvShowallrecord.Columns[0].HeaderText = "Person ID";
                dgvShowallrecord.Columns[0].Width = 110;

                dgvShowallrecord.Columns[1].HeaderText = "National No.";
                dgvShowallrecord.Columns[1].Width = 120;


                dgvShowallrecord.Columns[2].HeaderText = "First Name";
                dgvShowallrecord.Columns[2].Width = 120;

                dgvShowallrecord.Columns[3].HeaderText = "Second Name";
                dgvShowallrecord.Columns[3].Width = 140;


                dgvShowallrecord.Columns[4].HeaderText = "Third Name";
                dgvShowallrecord.Columns[4].Width = 120;

                dgvShowallrecord.Columns[5].HeaderText = "Last Name";
                dgvShowallrecord.Columns[5].Width = 120;

                dgvShowallrecord.Columns[6].HeaderText = "Gendor";
                dgvShowallrecord.Columns[6].Width = 120;

                dgvShowallrecord.Columns[7].HeaderText = "Date Of Birth";
                dgvShowallrecord.Columns[7].Width = 140;

                dgvShowallrecord.Columns[8].HeaderText = "Nationality";
                dgvShowallrecord.Columns[8].Width = 120;


                dgvShowallrecord.Columns[9].HeaderText = "Phone";
                dgvShowallrecord.Columns[9].Width = 120;


                dgvShowallrecord.Columns[10].HeaderText = "Email";
                dgvShowallrecord.Columns[10].Width = 170;
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilter.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void BtPlus_Click(object sender, EventArgs e)
        {

            frrm_Add_Edit_Person_Info AddEditPerson = new frrm_Add_Edit_Person_Info();
            AddEditPerson.ShowDialog();

            _ref();
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilter.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }



        private void txtdetail_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lRecord.Text = dgvShowallrecord.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.

                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lRecord.Text = dgvShowallrecord.Rows.Count.ToString();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvShowallrecord.CurrentRow.Cells[0].Value;
            Form frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frrm_Add_Edit_Person_Info AddEditPerson = new frrm_Add_Edit_Person_Info();
            AddEditPerson.ShowDialog();

            _ref();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form frm = new frrm_Add_Edit_Person_Info((int)dgvShowallrecord.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _ref();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvShowallrecord.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {
                if (clsPeopleManagement.Delete((int)dgvShowallrecord.CurrentRow.Cells[0].Value))
                {

                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ref();
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}
