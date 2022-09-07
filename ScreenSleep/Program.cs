using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ScreenSleep
{
    class Setting : SettingForm
    {
        public void keyDown(object sender, KeyEventArgs e)
        {
            StringBuilder keyValue = new StringBuilder();
            keyValue.Length = 0;
            keyValue.Append("");
            if (e.Modifiers != 0)
            {
                if (e.Control)
                    keyValue.Append("Ctrl+");
                if (e.Alt)
                    keyValue.Append("Alt+");
                if (e.Shift)
                    keyValue.Append("Shift+");
            }
            if ((e.KeyValue >= 33 && e.KeyValue <= 40) ||
                (e.KeyValue >= 65 && e.KeyValue <= 90) ||   //a-z/A-Z
                (e.KeyValue >= 112 && e.KeyValue <= 123))   //F1-F12
            {
                keyValue.Append(e.KeyCode);
            }
            else if ((e.KeyValue >= 48 && e.KeyValue <= 57))    //0-9
            {
                keyValue.Append(e.KeyCode.ToString().Substring(1));
            }
            ActiveControl.Text = "";
            //设置当前活动控件的文本内容
            ActiveControl.Text = keyValue.ToString();
        }

        public void keyUp(object sender, KeyEventArgs e)
        {
            string str = ActiveControl.Text.TrimEnd();
            int len = str.Length;
            if (len >= 1 && str.Substring(str.Length - 1) == "+")
            {
                ActiveControl.Text = "";
            }
        }

        public void keyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

        }
        public bool ApplyClick(object sender, EventArgs e,out SystemHotKey.KeyModifiers keyModifier, out Keys keys)
        {
            bool flag = true;
            SystemHotKey.KeyModifiers keyModifiers = SystemHotKey.KeyModifiers.None;
            Keys key = new Keys();
            string[] hotkeys = textBox1.Text.Split('+');
            if (textBox1.Text == String.Empty)
            {
                keyModifiers = MakeNotifyIcon.NowModifier;
                key = MakeNotifyIcon.Nowkeys;
                flag = false;
            }
            if (textBox2.Text == String.Empty)
            {
                textBox2.Text = "0";
            }
            if(flag)
                foreach (var tmp in hotkeys)
                {
                    switch (tmp)
                    {
                        case "Ctrl":
                            keyModifiers = keyModifiers | SystemHotKey.KeyModifiers.Ctrl;
                            break;
                        case "Alt":
                            keyModifiers = keyModifiers | SystemHotKey.KeyModifiers.Alt;
                            break;
                        case "Shift":
                            keyModifiers = keyModifiers | SystemHotKey.KeyModifiers.Shift;
                            break;
                        default:
                            key = (Keys)tmp.ToCharArray()[0];
                            break;
                    }
                }
            keyModifier = keyModifiers;
            keys = key;
            MakeNotifyIcon.Delay = Convert.ToInt32(textBox2.Text);
            MakeNotifyIcon.SaveIni(keyModifier, keys, Convert.ToInt32(textBox2.Text));
            return flag;
        }
        public bool YesClick(object sender, EventArgs e, out SystemHotKey.KeyModifiers keyModifiers, out Keys keys)
        {
            return ApplyClick(sender, e, out keyModifiers, out keys);
        }

    }
    public partial class Settingini : Component
    {

        //配置文件路径，可以扩展做成多配置文件
        private static string IniFilePath = @".\Config.ini";

        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filepath);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder returnvalue, int buffersize, string filepath);

        //公开接口：读取配置
        public static void GetValue(string section, string key, out string value)
        {

            StringBuilder stringBuilder = new StringBuilder();
            GetPrivateProfileString(section, key, "", stringBuilder, 1024, IniFilePath);
            value = stringBuilder.ToString() ?? "";
        }

        //公开接口：设置配置
        public static void SetValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, IniFilePath);
        }
    }

    class SystemScreenSleep
    {
        private const uint WM_SYSCOMMAND = 0x112;                    //系统消息
        private const int SC_MONITORPOWER = 0xF170;                  //关闭显示器的系统命令
        private const int MonitorPowerOff = 2;                       //2为PowerOff, 1为省电状态，-1为开机
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        static public void Close_Screen(IntPtr intPtr) 
        {
            SendMessage(intPtr, WM_SYSCOMMAND, SC_MONITORPOWER, MonitorPowerOff);
        }
    }
    class MakeNotifyIcon : Control
    {
        static private int delay = 300;
        static public int Delay
        {
            get => delay;
            set => delay = value;
        }
        static public SystemHotKey.KeyModifiers NowModifier;
        static public Keys Nowkeys;
        private Setting SettingForms = new Setting();
        private NotifyIcon Icons;
        public void Creat_NI()
        {
            NowModifier = SystemHotKey.KeyModifiers.Ctrl | SystemHotKey.KeyModifiers.Shift;
            Nowkeys = Keys.P;
            if (!File.Exists(@".\Config.ini"))
            {
                SaveIni(NowModifier, Nowkeys, Delay);
            }
            else
            {
                string modtmp,keytmp,delaytmp;
                Settingini.GetValue("Main", "Modifier",out modtmp);
                Enum.TryParse(modtmp, out NowModifier);
                Settingini.GetValue("Main", "Key", out keytmp);
                Nowkeys = (Keys)keytmp.ToCharArray()[0];
                Settingini.GetValue("Main", "Delay", out delaytmp);
                Delay = Convert.ToInt32(delaytmp);
            }
                
            SettingForms.textBox1.KeyDown += TextBox1_KeyDown;
            SettingForms.textBox1.KeyUp += TextBox1_KeyUp;
            SettingForms.textBox2.KeyPress += TextBox2_KeyPress;
            SettingForms.apply.Click += Apply_Click;
            SettingForms.yes.Click += Yes_Click;
            SettingForms.cancel.Click += Cancel_Click;
            string outmod = NowModifier.ToString().Replace(",", "+").Replace(" ",string.Empty);
            SettingForms.textBox1.Text = outmod+"+"+Nowkeys.ToString();
            SettingForms.textBox2.Text = Delay.ToString();

            Icons = new NotifyIcon()
            {
                Icon = Properties.Resources.icon,

                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("setting", Setting),
                    new MenuItem("Exit", Exit)
                }),
                Visible = true
            };
            Icons.DoubleClick += Icons_DoubleClick;
            SystemHotKey.RegKey(Handle,Nowkeys, NowModifier);
        }

        static public void SaveIni(SystemHotKey.KeyModifiers keyModifiers, Keys keys, int delay)
        {
            Settingini.SetValue("Main", "Modifier", keyModifiers.ToString());
            Settingini.SetValue("Main", "Key", keys.ToString());
            Settingini.SetValue("Main", "Delay", delay.ToString());
        }

        private void Setting(object sender, EventArgs e) 
        {
            if(!SettingForms.Visible)
                SettingForms.Visible = true;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            SettingForms.Visible = false;
        }

        private void Yes_Click(object sender, EventArgs e)
        {
            SettingForms.Visible = false;
            SystemHotKey.UnRegKey(Handle);
            if(SettingForms.YesClick(sender, e, out NowModifier, out Nowkeys))
                SystemHotKey.RegKey(Handle, Nowkeys, NowModifier);

        }

        private void Apply_Click(object sender, EventArgs e)
        {
            SystemHotKey.UnRegKey(Handle);
            if (SettingForms.ApplyClick(sender, e, out NowModifier, out Nowkeys))
                SystemHotKey.RegKey(Handle, Nowkeys, NowModifier);
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            SettingForms.keyPress(sender, e);
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            SettingForms.keyDown(sender, e);
        }
        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            SettingForms.keyUp(sender, e);
        }

        private void Icons_DoubleClick(object sender, EventArgs e)
        {
            SystemScreenSleep.Close_Screen(Handle);
        }

        private void Exit(object sender, EventArgs e)
        {
            SystemHotKey.UnRegKey(Handle);
            Icons.Visible = false;
            Application.Exit();
        }
        public bool AppInstance()
        {
            System.Diagnostics.Process[] MyProcesses = System.Diagnostics.Process.GetProcesses();
            bool flag = false;
            foreach (System.Diagnostics.Process MyProcess in MyProcesses)
            {
                if (MyProcess.ProcessName == System.Diagnostics.Process.GetCurrentProcess().ProcessName)
                {
                    if (flag == true) return true;
                    flag = true;
                }
            }
            return false;
        }
        const int WM_HOTKEY = 0x0312;

        protected override void WndProc(ref Message m)
        {
            
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == SystemHotKey.HotKeyID)
            {
                Thread.Sleep(Delay);
                SystemScreenSleep.Close_Screen(Handle);
            }
            base.WndProc(ref m);
        }
    }
    class SystemHotKey
    {
        public const int HotKeyID = 114514; //热键ID

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [Flags()]
        public enum KeyModifiers

        {
            None = 0,

            Alt = 1,

            Ctrl = 2,

            Shift = 4,

            WindowsKey = 8
        }

        static public void RegKey(IntPtr hwnd, Keys keys, KeyModifiers keyModifiers)
        {

            if (!RegisterHotKey(hwnd, HotKeyID, keyModifiers, keys))
            {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode == 1409)
                    {
                        MessageBox.Show("热键被占用 ！");
                    }
                else
                    {
                        MessageBox.Show("注册热键失败！错误代码：" + errorCode);
                    }
            }
        }
        static public void UnRegKey(IntPtr hwnd)
        {
            UnregisterHotKey(hwnd, HotKeyID);
        }
    }
    static class Program
    {

        [STAThread]
        static void Main()
        {
            MakeNotifyIcon CreateIcon = new MakeNotifyIcon();
            if (CreateIcon.AppInstance())
            {
                return;
            }
            CreateIcon.Creat_NI();
            Application.Run();
        }

    }
}
