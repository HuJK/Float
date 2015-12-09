namespace Float
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MoveMe = new System.Windows.Forms.Timer(this.components);
            this.BackGroundLU = new System.Windows.Forms.ImageList(this.components);
            this.BackGroundRU = new System.Windows.Forms.ImageList(this.components);
            this.BackGroundLD = new System.Windows.Forms.ImageList(this.components);
            this.BackGroundRD = new System.Windows.Forms.ImageList(this.components);
            this.BackGroundUp = new System.Windows.Forms.ImageList(this.components);
            this.BackGroundDn = new System.Windows.Forms.ImageList(this.components);
            this.MainLabel = new System.Windows.Forms.Label();
            this.LeftTimeCount = new System.Windows.Forms.Timer(this.components);
            this.TargetDayLabel = new System.Windows.Forms.Label();
            this.MaxLabel = new System.Windows.Forms.Label();
            this.AfterLabel = new System.Windows.Forms.Label();
            this.TargetTimeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MoveMe
            // 
            this.MoveMe.Enabled = true;
            this.MoveMe.Interval = 10;
            this.MoveMe.Tick += new System.EventHandler(this.MoveMe_Tick);
            // 
            // BackGroundLU
            // 
            this.BackGroundLU.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("BackGroundLU.ImageStream")));
            this.BackGroundLU.TransparentColor = System.Drawing.Color.Transparent;
            this.BackGroundLU.Images.SetKeyName(0, "Background_LU.png");
            // 
            // BackGroundRU
            // 
            this.BackGroundRU.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("BackGroundRU.ImageStream")));
            this.BackGroundRU.TransparentColor = System.Drawing.Color.Transparent;
            this.BackGroundRU.Images.SetKeyName(0, "Background_RU.png");
            // 
            // BackGroundLD
            // 
            this.BackGroundLD.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("BackGroundLD.ImageStream")));
            this.BackGroundLD.TransparentColor = System.Drawing.Color.Transparent;
            this.BackGroundLD.Images.SetKeyName(0, "Background_LD.png");
            // 
            // BackGroundRD
            // 
            this.BackGroundRD.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("BackGroundRD.ImageStream")));
            this.BackGroundRD.TransparentColor = System.Drawing.Color.Transparent;
            this.BackGroundRD.Images.SetKeyName(0, "Background_RD.png");
            // 
            // BackGroundUp
            // 
            this.BackGroundUp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("BackGroundUp.ImageStream")));
            this.BackGroundUp.TransparentColor = System.Drawing.Color.Transparent;
            this.BackGroundUp.Images.SetKeyName(0, "Background_Up.png");
            // 
            // BackGroundDn
            // 
            this.BackGroundDn.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("BackGroundDn.ImageStream")));
            this.BackGroundDn.TransparentColor = System.Drawing.Color.Transparent;
            this.BackGroundDn.Images.SetKeyName(0, "Background_Dn.png");
            // 
            // MainLabel
            // 
            this.MainLabel.AutoSize = true;
            this.MainLabel.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MainLabel.ForeColor = System.Drawing.Color.Red;
            this.MainLabel.Location = new System.Drawing.Point(0, 0);
            this.MainLabel.Name = "MainLabel";
            this.MainLabel.Size = new System.Drawing.Size(154, 31);
            this.MainLabel.TabIndex = 0;
            this.MainLabel.Text = "Initializing...";
            this.MainLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AllWindowDrag_MouseDown);
            // 
            // LeftTimeCount
            // 
            this.LeftTimeCount.Enabled = true;
            this.LeftTimeCount.Interval = 1000;
            this.LeftTimeCount.Tick += new System.EventHandler(this.LeftTimeCount_Tick);
            // 
            // TargetDayLabel
            // 
            this.TargetDayLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetDayLabel.AutoSize = true;
            this.TargetDayLabel.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TargetDayLabel.Location = new System.Drawing.Point(5, 37);
            this.TargetDayLabel.Name = "TargetDayLabel";
            this.TargetDayLabel.Size = new System.Drawing.Size(171, 20);
            this.TargetDayLabel.TabIndex = 1;
            this.TargetDayLabel.Text = "9999/99/99           還剩";
            this.TargetDayLabel.Visible = false;
            this.TargetDayLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AllWindowDrag_MouseDown);
            // 
            // MaxLabel
            // 
            this.MaxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxLabel.AutoSize = true;
            this.MaxLabel.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MaxLabel.ForeColor = System.Drawing.Color.Red;
            this.MaxLabel.Location = new System.Drawing.Point(7, 57);
            this.MaxLabel.Name = "MaxLabel";
            this.MaxLabel.Size = new System.Drawing.Size(63, 35);
            this.MaxLabel.TabIndex = 2;
            this.MaxLabel.Text = "999";
            this.MaxLabel.Visible = false;
            this.MaxLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AllWindowDrag_MouseDown);
            // 
            // AfterLabel
            // 
            this.AfterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AfterLabel.AutoSize = true;
            this.AfterLabel.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AfterLabel.Location = new System.Drawing.Point(61, 70);
            this.AfterLabel.Name = "AfterLabel";
            this.AfterLabel.Size = new System.Drawing.Size(78, 17);
            this.AfterLabel.TabIndex = 3;
            this.AfterLabel.Text = "天 00:00:00";
            this.AfterLabel.Visible = false;
            this.AfterLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AllWindowDrag_MouseDown);
            // 
            // TargetTimeLabel
            // 
            this.TargetTimeLabel.AutoSize = true;
            this.TargetTimeLabel.Location = new System.Drawing.Point(94, 45);
            this.TargetTimeLabel.Name = "TargetTimeLabel";
            this.TargetTimeLabel.Size = new System.Drawing.Size(47, 12);
            this.TargetTimeLabel.TabIndex = 4;
            this.TargetTimeLabel.Text = "99:99:99";
            this.TargetTimeLabel.Visible = false;
            this.TargetTimeLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AllWindowDrag_MouseDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 96);
            this.Controls.Add(this.TargetTimeLabel);
            this.Controls.Add(this.AfterLabel);
            this.Controls.Add(this.MaxLabel);
            this.Controls.Add(this.TargetDayLabel);
            this.Controls.Add(this.MainLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AllWindowDrag_MouseDown);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer MoveMe;
        private System.Windows.Forms.ImageList BackGroundLU;
        private System.Windows.Forms.ImageList BackGroundRU;
        private System.Windows.Forms.ImageList BackGroundLD;
        private System.Windows.Forms.ImageList BackGroundRD;
        private System.Windows.Forms.ImageList BackGroundUp;
        private System.Windows.Forms.ImageList BackGroundDn;
        private System.Windows.Forms.Label MainLabel;
        private System.Windows.Forms.Timer LeftTimeCount;
        private System.Windows.Forms.Label TargetDayLabel;
        private System.Windows.Forms.Label MaxLabel;
        private System.Windows.Forms.Label AfterLabel;
        private System.Windows.Forms.Label TargetTimeLabel;
    }
}

