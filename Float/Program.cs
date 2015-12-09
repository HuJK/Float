using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Drawing;

namespace Float
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SystemEvents.UserPreferenceChanging += new
            UserPreferenceChangingEventHandler(SystemEvents_UserPreferenceChanging);
            SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
            //Program.Open();
            Application.Run(new MainForm());
        }

        static void SystemEvents_UserPreferenceChanging(object sender, UserPreferenceChangingEventArgs e)
        {
            //Console.WriteLine("The user preference is changing. Category={0}", e.Category);
        }
        static void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            WindowSize.X = Screen.PrimaryScreen.Bounds.Width;
            WindowSize.Y = Screen.PrimaryScreen.Bounds.Height;
            if (WindowSize.X > WindowSize.Y)
                WindowSize.S = WindowSize.Y;
            else WindowSize.S = WindowSize.X;
        }
    }
}
