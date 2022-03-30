
namespace EliteFlower
{
    partial class AddPackage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPackage));
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.chbDelete = new System.Windows.Forms.CheckBox();
            this.chbCreate = new System.Windows.Forms.CheckBox();
            this.cbDeletePackage = new System.Windows.Forms.ComboBox();
            this.lblID = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtCreatePackage = new System.Windows.Forms.TextBox();
            this.helpProv = new System.Windows.Forms.HelpProvider();
            this.gbOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.chbDelete);
            this.gbOptions.Controls.Add(this.chbCreate);
            this.gbOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOptions.Location = new System.Drawing.Point(35, 31);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(200, 110);
            this.gbOptions.TabIndex = 35;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            this.gbOptions.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.gbOptions_HelpRequested);
            // 
            // chbDelete
            // 
            this.chbDelete.AutoSize = true;
            this.chbDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbDelete.Location = new System.Drawing.Point(63, 65);
            this.chbDelete.Name = "chbDelete";
            this.chbDelete.Size = new System.Drawing.Size(77, 24);
            this.chbDelete.TabIndex = 2;
            this.chbDelete.Text = "Delete";
            this.chbDelete.UseVisualStyleBackColor = true;
            this.chbDelete.CheckedChanged += new System.EventHandler(this.chbDelete_CheckedChanged);
            // 
            // chbCreate
            // 
            this.chbCreate.AutoSize = true;
            this.chbCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbCreate.Location = new System.Drawing.Point(63, 35);
            this.chbCreate.Name = "chbCreate";
            this.chbCreate.Size = new System.Drawing.Size(78, 24);
            this.chbCreate.TabIndex = 0;
            this.chbCreate.Text = "Create";
            this.chbCreate.UseVisualStyleBackColor = true;
            this.chbCreate.CheckedChanged += new System.EventHandler(this.chbCreate_CheckedChanged);
            // 
            // cbDeletePackage
            // 
            this.cbDeletePackage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeletePackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDeletePackage.FormattingEnabled = true;
            this.cbDeletePackage.Location = new System.Drawing.Point(329, 48);
            this.cbDeletePackage.Name = "cbDeletePackage";
            this.cbDeletePackage.Size = new System.Drawing.Size(137, 28);
            this.cbDeletePackage.TabIndex = 56;
            this.cbDeletePackage.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.cbDeletePackage_HelpRequested);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(246, 51);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(71, 20);
            this.lblID.TabIndex = 55;
            this.lblID.Text = "Package";
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(329, 111);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(137, 30);
            this.btnCreate.TabIndex = 57;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            this.btnCreate.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnCreate_HelpRequested);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(329, 111);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(137, 30);
            this.btnDelete.TabIndex = 58;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnDelete.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnDelete_HelpRequested);
            // 
            // txtCreatePackage
            // 
            this.txtCreatePackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreatePackage.Location = new System.Drawing.Point(329, 48);
            this.txtCreatePackage.Name = "txtCreatePackage";
            this.txtCreatePackage.Size = new System.Drawing.Size(137, 26);
            this.txtCreatePackage.TabIndex = 59;
            this.txtCreatePackage.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.txtCreatePackage_HelpRequested);
            // 
            // AddPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 182);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cbDeletePackage);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.txtCreatePackage);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddPackage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddPackage";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.AddPackage_HelpButtonClicked);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddPackage_FormClosed);
            this.Load += new System.EventHandler(this.AddPackage_Load);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox chbDelete;
        private System.Windows.Forms.CheckBox chbCreate;
        private System.Windows.Forms.ComboBox cbDeletePackage;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtCreatePackage;
        private System.Windows.Forms.HelpProvider helpProv;
    }
}