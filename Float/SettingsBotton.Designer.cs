namespace Float
{
    partial class SettingsBotton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsBotton));
            this.ShowSettings = new System.Windows.Forms.Button();
            this.CloseBotton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ShowSettings
            // 
            this.ShowSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ShowSettings.BackgroundImage")));
            this.ShowSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ShowSettings.Location = new System.Drawing.Point(0, 0);
            this.ShowSettings.MaximumSize = new System.Drawing.Size(30, 30);
            this.ShowSettings.MinimumSize = new System.Drawing.Size(30, 30);
            this.ShowSettings.Name = "ShowSettings";
            this.ShowSettings.Size = new System.Drawing.Size(30, 30);
            this.ShowSettings.TabIndex = 0;
            this.ShowSettings.UseVisualStyleBackColor = true;
            this.ShowSettings.Click += new System.EventHandler(this.ShowSettings_Click);
            // 
            // CloseBotton
            // 
            this.CloseBotton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseBotton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseBotton.BackgroundImage")));
            this.CloseBotton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CloseBotton.Location = new System.Drawing.Point(150, 0);
            this.CloseBotton.MaximumSize = new System.Drawing.Size(30, 30);
            this.CloseBotton.MinimumSize = new System.Drawing.Size(30, 30);
            this.CloseBotton.Name = "CloseBotton";
            this.CloseBotton.Size = new System.Drawing.Size(30, 30);
            this.CloseBotton.TabIndex = 1;
            this.CloseBotton.UseVisualStyleBackColor = true;
            this.CloseBotton.Click += new System.EventHandler(this.CloseBotton_Click);
            // 
            // SettingsBotton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 30);
            this.Controls.Add(this.CloseBotton);
            this.Controls.Add(this.ShowSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(30, 30);
            this.Name = "SettingsBotton";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SettingsBotton";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ShowSettings;
        private System.Windows.Forms.Button CloseBotton;
    }
}