using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuisnessLayer;
using DVLD_Project.Properties;
using System.IO;
namespace DVLD_Project
{
    public partial class ctrlPersonCard : UserControl
    {

        private clsPeopleManagement _Person;

        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }
        public clsPeopleManagement Person
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }
        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGendor.Image = Resources.Man_32;
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;

        }

        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


        private void _FillPersonInfo()
        {
            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationaleNo;
            lblFullName.Text = _Person.FullName;
            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateofBirth.ToShortDateString();
            int idd = (int)_Person.NationalityCo;
            lblCountry.Text = clsCountry.Find(_Person.NationalityCo).CountryName;
            lblAddress.Text = _Person.Address;
            _LoadPersonImage();




        }

        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPeopleManagement.Find(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();

        }
        public void LoadPersonInfo(string NationalNO)
        {
            _Person = clsPeopleManagement.Find(NationalNO);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with NationalNO = " + NationalNO, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();

        }
        private void ctrlPersonCard_Load(object sender, EventArgs e)
        {

        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frrm_Add_Edit_Person_Info(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
