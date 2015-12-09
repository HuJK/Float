using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
#if DEBUG
using System.Diagnostics;
#endif

namespace Float
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /*****實現*****/
        #region 全視窗拖動+Ctrl偵測+一堆變數
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private void AllWindowDrag_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x0112, 0xF012, 0);
        }
        [DllImport("user32", EntryPoint = "SetWindowPos")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, int flags);
        /*********/
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);
        /**********/
        double TempX;
        double TempY;
        int LastMouseX;
        int LastMouseY;
        long AltKeySituation = GetAsyncKeyState(0);
        bool NotRemindedYet = true;
        DebugWindow d = new DebugWindow();
        DebugSettings ds = new DebugSettings();
        ShowActiveZoom SAZ = new ShowActiveZoom();
        SettingsBotton STBN = new SettingsBotton();
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
#if !DEBUG
            this.ShowInTaskbar = false;
#endif

            OpenSave.BeforeOpen();//開檔+存預設
            if (WindowSize.X > WindowSize.Y)//設定小的為WindowsSize.S
                WindowSize.S = WindowSize.Y;
            else WindowSize.S = WindowSize.X;//
            LastMouseX = System.Windows.Forms.Cursor.Position.X;
            LastMouseY = System.Windows.Forms.Cursor.Position.Y;
            this.TopMost = true;
            SetWindowPos(Handle, new IntPtr(-1), 0, 0, 0, 0, 3);
            Thread ThreadPrintBG = new Thread(new ThreadStart(PrintBackGround));
            ThreadPrintBG.IsBackground = true;
            ThreadPrintBG.Start();
            this.TransparencyKey = Color.FromArgb(0, 102, 103, 103);
        }

        private void MoveMe_Tick(object sender, EventArgs e)
        {
            
            #region 視窗外觀部分
            if (TEMPVaribles.isSizeChanged)
            {
                TEMPVaribles.isSizeChanged = false;
                this.Width = SettingValues.Width;
                this.Height = SettingValues.Height;
                Thread ThreadPrintBG = new Thread(new ThreadStart(PrintBackGround));
                ThreadPrintBG.IsBackground = true;
                ThreadPrintBG.Start();
                this.TransparencyKey = Color.FromArgb(0, 102, 103, 103);
            }
            if (TEMPVaribles.BGFined)
            {
                this.BackgroundImage = TEMPVaribles.BG;
                TEMPVaribles.BGFined = false;
            }
            if (TEMPVaribles.SBGFined)
            {
                STBN.STS.ViewPanel.BackgroundImage = TEMPVaribles.SBG;
                TEMPVaribles.SBGFined = false;
            }

            if (TEMPVaribles.isdebug)
            {
                d.Show();
                ds.SetNew += new NewLocation(SetNewLocation);
                ds.ActionCtrl += new ActionCtrlEvent(TimerSet);
                ds.Show();
            }
            else
            {
                ds.Hide();
            }
            /**********/

            //捕捉Alt鍵，開啟設定和關閉按鈕
                AltKeySituation = GetAsyncKeyState(18);
                if (AltKeySituation != 0)
                {
                    STBN.Left = this.Left;
                    if (this.Top < WindowSize.Y - 40 - this.Height - 40)
                        STBN.Top = this.Top + this.Height + 10;
                    else
                        STBN.Top = this.Top - 40;
                    
                    STBN.STS.BarRANGEvaule.Minimum = (int)(Math.Sqrt(this.Height * this.Height + this.Width * this.Width) / 2);
                    STBN.Width = this.Width;
                    STBN.STS.SetNew += new NewLocation(SetNewLocation);
                    STBN.STS.redrawview += new ReDrawView(PrintBackGroundView);
                    STBN.Show();
                }
                else
                {
                    STBN.Hide();
                }
             #endregion


            #region 視窗移動部分
                /***↓↓↓↓↓↓↓↓↓↓↓↓↓↓***/
                //視窗移動部分
            SpeedCount();
            #region 滑鼠偵測&無慣性移動
            /***********************************/
            //滑鼠偵測&大摩擦力移動
            //滑鼠進到視窗內
            if ((MouseLocation.X > this.Location.X +1) && (MouseLocation.X < (this.Location.X + this.Width) -1) && MouseLocation.Y > this.Location.Y +1&& MouseLocation.Y < this.Location.Y + this.Height -1)
            {
                if(SettingValues.IsEscape && SettingValues.Meet_Border_Is_Rebound)
                    Escape();
            }
            //滑鼠在感應範圍內
            else if (((MouseLocation.X - this.Location.X - (this.Width / 2)) * (MouseLocation.X - this.Location.X - (this.Width / 2)) + (MouseLocation.Y - this.Location.Y - (this.Height / 2)) * (MouseLocation.Y - this.Location.Y - (this.Height / 2))) < SettingValues.Range * SettingValues.Range)
            {
                if (AltKeySituation == 0 && SettingValues.Range != 1)
                ActionChange(this.Location.X + (this.Width / 2), (this.Location.Y + (this.Height / 2)));
            }
            #endregion
            #region DEBUG模式的紀錄
            /*****************************/
            //DEBUG模式的紀錄
            if (TEMPVaribles.isdebug)
            {
                if (ds.RecordList.Items.Count > (ds.Height - 85) / 12 && ds.RecordList.Items.Count > 5)
                {
                    ds.RecordList.Items.RemoveAt(0);
                    ds.RecordList.Items.RemoveAt(0);
                }
                ds.RecordList.Items.Add("L(" + Convert.ToString(this.Location.X).PadLeft(4, '0') + "," + Convert.ToString(this.Location.Y).PadLeft(4, '0') + ")" +
                    "  S(" + Convert.ToString(MySpeed.X) + "," + Convert.ToString(MySpeed.Y) + ")");
            }

            /*********************/
            //超出範圍(也就是發生錯誤了!理論上這永遠不應該被執行到)
            if (this.Location.X < 1 || this.Location.X + this.Width > WindowSize.X || this.Location.Y < 1 || this.Location.Y + this.Height > WindowSize.Y)
                if (TEMPVaribles.isdebug)
                {
                    this.MoveMe.Enabled = false;
                }
                else
                {
                    SetNewLocation((WindowSize.X - this.Width) / 2, (WindowSize.Y - this.Height) / 2);
                }
            #endregion
            /*************************/
            if (SettingValues.Resistance < WindowSize.S / 100)//磨擦力過大不移動(直接取消慣性)
            move();//move函數=計算有慣性下的移動方式
            #region 是否顯示範圍
            /******************/
            //是否顯示範圍
            if (SettingValues.Is_Range_Show || TEMPVaribles.israngeshow)
            {
                SAZ.Show();
                SAZ.Top = this.Location.Y + this.Size.Height / 2 - SettingValues.Range;
                SAZ.Left = this.Location.X + this.Size.Width / 2 - SettingValues.Range;
                STBN.STS.SAZ_Blue.Top = this.Location.Y + this.Size.Height / 2 - STBN.STS.BarRANGEvaule.Value;
                STBN.STS.SAZ_Blue.Left = this.Location.X + this.Size.Width / 2 - STBN.STS.BarRANGEvaule.Value;
            }
            else SAZ.Hide();
            #endregion

            #endregion
        }


        private void MainForm_Move(object sender, EventArgs e)//防止移動超出範圍
        {
#if !DEBUG//DEBUG模式下不執行超出範圍保護
            if (this.Left < 1)
                this.Left = 1;
            else if (this.Left + this.Width > WindowSize.X)
                this.Left = WindowSize.X - 1 - this.Width;
            if (this.Top < 1)
                this.Top = 1;
            else if (this.Top + this.Height > WindowSize.Y)
                this.Top = WindowSize.Y - 1 - this.Height;
#endif
        } 



        /*******以下是一堆方法*******/
        void ActionChange(int X, int Y)
        {
            if (X == -1 && Y == -1)
            {
                MySpeed.X = 0;
                MySpeed.Y = 0;
            }
            else
            {
                orthogonal_projection((MouseSpeed.X - MySpeed.X), (MouseSpeed.Y - MySpeed.Y), (X - MouseLocation.X), (Y - MouseLocation.Y));
                if ((MySpeed.X = MySpeed.X + (2 * TempX)) > WindowSize.X - 6)
                    MySpeed.X = WindowSize.X - 6;
                if ((MySpeed.Y = MySpeed.Y + (2 * TempY)) > WindowSize.Y - 6)
                    MySpeed.Y = WindowSize.Y - 6;
            }
            //if ((MouseLocation.X * MouseLocation.X) + (MouseLocation.Y * MouseLocation.Y) > (Variables.Range - 10) * (Variables.Range - 10))
            {
                int LX = X - MouseLocation.X;
                int LY = Y - MouseLocation.Y;
                double L = Math.Sqrt(LX * LX + LY * LY);
                int NewLeft = this.Left + (int)(((SettingValues.Range - L) / Math.Sqrt(LX * LX + LY * LY)) * LX);
                int NewTop = this.Top + (int)(((SettingValues.Range - L) / Math.Sqrt(LX * LX + LY * LY)) * LY);
                if (SettingValues.Meet_Border_Is_Rebound)
                {
                    if (NewLeft < 1)
                        NewLeft = 1;
                    else if (NewLeft + this.Width > WindowSize.X)
                        NewLeft = WindowSize.X - 1 - this.Width;
                    if (NewTop < 1)
                        NewTop = 1;
                    else if (NewTop + this.Height > WindowSize.Y)
                        NewTop = WindowSize.Y - 1 - this.Height;
                }
                else
                {
                    if (NewLeft < 1)
                        NewLeft = WindowSize.X - 1 - this.Width;
                    else if (NewLeft + this.Width > WindowSize.X)
                        NewLeft = 1;
                    if (NewTop < 1)
                        NewTop = WindowSize.Y - 1 - this.Height;
                    else if (NewTop + this.Height > WindowSize.Y)
                        NewTop = 1;
                }
                this.Left = NewLeft;
                this.Top = NewTop;
            }
        }
        void move()
        {


            if((MySpeed.X == 0) && (MySpeed.Y == 0))
            {
                return;
            }
            if (((0 < this.Location.X + (int)MySpeed.X) && (this.Location.X + this.Width + (int)MySpeed.X < WindowSize.X)) && ((0 < this.Location.Y + (int)MySpeed.Y) && (this.Location.Y + this.Height + (int)MySpeed.Y < WindowSize.Y)))
            {
                //無反彈
                this.Left = this.Left + (int)MySpeed.X;
                this.Top = this.Top + (int)MySpeed.Y;
                double a = (SettingValues.Resistance * SettingValues.Resistance / Math.Sqrt(MySpeed.X * MySpeed.X + MySpeed.Y * MySpeed.Y));
                if ((Math.Abs(MySpeed.X) - Math.Abs(MySpeed.X * a) > 0) ||
                    (Math.Abs(MySpeed.Y) - Math.Abs(MySpeed.Y * a) > 0))
                {
                    MySpeed.X -= MySpeed.X * a;
                    MySpeed.Y -= MySpeed.Y * a;
                }
                else
                {
                    MySpeed.X = 0;
                    MySpeed.Y = 0;
                }
            }
            else
            {
                //有反彈
                if (MySpeed.X < 0 && MySpeed.Y < 0)//左上角
                {
                    double a = (SettingValues.Resistance * SettingValues.Resistance / Math.Sqrt(MySpeed.X * MySpeed.X + MySpeed.Y * MySpeed.Y));
                    if ((!SettingValues.Meet_Border_Is_Rebound && this.Location.X == 1 && this.Location.Y == 1))//正左上角
                    {
#if DEBUG
                        System.Diagnostics.Debug.Print("正左上角");
#endif
                        this.Left = (WindowSize.X - 1) - this.Width;//跳到右下角
                        this.Top = this.Top = WindowSize.Y - 1 - this.Height;
                        MySpeed.X = (MySpeed.X - MySpeed.X * a);
                        MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                    }
                    else if (this.Location.Y == 1)//處理剛好在邊界的情況，否則計算斜率時分母為0
                    {
                        if (SettingValues.Meet_Border_Is_Rebound)
                        {
                            MySpeed.X = (MySpeed.X - MySpeed.X * a);
                            MySpeed.Y = (MySpeed.Y - MySpeed.Y * a) * -1;
                        }
                        else
                        {
                            this.Top = WindowSize.Y - 1 - this.Height;
                            MySpeed.X = (MySpeed.X - MySpeed.X * a);
                            MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                        }
                    }
                    else if (MySpeed.X / MySpeed.Y > ((double)1 - this.Location.X) / (1 - this.Location.Y))
                    {
                        ReBoundLeft();
                    }
                    else
                    {
                        ReBoundUp();
                    }
                }//End左上角↑
                else if (MySpeed.X > 0 && MySpeed.Y < 0)//右上角
                {
                   double a = (SettingValues.Resistance * SettingValues.Resistance / Math.Sqrt(MySpeed.X * MySpeed.X + MySpeed.Y * MySpeed.Y));
                   if (!SettingValues.Meet_Border_Is_Rebound && this.Location.X + this.Width == WindowSize.X - 1 && (this.Location.Y == 1))//正右上角
                   {
#if DEBUG
                       System.Diagnostics.Debug.Print("正右上角");
#endif
                       this.Left = 1;//跳到左下角
                       this.Top = this.Top = WindowSize.Y - 1 - this.Height;
                       MySpeed.X = (MySpeed.X - MySpeed.X * a);
                       MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                   }
                    if (this.Location.Y == 1)//處理剛好在邊界的情況，否則計算斜率時分母為0
                    {
                        if (SettingValues.Meet_Border_Is_Rebound)
                        {
                            MySpeed.X = (MySpeed.X - MySpeed.X * a);
                            MySpeed.Y = (MySpeed.Y - MySpeed.Y * a) * -1;
                        }
                        else
                        {
                            this.Top = WindowSize.Y - 1 - this.Height;
                            MySpeed.X = (MySpeed.X - MySpeed.X * a);
                            MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                        }
                    }
                    else if (MySpeed.X / MySpeed.Y > ((WindowSize.X - 1) - (this.Location.X + this.Width)) / (1 - this.Location.Y))
                    {
                        ReBoundUp();
                    }
                    else
                    {
                        ReBoundRight();
                    }

                }//End右上角↑
                else if (MySpeed.X < 0 && MySpeed.Y > 0)//左下角
                {
                    double a = (SettingValues.Resistance * SettingValues.Resistance / Math.Sqrt(MySpeed.X * MySpeed.X + MySpeed.Y * MySpeed.Y));
                    if (!SettingValues.Meet_Border_Is_Rebound && this.Location.X == 1 && this.Location.Y + this.Height == WindowSize.Y - 1)//正左下角
                    {       
#if DEBUG
                    System.Diagnostics.Debug.Print("正左下角");
#endif
                    this.Left = (WindowSize.X - 1) - this.Width;//跳到右上角
                    this.Top = 1;
                    MySpeed.X = (MySpeed.X - MySpeed.X * a);
                    MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                    }   
                    if (((WindowSize.Y - 1) - (this.Location.Y + this.Height)) == 0)//處理剛好在邊界的情況，否則計算斜率時分母為0
                    {
                        if (SettingValues.Meet_Border_Is_Rebound)
                        {
                            MySpeed.X = (MySpeed.X - MySpeed.X * a);
                            MySpeed.Y = (MySpeed.Y - MySpeed.Y * a) * -1;
                        }
                        else
                        {
                            this.Top = 1;
                            MySpeed.X = (MySpeed.X - MySpeed.X * a);
                            MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                        }
                    }
                    else if (MySpeed.X / MySpeed.Y > ((1 - this.Location.X) / ((WindowSize.Y - 1) - (this.Location.Y + this.Height))))
                    {
                        ReBoundDown();
                    }
                    else
                    {
                        ReBoundLeft();
                    }
                }//End左下角
                else if (MySpeed.X > 0 && MySpeed.Y > 0)//右下角
                {
                    double a = (SettingValues.Resistance * SettingValues.Resistance / Math.Sqrt(MySpeed.X * MySpeed.X + MySpeed.Y * MySpeed.Y));
                    if (!SettingValues.Meet_Border_Is_Rebound && this.Location.X + this.Width == WindowSize.X - 1 && this.Location.Y + this.Height == WindowSize.Y - 1)//正右下角
                    {
#if DEBUG
                        System.Diagnostics.Debug.Print("正右下角");
#endif
                        this.Left = 1;//跳到左上角
                        this.Top = 1;
                        MySpeed.X = (MySpeed.X - MySpeed.X * a);
                        MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                    }
                    if (((WindowSize.Y - 1) - (this.Location.Y + this.Height)) == 0)//處理剛好在邊界的情況，否則計算斜率時分母為0
                    {
                        if (SettingValues.Meet_Border_Is_Rebound)
                        {
                            MySpeed.X = (MySpeed.X - MySpeed.X * a);
                            MySpeed.Y = (MySpeed.Y - MySpeed.Y * a) * -1;
                        }
                        else
                        {
                            this.Top = 1;
                            MySpeed.X = (MySpeed.X - MySpeed.X * a);
                            MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                        }
                    }
                    else if (MySpeed.X / MySpeed.Y > ((WindowSize.X - 1) - (this.Location.X + this.Width)) / ((WindowSize.Y - 1) - (this.Location.Y + this.Height)))
                    {
                        ReBoundRight();
                    }
                    else
                    {
                        ReBoundDown();
                    }
                }//End右下角
                else if (MySpeed.Y == 0)//水平
                {
                    if (SettingValues.Meet_Border_Is_Rebound)
                    {
                        if (MySpeed.X > 0)
                        {
                            this.Left = WindowSize.X - 1 - this.Width;
                            MySpeed.X = SettingValues.Resistance - MySpeed.X;
                        }
                        else
                        {
                            this.Left = 1;
                            MySpeed.X = -1 * (MySpeed.X + SettingValues.Resistance);
                        }
                    }
                    else
                    {
                        if (MySpeed.X > 0)
                        {
                            this.Left = 1;
                            MySpeed.X = MySpeed.X - SettingValues.Resistance;
                        }
                        else
                        {
                            this.Left = WindowSize.X - 1 - this.Width; ;
                            MySpeed.X = MySpeed.X + SettingValues.Resistance;
                        }
                    }
                }//End水平↑
                else if (MySpeed.X == 0)//垂直
                {
                    if (SettingValues.Meet_Border_Is_Rebound)
                    {
                        if (MySpeed.Y > 0)
                        {
                            this.Top = WindowSize.Y - 1 - this.Height;
                            MySpeed.Y = SettingValues.Resistance - MySpeed.Y;
                        }
                        else
                        {
                            this.Top = 1;
                            MySpeed.Y = -1 * (MySpeed.Y + SettingValues.Resistance);
                        }
                    }
                    else
                    {
                        if (MySpeed.Y > 0)
                        {
                            this.Top = 1;
                            MySpeed.Y = MySpeed.Y - SettingValues.Resistance;
                        }
                        else
                        {
                            this.Top = WindowSize.Y - 1 - this.Height;
                            MySpeed.Y =MySpeed.Y + SettingValues.Resistance;
                        }
                    }
                }//End垂直↑
            }//End有反彈↑
        }
        void Escape()
        { 
            if ((MouseLocation.X > this.Location.X +2) && (MouseLocation.X < (this.Location.X + this.Width) -2) && MouseLocation.Y > this.Location.Y +2 && MouseLocation.Y < this.Location.Y + this.Height -2)
            {
                
                double a = (SettingValues.Resistance * SettingValues.Resistance / Math.Sqrt(MySpeed.X * MySpeed.X + MySpeed.Y * MySpeed.Y));
                if (this.Location.X == 1)
                {
                    if (this.Location.Y == 1)
                    {
#if DEBUG
                        System.Diagnostics.Debug.Print("Escaped to 右下");
#endif
                        this.Left = (WindowSize.X - 1) - this.Width;//跳到右下角
                        this.Top = this.Top = WindowSize.Y - 1 - this.Height;
                        MySpeed.X = Math.Sqrt(WindowSize.X * WindowSize.X + WindowSize.Y * WindowSize.Y) / -25;
                        MySpeed.Y = Math.Sqrt(WindowSize.X * WindowSize.X + WindowSize.Y * WindowSize.Y) / -25;
                    }
                    else if (this.Location.Y == WindowSize.Y - 1 - this.Height)
                    {
#if DEBUG
                        System.Diagnostics.Debug.Print("Escaped to 右上");
#endif
                        this.Left = (WindowSize.X - 1) - this.Width;//跳到右上角
                        this.Top = 1;
                        MySpeed.X = Math.Sqrt(WindowSize.X * WindowSize.X + WindowSize.Y * WindowSize.Y) / -25;
                        MySpeed.Y = Math.Sqrt(WindowSize.X * WindowSize.X + WindowSize.Y * WindowSize.Y) / 25;
                    }
                    else
                    {
#if DEBUG
                        System.Diagnostics.Debug.Print("Escaped to 右");
#endif
                        this.Left = (WindowSize.X - 1) - this.Width;//跳到右
                        MySpeed.X = Math.Sqrt(WindowSize.X * WindowSize.X + WindowSize.Y * WindowSize.Y) / -25;

                    }

                }
                else if (this.Location.X == WindowSize.X - 1 - this.Width)
                {
                    if(this.Location.Y == 1)
                    {
#if DEBUG
                        System.Diagnostics.Debug.Print("Escaped to 左下");
#endif
                        this.Left = 1;//跳到左下角
                        this.Top = this.Top = WindowSize.Y - 1 - this.Height;
                        MySpeed.X = Math.Sqrt(WindowSize.X * WindowSize.X + WindowSize.Y * WindowSize.Y) / 25;
                        MySpeed.Y = Math.Sqrt(WindowSize.X * WindowSize.X + WindowSize.Y * WindowSize.Y) / -25;
                    }
                    else if (this.Location.Y == WindowSize.Y - 1 - this.Height)
                    {
#if DEBUG
                        System.Diagnostics.Debug.Print("Escaped to 左上");
#endif
                        this.Left = 1;//跳到左上角
                        this.Top = 1;
                        MySpeed.X = Math.Sqrt(WindowSize.X * WindowSize.X + WindowSize.Y * WindowSize.Y) / 25;
                        MySpeed.Y = Math.Sqrt(WindowSize.X * WindowSize.X + WindowSize.Y * WindowSize.Y) / 25;
                    }
                    else
                    {
#if DEBUG
                        System.Diagnostics.Debug.Print("Escaped to 左");
#endif
                        this.Left = 1;//跳到左
                        MySpeed.X = Math.Sqrt(WindowSize.X * WindowSize.X + WindowSize.Y * WindowSize.Y) / 25;
                    }
                }
                else if (this.Location.Y == 1)
                {
#if DEBUG
                    System.Diagnostics.Debug.Print("Escaped to 下");
#endif
                    this.Top = WindowSize.Y - 1 - this.Height;//跳到下
                    MySpeed.Y = Math.Sqrt(WindowSize.X * WindowSize.X + WindowSize.Y * WindowSize.Y) / -25;
                }
                else if (this.Location.Y == WindowSize.Y - 1 - this.Height)
                {
#if DEBUG
                    System.Diagnostics.Debug.Print("Escaped to 上");
#endif
                    this.Top = 1;//跳到上
                    MySpeed.Y = Math.Sqrt(WindowSize.X * WindowSize.X + WindowSize.Y * WindowSize.Y) / 25;
                }
            }
        }
        #region 反彈
        int ReBoundUp()
        {
#if DEBUG
            //System.Diagnostics.Debug.Print("上");
#endif
            int x = this.Left + Convert.ToInt32((1 - (this.Location.Y)) * MySpeed.X / MySpeed.Y);
            double a = (SettingValues.Resistance * SettingValues.Resistance / Math.Sqrt(MySpeed.X * MySpeed.X + MySpeed.Y * MySpeed.Y));
            if (x < 1 || x + this.Width > WindowSize.X)
            {
                if (SettingValues.Meet_Border_Is_Rebound)
                {
                    MySpeed.X = (MySpeed.X - MySpeed.X * a) * -1;
                    MySpeed.Y = (MySpeed.Y - MySpeed.Y * a) * -1;
                }
                else
                {
                    if (MySpeed.X > 0)
                    {
                        this.Left = 1;//跳到左下角
                        this.Top = this.Top = WindowSize.Y - 1 - this.Height;
                        MySpeed.X = (MySpeed.X - MySpeed.X * a);
                        MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                    }
                    else
                    {
                        this.Left = (WindowSize.X - 1) - this.Width;//跳到右下角
                        this.Top = this.Top = WindowSize.Y - 1 - this.Height;
                        MySpeed.X = (MySpeed.X - MySpeed.X * a);
                        MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                    }
                }
            }
            else
            {
                if (SettingValues.Meet_Border_Is_Rebound)
                {
                    this.Left = x;
                    this.Top = 1;
                    MySpeed.X = (MySpeed.X - MySpeed.X * a);
                    //MySpeed.Y = Math.Abs((MySpeed.Y - MySpeed.Y * a));
                    MySpeed.Y = (MySpeed.Y - MySpeed.Y * a) * -1;
                }
                else
                {
                    this.Left = x;
                    this.Top = WindowSize.Y - 1 - this.Height;
                    MySpeed.X = (MySpeed.X - MySpeed.X * a);
                    MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                }
            }
            return 0;
        }
        int ReBoundDown()
        {
#if DEBUG
            //System.Diagnostics.Debug.Print("下");
#endif
            int x = this.Left + Convert.ToInt32(((WindowSize.Y - 1) - (this.Location.Y + this.Height)) * MySpeed.X / MySpeed.Y);
            double a = (SettingValues.Resistance * SettingValues.Resistance / Math.Sqrt(MySpeed.X * MySpeed.X + MySpeed.Y * MySpeed.Y));
            if (x < 1 || x + this.Width > WindowSize.X)
            {
                if (SettingValues.Meet_Border_Is_Rebound)
                {
                    MySpeed.X = (MySpeed.X - MySpeed.X * a) * -1;
                    MySpeed.Y = (MySpeed.Y - MySpeed.Y * a) * -1;
                }
                else
                {
                    if (MySpeed.X > 0)
                    {
                        this.Left = 1;//跳到左上角
                        this.Top = 1;
                        MySpeed.X = (MySpeed.X - MySpeed.X * a);
                        MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                    }
                    else
                    {
                        this.Left = (WindowSize.X - 1) - this.Width;//跳到右上角
                        this.Top = 1;
                        MySpeed.X = (MySpeed.X - MySpeed.X * a);
                        MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                    }
                }
            }
            else
            {
                if (SettingValues.Meet_Border_Is_Rebound)
                {
                    this.Left = x;
                    this.Top = WindowSize.Y - 1 - this.Height;
                    MySpeed.X = (MySpeed.X - MySpeed.X * a);
                    //MySpeed.Y = Math.Abs((MySpeed.Y - MySpeed.Y * a)) * -1;
                    MySpeed.Y = (MySpeed.Y - MySpeed.Y * a) * -1;
                }
                else
                {
                    this.Left = x;
                    this.Top = 1;
                    MySpeed.X = (MySpeed.X - MySpeed.X * a);
                    MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                }
            }
            return 0;
        }
        int ReBoundLeft()
        {
#if DEBUG
            //System.Diagnostics.Debug.Print("左");
#endif
            int y = this.Top + Convert.ToInt32(((1 - this.Location.X)) * MySpeed.Y / MySpeed.X);
            double a = (SettingValues.Resistance * SettingValues.Resistance / Math.Sqrt(MySpeed.X * MySpeed.X + MySpeed.Y * MySpeed.Y));
            if (y < 1 || y + this.Height > WindowSize.Y - 1)
            {
                if (SettingValues.Meet_Border_Is_Rebound)
                {
                    MySpeed.X = (MySpeed.X - MySpeed.X * a) * -1;
                    MySpeed.Y = (MySpeed.Y - MySpeed.Y * a) * -1;
                }
                else
                {
                    if (MySpeed.Y > 0)
                    {
                        this.Left = (WindowSize.X - 1) - this.Width;//跳到右上角
                        this.Top = 1;
                        MySpeed.X = (MySpeed.X - MySpeed.X * a);
                        MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                    }
                    else
                    {
                        this.Left = (WindowSize.X - 1) - this.Width;//跳到右下角
                        this.Top = this.Top = WindowSize.Y - 1 - this.Height;
                        MySpeed.X = (MySpeed.X - MySpeed.X * a);
                        MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                    }
                }
            }
            else
            {
                if (SettingValues.Meet_Border_Is_Rebound)
                {
                    this.Top = y;
                    this.Left = 1;
                    //MySpeed.X = Math.Abs((MySpeed.X - MySpeed.X * a));
                    MySpeed.X = (MySpeed.X - MySpeed.X * a) * -1;
                    MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                }
                else
                {
                    this.Top = y;
                    this.Left = this.Left = (WindowSize.X - 1) - this.Width;
                    MySpeed.X = (MySpeed.X - MySpeed.X * a);
                    MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                }
            }
            return 0;
        }
        int ReBoundRight()
        {
#if DEBUG
            //System.Diagnostics.Debug.Print("右");
#endif
            int y = this.Top + Convert.ToInt32((double)((WindowSize.X - 1) - (this.Location.X + this.Width)) * MySpeed.Y / MySpeed.X);
            double a = (SettingValues.Resistance * SettingValues.Resistance / Math.Sqrt(MySpeed.X * MySpeed.X + MySpeed.Y * MySpeed.Y));
            if (y < 1 || y + this.Height > WindowSize.Y - 1)
            {
                if (SettingValues.Meet_Border_Is_Rebound)
                {
                    MySpeed.X = (MySpeed.X - MySpeed.X * a) * -1;
                    MySpeed.Y = (MySpeed.Y - MySpeed.Y * a) * -1;
                }
                else
                {
                    if (MySpeed.Y > 0)
                    {
                        this.Left = 1;//跳到左上角
                        this.Top = 1;
                        MySpeed.X = (MySpeed.X - MySpeed.X * a);
                        MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                    }
                    else
                    {
                        this.Left = 1;//跳到左下角
                        this.Top = this.Top = WindowSize.Y - 1 - this.Height;
                        MySpeed.X = (MySpeed.X - MySpeed.X * a);
                        MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                    }
                }
            }
            else
            {
                if (SettingValues.Meet_Border_Is_Rebound)
                {
                    this.Top = y;
                    this.Left = (WindowSize.X - 1) - this.Width;
                    //MySpeed.X = Math.Abs((MySpeed.X - MySpeed.X * a)) * -1;
                    MySpeed.X = (MySpeed.X - MySpeed.X * a) * -1;
                    MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);
                }
                else
                {
                    this.Top = y;
                    this.Left = 1;
                    MySpeed.X = (MySpeed.X - MySpeed.X * a);
                    MySpeed.Y = (MySpeed.Y - MySpeed.Y * a);

                }
            }
            return 0;
        }
        #endregion
        void PrintBackGround()
        {
            Bitmap BackgroundGraphics = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(BackgroundGraphics);
            g.Clear(Color.FromArgb(234, 234, 234));
            for (int i = 13; i < this.Width - 42; i += 26)
            {
                g.DrawImage(this.BackGroundUp.Images[0], i, 0);
                g.DrawImage(this.BackGroundDn.Images[0], i, this.Height - 35);
            }
            g.DrawImage(this.BackGroundLU.Images[0], 0, 0);
            g.DrawImage(this.BackGroundRU.Images[0], this.Width - 42, 0);
            g.DrawImage(this.BackGroundLD.Images[0], 0, this.Height - 35);
            g.DrawImage(this.BackGroundRD.Images[0], this.Width - 42, this.Height - 35);
            for (int i = 47; i < this.Height - 35; i++)
            {
                BackgroundGraphics.SetPixel(0, i, Color.FromArgb(103, 103, 103));
                BackgroundGraphics.SetPixel(1, i, Color.FromArgb(103, 103, 103));
                BackgroundGraphics.SetPixel(this.Width - 1, i, Color.FromArgb(103, 103, 103));
                BackgroundGraphics.SetPixel(this.Width - 2, i, Color.FromArgb(103, 103, 103));
            }
            {
                SetCornerGradient(0, 0, this.Width, this.Height, ref BackgroundGraphics);
                SetCornerGradient(0, 1, this.Width, this.Height, ref BackgroundGraphics);
                SetCornerGradient(0, 2, this.Width, this.Height, ref BackgroundGraphics);
                SetCornerGradient(0, 3, this.Width, this.Height, ref BackgroundGraphics);
                SetCornerGradient(1, 0, this.Width, this.Height, ref BackgroundGraphics);
                SetCornerGradient(1, 1, this.Width, this.Height, ref BackgroundGraphics);
                SetCornerGradient(2, 0, this.Width, this.Height, ref BackgroundGraphics);
                SetCornerGradient(3, 0, this.Width, this.Height, ref BackgroundGraphics);
                /*  以後可能會用到
                0     0  33 148 223 247 255
                0   104 243
                33  243
                148
                223
                247
                255
                 * 
                     13      42
                47                 47
                35                 35
                     13      42
                */
            }
            TEMPVaribles.BG = BackgroundGraphics;
            TEMPVaribles.BGFined = true;
        }
        void PrintBackGroundView(int X, int Y)
        {
            TEMPVaribles.SBGX = X;
            TEMPVaribles.SBGY = Y;
            Thread ThreadPrintSBG = new Thread(new ThreadStart(PrintBackGroundViewThread));
            ThreadPrintSBG.IsBackground = true;
            ThreadPrintSBG.Start();

        }
        void PrintBackGroundViewThread()
        {
            int X = TEMPVaribles.SBGX;
            int Y = TEMPVaribles.SBGY;
            Bitmap BackgroundGraphics = new Bitmap(X, Y);
            Graphics g = Graphics.FromImage(BackgroundGraphics);
            g.Clear(Color.FromArgb(234, 234, 234));
            for (int i = 13; i < X - 42; i += 26)
            {
                g.DrawImage(this.BackGroundUp.Images[0], i, 0);
                g.DrawImage(this.BackGroundDn.Images[0], i, Y - 35);
            }
            g.DrawImage(this.BackGroundLU.Images[0], 0, 0);
            g.DrawImage(this.BackGroundRU.Images[0], X - 42, 0);
            g.DrawImage(this.BackGroundLD.Images[0], 0, Y - 35);
            g.DrawImage(this.BackGroundRD.Images[0], X - 42, Y - 35);
            for (int i = 47; i < Y - 35; i++)
            {
                BackgroundGraphics.SetPixel(0, i, Color.FromArgb(103, 103, 103));
                BackgroundGraphics.SetPixel(1, i, Color.FromArgb(103, 103, 103));
                BackgroundGraphics.SetPixel(X - 1, i, Color.FromArgb(103, 103, 103));
                BackgroundGraphics.SetPixel(X - 2, i, Color.FromArgb(103, 103, 103));
            }
            {
                SetCornerGradient(0, 0, Color.White, X, Y, ref BackgroundGraphics);
                SetCornerGradient(0, 1, Color.White, X, Y, ref BackgroundGraphics);
                SetCornerGradient(0, 2, Color.White, X, Y, ref BackgroundGraphics);
                SetCornerGradient(0, 3, Color.White, X, Y, ref BackgroundGraphics);
                SetCornerGradient(1, 0, Color.White, X, Y, ref BackgroundGraphics);
                SetCornerGradient(1, 1, Color.White, X, Y, ref BackgroundGraphics);
                SetCornerGradient(2, 0, Color.White, X, Y, ref BackgroundGraphics);
                SetCornerGradient(3, 0, Color.White, X, Y, ref BackgroundGraphics);
            }
            TEMPVaribles.SBG = BackgroundGraphics;
            TEMPVaribles.SBGFined = true;
        }
        void SetCornerGradient(int X, int Y, int PicX, int PicY, ref Bitmap Pic)
        {
            Pic.SetPixel(X,Y,Color.FromArgb(102,103,103));
            Pic.SetPixel(PicX - X -1,Y,Color.FromArgb(102,103,103));
            Pic.SetPixel(X,PicY - Y -1,Color.FromArgb(102,103,103));
            Pic.SetPixel(PicX - X -1, PicY - Y -1, Color.FromArgb(102, 103, 103));
        }
        void SetCornerGradient(int X, int Y,Color thecolor, int PicX, int PicY, ref Bitmap Pic)
        {
            Pic.SetPixel(X, Y,thecolor);
            Pic.SetPixel(PicX - X - 1, Y,thecolor);
            Pic.SetPixel(X, PicY - Y - 1, thecolor);
            Pic.SetPixel(PicX - X - 1, PicY - Y - 1, thecolor);
        }

        int orthogonal_projection(double X1, double Y1, double X2, double Y2)
        {
            //計算(X1,Y1)正射影於(X2,Y2),並將結果儲存至TempX和TempY
            double up, down;
            up = (X1 * X2) + (Y1 * Y2);
            down = (X2 * X2) + (Y2 * Y2);
            TempX = (up / down) * X2;
            TempY = (up / down) * Y2;
            return 0;
        }
        void SpeedCount()
        {
            LastMouseX = MouseLocation.X;
            LastMouseY = MouseLocation.Y;
            MouseLocation.X = System.Windows.Forms.Cursor.Position.X;
            MouseLocation.Y = System.Windows.Forms.Cursor.Position.Y;
            MouseSpeed.X = MouseLocation.X - LastMouseX;
            MouseSpeed.Y = MouseLocation.Y - LastMouseY;
            /**********/
            FormLocation.X = this.Location.X;
            FormLocation.Y = this.Location.Y;
            /************/
        }
        void SetNewLocation(int X,int Y) 
        {
            if(X != -1)
            this.Left = X;
            if(Y != -1)
            this.Top = Y;
        }
        void TimerSet(bool IsMoveWork)
        {
            MoveMe.Enabled = IsMoveWork;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            MainLabel.BackColor = Color.Transparent;
            TargetDayLabel.BackColor = Color.Transparent;
            TargetTimeLabel.BackColor = Color.Transparent;
            MaxLabel.BackColor = Color.Transparent;
            AfterLabel.BackColor = Color.Transparent;
            TargetDayLabel.Left = this.Width - TargetDayLabel.Width;
            TargetDayLabel.Top = this.Height - 57;
            AfterLabel.Left = this.Width - AfterLabel.Width - 12;
            AfterLabel.Top = this.Height - AfterLabel.Height - 9;
            AfterLabel. BringToFront();
            MaxLabel.Left = AfterLabel.Left - MaxLabel.Width + 9;
            MaxLabel.Top = AfterLabel.Top - 13;
            if (SettingValues.checkTIME && SettingValues.checkDATE)
            {
                TargetTimeLabel.Left = TargetDayLabel.Left + 89;
                TargetTimeLabel.Top = TargetDayLabel.Top + 8;
            }
        }

        private void LeftTimeCount_Tick(object sender, EventArgs e)
        {
            //文字設定
            MainLabel.Text = SettingValues.TEXT;
            MainLabel.ForeColor = SettingValues.TEXTcolor;
            this.Text = SettingValues.TEXT;
            #region 日期+時間
            if (SettingValues.checkDATE && SettingValues.checkTIME)//
            {
                TimeSpan L = SettingValues.datetime().Subtract(DateTime.Now);
                #region 面板顯示
                TargetTimeLabel.Visible = true;
                TargetTimeLabel.Text = SettingValues.time.ToString("HH:mm:ss");
                TargetDayLabel.Visible = true;
                MaxLabel.Visible = true;
                AfterLabel.Visible = true;
                if (SettingValues.changd_date_at_0000)
                {
                    /****************************/
                    //未完成--午夜換日
                    /****************************/
                }
                else
                {
                    if (SettingValues.DayOnly)
                    {
                        if (L.Days == 0)
                        {
                            if (L.TotalSeconds > 1)
                            {
                                TargetDayLabel.Text = SettingValues.date.ToString("yyyy/MM/dd") + "            還剩";
                                MaxLabel.ForeColor = Color.Red;
                                if (L.Hours > 0)
                                {
                                    MaxLabel.Text = L.Hours.ToString() + "小時";
                                    AfterLabel.Text = (L.Minutes).ToString("00") + "分" + (L.Seconds).ToString("00") + "秒";
                                }
                                else if (L.Minutes > 0)
                                {
                                    MaxLabel.Text = L.Minutes.ToString() + "分鐘";
                                    AfterLabel.Text = L.Seconds.ToString("00") + "秒";
                                }
                                else
                                {
                                    MaxLabel.Text = L.Seconds.ToString() + "秒";
                                    AfterLabel.Text = "";
                                }
                            }
                            else if (L.TotalSeconds < -1)
                            {
                                TargetDayLabel.Text = SettingValues.date.ToString("yyyy/MM/dd") + "            已過";
                                MaxLabel.ForeColor = Color.DarkRed;
                                if (L.Hours < 0)
                                {
                                    MaxLabel.Text = (L.Hours * -1).ToString() + "小時";
                                    AfterLabel.Text = (L.Minutes * -1).ToString("00") + "分" + (L.Seconds * -1).ToString("00") + "秒";
                                }
                                else if (L.Minutes < 0)
                                {
                                    MaxLabel.Text = (L.Minutes * -1).ToString() + "分鐘";
                                    AfterLabel.Text = (L.Seconds * -1).ToString("00") + "秒";
                                }
                                else
                                {
                                    MaxLabel.Text = (L.Seconds * -1).ToString() + "秒";
                                    AfterLabel.Text = "";
                                }
                            }
                            else
                            {
                                MaxLabel.Text = "到了!!!";
                                MaxLabel.ForeColor = Color.OrangeRed;
                                AfterLabel.Text = "";
                            }
                        }
                        else if (L.Days > 0)
                        {
                            TargetDayLabel.Text = SettingValues.date.ToString("yyyy/MM/dd") + "            還剩";
                            MaxLabel.ForeColor = Color.Red;
                            MaxLabel.Text = L.Days.ToString();
                            AfterLabel.Text = "天 " + L.Hours.ToString("00") + ":" + L.Minutes.ToString("00") + ":" + L.Seconds.ToString("00");
                        }
                        else
                        {
                            TargetDayLabel.Text = SettingValues.date.ToString("yyyy/MM/dd") + "            已過";
                            MaxLabel.ForeColor = Color.DarkRed;
                            MaxLabel.Text = (L.Days * -1).ToString();
                            AfterLabel.Text = "天 又" + (L.Hours * -1).ToString("00") + ":" + (L.Minutes * -1).ToString("00") + ":" + (L.Seconds * -1).ToString("00");
                        }
                    }
                    else
                    {
                        /****************************/
                        //未完成---使用年
                        /****************************/
                    }

                }
                #endregion
                #region 時間到提醒
                if (L.Days == 0)
                {
                    if (L.TotalSeconds < 1 && L.TotalSeconds > -1)
                    {
                        if (NotRemindedYet)
                        {
                            remind();
                            NotRemindedYet = false;
                        }
                    }
                    else
                    {
                        NotRemindedYet = true;
                    }
                }
                #endregion
            }
            #endregion
            #region 日期Only
            else if (SettingValues.checkDATE && !SettingValues.checkTIME)
            {
                TimeSpan L = SettingValues.date.Subtract(DateTime.Now);
                #region 面板顯示
                TargetTimeLabel.Visible = false;
                TargetDayLabel.Visible = true;
                MaxLabel.Visible = true;
                AfterLabel.Visible = true;
                if (SettingValues.changd_date_at_0000)
                {
                    /****************************/
                    //未完成--午夜換日
                    /****************************/
                }
                else
                {
                    if (SettingValues.DayOnly)
                    {
                        if (L.Days == 0)
                        {
                            if (L.TotalSeconds > 1)
                            {
                                TargetDayLabel.Text = SettingValues.date.ToString("yyyy/MM/dd") + "還剩";
                                MaxLabel.ForeColor = Color.Red;
                                if (L.Hours > 0)
                                {
                                    MaxLabel.Text = L.Hours.ToString() + "小時";
                                    AfterLabel.Text = (L.Minutes).ToString("00") + "分" + (L.Seconds).ToString("00") +"秒";
                                }
                                else if (L.Minutes > 0)
                                {
                                    MaxLabel.Text = L.Minutes.ToString() + "分鐘";
                                    AfterLabel.Text = L.Seconds.ToString("00") + "秒";
                                }
                                else
                                {
                                    MaxLabel.Text = L.Seconds.ToString() + "秒";
                                    AfterLabel.Text = "";
                                }
                            }
                            else if (L.TotalSeconds < -1)
                            {
                                TargetDayLabel.Text = SettingValues.date.ToString("yyyy/MM/dd") + "已過";
                                MaxLabel.ForeColor = Color.DarkRed;
                                if (L.Hours < 0)
                                {
                                    MaxLabel.Text = (L.Hours * -1).ToString() + "小時";
                                    AfterLabel.Text = (L.Minutes * -1).ToString("00") + "分" + (L.Seconds * -1).ToString("00") + "秒";
                                }
                                else if (L.Minutes < 0)
                                {
                                    MaxLabel.Text = (L.Minutes * -1).ToString() + "分鐘";
                                    AfterLabel.Text = (L.Seconds * -1).ToString("00") + "秒";
                                }
                                else
                                {
                                    MaxLabel.Text = (L.Seconds * -1).ToString() + "秒";
                                    AfterLabel.Text = "";
                                }
                            }
                            else
                            {
                                MaxLabel.Text = "到了!!!";
                                MaxLabel.ForeColor = Color.OrangeRed;
                                AfterLabel.Text = "";
#if DEBUG
                                System.Diagnostics.Debug.Print("到了!!!");
#endif
                            }
                        }
                        else if (L.Days > 0)
                        {
                            TargetDayLabel.Text = SettingValues.date.ToString("yyyy/MM/dd") + "還剩";
                            MaxLabel.ForeColor = Color.Red;
                            MaxLabel.Text = L.Days.ToString();
                            AfterLabel.Text = "天 " + L.Hours.ToString("00") + ":" + L.Minutes.ToString("00") + ":" + L.Seconds.ToString("00");
                        }
                        else
                        {
                            TargetDayLabel.Text = SettingValues.date.ToString("yyyy/MM/dd") + "已過";
                            MaxLabel.ForeColor = Color.DarkRed;
                            MaxLabel.Text = (L.Days * -1).ToString();
                            AfterLabel.Text = "天 又" + (L.Hours * -1).ToString("00") + ":" + (L.Minutes * -1).ToString("00") + ":" + (L.Seconds * -1).ToString("00");
                        }
                    }
                    else
                    {
                        /****************************/
                        //未完成---使用年
                        /****************************/
                    }
                }
                #endregion
                #region 時間到提醒
                if (L.Days == 0)
                {
                    if (L.TotalSeconds < 1 && L.TotalSeconds > -1)
                    {
                        if (NotRemindedYet)
                        {
                            remind();
                            NotRemindedYet = false;
                        }
                    }
                    else
                    {
                        NotRemindedYet = true;
                    }
                }
                #endregion
            }
            #endregion
            #region 時間Only
            else if (!SettingValues.checkDATE && SettingValues.checkTIME)//
            {
                TimeSpan L;
                DateTime Dt = DateTime.Now.Date;
                Dt = Dt.AddHours(SettingValues.time.Hour);
                Dt = Dt.AddMinutes(SettingValues.time.Minute);
                Dt = Dt.AddSeconds(SettingValues.time.Second);
                L = Dt.Subtract(DateTime.Now);
                #region 面板顯示
                TargetDayLabel.Visible = true;
                TargetTimeLabel.Visible = false;
                MaxLabel.Visible = true;
                AfterLabel.Visible = true;

                if (L.TotalSeconds > 1)
                {
                    TargetDayLabel.Text = SettingValues.time.ToString("HH:mm:ss") + "還剩";
                    MaxLabel.ForeColor = Color.Red;
                    if (L.Hours > 0)
                    {
                        MaxLabel.Text = L.Hours.ToString() + "小時";
                        AfterLabel.Text = (L.Minutes).ToString("00") + "分" + (L.Seconds).ToString("00") + "秒";
                    }
                    else if (L.Minutes > 0)
                    {
                        MaxLabel.Text = L.Minutes.ToString() + "分鐘";
                        AfterLabel.Text = L.Seconds.ToString("00") + "秒";
                    }
                    else
                    {
                        MaxLabel.Text = L.Seconds.ToString() + "秒";
                        AfterLabel.Text = "";
                    }
                }
                else if (L.TotalSeconds < -1)
                {
                    TargetDayLabel.Text = SettingValues.time.ToString("HH:mm:ss") + "已過";
                    MaxLabel.ForeColor = Color.DarkRed;
                    if (L.Hours < 0)
                    {
                        MaxLabel.Text = (L.Hours * -1).ToString() + "小時";
                        AfterLabel.Text = (L.Minutes * -1).ToString("00") + "分" + (L.Seconds * -1).ToString("00") + "秒";
                    }
                    else if (L.Minutes < 0)
                    {
                        MaxLabel.Text = (L.Minutes * -1).ToString() + "分鐘";
                        AfterLabel.Text = (L.Seconds * -1).ToString("00") + "秒";
                    }
                    else
                    {
                        MaxLabel.Text = (L.Seconds * -1).ToString() + "秒";
                        AfterLabel.Text = "";
                    }
                }
                else
                {
                    MaxLabel.Text = "到了!!!";
                    MaxLabel.ForeColor = Color.OrangeRed;
                    AfterLabel.Text = "";
#if DEBUG
                    System.Diagnostics.Debug.Print("到了!!!");
#endif
                }
                #endregion
                #region 時間到提醒
                if (L.TotalSeconds < 1 && L.TotalSeconds > -1)
                {
                    if (NotRemindedYet)
                    {
                        remind();
                        NotRemindedYet = false;
                    }
                }
                else
                {
                    NotRemindedYet = true;
                }
                #endregion
            }
            #endregion
            #region Both Not Select
            else if (!SettingValues.checkDATE && !SettingValues.checkTIME)//都不選，純便條貼
            {
                TargetTimeLabel.Visible = false;
                TargetDayLabel.Visible = false;
                MaxLabel.Visible = false;
                AfterLabel.Visible = false;
            }
            #endregion


        }
        //static 
        void remind()
        {
            Thread ThreadRemind = new Thread(new ThreadStart(RealRemind));
            ThreadRemind.Start();
            
        }
        static void RealRemind()
        {
#if DEBUG
            System.Diagnostics.Debug.Print("提醒!!!!!");
#endif
            if (SettingValues.TimeUpRemind_Speed)
            {
                MySpeed.X += SettingValues.TimeUpRemind_Speed_X;
                MySpeed.Y += SettingValues.TimeUpRemind_Speed_Y;
            }
            if (SettingValues.TimeUpRemind_MessabeBox)
            {
                MessageBox.Show(SettingValues.TimeUpRemind_TEXT, SettingValues.TimeUpRemind_Title, MessageBoxButtons.OK, SettingValues.TimeUpRemind_MessabeBox_Icons, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
            return;
        }
    }

     class ThreadMethodHelper
    {
          //线程输入参数
          public int x = 0;
          public int y = 0;
          //函数返回值
          //public long returnVaule;
    }
}
