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

    public partial class SettingsBotton : Form
    {
        public Settings STS = new Settings();
        public SettingsBotton()
        {
            InitializeComponent();
        }

        private void ShowSettings_Click(object sender, EventArgs e)
        {
            STS.Show();
        }
        private void CloseBotton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Exit?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OpenSave.Save(1);
                Application.Exit();
                Environment.Exit(0);
            }
        }
    }
}
