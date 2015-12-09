using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace Float
{
    public static class SettingValues
    {
        public static string Verson = "Beta.4";//O
        public static int Range = 150;//O
        public static bool Is_Range_Show = false;//O
        public static double Resistance = 1;//O
        public static bool Meet_Border_Is_Rebound = true;//O
        public static bool IsEscape = true;//O
        public static int Width = 180;//O
        public static int Height = 90;//O
        public static Color TEXTcolor = Color.Blue;//O
        public static bool Is_Tip_Mode = true;//O
        public static bool checkDATE = true;//日期O
        public static bool checkTIME = true;//時間O
        public static bool DayOnly = true;//只顯示天數O
        public static bool changd_date_at_0000 = false;//切換天數於午夜O
        public static bool TimeUpRemind = false;//時間到提醒O
        public static bool TimeUpRemind_Speed = false;//O
        public static int TimeUpRemind_Speed_X = 500;//O
        public static int TimeUpRemind_Speed_Y = -500;//O
        public static bool TimeUpRemind_MessabeBox = true;//O
        public static MessageBoxIcon TimeUpRemind_MessabeBox_Icons = MessageBoxIcon.Information;//O
        public static string TimeUpRemind_Title = "時間到!";//O
        public static string TimeUpRemind_TEXT = "時間到囉!";//O
        public static DateTime date = DateTime.Parse("2015-02-01");//O
        public static DateTime time = DateTime.ParseExact("09:20:00",
                                  TEMPVaribles.DateTimeList,
                                  CultureInfo.InvariantCulture,
                                  DateTimeStyles.AllowWhiteSpaces
                                  );//O

        public static string TEXT = "2015大學學測";


        public static DateTime datetime()
        {
            DateTime R = date;
            R = R.AddHours(time.Hour);
            R = R.AddMinutes(time.Minute);
            R = R.AddSeconds(time.Second);
            return R;
        }


    }
    public static class MySpeed
    {
        public static double X;
        public static double Y;
    }
    public static class MouseSpeed
    {
        public static int X;
        public static int Y;
    }
    public static class MouseLocation
    {
        public static int X;
        public static int Y;
    }
    public static class WindowSize
    {
        public static int X = Screen.PrimaryScreen.Bounds.Width;
        public static int Y = Screen.PrimaryScreen.Bounds.Height;
        public static int S;
    }
    public static class TEMPVaribles
    {
        public static bool isSizeChanged =true;
        public static bool israngechanged = false;
        public static bool isdebug = false;
        public static bool israngeshow = false;
        public static int savenumber = 1;
        public static Image BG;
        public static bool BGFined = false;
        public static Image SBG;
        public static bool SBGFined = false;
        public static int SBGX;
        public static int SBGY;
        public static string[] DateTimeList = { 
                            "yyyy/M/d tt hh:mm:ss", 
                            "yyyy/MM/dd tt hh:mm:ss", 
                            "yyyy/MM/dd HH:mm:ss", 
                            "yyyy/M/d HH:mm:ss", 
                            "yyyy/M/d", 
                            "yyyy/MM/dd" ,
                            "hh:mm:ss",
                            "HH:mm:ss"
                        }; 
    }
    /***********DEBUG*********/
    public static class FormLocation
    {
        public static int X;
        public static int Y;
        //public static bool seted = false;
    }
    public static class OpenSave
    {
        static StreamWriter sw;
        static string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Float";
        public static void BeforeOpen()
        {
            try
            {
                if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);
            }
            catch
            {
                MessageBox.Show("存取被拒。本次使用將無法存檔", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            while (IsFileInUse(Path(TEMPVaribles.savenumber)))
            {
                TEMPVaribles.savenumber++;//改N
            }
            if (!Open(TEMPVaribles.savenumber))//讀取失敗，fs也是在此時設定
            {
                //if (System.IO.File.Exists(Path(0)))
                {
                    if (!Open(0))
                        Save(0);
                }
                /*else
                {
                    Save(0);
                }*/
            }
            //Occupancy(TEMPVaribles.savenumber);//占用
        }
        static FileStream fs;//目標檔案
        static FileStream fswD;//Default檔案
        public static bool Open(int FileNumber)//回報True表示讀檔成功
        {
            StreamReader sr;
            if (FileNumber != 0)
            {
                fs = new FileStream(Path(FileNumber), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                sr = new StreamReader(fs, Encoding.UTF8);
            }
            else
            {
                fswD = new FileStream(Path(0), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                sr = new StreamReader(fswD, Encoding.UTF8);
            }
            try
            {
                if ((sr.ReadLine().Split('=')[1]) != SettingValues.Verson)
                {
                    MessageBox.Show("存檔版本不同，現在開始升級存檔", "更新", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("此功能未完成", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                SettingValues.Range = Convert.ToInt32(sr.ReadLine().Split('=')[1]);
                SettingValues.Is_Range_Show = Convert.ToBoolean(sr.ReadLine().Split('=')[1]);
                SettingValues.Resistance = Convert.ToDouble(sr.ReadLine().Split('=')[1]);
                SettingValues.Meet_Border_Is_Rebound = Convert.ToBoolean(sr.ReadLine().Split('=')[1]);
                SettingValues.IsEscape = Convert.ToBoolean(sr.ReadLine().Split('=')[1]);
                SettingValues.Width = Convert.ToInt32(sr.ReadLine().Split('=')[1]);
                SettingValues.Height = Convert.ToInt32(sr.ReadLine().Split('=')[1]);
                SettingValues.TEXTcolor = Color.FromArgb(Convert.ToByte(sr.ReadLine().Split('=')[1]), 0, 0);
                SettingValues.TEXTcolor = Color.FromArgb(SettingValues.TEXTcolor.R, Convert.ToByte(sr.ReadLine().Split('=')[1]), 0);
                SettingValues.TEXTcolor = Color.FromArgb(SettingValues.TEXTcolor.R, SettingValues.TEXTcolor.G, Convert.ToByte(sr.ReadLine().Split('=')[1]));
                SettingValues.Is_Tip_Mode = Convert.ToBoolean(sr.ReadLine().Split('=')[1]);
                SettingValues.checkDATE = Convert.ToBoolean(sr.ReadLine().Split('=')[1]);
                SettingValues.checkTIME = Convert.ToBoolean(sr.ReadLine().Split('=')[1]);
                SettingValues.DayOnly = Convert.ToBoolean(sr.ReadLine().Split('=')[1]);
                SettingValues.changd_date_at_0000 = Convert.ToBoolean(sr.ReadLine().Split('=')[1]);
                SettingValues.TimeUpRemind = Convert.ToBoolean(sr.ReadLine().Split('=')[1]);
                SettingValues.TimeUpRemind_Speed = Convert.ToBoolean(sr.ReadLine().Split('=')[1]);
                SettingValues.TimeUpRemind_Speed_X = Convert.ToInt32(sr.ReadLine().Split('=')[1]);
                SettingValues.TimeUpRemind_Speed_Y = Convert.ToInt32(sr.ReadLine().Split('=')[1]);
                SettingValues.TimeUpRemind_MessabeBox = Convert.ToBoolean(sr.ReadLine().Split('=')[1]);
                SettingValues.TimeUpRemind_MessabeBox_Icons = StringtoMsgIcon(sr.ReadLine().Split('=')[1]);
                SettingValues.TimeUpRemind_Title = sr.ReadLine().Substring(19);
                int NewLines = Convert.ToInt32((sr.ReadLine().Split('=')[1]));
                SettingValues.TimeUpRemind_TEXT = "";
                for (int i = 0; i < NewLines; i++)
                {
                    SettingValues.TimeUpRemind_TEXT += sr.ReadLine();
                    if (i + 1 != NewLines)
                        SettingValues.TimeUpRemind_TEXT += "\r\n";
                }

                SettingValues.date = DateTime.Parse(sr.ReadLine().Split('=')[1]);
                SettingValues.time = DateTime.ParseExact(sr.ReadLine().Split('=')[1], TEMPVaribles.DateTimeList, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces);
                int NewLines2 = Convert.ToInt32((sr.ReadLine().Split('=')[1]));
                SettingValues.TEXT = "";
                for(int i =0; i < NewLines2; i++)
                {
                    SettingValues.TEXT += sr.ReadLine();
                    if (i + 1 != NewLines2)
                        SettingValues.TEXT += "\r\n";
                }
            }
            catch
            {
                if(fs.Length !=0)//確認並不是剛剛新建立的檔案
                    MessageBox.Show("設定檔格格式不正確或損毀，於本程式關閉時會重建", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    try
                    {
                if(fswD.Length !=0)
                    MessageBox.Show("預設設定檔格格式不正確或損毀，即將重建", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                catch{}
                return false;
            }
            return true;
        }
        /*public static void Occupancy(int FileNumber)
        {
            fs = new FileStream(Path(FileNumber), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        }*/
        public static void Save(int FileNumber)
        {
            /*fs.Close();
            sw = new StreamWriter(Path(FileNumber), true);*/
            if (FileNumber == 0)
            {
                fswD.SetLength(0);
                sw = new StreamWriter(fswD, Encoding.UTF8);
            }
            else
            {
                fs.SetLength(0);
                sw = new StreamWriter(fs, Encoding.UTF8);
            }
            sw.WriteLine("Verson=" + SettingValues.Verson);
            sw.WriteLine("Range=" + SettingValues.Range.ToString());
            sw.WriteLine("Is_Range_Show=" + SettingValues.Is_Range_Show.ToString());
            sw.WriteLine("Resistance=" + SettingValues.Resistance.ToString());
            sw.WriteLine("Meet_Border_Is_Rebound=" + SettingValues.Meet_Border_Is_Rebound.ToString());
            sw.WriteLine("Escape=" + SettingValues.IsEscape.ToString());
            sw.WriteLine("Width=" + SettingValues.Width.ToString());
            sw.WriteLine("Height=" + SettingValues.Height.ToString());
            sw.WriteLine("TEXTcolor.R=" + SettingValues.TEXTcolor.R.ToString());
            sw.WriteLine("TEXTcolor.G=" + SettingValues.TEXTcolor.G.ToString());
            sw.WriteLine("TEXTcolor.B=" + SettingValues.TEXTcolor.B.ToString());
            sw.WriteLine("Is_Tip_Mode=" + SettingValues.Is_Tip_Mode.ToString());
            sw.WriteLine("checkDATE=" + SettingValues.checkDATE.ToString());
            sw.WriteLine("checkTIME=" + SettingValues.checkTIME.ToString());
            sw.WriteLine("DayOnly=" + SettingValues.DayOnly.ToString());
            sw.WriteLine("changd_date_at_0000=" + SettingValues.changd_date_at_0000.ToString());
            sw.WriteLine("TimeUpRemind=" + SettingValues.TimeUpRemind.ToString());
            sw.WriteLine("TimeUpRemind_Speed=" + SettingValues.TimeUpRemind_Speed.ToString());
            sw.WriteLine("TimeUpRemind_Speed_X=" + SettingValues.TimeUpRemind_Speed_X.ToString());
            sw.WriteLine("TimeUpRemind_Speed_Y=" + SettingValues.TimeUpRemind_Speed_Y.ToString());
            sw.WriteLine("TimeUpRemind_MessabeBox=" + SettingValues.TimeUpRemind_MessabeBox.ToString());
            sw.WriteLine("TimeUpRemind_MessabeBox_Icons=" + SettingValues.TimeUpRemind_MessabeBox_Icons.ToString());
            sw.WriteLine("TimeUpRemind_Title=" + SettingValues.TimeUpRemind_Title);
            sw.WriteLine("TimeUpRemind_TEXT_____Total_NewLines=" + SettingValues.TimeUpRemind_TEXT.Split('\n').GetLength(0).ToString());
            sw.WriteLine(SettingValues.TimeUpRemind_TEXT);

            sw.WriteLine("date=" + SettingValues.date.ToString("yyyy-MM-dd"));
            sw.WriteLine("time=" + SettingValues.time.ToString("HH:mm:ss"));
            sw.WriteLine("TEX_____Total_NewLinesT=" + SettingValues.TEXT.Split('\n').GetLength(0).ToString());
            sw.Write(SettingValues.TEXT);
            sw.Close();

        }

        public static string Path(int Number)
        {
            if (Number != 0) return FolderPath + @"\Float" + Number.ToString() + ".conf";
            else return FolderPath + @"\Default.conf";
        }



        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);
        public static bool IsFileInUse(string fileName)
        {
            if (!File.Exists(fileName))
            {
                //MessageBox.Show("文件都不存在，你就不要拿来耍了");
                return false;
            }
            IntPtr vHandle = _lopen(fileName, OF_READWRITE | OF_SHARE_DENY_NONE);
            if (vHandle == HFILE_ERROR)
            {
                //MessageBox.Show("文件被占用！");
                return true;
            }
            CloseHandle(vHandle);
            //MessageBox.Show("没有被占用！");
            return false;
        }

        public static MessageBoxIcon StringtoMsgIcon(string iconstr)
        {
            switch (iconstr)
            {
                case "None":
                    return MessageBoxIcon.None;
                case "Hand":
                    return MessageBoxIcon.Error;
                case "Question":
                    return MessageBoxIcon.Question;
                case "Warning":
                    return MessageBoxIcon.Warning;
                default:
                    return MessageBoxIcon.Information;
            }
        }








    }

}
