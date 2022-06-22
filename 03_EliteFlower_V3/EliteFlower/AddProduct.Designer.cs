
namespace EliteFlower
{
    partial class AddProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProduct));
            this.cbUpdateID = new System.Windows.Forms.ComboBox();
            this.cbUpdatePack = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cbDeleteID = new System.Windows.Forms.ComboBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.cbCreatePack = new System.Windows.Forms.ComboBox();
            this.pcbDelete = new System.Windows.Forms.PictureBox();
            this.btnOpenCreate = new System.Windows.Forms.Button();
            this.lblImg = new System.Windows.Forms.Label();
            this.pcbUpdate = new System.Windows.Forms.PictureBox();
            this.pcbCreate = new System.Windows.Forms.PictureBox();
            this.lblPack = new System.Windows.Forms.Label();
            this.txtCreateID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.chbUpdate = new System.Windows.Forms.CheckBox();
            this.chbDelete = new System.Windows.Forms.CheckBox();
            this.chbCreate = new System.Windows.Forms.CheckBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.helpProv = new System.Windows.Forms.HelpProvider();
            this.btnOpenUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pcbDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbCreate)).BeginInit();
            this.gbOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbUpdateID
            // 
            this.cbUpdateID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUpdateID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUpdateID.FormattingEnabled = true;
            this.cbUpdateID.Location = new System.Drawing.Point(128, 186);
            this.cbUpdateID.Name = "cbUpdateID";
            this.cbUpdateID.Size = new System.Drawing.Size(111, 28);
            this.cbUpdateID.TabIndex = 40;
            this.cbUpdateID.SelectedIndexChanged += new System.EventHandler(this.cbUpdateID_SelectedIndexChanged);
            this.cbUpdateID.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.cbUpdateID_HelpRequested);
            // 
            // cbUpdatePack
            // 
            this.cbUpdatePack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUpdatePack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUpdatePack.FormattingEnabled = true;
            this.cbUpdatePack.Location = new System.Drawing.Point(128, 232);
            this.cbUpdatePack.Name = "cbUpdatePack";
            this.cbUpdatePack.Size = new System.Drawing.Size(111, 28);
            this.cbUpdatePack.TabIndex = 34;
            this.cbUpdatePack.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.cbUpdatePack_HelpRequested);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(102, 330);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(137, 30);
            this.btnDelete.TabIndex = 39;
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
            this.cbDeleteID.Location = new System.Drawing.Point(139, 186);
            this.cbDeleteID.Name = "cbDeleteID";
            this.cbDeleteID.Size = new System.Drawing.Size(100, 28);
            this.cbDeleteID.TabIndex = 41;
            this.cbDeleteID.SelectedIndexChanged += new System.EventHandler(this.cbDeleteID_SelectedIndexChanged);
            this.cbDeleteID.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.cbDeleteID_HelpRequested);
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(102, 330);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(137, 30);
            this.btnCreate.TabIndex = 49;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            this.btnCreate.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnCreate_HelpRequested);
            // 
            // cbCreatePack
            // 
            this.cbCreatePack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCreatePack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCreatePack.FormattingEnabled = true;
            this.cbCreatePack.Location = new System.Drawing.Point(139, 232);
            this.cbCreatePack.Name = "cbCreatePack";
            this.cbCreatePack.Size = new System.Drawing.Size(100, 28);
            this.cbCreatePack.TabIndex = 46;
            this.cbCreatePack.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.cbCreatePack_HelpRequested);
            // 
            // pcbDelete
            // 
            this.pcbDelete.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcbDelete.InitialImage")));
            this.pcbDelete.Location = new System.Drawing.Point(278, 20);
            this.pcbDelete.Name = "pcbDelete";
            this.pcbDelete.Size = new System.Drawing.Size(340, 340);
            this.pcbDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbDelete.TabIndex = 36;
            this.pcbDelete.TabStop = false;
            // 
            // btnOpenCreate
            // 
            this.btnOpenCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenCreate.Location = new System.Drawing.Point(102, 280);
            this.btnOpenCreate.Name = "btnOpenCreate";
            this.btnOpenCreate.Size = new System.Drawing.Size(137, 30);
            this.btnOpenCreate.TabIndex = 48;
            this.btnOpenCreate.Text = "Open";
            this.btnOpenCreate.UseVisualStyleBackColor = true;
            this.btnOpenCreate.Click += new System.EventHandler(this.btnOpenCreate_Click);
            this.btnOpenCreate.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnOpenCreate_HelpRequested);
            // 
            // lblImg
            // 
            this.lblImg.AutoSize = true;
            this.lblImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImg.Location = new System.Drawing.Point(27, 285);
            this.lblImg.Name = "lblImg";
            this.lblImg.Size = new System.Drawing.Size(54, 20);
            this.lblImg.TabIndex = 45;
            this.lblImg.Text = "Image";
            // 
            // pcbUpdate
            // 
            this.pcbUpdate.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcbUpdate.InitialImage")));
            this.pcbUpdate.Location = new System.Drawing.Point(278, 20);
            this.pcbUpdate.Name = "pcbUpdate";
            this.pcbUpdate.Size = new System.Drawing.Size(340, 340);
            this.pcbUpdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbUpdate.TabIndex = 35;
            this.pcbUpdate.TabStop = false;
            // 
            // pcbCreate
            // 
            this.pcbCreate.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcbCreate.InitialImage")));
            this.pcbCreate.Location = new System.Drawing.Point(278, 20);
            this.pcbCreate.Name = "pcbCreate";
            this.pcbCreate.Size = new System.Drawing.Size(340, 340);
            this.pcbCreate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbCreate.TabIndex = 47;
            this.pcbCreate.TabStop = false;
            // 
            // lblPack
            // 
            this.lblPack.AutoSize = true;
            this.lblPack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPack.Location = new System.Drawing.Point(27, 235);
            this.lblPack.Name = "lblPack";
            this.lblPack.Size = new System.Drawing.Size(71, 20);
            this.lblPack.TabIndex = 44;
            this.lblPack.Text = "Package";
            // 
            // txtCreateID
            // 
            this.txtCreateID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreateID.Location = new System.Drawing.Point(139, 186);
            this.txtCreateID.Name = "txtCreateID";
            this.txtCreateID.Size = new System.Drawing.Size(100, 26);
            this.txtCreateID.TabIndex = 43;
            this.txtCreateID.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.txtCreateID_HelpRequested);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(27, 189);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(26, 20);
            this.lblID.TabIndex = 42;
            this.lblID.Text = "ID";
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.chbUpdate);
            this.gbOptions.Controls.Add(this.chbDelete);
            this.gbOptions.Controls.Add(this.chbCreate);
            this.gbOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOptions.Location = new System.Drawing.Point(31, 20);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(208, 142);
            this.gbOptions.TabIndex = 33;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            this.gbOptions.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.gbOptions_HelpRequested);
            // 
            // chbUpdate
            // 
            this.chbUpdate.AutoSize = true;
            this.chbUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbUpdate.Location = new System.Drawing.Point(71, 67);
            this.chbUpdate.Name = "chbUpdate";
            this.chbUpdate.Size = new System.Drawing.Size(81, 24);
            this.chbUpdate.TabIndex = 1;
            this.chbUpdate.Text = "Update";
            this.chbUpdate.UseVisualStyleBackColor = true;
            this.chbUpdate.CheckedChanged += new System.EventHandler(this.chbUpdate_CheckedChanged);
            // 
            // chbDelete
            // 
            this.chbDelete.AutoSize = true;
            this.chbDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbDelete.Location = new System.Drawing.Point(71, 97);
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
            this.chbCreate.Location = new System.Drawing.Point(71, 37);
            this.chbCreate.Name = "chbCreate";
            this.chbCreate.Size = new System.Drawing.Size(78, 24);
            this.chbCreate.TabIndex = 0;
            this.chbCreate.Text = "Create";
            this.chbCreate.UseVisualStyleBackColor = true;
            this.chbCreate.CheckedChanged += new System.EventHandler(this.chbCreate_CheckedChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(102, 330);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(137, 30);
            this.btnUpdate.TabIndex = 38;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnUpdate.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnUpdate_HelpRequested);
            // 
            // btnOpenUpdate
            // 
            this.btnOpenUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenUpdate.Location = new System.Drawing.Point(102, 280);
            this.btnOpenUpdate.Name = "btnOpenUpdate";
            this.btnOpenUpdate.Size = new System.Drawing.Size(137, 30);
            this.btnOpenUpdate.TabIndex = 50;
            this.btnOpenUpdate.Text = "Open";
            this.btnOpenUpdate.UseVisualStyleBackColor = true;
            this.btnOpenUpdate.Click += new System.EventHandler(this.btnOpenUpdate_Click);
            // 
            // AddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 381);
            this.Controls.Add(this.btnOpenUpdate);
            this.Controls.Add(this.cbUpdateID);
            this.Controls.Add(this.cbUpdatePack);
            this.Controls.Add(this.cbDeleteID);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.cbCreatePack);
            this.Controls.Add(this.pcbDelete);
            this.Controls.Add(this.btnOpenCreate);
            this.Controls.Add(this.lblImg);
            this.Controls.Add(this.pcbUpdate);
            this.Controls.Add(this.pcbCreate);
            this.Controls.Add(this.lblPack);
            this.Controls.Add(this.txtCreateID);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database products";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.AddProduct_HelpButtonClicked);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddProduct_FormClosed);
            this.Load += new System.EventHandler(this.AddProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbCreate)).EndInit();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbUpdateID;
        private System.Windows.Forms.ComboBox cbUpdatePack;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cbDeleteID;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ComboBox cbCreatePack;
        private System.Windows.Forms.PictureBox pcbDelete;
        private System.Windows.Forms.Button btnOpenCreate;
        private System.Windows.Forms.Label lblImg;
        private System.Windows.Forms.PictureBox pcbUpdate;
        private System.Windows.Forms.PictureBox pcbCreate;
        private System.Windows.Forms.Label lblPack;
        private System.Windows.Forms.TextBox txtCreateID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox chbUpdate;
        private System.Windows.Forms.CheckBox chbDelete;
        private System.Windows.Forms.CheckBox chbCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.HelpProvider helpProv;
        private System.Windows.Forms.Button btnOpenUpdate;
    }
}