using BuisnessLayer;
using DVLD_Project.Applications.Local_Driving_License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.People.Controls
{
    public partial class ctrPersonCardWithFilter : UserControl
    {
        //event handler
        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler =  OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID);
            }
        }


        private int _PersonID = -1;


        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }
       
        private clsPeopleManagement _People;

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }
        public ctrPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {


        
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void FindNow()
        {

            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));

                    break;

                case "National No.":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;

                default:
                    break;
            }
            if(OnPersonSelected != null)
            {
                OnPersonSelected(ctrlPersonCard1.PersonID);
            }


        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            FindNow();

        }


        public void LoadPersonInfo(int PersonID)
        {

            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            FindNow();

        }

      
        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);



        }
        private void ctrPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frrm_Add_Edit_Person_Info frm = new frrm_Add_Edit_Person_Info();
            frm.DataBack += DataBackEvent;//subscribe

            frm.ShowDialog();
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            // Handle the data received

            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }
        private void gbFilters_Enter(object sender, EventArgs e)
        {

        }
    }
}
