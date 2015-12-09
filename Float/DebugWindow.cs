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

    public partial class DebugWindow : Form
    {
        public DebugWindow()
        {
            InitializeComponent();
        }
        int CX;
        int CY;
        private void MouseSpeedShow_Tick(object sender, EventArgs e)
        {
            infomation_update();
            if (nowmousedown == false)
            {
                Pen p = new Pen(Color.Blue, 5);
                Pen p2 = new Pen(Color.Blue, 1);
                Graphics g = this.CreateGraphics();
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                g.Clear(Color.White);
                g.DrawLine(p, CX, CY, Convert.ToInt32(MySpeed.X) + CX, Convert.ToInt32(MySpeed.Y) + CY);
                g.DrawEllipse(p2, CX, CY, 1, 1);
                g.Dispose();
                p.Dispose();
            }
        }


        void infomation_update()
        {
            this.infomationlabel.Text =
                "Mouse Location X:" + Convert.ToString(MouseLocation.X) + "\n" +
                "Mouse Location Y:" + Convert.ToString(MouseLocation.Y) + "\n\n" +
                "Mouse Speed X:" + Convert.ToString(MouseSpeed.X) + "\n" +
                "Mouse Speed Y:" + Convert.ToString(MouseSpeed.Y) + "\n\n" +
                "Location X:" + Convert.ToString(FormLocation.X) + "\n" +
                "Location Y:" + Convert.ToString(FormLocation.Y) + "\n\n" +
                "MySpeed X:" + Convert.ToString(MySpeed.X) + "\n" +
                "MySpeed Y:" + Convert.ToString(MySpeed.Y) + "\n";
        }
        bool nowmousedown;


        private void DebugWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (nowmousedown == true)
            {
                Pen p = new Pen(Color.Blue, 5);
                Graphics g = this.CreateGraphics();
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                g.Clear(Color.White);
                g.DrawLine(p, CX, CY, e.X, e.Y);
                g.Dispose();
                p.Dispose();
            }
        }
        private void DebugWindow_MouseDown(object sender, MouseEventArgs e)
        {
            nowmousedown = true;
            CX = e.X;
            CY = e.Y;
        }
        private void DebugWindow_MouseUp(object sender, MouseEventArgs e)
        {
            MySpeed.X = e.X - CX;
            MySpeed.Y = e.Y - CY;
            nowmousedown = false;
        }

        private void DebugWindow_Load(object sender, EventArgs e)
        {
            CX = this.Width / 2;
            CY = this.Width / 2;
        }

        private void DebugWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            TEMPVaribles.isdebug = false;
        }
    }
}
