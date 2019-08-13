using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ArcDPS_uptool
{
    public partial class Form2 : Form
    {
        int a = 0;
        public string 本地路劲 = Application.StartupPath;
        public string 插件路劲 = Application.StartupPath + "\\addons\\arcdps";
        public string 插件路劲B = Application.StartupPath + "\\addons\\sct";
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Shown(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.环境检测)
            {
                Jiancehuanjing();
            }
        }

        int siide = 0;

        public void Jiancehuanjing()
        {
            textBox1.AppendText("开始检测运行环境\r\n");
            textBox1.AppendText("=====================================\r\n");
            if (Regex.IsMatch(Application.StartupPath, @"[\u4e00-\u9fa5]") == true)
            {
                textBox1.AppendText("所在目录为:" + Application.StartupPath + "\r\n");
                textBox1.AppendText("×目录含中文会导致插件不识别中文请修改\r\n");
                textBox1.ForeColor = Color.Red;
                siide++;
            }
            else
            {
                textBox1.AppendText("√游戏目录无中文可正常运行\r\n");
            }
            textBox1.AppendText("=====================================\r\n");
            string path = @"./Gw2-64.exe";
            if (File.Exists(path))
            {
                textBox1.AppendText("√游戏目录下包含程序名Gw2-64.exe\r\n");
            }
            else
            {
                textBox1.AppendText("×没有发现Gw2-64.exe请确认程序在游戏根目录或客户端程序名是否为Gw2-64.exe\r\n");
                textBox1.AppendText("×请确保您开启了文件后缀名显示\r\n");
                siide++;
                textBox1.ForeColor = Color.Red;
            }
            textBox1.AppendText("=====================================\r\n");
            //Environment.GetFolderPath(Environment.SpecialFolder.System)
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System)+"//D3DX9_43.dll"))
            {
                a = 1; //安装dx
                textBox1.AppendText("×没有检测到DX9.0c运行库!!!\r\n");
                textBox1.ForeColor = Color.Red;
            }
            else
            {
                textBox1.AppendText("√检测到DX9.0c运行库\r\n");
            }
            textBox1.AppendText("=====================================\r\n");
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System)+ "//vccorlib120.dll"))
            {
                if (a == 1)
                {
                    a = 3; //安装2013 和dx
                }
                else
                {
                    a = 2;//安装2013
                }
                textBox1.AppendText("×没有检测到VC++2013运行库!!!\r\n");
                textBox1.ForeColor = Color.Red;
            }
            else
            {
                textBox1.AppendText("√检测到VC++2013运行库\r\n");
            }
            textBox1.AppendText("=====================================\r\n");
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System)+"//vcamp140.dll"))
            {
                if (a == 0)
                {
                    a = 4; //安装2015
                }
                else if (a == 1)
                {
                    a = 5; //安装dx 和2015
                }
                else if (a == 2)
                {
                    a = 6; //安装2013 和 2015
                }
                else if (a == 3)
                {
                    a = 7; //安装2013 2015 dx
                }
                textBox1.AppendText("×没有检测到VC++2015运行库!!!\r\n");
                textBox1.ForeColor = Color.Red;
            }
            else
            {
                textBox1.AppendText("√检测到VC++2015运行库\r\n");
            }
            textBox1.AppendText("=====================================\r\n");
            if (a > 0)
            {
                textBox1.AppendText("所有项目检测完成!!请根据提示修改目录或安装运行库!\r\n");
            }
            else
            {
                textBox1.AppendText("所有项目检测完成!!\r\n");
            }
            textBox1.AppendText("=====================================\r\n");
            if (true)
            {
                textBox1.AppendText("开始解压插件配置文件和字体文件\r\n");
                初次运行();
            }

        }

        public void 初次运行()
        {
            if (!Directory.Exists(插件路劲))
            {
                解压(1);
            }
            else
            {
                解压(2);
            }
        }

        public void 解压(int aii)
        {
            if (aii == 1)
            {
                Directory.CreateDirectory(插件路劲);
                Application.DoEvents();
                if (!Directory.Exists(插件路劲B))
                {
                    Directory.CreateDirectory(插件路劲B);
                    Application.DoEvents();
                    Directory.CreateDirectory(插件路劲B + "\\fonts");
                    Application.DoEvents();
                }

                byte[] Save = Properties.Resources.arcdps;
                FileStream fsObj = new FileStream(插件路劲 + "\\arcdps.ini", FileMode.CreateNew);
                fsObj.Write(Save, 0, Save.Length);
                fsObj.Close();
                Application.DoEvents();

                byte[] Save1 = Properties.Resources.arcdps_font;
                FileStream fsObj1 = new FileStream(插件路劲 + "\\arcdps_font.ttf", FileMode.CreateNew);
                fsObj1.Write(Save1, 0, Save1.Length);
                fsObj1.Close();
                Application.DoEvents();

                byte[] Save2 = Properties.Resources.arcdps_font;
                FileStream fsObj2 = new FileStream(插件路劲B + "\\fonts\\arcdps_font.ttf", FileMode.CreateNew);
                fsObj2.Write(Save2, 0, Save2.Length);
                fsObj2.Close();
                Application.DoEvents();

                byte[] Save3 = Properties.Resources.sct;
                FileStream fsObj3 = new FileStream(插件路劲B + "\\sct.ini", FileMode.CreateNew);
                fsObj3.Write(Save3, 0, Save3.Length);
                fsObj3.Close();
                Application.DoEvents();

                byte[] Save4 = Properties.Resources.lang;
                FileStream fsObj4 = new FileStream(插件路劲B + "\\lang.ini", FileMode.CreateNew);
                fsObj4.Write(Save4, 0, Save4.Length);
                fsObj4.Close();
                Application.DoEvents();


            }
            else if (aii == 2)
            {
                if (!Directory.Exists(插件路劲B))
                {
                    Directory.CreateDirectory(插件路劲B);
                    Application.DoEvents();
                    Directory.CreateDirectory(插件路劲B + "\\fonts");
                    Application.DoEvents();
                }
                if (!File.Exists(插件路劲 + "\\arcdps.ini"))
                {
                    byte[] Save = Properties.Resources.arcdps;
                    FileStream fsObj = new FileStream(插件路劲 + "\\arcdps.ini", FileMode.CreateNew);
                    fsObj.Write(Save, 0, Save.Length);
                    fsObj.Close();
                }
                if (!File.Exists(插件路劲 + "\\arcdps_font.ttf"))
                {
                    byte[] Save1 = Properties.Resources.arcdps_font;
                    FileStream fsObj1 = new FileStream(插件路劲 + "\\arcdps_font.ttf", FileMode.CreateNew);
                    fsObj1.Write(Save1, 0, Save1.Length);
                    fsObj1.Close();
                }
                if (!File.Exists(插件路劲B + "\\fonts\\arcdps_font.ttf"))
                {
                    byte[] Save2 = Properties.Resources.arcdps_font;
                    FileStream fsObj2 = new FileStream(插件路劲B + "\\fonts\\arcdps_font.ttf", FileMode.CreateNew);
                    fsObj2.Write(Save2, 0, Save2.Length);
                    fsObj2.Close();
                }
                if (!File.Exists(插件路劲B + "\\sct.ini"))
                {
                    byte[] Save3 = Properties.Resources.sct;
                    FileStream fsObj3 = new FileStream(插件路劲B + "\\sct.ini", FileMode.CreateNew);
                    fsObj3.Write(Save3, 0, Save3.Length);
                    fsObj3.Close();
                }
                if (!File.Exists(插件路劲B + "\\lang.ini"))
                {
                    byte[] Save4 = Properties.Resources.lang;
                    FileStream fsObj4 = new FileStream(插件路劲B + "\\lang.ini", FileMode.CreateNew);
                    fsObj4.Write(Save4, 0, Save4.Length);
                    fsObj4.Close();
                }
            }
            textBox1.AppendText("解压插件配置文件和字体文件完成\r\n");
        }
        public void 安装运行库()
        {
            if (a > 0)
            {

                //string ass = "";
                if (a == 1)
                {
                    //ass = "检测到你的电脑没有安装DX9.0c,点击确认开始安装";
                    //if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
                else if (a == 2)
                {
                    //ass = "检测到你的电脑没有安装VC++2013,点击确认开始安装";
                    //if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
                else if (a == 3)
                {
                    //ass = "检测到你的电脑没有安装DX9.0c和VC++2013,点击确认开始安装";
                    //if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
                else if (a == 4)
                {
                    //ass = "检测到你的电脑没有安装VC++2015,点击确认开始安装";
                    //if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
                else if (a == 5)
                {
                    //ass = "检测到你的电脑没有安装DX9.0c和VC++2015,点击确认开始安装";
                    //if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
                else if (a == 6)
                {
                    //ass = "检测到你的电脑没有安装VC++2015和VC++2013,点击确认开始安装";
                    //if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
                else if (a == 7)
                {
                    //ass = "检测到你的电脑没有安装DX9.0c和VC++2013和VC++2015,点击确认开始安装";
                    //if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
            }
        }

        private void 安装运行库(int a)
        {
            switch (a)
            {
                case 1:
                    byte[] Save1 = Properties.Resources.dxwebsetup;
                    FileStream fsObj1 = new FileStream(本地路劲 + "\\dxwebsetup.exe", FileMode.CreateNew);
                    fsObj1.Write(Save1, 0, Save1.Length);
                    fsObj1.Close();
                    
                    ProcessStartInfo info1 = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                    _ = Process.Start(info1);
                    break;
                case 2:
                    byte[] Save2 = Properties.Resources.vcredist_x64;
                    FileStream fsObj2 = new FileStream(本地路劲 + "\\vcredist_x64.exe", FileMode.CreateNew);
                    fsObj2.Write(Save2, 0, Save2.Length);
                    fsObj2.Close();
                    ProcessStartInfo info2 = new ProcessStartInfo { FileName = @".\\vcredist_x64.exe" };
                    _ = Process.Start(info2);
                    break;
                case 3:
                    byte[] Save3 = Properties.Resources.vcredist_x64;
                    FileStream fsObj3 = new FileStream(本地路劲 + "\\vcredist_x64.exe", FileMode.CreateNew);
                    fsObj3.Write(Save3, 0, Save3.Length);
                    fsObj3.Close();
                    byte[] Save4 = Properties.Resources.dxwebsetup;
                    FileStream fsObj4 = new FileStream(本地路劲 + "\\dxwebsetup.exe", FileMode.CreateNew);
                    fsObj4.Write(Save4, 0, Save4.Length);
                    fsObj4.Close();
                    ProcessStartInfo info3 = new ProcessStartInfo { FileName = @".\\vcredist_x64.exe" };
                    _ = Process.Start(info3);
                    ProcessStartInfo info4 = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                    _ = Process.Start(info4);
                    break;
                case 4:
                    byte[] Save5 = Properties.Resources.vcredist2015;
                    FileStream fsObj5 = new FileStream(本地路劲 + "\\vcredist2015.exe", FileMode.CreateNew);
                    fsObj5.Write(Save5, 0, Save5.Length);
                    fsObj5.Close();
                    ProcessStartInfo info5 = new ProcessStartInfo { FileName = @".\\vcredist2015.exe" };
                    _ = Process.Start(info5);
                    break;
                case 5:
                    //dx 2015
                    byte[] Save6 = Properties.Resources.dxwebsetup;
                    FileStream fsObj6 = new FileStream(本地路劲 + "\\dxwebsetup.exe", FileMode.CreateNew);
                    fsObj6.Write(Save6, 0, Save6.Length);
                    fsObj6.Close();
                    byte[] Save7 = Properties.Resources.vcredist_x64;
                    FileStream fsObj7 = new FileStream(本地路劲 + "\\vcredist2015.exe", FileMode.CreateNew);
                    fsObj7.Write(Save7, 0, Save7.Length);
                    fsObj7.Close();
                    ProcessStartInfo info6 = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                    _ = Process.Start(info6);
                    ProcessStartInfo info7 = new ProcessStartInfo { FileName = @".\\vcredist2015.exe" };
                    _ = Process.Start(info7);
                    break;
                case 6:
                    //2013 2015
                    byte[] Save8 = Properties.Resources.vcredist_x64;
                    FileStream fsObj8 = new FileStream(本地路劲 + "\\vcredist_x64.exe", FileMode.CreateNew);
                    fsObj8.Write(Save8, 0, Save8.Length);
                    fsObj8.Close();
                    byte[] Save9 = Properties.Resources.vcredist2015;
                    FileStream fsObj9 = new FileStream(本地路劲 + "\\vcredist2015.exe", FileMode.CreateNew);
                    fsObj9.Write(Save9, 0, Save9.Length);
                    fsObj9.Close();
                    ProcessStartInfo info8 = new ProcessStartInfo { FileName = @".\\vcredist_x64.exe" };
                    _ = Process.Start(info8);
                    ProcessStartInfo info9 = new ProcessStartInfo { FileName = @".\\vcredist2015.exe" };
                    _ = Process.Start(info9);
                    break;
                case 7:
                    byte[] Save10 = Properties.Resources.vcredist_x64;
                    FileStream fsObj10 = new FileStream(本地路劲 + "\\vcredist_x64.exe", FileMode.CreateNew);
                    fsObj10.Write(Save10, 0, Save10.Length);
                    fsObj10.Close();
                    byte[] Save11 = Properties.Resources.vcredist2015;
                    FileStream fsObj11 = new FileStream(本地路劲 + "\\vcredist2015.exe", FileMode.CreateNew);
                    fsObj11.Write(Save11, 0, Save11.Length);
                    fsObj11.Close();
                    byte[] Save12 = Properties.Resources.dxwebsetup;
                    FileStream fsObj12 = new FileStream(本地路劲 + "\\dxwebsetup.exe", FileMode.CreateNew);
                    fsObj12.Write(Save12, 0, Save12.Length);
                    fsObj12.Close();
                    ProcessStartInfo info11 = new ProcessStartInfo { FileName = @".\\vcredist2015.exe" };
                    _ = Process.Start(info11);
                    ProcessStartInfo info10 = new ProcessStartInfo { FileName = @".\\vcredist_x64.exe" };
                    _ = Process.Start(info10);
                    ProcessStartInfo info12 = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                    _ = Process.Start(info12);
                    break;
                default:

                    break;
            }
            Application.DoEvents();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            安装运行库();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (siide>0||a>0)
            {
                Close();
            }
            else
            {
                Properties.Settings.Default.环境检测 = true;
                Properties.Settings.Default.Save();
                Form1: Show();
                Close();
            }

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (a==0)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (siide>0)
            {
                Process.GetCurrentProcess().Kill();
            }
        }
    }
}
