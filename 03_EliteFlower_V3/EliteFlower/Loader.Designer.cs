
namespace EliteFlower
{
    partial class Loader
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
            this.pcLoader = new System.Windows.Forms.PictureBox();
            this.lblLoader = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcLoader)).BeginInit();
            this.SuspendLayout();
            // 
            // pcLoader
            // 
            this.pcLoader.Image = global::EliteFlower.Properties.Resources.Blocks_1s_200px;
            this.pcLoader.Location = new System.Drawing.Point(120, 58);
            this.pcLoader.Name = "pcLoader";
            this.pcLoader.Size = new System.Drawing.Size(200, 200);
            this.pcLoader.TabIndex = 0;
            this.pcLoader.TabStop = false;
            // 
            // lblLoader
            // 
            this.lblLoader.AutoSize = true;
            this.lblLoader.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.lblLoader.Location = new System.Drawing.Point(90, 289);
            this.lblLoader.Name = "lblLoader";
            this.lblLoader.Size = new System.Drawing.Size(262, 73);
            this.lblLoader.TabIndex = 1;
            this.lblLoader.Text = "Loading";
            // 
            // Loader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 480);
            this.Controls.Add(this.lblLoader);
            this.Controls.Add(this.pcLoader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Loader";
            this.Opacity = 0.9D;
            this.Text = "Loader";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Loader_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcLoader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcLoader;
        private System.Windows.Forms.Label lblLoader;
    }
}