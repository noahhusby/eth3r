namespace Eth3r.Interfaces
{
    partial class Splashcs
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
            this.Splash = new System.Windows.Forms.PictureBox();
            this.Progress = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Splash)).BeginInit();
            this.SuspendLayout();
            // 
            // Splash
            // 
            this.Splash.BackColor = System.Drawing.Color.Turquoise;
            this.Splash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splash.Image = global::Eth3r.Properties.Resources.Splash;
            this.Splash.Location = new System.Drawing.Point(0, 0);
            this.Splash.Name = "Splash";
            this.Splash.Size = new System.Drawing.Size(784, 411);
            this.Splash.TabIndex = 0;
            this.Splash.TabStop = false;
            // 
            // Progress
            // 
            this.Progress.BackColor = System.Drawing.Color.AliceBlue;
            this.Progress.Location = new System.Drawing.Point(0, 405);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(0, 5);
            this.Progress.TabIndex = 1;
            // 
            // Splashcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.Splash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Splashcs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splashcs";
            this.Load += new System.EventHandler(this.Splashcs_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Splashcs_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.Splash)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Splash;
        private System.Windows.Forms.Panel Progress;
    }
}