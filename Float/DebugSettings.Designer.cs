namespace Float
{
    partial class DebugSettings
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
            this.RecordList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BoxLX = new System.Windows.Forms.TextBox();
            this.BoxLY = new System.Windows.Forms.TextBox();
            this.BoxSY = new System.Windows.Forms.TextBox();
            this.BoxSX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SetBotton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.ReserBotton = new System.Windows.Forms.Button();
            this.StopBotton = new System.Windows.Forms.Button();
            this.StartBotton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RecordList
            // 
            this.RecordList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RecordList.FormattingEnabled = true;
            this.RecordList.ItemHeight = 12;
            this.RecordList.Location = new System.Drawing.Point(225, 12);
            this.RecordList.Name = "RecordList";
            this.RecordList.Size = new System.Drawing.Size(310, 256);
            this.RecordList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Location:(                      ,                      )";
            // 
            // BoxLX
            // 
            this.BoxLX.Location = new System.Drawing.Point(81, 9);
            this.BoxLX.Name = "BoxLX";
            this.BoxLX.Size = new System.Drawing.Size(56, 22);
            this.BoxLX.TabIndex = 2;
            // 
            // BoxLY
            // 
            this.BoxLY.Location = new System.Drawing.Point(149, 9);
            this.BoxLY.Name = "BoxLY";
            this.BoxLY.Size = new System.Drawing.Size(56, 22);
            this.BoxLY.TabIndex = 3;
            // 
            // BoxSY
            // 
            this.BoxSY.Location = new System.Drawing.Point(149, 37);
            this.BoxSY.Name = "BoxSY";
            this.BoxSY.Size = new System.Drawing.Size(56, 22);
            this.BoxSY.TabIndex = 6;
            // 
            // BoxSX
            // 
            this.BoxSX.Location = new System.Drawing.Point(81, 37);
            this.BoxSX.Name = "BoxSX";
            this.BoxSX.Size = new System.Drawing.Size(56, 22);
            this.BoxSX.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Speed:    (                      ,                      )";
            // 
            // SetBotton
            // 
            this.SetBotton.Location = new System.Drawing.Point(113, 62);
            this.SetBotton.Name = "SetBotton";
            this.SetBotton.Size = new System.Drawing.Size(93, 50);
            this.SetBotton.TabIndex = 7;
            this.SetBotton.Text = "Set";
            this.SetBotton.UseVisualStyleBackColor = true;
            this.SetBotton.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(12, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(12, 40);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 9;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // ReserBotton
            // 
            this.ReserBotton.Location = new System.Drawing.Point(12, 63);
            this.ReserBotton.Name = "ReserBotton";
            this.ReserBotton.Size = new System.Drawing.Size(95, 49);
            this.ReserBotton.TabIndex = 10;
            this.ReserBotton.Text = "Reset";
            this.ReserBotton.UseVisualStyleBackColor = true;
            this.ReserBotton.Click += new System.EventHandler(this.button2_Click);
            // 
            // StopBotton
            // 
            this.StopBotton.Location = new System.Drawing.Point(15, 118);
            this.StopBotton.Name = "StopBotton";
            this.StopBotton.Size = new System.Drawing.Size(92, 49);
            this.StopBotton.TabIndex = 11;
            this.StopBotton.Text = "Stop";
            this.StopBotton.UseVisualStyleBackColor = true;
            this.StopBotton.Click += new System.EventHandler(this.button3_Click);
            // 
            // StartBotton
            // 
            this.StartBotton.Location = new System.Drawing.Point(111, 118);
            this.StartBotton.Name = "StartBotton";
            this.StartBotton.Size = new System.Drawing.Size(95, 49);
            this.StartBotton.TabIndex = 12;
            this.StartBotton.Text = "Start";
            this.StartBotton.UseVisualStyleBackColor = true;
            this.StartBotton.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(190, 29);
            this.button1.TabIndex = 13;
            this.button1.Text = "DebugAddDividing line";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // DebugSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 287);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.StartBotton);
            this.Controls.Add(this.StopBotton);
            this.Controls.Add(this.ReserBotton);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.SetBotton);
            this.Controls.Add(this.BoxSY);
            this.Controls.Add(this.BoxSX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BoxLY);
            this.Controls.Add(this.BoxLX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RecordList);
            this.MinimumSize = new System.Drawing.Size(16, 107);
            this.Name = "DebugSettings";
            this.Text = "DebugSettings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugSettings_FormClosing);
            this.Load += new System.EventHandler(this.DebugSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox BoxLX;
        private System.Windows.Forms.TextBox BoxLY;
        private System.Windows.Forms.TextBox BoxSY;
        private System.Windows.Forms.TextBox BoxSX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SetBotton;
        public System.Windows.Forms.ListBox RecordList;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button ReserBotton;
        private System.Windows.Forms.Button StopBotton;
        private System.Windows.Forms.Button StartBotton;
        private System.Windows.Forms.Button button1;

    }
}