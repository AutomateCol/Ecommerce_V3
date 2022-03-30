
namespace EliteFlower
{
    partial class AddOn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddOn));
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.chbDelete = new System.Windows.Forms.CheckBox();
            this.chbCreate = new System.Windows.Forms.CheckBox();
            this.lblImg = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.btnOpenCreate = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cbDeleteID = new System.Windows.Forms.ComboBox();
            this.txtCreateID = new System.Windows.Forms.TextBox();
            this.pcbCreate = new System.Windows.Forms.PictureBox();
            this.pcbDelete = new System.Windows.Forms.PictureBox();
            this.helpProv = new System.Windows.Forms.HelpProvider();
            this.gbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbCreate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.chbDelete);
            this.gbOptions.Controls.Add(this.chbCreate);
            this.gbOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOptions.Location = new System.Drawing.Point(52, 22);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(200, 110);
            this.gbOptions.TabIndex = 34;
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
            // lblImg
            // 
            this.lblImg.AutoSize = true;
            this.lblImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImg.Location = new System.Drawing.Point(48, 210);
            this.lblImg.Name = "lblImg";
            this.lblImg.Size = new System.Drawing.Size(54, 20);
            this.lblImg.TabIndex = 50;
            this.lblImg.Text = "Image";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(48, 156);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(26, 20);
            this.lblID.TabIndex = 49;
            this.lblID.Text = "ID";
            // 
            // btnOpenCreate
            // 
            this.btnOpenCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenCreate.Location = new System.Drawing.Point(115, 205);
            this.btnOpenCreate.Name = "btnOpenCreate";
            this.btnOpenCreate.Size = new System.Drawing.Size(137, 30);
            this.btnOpenCreate.TabIndex = 51;
            this.btnOpenCreate.Text = "Open";
            this.btnOpenCreate.UseVisualStyleBackColor = true;
            this.btnOpenCreate.Click += new System.EventHandler(this.btnOpenCreate_Click);
            this.btnOpenCreate.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnOpenCreate_HelpRequested);
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(115, 260);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(137, 30);
            this.btnCreate.TabIndex = 52;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            this.btnCreate.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnCreate_HelpRequested);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(115, 260);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(137, 30);
            this.btnDelete.TabIndex = 53;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnDelete.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnDelete_HelpRequested);
            // 
            // cbDeleteID
            // 
            this.cbDeleteID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeleteID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDeleteID.FormattingEnabled = true;
            this.cbDeleteID.Location = new System.Drawing.Point(152, 154);
            this.cbDeleteID.Name = "cbDeleteID";
            this.cbDeleteID.Size = new System.Drawing.Size(100, 28);
            this.cbDeleteID.TabIndex = 54;
            this.cbDeleteID.SelectedIndexChanged += new System.EventHandler(this.cbDeleteID_SelectedIndexChanged);
            this.cbDeleteID.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.cbDeleteID_HelpRequested);
            // 
            // txtCreateID
            // 
            this.txtCreateID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreateID.Location = new System.Drawing.Point(152, 154);
            this.txtCreateID.Name = "txtCreateID";
            this.txtCreateID.Size = new System.Drawing.Size(100, 26);
            this.txtCreateID.TabIndex = 55;
            this.txtCreateID.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.txtCreateID_HelpRequested);
            // 
            // pcbCreate
            // 
            this.pcbCreate.InitialImage = global::EliteFlower.Properties.Resources.NoAddOn;
            this.pcbCreate.Location = new System.Drawing.Point(266, 22);
            this.pcbCreate.Name = "pcbCreate";
            this.pcbCreate.Size = new System.Drawing.Size(340, 340);
            this.pcbCreate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbCreate.TabIndex = 48;
            this.pcbCreate.TabStop = false;
            this.pcbCreate.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.pcbCreate_HelpRequested);
            // 
            // pcbDelete
            // 
            this.pcbDelete.InitialImage = global::EliteFlower.Properties.Resources.NoAddOn;
            this.pcbDelete.Location = new System.Drawing.Point(266, 22);
            this.pcbDelete.Name = "pcbDelete";
            this.pcbDelete.Size = new System.Drawing.Size(340, 340);
            this.pcbDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbDelete.TabIndex = 37;
            this.pcbDelete.TabStop = false;
            this.pcbDelete.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.pcbDelete_HelpRequested);
            // 
            // AddOn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 381);
            this.Controls.Add(this.cbDeleteID);
            this.Controls.Add(this.btnOpenCreate);
            this.Controls.Add(this.lblImg);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.pcbDelete);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.txtCreateID);
            this.Controls.Add(this.pcbCreate);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnDelete);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddOn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddOn";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.AddOn_HelpButtonClicked);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddOn_FormClosed);
            this.Load += new System.EventHandler(this.AddOn_Load);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbCreate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbDelete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox chbDelete;
        private System.Windows.Forms.CheckBox chbCreate;
        private System.Windows.Forms.PictureBox pcbDelete;
        private System.Windows.Forms.PictureBox pcbCreate;
        private System.Windows.Forms.Label lblImg;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button btnOpenCreate;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cbDeleteID;
        private System.Windows.Forms.TextBox txtCreateID;
        private System.Windows.Forms.HelpProvider helpProv;
    }
}