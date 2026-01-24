namespace DVLD
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.applicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drivingLicensesServicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLocalLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.internationalLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renewDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.releaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRetakeTest = new System.Windows.Forms.ToolStripMenuItem();
            this.manageApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLocalDrivingLicenseApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.internationalDrivingLicenseApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.detainLicensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuManageApplicationTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuManageTestTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPeople = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDrivers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.accountSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCurrentUserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSignOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationsToolStripMenuItem,
            this.mnuPeople,
            this.mnuDrivers,
            this.mnuUsers,
            this.accountSettingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1006, 80);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // applicationsToolStripMenuItem
            // 
            this.applicationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drivingLicensesServicesToolStripMenuItem,
            this.manageApplicationsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.detainLicensesToolStripMenuItem,
            this.toolStripMenuItem4,
            this.mnuManageApplicationTypes,
            this.toolStripMenuItem5,
            this.mnuManageTestTypes});
            this.applicationsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.applicationsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("applicationsToolStripMenuItem.Image")));
            this.applicationsToolStripMenuItem.Name = "applicationsToolStripMenuItem";
            this.applicationsToolStripMenuItem.Size = new System.Drawing.Size(184, 76);
            this.applicationsToolStripMenuItem.Text = "&Applications";
            // 
            // drivingLicensesServicesToolStripMenuItem
            // 
            this.drivingLicensesServicesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDrivingLicenseToolStripMenuItem,
            this.renewDrivingLicenseToolStripMenuItem,
            this.toolStripMenuItem7,
            this.replaceToolStripMenuItem,
            this.toolStripMenuItem6,
            this.releaseToolStripMenuItem,
            this.mnuRetakeTest});
            this.drivingLicensesServicesToolStripMenuItem.Image = global::DVLD.Properties.Resources.DrivingLicensesServices;
            this.drivingLicensesServicesToolStripMenuItem.Name = "drivingLicensesServicesToolStripMenuItem";
            this.drivingLicensesServicesToolStripMenuItem.Size = new System.Drawing.Size(340, 34);
            this.drivingLicensesServicesToolStripMenuItem.Text = "Driving Licenses Services";
            // 
            // newDrivingLicenseToolStripMenuItem
            // 
            this.newDrivingLicenseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLocalLicense,
            this.internationalLicenseToolStripMenuItem});
            this.newDrivingLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.NewDrivingLicense;
            this.newDrivingLicenseToolStripMenuItem.Name = "newDrivingLicenseToolStripMenuItem";
            this.newDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(476, 34);
            this.newDrivingLicenseToolStripMenuItem.Text = "&New Driving License";
            // 
            // mnuLocalLicense
            // 
            this.mnuLocalLicense.Name = "mnuLocalLicense";
            this.mnuLocalLicense.Size = new System.Drawing.Size(288, 34);
            this.mnuLocalLicense.Text = "Local License";
            this.mnuLocalLicense.Click += new System.EventHandler(this.mnuLocalLicense_Click);
            // 
            // internationalLicenseToolStripMenuItem
            // 
            this.internationalLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.International;
            this.internationalLicenseToolStripMenuItem.Name = "internationalLicenseToolStripMenuItem";
            this.internationalLicenseToolStripMenuItem.Size = new System.Drawing.Size(288, 34);
            this.internationalLicenseToolStripMenuItem.Text = "International license";
            // 
            // renewDrivingLicenseToolStripMenuItem
            // 
            this.renewDrivingLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.RenewDrivingLicense;
            this.renewDrivingLicenseToolStripMenuItem.Name = "renewDrivingLicenseToolStripMenuItem";
            this.renewDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(476, 34);
            this.renewDrivingLicenseToolStripMenuItem.Text = "Renew Driving License";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(473, 6);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Image = global::DVLD.Properties.Resources.ReplacementDrivingLicense;
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(476, 34);
            this.replaceToolStripMenuItem.Text = "Replacement For Lost Or Damaged License";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(473, 6);
            // 
            // releaseToolStripMenuItem
            // 
            this.releaseToolStripMenuItem.Image = global::DVLD.Properties.Resources.DetainedDrivingLicense;
            this.releaseToolStripMenuItem.Name = "releaseToolStripMenuItem";
            this.releaseToolStripMenuItem.Size = new System.Drawing.Size(476, 34);
            this.releaseToolStripMenuItem.Text = "Release Detained Driving License";
            // 
            // mnuRetakeTest
            // 
            this.mnuRetakeTest.Image = global::DVLD.Properties.Resources.Test;
            this.mnuRetakeTest.Name = "mnuRetakeTest";
            this.mnuRetakeTest.Size = new System.Drawing.Size(476, 34);
            this.mnuRetakeTest.Text = "Retake Test";
            this.mnuRetakeTest.Click += new System.EventHandler(this.mnuRetakeTest_Click);
            // 
            // manageApplicationsToolStripMenuItem
            // 
            this.manageApplicationsToolStripMenuItem.Name = "manageApplicationsToolStripMenuItem";
            this.manageApplicationsToolStripMenuItem.Size = new System.Drawing.Size(337, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLocalDrivingLicenseApplications,
            this.internationalDrivingLicenseApplicationsToolStripMenuItem});
            this.toolStripMenuItem2.Image = global::DVLD.Properties.Resources.ManageApplications;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(340, 34);
            this.toolStripMenuItem2.Text = "Manage Applications";
            // 
            // mnuLocalDrivingLicenseApplications
            // 
            this.mnuLocalDrivingLicenseApplications.Image = global::DVLD.Properties.Resources.Local;
            this.mnuLocalDrivingLicenseApplications.Name = "mnuLocalDrivingLicenseApplications";
            this.mnuLocalDrivingLicenseApplications.Size = new System.Drawing.Size(470, 34);
            this.mnuLocalDrivingLicenseApplications.Text = "Local Driving License Applications";
            this.mnuLocalDrivingLicenseApplications.Click += new System.EventHandler(this.mnuLocalDrivingLicenseApplications_Click);
            // 
            // internationalDrivingLicenseApplicationsToolStripMenuItem
            // 
            this.internationalDrivingLicenseApplicationsToolStripMenuItem.Image = global::DVLD.Properties.Resources.International;
            this.internationalDrivingLicenseApplicationsToolStripMenuItem.Name = "internationalDrivingLicenseApplicationsToolStripMenuItem";
            this.internationalDrivingLicenseApplicationsToolStripMenuItem.Size = new System.Drawing.Size(470, 34);
            this.internationalDrivingLicenseApplicationsToolStripMenuItem.Text = "International Driving License Applications";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(337, 6);
            // 
            // detainLicensesToolStripMenuItem
            // 
            this.detainLicensesToolStripMenuItem.Name = "detainLicensesToolStripMenuItem";
            this.detainLicensesToolStripMenuItem.Size = new System.Drawing.Size(340, 34);
            this.detainLicensesToolStripMenuItem.Text = "Detain Licenses";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(337, 6);
            // 
            // mnuManageApplicationTypes
            // 
            this.mnuManageApplicationTypes.Image = global::DVLD.Properties.Resources.ManageApplicationTypes;
            this.mnuManageApplicationTypes.Name = "mnuManageApplicationTypes";
            this.mnuManageApplicationTypes.Size = new System.Drawing.Size(340, 34);
            this.mnuManageApplicationTypes.Text = "Manage Application &Types";
            this.mnuManageApplicationTypes.Click += new System.EventHandler(this.mnuManageApplicationTypes_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(337, 6);
            // 
            // mnuManageTestTypes
            // 
            this.mnuManageTestTypes.Image = global::DVLD.Properties.Resources.ManageTestTypes;
            this.mnuManageTestTypes.Name = "mnuManageTestTypes";
            this.mnuManageTestTypes.Size = new System.Drawing.Size(340, 34);
            this.mnuManageTestTypes.Text = "Manage Test Types";
            this.mnuManageTestTypes.Click += new System.EventHandler(this.mnuManageTestTypes_Click);
            // 
            // mnuPeople
            // 
            this.mnuPeople.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.mnuPeople.Image = ((System.Drawing.Image)(resources.GetObject("mnuPeople.Image")));
            this.mnuPeople.Name = "mnuPeople";
            this.mnuPeople.Size = new System.Drawing.Size(135, 76);
            this.mnuPeople.Text = "&People";
            this.mnuPeople.Click += new System.EventHandler(this.mnuPeople_Click);
            // 
            // mnuDrivers
            // 
            this.mnuDrivers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.mnuDrivers.Image = ((System.Drawing.Image)(resources.GetObject("mnuDrivers.Image")));
            this.mnuDrivers.Name = "mnuDrivers";
            this.mnuDrivers.Size = new System.Drawing.Size(138, 76);
            this.mnuDrivers.Text = "&Drivers";
            this.mnuDrivers.Click += new System.EventHandler(this.mnuDrivers_Click);
            // 
            // mnuUsers
            // 
            this.mnuUsers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.mnuUsers.Image = ((System.Drawing.Image)(resources.GetObject("mnuUsers.Image")));
            this.mnuUsers.Name = "mnuUsers";
            this.mnuUsers.Size = new System.Drawing.Size(124, 76);
            this.mnuUsers.Text = "&Users";
            this.mnuUsers.Click += new System.EventHandler(this.mnuUsers_Click);
            // 
            // accountSettingsToolStripMenuItem
            // 
            this.accountSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCurrentUserInfo,
            this.mnuChangePassword,
            this.toolStripMenuItem1,
            this.mnuSignOut});
            this.accountSettingsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.accountSettingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("accountSettingsToolStripMenuItem.Image")));
            this.accountSettingsToolStripMenuItem.Name = "accountSettingsToolStripMenuItem";
            this.accountSettingsToolStripMenuItem.Size = new System.Drawing.Size(223, 76);
            this.accountSettingsToolStripMenuItem.Text = "Account &Settings";
            // 
            // mnuCurrentUserInfo
            // 
            this.mnuCurrentUserInfo.Image = global::DVLD.Properties.Resources.Man;
            this.mnuCurrentUserInfo.Name = "mnuCurrentUserInfo";
            this.mnuCurrentUserInfo.Size = new System.Drawing.Size(263, 34);
            this.mnuCurrentUserInfo.Text = "Current User Info";
            this.mnuCurrentUserInfo.Click += new System.EventHandler(this.mnuCurrentUserInfo_Click);
            // 
            // mnuChangePassword
            // 
            this.mnuChangePassword.Image = global::DVLD.Properties.Resources.ChangePassword;
            this.mnuChangePassword.Name = "mnuChangePassword";
            this.mnuChangePassword.Size = new System.Drawing.Size(263, 34);
            this.mnuChangePassword.Text = "Change Password";
            this.mnuChangePassword.Click += new System.EventHandler(this.mnuChangePassword_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(260, 6);
            // 
            // mnuSignOut
            // 
            this.mnuSignOut.Image = global::DVLD.Properties.Resources.SignOut;
            this.mnuSignOut.Name = "mnuSignOut";
            this.mnuSignOut.Size = new System.Drawing.Size(263, 34);
            this.mnuSignOut.Text = "Sign Out";
            this.mnuSignOut.Click += new System.EventHandler(this.mnuSignOut_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 578);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem applicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuPeople;
        private System.Windows.Forms.ToolStripMenuItem mnuDrivers;
        private System.Windows.Forms.ToolStripMenuItem mnuUsers;
        private System.Windows.Forms.ToolStripMenuItem accountSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuCurrentUserInfo;
        private System.Windows.Forms.ToolStripMenuItem mnuChangePassword;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuSignOut;
        private System.Windows.Forms.ToolStripMenuItem drivingLicensesServicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator manageApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem detainLicensesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuManageApplicationTypes;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuManageTestTypes;
        private System.Windows.Forms.ToolStripMenuItem newDrivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renewDrivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem releaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuRetakeTest;
        private System.Windows.Forms.ToolStripMenuItem mnuLocalLicense;
        private System.Windows.Forms.ToolStripMenuItem internationalLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuLocalDrivingLicenseApplications;
        private System.Windows.Forms.ToolStripMenuItem internationalDrivingLicenseApplicationsToolStripMenuItem;
    }
}