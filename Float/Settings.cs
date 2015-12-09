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
    public delegate void ReDrawView(int Width, int Height);

    public partial class Settings : Form
    {
        public event ReDrawView redrawview;
        public event NewLocation SetNew;
        public Settings()
        {
            InitializeComponent();
        }

        public ShowActiveZoom SAZ_Blue = new ShowActiveZoom();
        #region Global
        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.VisibleChanged -= new EventHandler(Settings_VisibleChanged);
            e.Cancel = true;
            this.Hide();
            this.SAZ_Blue.Hide();
            TEMPVaribles.israngeshow = false;
            this.VisibleChanged += new EventHandler(Settings_VisibleChanged);
        }

        #region 確定取消套用
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.VisibleChanged -= new EventHandler(Settings_VisibleChanged);
            this.Hide();
            this.SAZ_Blue.Hide();
            TEMPVaribles.israngeshow = false;
            this.VisibleChanged += new EventHandler(Settings_VisibleChanged);
        }
        private void OK_Click(object sender, EventArgs e)
        {
            apply();
            this.Hide();
        }
        private void Apply_Click(object sender, EventArgs e)
        {
            apply();
        }
        #endregion
        private void Settings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.D)
            {
                TEMPVaribles.isdebug = true;
            }
        }//打開Debug模式
        int RX;
        int RY;
        private void Settings_Load(object sender, EventArgs e)
        {
            labelAbout.Text += SettingValues.Verson;
            RX = this.Width - 180;//在不同解析度和DPI螢幕上，視窗大小的表現會有些不同!
            RY = this.Height - 96;
        }//Load
        private void Settings_VisibleChanged(object sender, EventArgs e)
        {
            /******************///外觀
            TEXT.Height = 31 * (SettingValues.TEXT.Split('\n').GetLength(0));
            TEXT.ForeColor = SettingValues.TEXTcolor;
            labelTEXT.ForeColor = SettingValues.TEXTcolor;
            ViewPanel.Width = SettingValues.Width;
            ViewPanel.Height = SettingValues.Height;
            UpDownW.Maximum = (int)(Math.Sqrt(2) * WindowSize.S / 2) - 10;
            UpDownH.Maximum = (int)(Math.Sqrt(2) * WindowSize.S / 2) - 10;
            UpDownW.Value = ViewPanel.Width;
            UpDownH.Value = ViewPanel.Height;
            checkCloseHint.Checked = !SettingValues.Is_Tip_Mode;
            labelTEXT.Text = SettingValues.TEXT;
            labelTEXT.BackColor = Color.Transparent;
            TEXT.Text = SettingValues.TEXT;
            redrawview(ViewPanel.Width, ViewPanel.Height);
            if (RunOnce) JustShowTip_ClickToEdit.Enabled = true;
            /******************///倒計時模式
            if (SettingValues.changd_date_at_0000) this.ChangeDate0000.Checked = true;
            else this.ChangeDate1234.Checked = true;
            checkDayOnly.Checked = SettingValues.DayOnly;
            checkDate.Checked = SettingValues.checkDATE;
            checkTime.Checked = SettingValues.checkTIME;
            DateSelect.Value = SettingValues.date;
            TimeSelect.Value = SettingValues.time;
            /*****************///運動模式
            if (SettingValues.Meet_Border_Is_Rebound) this.SelectRebound.Checked = true;
            else this.SelectOvergo.Checked = true;
            BarRANGEvaule.Maximum = WindowSize.S / 2;
            BarRANGEvaule.Minimum = (int)Math.Sqrt((SettingValues.Height * SettingValues.Height) + (SettingValues.Width * SettingValues.Width)) / 2;
            if (SettingValues.Range == 1) BarRANGEvaule.Value = BarRANGEvaule.Minimum;
            else BarRANGEvaule.Value = SettingValues.Range;
            CheckShowActineZoon.Checked = SettingValues.Is_Range_Show;
            CheckEscape.Checked = SettingValues.IsEscape;
            RESVaule.Value = (decimal)SettingValues.Resistance;
            label2.Text = "感應範圍:" + BarRANGEvaule.Value.ToString();
            this.BarRANGEvaule.Width = this.Width - (131 + this.label2.Width - 56);
            this.BarRANGEvaule.Left = 20 + this.label2.Width;
            SAZ_Blue.RangColor = Color.Blue;
            /*****************///時間到提醒
            checkTimeUpRemind.Checked = SettingValues.TimeUpRemind;
            checkRemindS.Checked = SettingValues.TimeUpRemind_Speed;
            ScaleUpDownX.Value = SettingValues.TimeUpRemind_Speed_X;
            ScaleUpDownY.Value = SettingValues.TimeUpRemind_Speed_Y;
            ScaleUpDownX.Maximum = WindowSize.S;
            ScaleUpDownY.Maximum = WindowSize.S;
            ScaleUpDownX.Minimum = WindowSize.S * -1;
            ScaleUpDownY.Minimum = WindowSize.S * -1;
            checkRemindM.Checked = SettingValues.TimeUpRemind_MessabeBox;
            textRemindMTitle.Text = SettingValues.TimeUpRemind_Title;
            textRemindM.Text = SettingValues.TimeUpRemind_TEXT;
            switch (SettingValues.TimeUpRemind_MessabeBox_Icons)
            {
                case MessageBoxIcon.None:
                    radioMIconsNone.Checked = true;
                    break;
                case MessageBoxIcon.Error:
                    radioMIconsError.Checked = true;
                    break;
                case MessageBoxIcon.Question:
                    radioMIconsQuestion.Checked = true;
                    break;
                case MessageBoxIcon.Warning:
                    radioMIconsWarning.Checked = true;
                    break;
                default:
                    radioMIconsInfomation.Checked =true;
                    break;
            }
            /*****************///Facebook提醒

        }//載入
        void apply()
        {

            this.VisibleChanged -= new EventHandler(Settings_VisibleChanged);

            /******************///外觀
            SettingValues.TEXT = labelTEXT.Text;
            SettingValues.TEXTcolor = TEXT.ForeColor;
            labelTEXT.BackColor = Color.Transparent;
            TargetDayLabel.BackColor = Color.Transparent;
            TargetTimeLabel.BackColor = Color.Transparent;
            MaxLabel.BackColor = Color.Transparent;
            AfterLabel.BackColor = Color.Transparent;
            SettingValues.Is_Tip_Mode = !checkCloseHint.Checked;
            if ((SettingValues.Width != ViewPanel.Width) || (SettingValues.Height != ViewPanel.Height))
            {
                MySpeed.X = 0; MySpeed.Y = 0;
                SetNew(10, 10);
                SettingValues.Width = ViewPanel.Width;
                SettingValues.Height = ViewPanel.Height;
                TEMPVaribles.isSizeChanged = true;
                BarRANGEvaule.Minimum = (int)Math.Sqrt((SettingValues.Height * SettingValues.Height) + (SettingValues.Width * SettingValues.Width)) / 2;
                if (SettingValues.Range < BarRANGEvaule.Minimum && SettingValues.Range != 1)
                {
                    SettingValues.Range = BarRANGEvaule.Minimum + 1;
                    TEMPVaribles.israngechanged = true;
                }
            }
            /******************///倒計時模式
            SettingValues.changd_date_at_0000 = ChangeDate0000.Checked;
            SettingValues.date = DateSelect.Value;
            SettingValues.time = TimeSelect.Value;
            SettingValues.checkDATE = checkDate.Checked;
            SettingValues.checkTIME = checkTime.Checked;
            SettingValues.DayOnly = checkDayOnly.Checked;
            /*****************///運動模式
            if (BarRANGEvaule.Value == BarRANGEvaule.Minimum)
            {
                SettingValues.Range = 1;
            }
            else
            {
                SettingValues.Range = BarRANGEvaule.Value;
            }
            SettingValues.Is_Range_Show = CheckShowActineZoon.Checked;
            TEMPVaribles.israngechanged = true;
            SettingValues.Resistance = (double)RESVaule.Value;
            SettingValues.Meet_Border_Is_Rebound = this.SelectRebound.Checked;
            SettingValues.IsEscape = this.CheckEscape.Checked;
            if ((int)(Math.Sqrt(ViewPanel.Height * ViewPanel.Height + ViewPanel.Width * ViewPanel.Width) / 2) + 1 > SettingValues.Range && SettingValues.Range != 1)
            {
                SettingValues.Range = (int)(Math.Sqrt(ViewPanel.Height * ViewPanel.Height + ViewPanel.Width * ViewPanel.Width) / 2) + 1;
                BarRANGEvaule.Value = SettingValues.Range;
                label2.Text = "感應範圍:" + this.BarRANGEvaule.Value.ToString();
                this.BarRANGEvaule.Width = this.Width - (131 + this.label2.Width - 56);
                this.BarRANGEvaule.Left = 20 + this.label2.Width;
            }
            this.SAZ_Blue.Hide();
            TEMPVaribles.israngeshow = false;
            /*****************///時間到提醒
            SettingValues.TimeUpRemind = checkTimeUpRemind.Checked;
            SettingValues.TimeUpRemind_Speed = checkRemindS.Checked;
            SettingValues.TimeUpRemind_Speed_X = (int)ScaleUpDownX.Value;
            SettingValues.TimeUpRemind_Speed_Y = (int)ScaleUpDownY.Value;
            SettingValues.TimeUpRemind_MessabeBox = checkRemindM.Checked;
            SettingValues.TimeUpRemind_Title = textRemindMTitle.Text;
            SettingValues.TimeUpRemind_TEXT = textRemindM.Text;
            if (radioMIconsNone.Checked)
                SettingValues.TimeUpRemind_MessabeBox_Icons = MessageBoxIcon.None;
            else if (radioMIconsError.Checked)
                SettingValues.TimeUpRemind_MessabeBox_Icons = MessageBoxIcon.Error;
            else if (radioMIconsQuestion.Checked)
                SettingValues.TimeUpRemind_MessabeBox_Icons = MessageBoxIcon.Question;
            else if (radioMIconsWarning.Checked)
                SettingValues.TimeUpRemind_MessabeBox_Icons = MessageBoxIcon.Warning;
            else
                SettingValues.TimeUpRemind_MessabeBox_Icons = MessageBoxIcon.Information;
            /*****************///Facebook提醒


            this.VisibleChanged += new EventHandler(Settings_VisibleChanged);
        }//////////////////////確定
        #endregion

        #region 外觀 分頁
        private void ViewBotton_Click(object sender, EventArgs e)
        {
            ViewPanel.Width = (int)UpDownW.Value;
            ViewPanel.Height = (int)UpDownH.Value;
            B();
            redrawview(ViewPanel.Width, ViewPanel.Height);

            /*/*************************/
            //MessageBox.Show(DateSelect.Value.ToString() + "\n" + TimeSelect.Value.ToString() + "\n" + SettingValues.datetime().ToString());
            /***********************/
        }
        bool RunOnce = true;

        private void ViewPanel_Resize(object sender, EventArgs e)
        {
            if (this.Width - RX < ViewPanel.Width)
                this.Width = ViewPanel.Width + RX;
            if (this.Height - RY < ViewPanel.Height)
                this.Height = ViewPanel.Height + RY;
            MakeSureShowDragPoint.Location = (Point)ViewPanel.Size;


        }//調整設定視窗大小

        #region TEXT
        private void ShowTEXT_MouseUp(object sender, MouseEventArgs e)
        {
            TEXT.Visible = true;
            TEXT.Focus();
        }//顯示文字輸入框
        private void HideTEXT_Click(object sender, EventArgs e)
        {
            TEXT.Visible = false;
            labelTEXT.Text = TEXT.Text;
        }
        private void TEXT_Leave(object sender, EventArgs e)
        {
#if DEBUG
            //System.Diagnostics.Debug.Print("Leave");
#endif
            TEXT.Visible = false;
            labelTEXT.Text = TEXT.Text;
            labelTEXT.ForeColor = TEXT.ForeColor;
        }//隱藏文字輸入框
        private void TEXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (TEXT.Height > ViewPanel.Height)
                {
                    e.Handled = true;
                }
                else
                    TEXT.Height = 31 * (TEXT.Lines.GetLength(0) + 1);
            }
            else if ((e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete) && (TEXT.Lines.GetLength(0) > 1) && TEXT.Lines[TEXT.Lines.GetLength(0) - 1] == "")
            {
                TEXT.Height = 31 * (TEXT.Lines.GetLength(0) - 1);
            }
        }
        #endregion

        private void JustShowTip_ClickToEdit_Tick(object sender, EventArgs e)
        {
            if (RunOnce)
            {
                BarcodeControl(ViewPanel);
                TipMode.ToolTipTitle = "修改文字";
                TipMode.Show("按一下這附近可以更改文字\n按Enter換行", labelTEXT);
                TipMode.Show("按一下這附近可以更改文字\n按Enter換行", labelTEXT);
                RunOnce = false;
            }
            JustShowTip_ClickToEdit.Enabled = false;
            JustShowTip_ClickToEdit.Dispose();
        }//顯示"按一下這裡可以更改文字"

        private void RESVaule_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 46 && e.KeyChar != 8 && e.KeyChar != 127)
            {
                e.Handled = true;
            }
        }//摩擦力只能輸入數字

        private void TextColorBotton_Click(object sender, EventArgs e)
        {
            ColorDialog1.Color = TEXT.ForeColor;
            if (ColorDialog1.ShowDialog() == DialogResult.OK)
            {
                TEXT.ForeColor = ColorDialog1.Color;
                labelTEXT.ForeColor = TEXT.ForeColor;
            }
        }//修改顏色


        private void ViewPanel_Paint(object sender, PaintEventArgs e)
        {


            this.ViewPanel.Paint -= new PaintEventHandler(ViewPanel_Paint);
#if DEBUG
            System.Diagnostics.Debug.Print("Paint");
#endif
            TargetDayLabel.Left = this.ViewPanel.Width - TargetDayLabel.Width;
            TargetDayLabel.Top = this.ViewPanel.Height - 57;
            AfterLabel.Left = this.ViewPanel.Width - AfterLabel.Width - 12;
            AfterLabel.Top = this.ViewPanel.Height - AfterLabel.Height - 9;
            AfterLabel.BringToFront();
            MaxLabel.Left = AfterLabel.Left - MaxLabel.Width + 9;
            MaxLabel.Top = AfterLabel.Top - 13;
            if (checkDate.Checked && checkTime.Checked)
            {
                TargetTimeLabel.Left = TargetDayLabel.Left + 89;
                TargetTimeLabel.Top = TargetDayLabel.Top + 8;
            }
            this.ViewPanel.Paint += new PaintEventHandler(ViewPanel_Paint);
        }//重繪事件

        private void checkDate_and_Time_CheckedChanged(object sender, EventArgs e)
        {
#if DEBUG
            System.Diagnostics.Debug.Print("CheckedChanged");
#endif

            if (checkDate.Checked && checkTime.Checked)
            {
                DateSelect.Enabled = true;
                TimeSelect.Enabled = true;
                TargetTimeLabel.Visible = true;
                TargetTimeLabel.BringToFront();
                TargetTimeLabel.Text = TimeSelect.Value.ToString("HH:mm:ss");
                TargetDayLabel.Visible = true;
                MaxLabel.Visible = true;
                MaxLabel.Text = "999";
                AfterLabel.Visible = true;
                AfterLabel.Text = "天 23:59:59";
                TargetDayLabel.Text = DateSelect.Value.ToString("yyyy/MM/dd") + "            還剩";
                TargetTimeLabel.Text = TimeSelect.Value.ToString("HH:mm:ss");
                if (!checkCloseHint.Checked)
                {
                    TipMode.ToolTipTitle = "普通模式";
                    TipMode.Show("計算到目標的時間差。", panelDateTime_check, 6 + 13, 28 + 13);
                    TipMode.Show("計算到目標的時間差。", panelDateTime_check, 6 + 13, 28 + 13);
                }
            }
            else if (checkDate.Checked && !checkTime.Checked)
            {
                DateSelect.Enabled = true;
                TimeSelect.Enabled = false;
                TargetTimeLabel.Visible = false;
                TargetDayLabel.Visible = true;
                MaxLabel.Visible = true;
                MaxLabel.Text = "999";
                AfterLabel.Visible = true;
                AfterLabel.Text = "天 23:59:59";
                TargetDayLabel.Text = DateSelect.Value.ToString("yyyy/MM/dd") + "還剩";
                if (!checkCloseHint.Checked)
                {
                    TipMode.ToolTipTitle = "只選日期";
                    TipMode.Show("只選日期，計算到目標日期的時間差。", panelDateTime_check, 6 + 13, 3 + 13);
                    TipMode.Show("只選日期，計算到目標日期的時間差。", panelDateTime_check, 6 + 13, 3 + 13);
                }
            }
            else if (!checkDate.Checked && checkTime.Checked)
            {
                DateSelect.Enabled = false;
                TimeSelect.Enabled = true;
                TargetDayLabel.Visible = true;
                TargetTimeLabel.Visible = false;
                MaxLabel.Visible = true;
                MaxLabel.Text = "23小時";
                AfterLabel.Visible = true;
                AfterLabel.Text = "又 59:59";
                TargetDayLabel.Text = TimeSelect.Value.ToString("HH:mm:ss") + "還剩";
                if (!checkCloseHint.Checked)
                {
                    TipMode.ToolTipTitle = "只選時間";
                    TipMode.Show("只選時間，計算到目標的時間差。每日重複!", panelDateTime_check, 6 + 13, 28 + 13);
                    TipMode.Show("只選時間，計算到目標的時間差。每日重複!", panelDateTime_check, 6 + 13, 28 + 13);
                }
            }
            else
            {
                DateSelect.Enabled = false;
                TimeSelect.Enabled = false;
                TargetTimeLabel.Visible = false;
                TargetDayLabel.Visible = false;
                MaxLabel.Visible = false;
                AfterLabel.Visible = false;
                if (!checkCloseHint.Checked)
                {
                    TipMode.ToolTipTitle = "便利貼模式(都不選)";
                    TipMode.Show("時間和日期都不選，只顯示文字!當成便利貼來使用。\n\n可以寫些東西提醒自己，例如:\n沒事多喝水\n多喝水沒事", panelDateTime_check, 6 + 13, 28 + 13);
                    TipMode.Show("時間和日期都不選，只顯示文字!當成便利貼來使用。\n\n可以寫些東西提醒自己，例如:\n沒事多喝水\n多喝水沒事", panelDateTime_check, 6 + 13, 28 + 13);
                }
            }

        }

        #endregion
        #region 運動模式 分頁
        private void BarRANGEvaule_Scroll(object sender, EventArgs e)
        {
            if (this.BarRANGEvaule.Value == this.BarRANGEvaule.Minimum)
            {
                SAZ_Blue.Hide();
                label2.Text = "感應範圍:0";
                if (SettingValues.Is_Tip_Mode)
                {
                    TipMode.ToolTipTitle = "不感應";
                    TipMode.Show("感應範圍=0，表示不感應滑鼠。\n視窗可以手動拖移", BarRANGEvaule,10,25);
                    TipMode.Show("感應範圍=0，表示不感應滑鼠。\n視窗可以手動拖移。", BarRANGEvaule,10,25);
                }
            }
            else
            {
                SAZ_Blue.Width = this.BarRANGEvaule.Value * 2;
                SAZ_Blue.Height = this.BarRANGEvaule.Value * 2;
                SAZ_Blue.Show();
                this.Focus();
                label2.Text = "感應範圍:" + this.BarRANGEvaule.Value.ToString();
                this.BarRANGEvaule.Width = this.Width - (131 + this.label2.Width - 56);
                this.BarRANGEvaule.Left = 20 + this.label2.Width;
#if DEBUG
                System.Diagnostics.Debug.Print(this.Width.ToString() + "-(131+" + this.label2.Width.ToString() +"-56)="+(this.Width - (131+this.label2.Width-56)).ToString());
#endif
            }
        }//感應範圍修改
        private void BarRANGEvaule_MouseDown(object sender, MouseEventArgs e)
        {
            TEMPVaribles.israngeshow = true;
        }//顯示範圍預覽
        
        private void SelectRebound_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckEscape.Enabled = this.SelectRebound.Checked;
        }//啟用/禁用逃脫複選框

        #endregion 運動模式
        #region 時間到提醒 分頁
        private void checkTimeUpInfoem_CheckedChanged(object sender, EventArgs e)
        {
            groupTimeUpRemind.Enabled = checkTimeUpRemind.Checked;
        }
        private void buttonTestS_Click(object sender, EventArgs e)
        {
            MySpeed.X += (double)ScaleUpDownX.Value;
            MySpeed.Y += (double)ScaleUpDownY.Value;
        }
        private void checkRemindS_CheckedChanged(object sender, EventArgs e)
        {
            tableUpDownXY.Enabled = checkRemindS.Checked;
            buttonTESTS.Enabled = checkRemindS.Checked;
        }
        private void checkRemindM_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxRemimdIcons.Enabled = checkRemindM.Checked;
            textRemindM.ReadOnly = !checkRemindM.Checked;
            buttonTESTM.Enabled = checkRemindM.Checked;
        }

        private void buttonTESTM_Click(object sender, EventArgs e)
        {
            MessageBoxIcon MICON;
            if (radioMIconsNone.Checked) MICON = MessageBoxIcon.None;
            else if (radioMIconsError.Checked) MICON = MessageBoxIcon.Error;
            else if (radioMIconsQuestion.Checked) MICON = MessageBoxIcon.Question;
            else if (radioMIconsWarning.Checked) MICON = MessageBoxIcon.Warning;
            else MICON = MessageBoxIcon.Information;
            MessageBox.Show(textRemindM.Text, textRemindMTitle.Text, MessageBoxButtons.OK, MICON);
        }
        #endregion
        #region Facebook提醒 分頁
        #endregion
        #region 關於 分頁
        private void textBoxURL_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(textURL.Text);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                System.Diagnostics.Process.Start("IExplore.exe", textURL.Text);
            }
            catch (System.Exception other)
            {
                MessageBox.Show("錯誤訊息:" + other.Message);
            }
        }
        #endregion












        #region 繪製拖動點
        #region private

        private const int MIN_SIZE_X = 55; //X最小值
        private const int MIN_SIZE_Y = 74; //Y最小值 
        private const int BOX_SIZE = 7;  //調整大小觸模柄方框大小

        public bool _IsCtrlKey = false;
        private TextBox _textbox;
        private Control _MControl = null;
        private Point _oPointClicked;
        private Color BOX_COLOR = Color.White;
        private Label[] _lbl = new Label[8];
        private int _startl, _startt, _startw, _starth;
        private bool _dragging;
        private Cursor[] _arrArrow = new Cursor[] { Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeNS, };

        #endregion

        #region 构造函数

        /// <summary> 
        /// 建構物件拖動對象
        /// </summary> 
        /// <param name="moveControl">需要拖动的物件 </param> 
        public void BarcodeControl(Control moveControl)
        {
            // 
            // TODO: 在此處添加建構函數
            // 
            _MControl = moveControl;
            _MControl.MouseDown += new MouseEventHandler(this.Control_MouseDown);
            _MControl.MouseUp += new MouseEventHandler(this.Control_MouseUp);
            _MControl.Click += new System.EventHandler(this.Control_Click);
            //建構3個調整大小觸模柄
            for (int i = 0; i < 3; i++)
            {
                _lbl[i] = new Label();
                _lbl[i].TabIndex = i;
                _lbl[i].FlatStyle = 0;
                _lbl[i].BorderStyle = BorderStyle.FixedSingle;
                _lbl[i].BackColor = BOX_COLOR;
                _lbl[i].Cursor = _arrArrow[i];
                _lbl[i].Text = "";
                _lbl[i].BringToFront();
                _lbl[i].MouseDown += new MouseEventHandler(this.handle_MouseDown);
                _lbl[i].MouseMove += new MouseEventHandler(this.handle_MouseMove);
                _lbl[i].MouseUp += new MouseEventHandler(this.handle_MouseUp);
            }

            CreateTextBox();
            Create();

            MoveHandles();
            ShowHandles();
            _MControl.Visible = true;
            //Control_Click((object)sender, (System.EventArgs)e); 
        }

        #endregion
        private void B()
        {
            _dragging = false;
            MoveHandles();
            ShowHandles();
            if (BarRANGEvaule.Value == BarRANGEvaule.Minimum || BarRANGEvaule.Value > (int)(Math.Sqrt((ViewPanel.Width * ViewPanel.Width) + (ViewPanel.Height * ViewPanel.Height))) / 2)
            {
                BarRANGEvaule.Minimum = (int)(Math.Sqrt((ViewPanel.Width * ViewPanel.Width) + (ViewPanel.Height * ViewPanel.Height)) / 2);
            }
            else
            {
                BarRANGEvaule.Minimum = (int)(Math.Sqrt((ViewPanel.Width * ViewPanel.Width) + (ViewPanel.Height * ViewPanel.Height)) / 2);
                BarRANGEvaule.Value = BarRANGEvaule.Minimum + 1;
            }
        }

        #region 滑鼠事件

        private void Control_Click(object sender, System.EventArgs e)
        {
            _textbox.Focus();
            _MControl = (sender as Control);
            MoveHandles();

            if (_IsCtrlKey == false)
            {
                for (int i = 0; i < _MControl.Parent.Controls.Count; i++)
                {
                    if (_MControl.Parent.Controls[i].Text.Trim().Length == 0 && _MControl.Parent.Controls[i] is Label)
                    {
                        _MControl.Parent.Controls[i].Visible = false;
                    }
                }
            }
        }
        private void Control_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _oPointClicked = new Point(e.X, e.Y);
            for (int i = 0; i < 3; i++)
            {
                _MControl.Parent.Controls.Add(_lbl[i]);
                _lbl[i].BringToFront();
            }
            HideHandles();
        }

        private void Control_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MoveHandles();
            ShowHandles();
            _MControl.Visible = true;
        }



        #endregion
        #region 调整大小触模柄鼠标事件

        int MAX_X,MAX_Y;
        private void handle_MouseDown(object sender, MouseEventArgs e)
        {
            MAX_X = (int)(Math.Sqrt(2) * WindowSize.S / 2) - 10;
            MAX_Y = (int)(Math.Sqrt(2) * WindowSize.S / 2) - 10;
            while (((MAX_X * MAX_X) + (ViewPanel.Height * ViewPanel.Height)) >= (WindowSize.S) * (WindowSize.S) - 10)
                MAX_X -= 1;
            while (((MAX_Y * MAX_Y) + (ViewPanel.Width * ViewPanel.Width)) >= (WindowSize.S) * (WindowSize.S) - 10)
                MAX_Y -= 1;
            _dragging = true;
            _startl = _MControl.Left;
            _startt = _MControl.Top;
            _startw = _MControl.Width;
            _starth = _MControl.Height;
            HideHandles();
        }
        // 通过触模柄调整控件大小 
        //  0  1  2 
        //  7     3 
        //  6  5  4 
        private void handle_MouseMove(object sender, MouseEventArgs e)
        {
            int l = _MControl.Left;
            int w = _MControl.Width;
            int t = _MControl.Top;
            int h = _MControl.Height;
            if (_dragging)
            {
                switch (((Label)sender).TabIndex)
                {
                    //l算法：控件左边X坐标 ＋ 鼠标在触模柄X坐标 < 控件左边X坐标 ＋ 父控件宽度 - 控件大小 ？控件左边X坐标 ＋ 鼠标在触模柄X坐标 ：控件左边X坐标 ＋ 父控件宽度 - 控件大小  
                    //t算法： 
                    //w算法： 
                    //h算法： 
                    /********************************************/
                    case 0: // _dragging right-middle sizing box 
                        if (_startw + e.X > MAX_X)           { w = MAX_X;          UpDownW.Value = MAX_X; }
                        else if (_startw + e.X > MIN_SIZE_X) { w = _startw + e.X;  UpDownW.Value = _startw + e.X; }
                        else                                 { w = MIN_SIZE_X;     UpDownW.Value = MIN_SIZE_X; }
                        break;
                    /********************************************/
                    case 1: // _dragging right-bottom sizing box 
                        if (_startw + e.X > MAX_X) { w = MAX_X; UpDownW.Value = MAX_X; }
                        else if (_startw + e.X > MIN_SIZE_X) { w = _startw + e.X; UpDownW.Value = _startw + e.X; }
                        else { w = MIN_SIZE_X; UpDownW.Value = MIN_SIZE_X; }
                        if (_starth + e.Y > MAX_Y) { h = MAX_Y; UpDownH.Value = MAX_Y; }
                        else if (_starth + e.Y > MIN_SIZE_Y) { h = _starth + e.Y; UpDownH.Value = _starth + e.Y; }
                        else { h = MIN_SIZE_Y; UpDownH.Value = MIN_SIZE_Y; }
                        break;
                    /********************************************/
                    case 2: // _dragging center-bottom sizing box 
                        if (_starth + e.Y > MAX_Y)           { h = MAX_Y;         UpDownH.Value = MAX_Y; }
                        else if (_starth + e.Y > MIN_SIZE_Y) {h = _starth + e.Y;  UpDownH.Value =_starth + e.Y;}
                        else                                 {h = MIN_SIZE_Y;     UpDownH.Value=MIN_SIZE_Y;}
                        break;

                }
                l = (l < 0) ? 0 : l;
                t = (t < 0) ? 0 : t;
                _MControl.SetBounds(l, t, w, h);
            }
        }

        private void handle_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
            MoveHandles();
            ShowHandles();
            if (BarRANGEvaule.Value == BarRANGEvaule.Minimum || BarRANGEvaule.Value > (int)(Math.Sqrt((ViewPanel.Width * ViewPanel.Width) + (ViewPanel.Height * ViewPanel.Height)))/2)
            {
                BarRANGEvaule.Minimum = (int)(Math.Sqrt((ViewPanel.Width * ViewPanel.Width) + (ViewPanel.Height * ViewPanel.Height))/2);
            }
            else
            {
                BarRANGEvaule.Minimum = (int)(Math.Sqrt((ViewPanel.Width * ViewPanel.Width) + (ViewPanel.Height * ViewPanel.Height))/2);
                BarRANGEvaule.Value = BarRANGEvaule.Minimum + 1;
            }
            redrawview(ViewPanel.Width, ViewPanel.Height);
            ViewPanel.Location = new Point(0, 0);
        }

        #endregion

        #region private方法

        private void Create()
        {
            //_IsMouseDown = true; 
            //_oPointClicked = new Point(e.X,e.Y); 
            for (int i = 0; i < 3; i++)
            {
                _MControl.Parent.Controls.Add(_lbl[i]);
                _lbl[i].BringToFront();
            }
            HideHandles();
        }

        private void CreateTextBox()
        {
            _textbox = new TextBox();
            _textbox.CreateControl();
            _textbox.Parent = _MControl.Parent;
            _textbox.Width = 0;
            _textbox.Height = 0;
            _textbox.TabStop = true;
        }

        private void ControlLocality()
        {
            if (_MControl.Location.X < 0)
            {
                _MControl.Visible = false;
                _MControl.Left = 0;
            }
            if (_MControl.Location.Y < 0)
            {
                _MControl.Visible = false;
                _MControl.Top = 0;
            }
            if (_MControl.Location.X + _MControl.Width > _MControl.Parent.Width)
            {
                _MControl.Visible = false;
                _MControl.Left = _MControl.Parent.Width - _MControl.Width;
            }
            if (_MControl.Location.Y + _MControl.Height > _MControl.Parent.Height)
            {
                _MControl.Visible = false;
                _MControl.Top = _MControl.Parent.Height - _MControl.Height;
            }
        }

        private void HideHandles()
        {
            for (int i = 0; i < 3; i++)
            {
                _lbl[i].Visible = false;
            }
        }

        private void MoveHandles()
        {
            int sX = _MControl.Left - BOX_SIZE;
            int sY = _MControl.Top - BOX_SIZE;
            int sW = _MControl.Width + BOX_SIZE;
            int sH = _MControl.Height + BOX_SIZE;
            int hB = BOX_SIZE / 2;
            int[] arrPosX = new int[] {

 

                sX + sW-hB, 
                sX + sW-hB, 
                sX + sW / 2, 
                };

            int[] arrPosY = new int[] {
                sY + sH / 2, 
                sY + sH-hB, 
                sY + sH-hB, 
            };
            for (int i = 0; i < 3; i++)
            {
                _lbl[i].SetBounds(arrPosX[i], arrPosY[i], BOX_SIZE, BOX_SIZE);
            }
        }

        private void ShowHandles()
        {
            if (_MControl != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    _lbl[i].Visible = true;
                }
            }
        }

        #endregion


        #endregion




        


        #region 紀念我曾寫過這麼複雜的東西。雖然後來決定不採用
        /*private void UpDownW_ValueChanged(object sender, EventArgs e)
        {
            //UpDownH.VisibleChanged -= new EventHandler(UpDownH_ValueChanged);
            while (((UpDownH.Value * UpDownH.Value) + (UpDownW.Maximum * UpDownW.Maximum)) >= (WindowSize.S) * (WindowSize.S) - 10)
                UpDownW.Maximum -= 1;
            while (((UpDownH.Maximum * UpDownH.Maximum) + (UpDownW.Value * UpDownW.Value)) < (WindowSize.S) * (WindowSize.S) - 10)
                UpDownH.Maximum += 1;
            //UpDownH.VisibleChanged += new EventHandler(UpDownH_ValueChanged);
        }

        private void UpDownH_ValueChanged(object sender, EventArgs e)
        {

            //UpDownW.VisibleChanged -= new EventHandler(UpDownW_ValueChanged);
            while (((UpDownW.Value * UpDownW.Value) + (UpDownH.Maximum * UpDownH.Maximum)) >= (WindowSize.S) * (WindowSize.S) - 10)
                UpDownH.Maximum -= 1;
            while (((UpDownW.Maximum * UpDownW.Maximum) + (UpDownH.Value * UpDownH.Value)) < (WindowSize.S) * (WindowSize.S) - 10)
                UpDownW.Maximum += 1;
            //UpDownH.VisibleChanged += new EventHandler(UpDownH_ValueChanged);
        }*/
        #endregion
    }
}
