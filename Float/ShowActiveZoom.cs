using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Float
{
    public partial class ShowActiveZoom : Form
    {
        public ShowActiveZoom()
        {
            InitializeComponent();
        }
        public Color RangColor = Color.Red;
        int countreprint = 0;

        Graphics g;// = this.CreateGraphics();

        private void ShowActiveZoom_Load(object sender, EventArgs e)
        {
            this.Width = SettingValues.Range * 2;
            this.Height = SettingValues.Range * 2;
            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;
            g = this.CreateGraphics();
            this.TopMost = true;
        }

        private void ShowActiveZoom_Paint(object sender, PaintEventArgs e)
        {
            g = this.CreateGraphics();
            Pen p = new Pen(RangColor);
            g.DrawEllipse(p, 0, 0, this.Size.Width - 1, this.Size.Height - 1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (TEMPVaribles.israngechanged)
            {
                TEMPVaribles.israngechanged = false;
                this.Width = SettingValues.Range * 2;
                this.Height = SettingValues.Range * 2;
                this.BackColor = Color.White;
                this.TransparencyKey = Color.White;
                this.TopMost = true;
#if DEBUG
                System.Diagnostics.Debug.Print(this.Width.ToString() + "," + this.Height.ToString());
#endif

                if (RangColor == Color.Red &&this.Visible == true)
                {
                    this.Width = SettingValues.Range * 2;
                    this.Height = SettingValues.Range * 2;
                    Bitmap BG = new Bitmap(this.Width, this.Height);
                    Pen p = new Pen(RangColor);
                    g = Graphics.FromImage(BG);
                    g.DrawEllipse(p, 0, 0, SettingValues.Range * 2 - 1, SettingValues.Range * 2 - 1);
                    this.BackgroundImage = BG;
                }
            }
        }

        private void ShowActiveZoom_Move(object sender, EventArgs e)
        {

                if (countreprint < 100)
                {
                    countreprint++;
                    g = this.CreateGraphics();
                    g.Clear(Color.White);
                    Pen p = new Pen(RangColor);
#if DEBUG
                    System.Diagnostics.Debug.Print("Draw " + countreprint.ToString());
#endif
                    if (RangColor == Color.Red)
                    {
                        this.Width = SettingValues.Range * 2;
                        this.Height = SettingValues.Range * 2;
                        g.DrawEllipse(p, 0, 0, SettingValues.Range * 2 - 1, SettingValues.Range * 2 - 1);
                    }
                    else
                        g.DrawEllipse(p, 0, 0, this.Size.Width - 1, this.Size.Height - 1);
                }
        }

        private void ShowActiveZoom_Resize(object sender, EventArgs e)
        {
            countreprint = 0;
        }
    }
}
