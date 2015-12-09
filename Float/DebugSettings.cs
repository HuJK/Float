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
    public delegate void NewLocation(int NX,int NY);
    public delegate void ActionCtrlEvent(bool Satuation);

    public partial class DebugSettings : Form
    {
        public event NewLocation SetNew;
        public event ActionCtrlEvent ActionCtrl;

        public DebugSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (BoxLX.Text != "" && BoxLY.Text != "")
                    SetNew(Convert.ToInt32(BoxLX.Text), Convert.ToInt32(BoxLY.Text));
                else if (BoxLX.Text == "" && BoxLY.Text != "")
                    SetNew(-1, Convert.ToInt32(BoxLY.Text));
                else if (BoxLX.Text != "" && BoxLY.Text == "")
                    SetNew(Convert.ToInt32(BoxLX.Text), -1);
            }
            if (checkBox2.Checked)
            {
                if (BoxSY.Text != "")
                {
                    MySpeed.Y = Convert.ToDouble(BoxSY.Text);
                }
                if (BoxSX.Text != "")
                {
                    MySpeed.X = Convert.ToDouble(BoxSX.Text);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySpeed.X = 0;
            MySpeed.Y = 0;
            SetNew((WindowSize.X) /2,(WindowSize.Y) /2);
            ActionCtrl(true);
        }

        private void button3_Click(object sender, EventArgs e) { ActionCtrl(false); }

        private void button4_Click(object sender, EventArgs e) { ActionCtrl(true); }

        private void DebugSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            TEMPVaribles.isdebug = false;
        }

        private void DebugSettings_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Print("------------------------------");
        }

    }
}
