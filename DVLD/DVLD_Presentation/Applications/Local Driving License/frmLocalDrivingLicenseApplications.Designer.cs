namespace DVLD.Applications.Local_Driving_License_Applications
{
    partial class frmLocalDrivingLicenseApplications
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocalDrivingLicenseApplications));
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvLocalDrivingLicenseApplications = new System.Windows.Forms.DataGridView();
            this.cmsLocalApplications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuShowApplicationDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDeleteApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCancelApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuScheduleTests = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScheduleVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSechduleWrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSechduleStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuIssueDrivingLicenseFirstTime = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.cbFilters = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddNewApplication = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalDrivingLicenseApplications)).BeginInit();
            this.cmsLocalApplications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(96, 554);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(16, 17);
            this.lblRecordsCount.TabIndex = 46;
            this.lblRecordsCount.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 554);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 17);
            this.label2.TabIndex = 45;
            this.label2.Text = "# Records:";
            // 
            // dgvLocalDrivingLicenseApplications
            // 
            this.dgvLocalDrivingLicenseApplications.AllowUserToAddRows = false;
            this.dgvLocalDrivingLicenseApplications.AllowUserToDeleteRows = false;
            this.dgvLocalDrivingLicenseApplications.AllowUserToOrderColumns = true;
            this.dgvLocalDrivingLicenseApplications.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLocalDrivingLicenseApplications.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalDrivingLicenseApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalDrivingLicenseApplications.ContextMenuStrip = this.cmsLocalApplications;
            this.dgvLocalDrivingLicenseApplications.Location = new System.Drawing.Point(12, 240);
            this.dgvLocalDrivingLicenseApplications.Name = "dgvLocalDrivingLicenseApplications";
            this.dgvLocalDrivingLicenseApplications.ReadOnly = true;
            this.dgvLocalDrivingLicenseApplications.RowHeadersWidth = 62;
            this.dgvLocalDrivingLicenseApplications.Size = new System.Drawing.Size(888, 299);
            this.dgvLocalDrivingLicenseApplications.TabIndex = 44;
            // 
            // cmsLocalApplications
            // 
            this.cmsLocalApplications.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsLocalApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowApplicationDetails,
            this.toolStripMenuItem1,
            this.mnuDeleteApplication,
            this.mnuCancelApplication,
            this.toolStripMenuItem2,
            this.mnuScheduleTests,
            this.toolStripMenuItem3,
            this.mnuIssueDrivingLicenseFirstTime,
            this.toolStripMenuItem4,
            this.mnuShowLicense,
            this.toolStripMenuItem5,
            this.mnuShowPersonLicenseHistory});
            this.cmsLocalApplications.Name = "contextMenuStrip1";
            this.cmsLocalApplications.Size = new System.Drawing.Size(255, 244);
            this.cmsLocalApplications.Opening += new System.ComponentModel.CancelEventHandler(this.cmsLocalApplications_Opening);
            // 
            // mnuShowApplicationDetails
            // 
            this.mnuShowApplicationDetails.Image = global::DVLD.Properties.Resources.DetailsInfo;
            this.mnuShowApplicationDetails.Name = "mnuShowApplicationDetails";
            this.mnuShowApplicationDetails.Size = new System.Drawing.Size(254, 30);
            this.mnuShowApplicationDetails.Text = "Show Application Details";
            this.mnuShowApplicationDetails.Click += new System.EventHandler(this.mnuShowApplicationDetails_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(251, 6);
            // 
            // mnuDeleteApplication
            // 
            this.mnuDeleteApplication.Image = global::DVLD.Properties.Resources.Delete;
            this.mnuDeleteApplication.Name = "mnuDeleteApplication";
            this.mnuDeleteApplication.Size = new System.Drawing.Size(254, 30);
            this.mnuDeleteApplication.Text = "Delete Application";
            this.mnuDeleteApplication.Click += new System.EventHandler(this.mnuDeleteApplication_Click);
            // 
            // mnuCancelApplication
            // 
            this.mnuCancelApplication.Image = global::DVLD.Properties.Resources.Cancel;
            this.mnuCancelApplication.Name = "mnuCancelApplication";
            this.mnuCancelApplication.Size = new System.Drawing.Size(254, 30);
            this.mnuCancelApplication.Text = "Cancel Application";
            this.mnuCancelApplication.Click += new System.EventHandler(this.mnuCancelApplication_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(251, 6);
            // 
            // mnuScheduleTests
            // 
            this.mnuScheduleTests.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuScheduleVisionTest,
            this.mnuSechduleWrittenTest,
            this.mnuSechduleStreetTest});
            this.mnuScheduleTests.Image = ((System.Drawing.Image)(resources.GetObject("mnuScheduleTests.Image")));
            this.mnuScheduleTests.Name = "mnuScheduleTests";
            this.mnuScheduleTests.Size = new System.Drawing.Size(254, 30);
            this.mnuScheduleTests.Text = "Schedule Tests";
            // 
            // mnuScheduleVisionTest
            // 
            this.mnuScheduleVisionTest.Image = global::DVLD.Properties.Resources.VisionTest;
            this.mnuScheduleVisionTest.Name = "mnuScheduleVisionTest";
            this.mnuScheduleVisionTest.Size = new System.Drawing.Size(188, 22);
            this.mnuScheduleVisionTest.Text = "Schedule Vision Test";
            this.mnuScheduleVisionTest.Click += new System.EventHandler(this.mnuScheduleVisionTest_Click);
            // 
            // mnuSechduleWrittenTest
            // 
            this.mnuSechduleWrittenTest.Image = global::DVLD.Properties.Resources.WrittenTest;
            this.mnuSechduleWrittenTest.Name = "mnuSechduleWrittenTest";
            this.mnuSechduleWrittenTest.Size = new System.Drawing.Size(188, 22);
            this.mnuSechduleWrittenTest.Text = "Schedule Written Test";
            this.mnuSechduleWrittenTest.Click += new System.EventHandler(this.mnuSechduleWrittenTest_Click);
            // 
            // mnuSechduleStreetTest
            // 
            this.mnuSechduleStreetTest.Image = global::DVLD.Properties.Resources.StreetTest;
            this.mnuSechduleStreetTest.Name = "mnuSechduleStreetTest";
            this.mnuSechduleStreetTest.Size = new System.Drawing.Size(188, 22);
            this.mnuSechduleStreetTest.Text = "Schedule Street Test";
            this.mnuSechduleStreetTest.Click += new System.EventHandler(this.mnuSechduleStreetTest_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(251, 6);
            // 
            // mnuIssueDrivingLicenseFirstTime
            // 
            this.mnuIssueDrivingLicenseFirstTime.Enabled = false;
            this.mnuIssueDrivingLicenseFirstTime.Image = global::DVLD.Properties.Resources.GraduateLicense;
            this.mnuIssueDrivingLicenseFirstTime.Name = "mnuIssueDrivingLicenseFirstTime";
            this.mnuIssueDrivingLicenseFirstTime.Size = new System.Drawing.Size(254, 30);
            this.mnuIssueDrivingLicenseFirstTime.Text = "Issue Driving License (First Time)";
            this.mnuIssueDrivingLicenseFirstTime.Click += new System.EventHandler(this.mnuIssueDrivingLicenseFirstTime_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(251, 6);
            // 
            // mnuShowLicense
            // 
            this.mnuShowLicense.Enabled = false;
            this.mnuShowLicense.Image = global::DVLD.Properties.Resources.DriverLicense;
            this.mnuShowLicense.Name = "mnuShowLicense";
            this.mnuShowLicense.Size = new System.Drawing.Size(254, 30);
            this.mnuShowLicense.Text = "Show License";
            this.mnuShowLicense.Click += new System.EventHandler(this.mnuShowLicense_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(251, 6);
            // 
            // mnuShowPersonLicenseHistory
            // 
            this.mnuShowPersonLicenseHistory.Image = global::DVLD.Properties.Resources.History;
            this.mnuShowPersonLicenseHistory.Name = "mnuShowPersonLicenseHistory";
            this.mnuShowPersonLicenseHistory.Size = new System.Drawing.Size(254, 30);
            this.mnuShowPersonLicenseHistory.Text = "Show Person License History";
            this.mnuShowPersonLicenseHistory.Click += new System.EventHandler(this.mnuShowPersonLicenseHistory_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(262, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(377, 26);
            this.label1.TabIndex = 43;
            this.label1.Text = "Local Driving License Applications";
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.Location = new System.Drawing.Point(257, 212);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(237, 23);
            this.txtFilterValue.TabIndex = 50;
            this.txtFilterValue.Visible = false;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // cbFilters
            // 
            this.cbFilters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilters.FormattingEnabled = true;
            this.cbFilters.Items.AddRange(new object[] {
            "None",
            "L.D.L.AppID",
            "National No",
            "Full Name",
            "Status"});
            this.cbFilters.Location = new System.Drawing.Point(72, 211);
            this.cbFilters.Name = "cbFilters";
            this.cbFilters.Size = new System.Drawing.Size(178, 24);
            this.cbFilters.TabIndex = 49;
            this.cbFilters.SelectedIndexChanged += new System.EventHandler(this.cbFilters_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 48;
            this.label3.Text = "Filter By:";
            // 
            // btnAddNewApplication
            // 
            this.btnAddNewApplication.BackgroundImage = global::DVLD.Properties.Resources.AddNewApplication;
            this.btnAddNewApplication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddNewApplication.Location = new System.Drawing.Point(830, 164);
            this.btnAddNewApplication.Name = "btnAddNewApplication";
            this.btnAddNewApplication.Size = new System.Drawing.Size(70, 70);
            this.btnAddNewApplication.TabIndex = 51;
            this.btnAddNewApplication.UseVisualStyleBackColor = true;
            this.btnAddNewApplication.Click += new System.EventHandler(this.btnAddNewApplication_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = global::DVLD.Properties.Resources.Close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(770, 545);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(130, 35);
            this.btnClose.TabIndex = 47;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.LocalApplications;
            this.pictureBox1.Location = new System.Drawing.Point(375, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 42;
            this.pictureBox1.TabStop = false;
            // 
            // frmLocalDrivingLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(913, 598);
            this.Controls.Add(this.btnAddNewApplication);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.cbFilters);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvLocalDrivingLicenseApplications);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLocalDrivingLicenseApplications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Local Driving License Applications";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplications_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalDrivingLicenseApplications)).EndInit();
            this.cmsLocalApplications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvLocalDrivingLicenseApplications;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.ComboBox cbFilters;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddNewApplication;
        private System.Windows.Forms.ContextMenuStrip cmsLocalApplications;
        private System.Windows.Forms.ToolStripMenuItem mnuShowApplicationDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteApplication;
        private System.Windows.Forms.ToolStripMenuItem mnuCancelApplication;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuScheduleTests;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuIssueDrivingLicenseFirstTime;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuShowLicense;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuShowPersonLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem mnuScheduleVisionTest;
        private System.Windows.Forms.ToolStripMenuItem mnuSechduleWrittenTest;
        private System.Windows.Forms.ToolStripMenuItem mnuSechduleStreetTest;
    }
}