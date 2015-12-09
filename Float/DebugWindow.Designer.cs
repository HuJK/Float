namespace Float
{
    partial class DebugWindow
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
            this.infomationlabel = new System.Windows.Forms.Label();
            this.MouseSpeedShow = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // infomationlabel
            // 
            this.infomationlabel.AutoSize = true;
            this.infomationlabel.Location = new System.Drawing.Point(0, 0);
            this.infomationlabel.Name = "infomationlabel";
            this.infomationlabel.Size = new System.Drawing.Size(0, 12);
            this.infomationlabel.TabIndex = 0;
            // 
            // MouseSpeedShow
            // 
            this.MouseSpeedShow.Enabled = true;
            this.MouseSpeedShow.Interval = 10;
            this.MouseSpeedShow.Tick += new System.EventHandler(this.MouseSpeedShow_Tick);
            // 
            // DebugWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.infomationlabel);
            this.Name = "DebugWindow";
            this.Text = "DebugWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugWindow_FormClosing);
            this.Load += new System.EventHandler(this.DebugWindow_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DebugWindow_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DebugWindow_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DebugWindow_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer MouseSpeedShow;
        public System.Windows.Forms.Label infomationlabel;
    }
}