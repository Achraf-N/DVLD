namespace DVLD_Project.Licenses
{
    partial class frmShowPersonLicenseHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbPersonImage = new System.Windows.Forms.PictureBox();
            this.ctrPersonCardWithFilter1 = new DVLD_Project.People.Controls.ctrPersonCardWithFilter();
            this.ctrlDriverLicenses1 = new DVLD_Project.Licenses.Controls.ctrlDriverLicenses();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(6, 1);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1062, 39);
            this.lblTitle.TabIndex = 131;
            this.lblTitle.Text = "License History";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbPersonImage
            // 
            this.pbPersonImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbPersonImage.Image = global::DVLD_Project.Properties.Resources.PersonLicenseHistory_512;
            this.pbPersonImage.InitialImage = null;
            this.pbPersonImage.Location = new System.Drawing.Point(7, 142);
            this.pbPersonImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbPersonImage.Name = "pbPersonImage";
            this.pbPersonImage.Size = new System.Drawing.Size(220, 189);
            this.pbPersonImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPersonImage.TabIndex = 132;
            this.pbPersonImage.TabStop = false;
            // 
            // ctrPersonCardWithFilter1
            // 
            this.ctrPersonCardWithFilter1.FilterEnabled = true;
            this.ctrPersonCardWithFilter1.Location = new System.Drawing.Point(286, 43);
            this.ctrPersonCardWithFilter1.Name = "ctrPersonCardWithFilter1";
            this.ctrPersonCardWithFilter1.Size = new System.Drawing.Size(852, 387);
            this.ctrPersonCardWithFilter1.TabIndex = 133;
            this.ctrPersonCardWithFilter1.OnPersonSelected += new System.Action<int>(this.ctrPersonCardWithFilter1_OnPersonSelected);
            // 
            // ctrlDriverLicenses1
            // 
            this.ctrlDriverLicenses1.Location = new System.Drawing.Point(-8, 436);
            this.ctrlDriverLicenses1.Name = "ctrlDriverLicenses1";
            this.ctrlDriverLicenses1.Size = new System.Drawing.Size(1060, 316);
            this.ctrlDriverLicenses1.TabIndex = 134;
            // 
            // btnClose
            // 
            this.btnClose.AutoEllipsis = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1072, 716);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(135, 36);
            this.btnClose.TabIndex = 135;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmShowPersonLicenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 749);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlDriverLicenses1);
            this.Controls.Add(this.ctrPersonCardWithFilter1);
            this.Controls.Add(this.pbPersonImage);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmShowPersonLicenseHistory";
            this.Text = "frmShowPersonLicenseHistory";
            this.Load += new System.EventHandler(this.frmShowPersonLicenseHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPersonImage;
        private System.Windows.Forms.Label lblTitle;
        private People.Controls.ctrPersonCardWithFilter ctrPersonCardWithFilter1;
        private Controls.ctrlDriverLicenses ctrlDriverLicenses1;
        private System.Windows.Forms.Button btnClose;
    }
}