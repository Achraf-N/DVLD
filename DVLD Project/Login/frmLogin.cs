using BuisnessLayer;
using DVLD_Project.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }


        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";
            if (clsGlobal.GetStoredCredential(ref UserName,ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;

                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;
        
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(),txtPassword.Text.Trim());
            if (user != null)
            {

                if (chkRememberMe.Checked)
                {
                    //store username and password
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

                }
                else
                {
                    //store empty username and password
                    clsGlobal.RememberUsernameAndPassword("", "");

                }

                if (!user.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                clsGlobal.CurrentUser = user;
                this.Hide();
                Form1 frm = new Form1(this);
                frm.ShowDialog();

            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void chkRememberMe_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
