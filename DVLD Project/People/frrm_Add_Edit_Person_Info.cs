using DVLD_Project.Properties;
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
using System.IO;
namespace DVLD_Project
{
    public partial class frrm_Add_Edit_Person_Info : Form
    {


        public delegate void DataBackPerson(object sender, int PersonID);
        public event DataBackPerson DataBack;

        private int _PersonId = -1;
        clsPeopleManagement _Person;
        public enum enMode { Add=0,Update=1}
        public enum enGendor { Male=0,Female=1}

        private enMode _Mode = enMode.Add;
        public frrm_Add_Edit_Person_Info()
        {
            InitializeComponent();
            _Mode = enMode.Add;
        }
       
        public frrm_Add_Edit_Person_Info(int id)
        {
            InitializeComponent();
            
               _Mode = enMode.Update;
            _PersonId = id;
        }

        private void _ResetDefualtValues()
        {
            _FillCountriesInComoboBox();
            if (_Mode == enMode.Add)
            {
                lblTitle.Text = "Add New Person";
            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            if (rbMale.Checked)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
            else
            {
                pbPersonImage.Image = Resources.Female_512;
            }

            //hide/show the remove linke incase there is no image for the person.
            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            //we set the max date to 18 years from today, and set the default value the same.
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            //should not allow adding age more than 100 years
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            //this will set default country to jordan.
            cbCountry.SelectedIndex = cbCountry.FindString("Jordan");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }
        
        private void _LoadData()
        {
             _Person = clsPeopleManagement.FindPeopleByID(_PersonId);
            lblTitle.Text = "Update Person";
            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonId, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            txtFirstName.Text = _Person.FirstName.ToString();
            txtSecondName.Text = _Person.SecondName.ToString();
            txtThirdName.Text = _Person.ThirdName.ToString();
            txtLastName.Text = _Person.LastName.ToString();
            txtNationalNo.Text = _Person.NationaleNo.ToString();
            rbMale.Checked = true;
            txtPhone.Text = _Person.Phone.ToString();
            txtEmail.Text = _Person.Email.ToString();
            txtAddress.Text = _Person.Address.ToString();
            if (_Person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;
            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.NationalityCo).CountryName);

            if (_Person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;

            }

            llRemoveImage.Visible = (_Person.ImagePath!="");



        }

        private void frrm_Add_Edit_Person_Info_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if(_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void _FillCountriesInComoboBox()
        {
            DataTable dt = clsCountry.GetAllCountries();
            foreach (DataRow row in dt.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }
            private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }




        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (pbPersonImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            _Person = new clsPeopleManagement();
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

           // if (!_HandlePersonImage())
             //   return;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationaleNo = txtNationalNo.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.DateofBirth = dtpDateOfBirth.Value;

            if (rbMale.Checked)
                _Person.Gendor = (short)enGendor.Male;
            else
                _Person.Gendor = (short)enGendor.Female;

            int NationalityCountryID = clsCountry.Find(cbCountry.Text).ID;

            _Person.NationalityCo = NationalityCountryID;

            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {

                lblPersonID.Text = _Person.PersonID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this,_Person.PersonID);

            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtNationalNo_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

           /*
            //Make sure the national number is not used by another person
            if (txtNationalNo.Text.Trim() != _Person.NationaleNo && clsPeopleManagement.isPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
           */
        }

        private void txtEmail_Validating_1(object sender, CancelEventArgs e)
        {
            //no need to validate the email incase it's empty.
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidatoin.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            };
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                llRemoveImage.Visible = true;
                // ...
            }
        }


        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            pbPersonImage.ImageLocation = null;



            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = false;
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
            //change the defualt image to female incase there is no image set.
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Female_512;
        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            //change the defualt image to male incase there is no image set.
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Male_512;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
