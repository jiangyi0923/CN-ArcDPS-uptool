using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GW2_Plug_Updatetool
{
    public partial class Form1 : Form
    {
        //==============参数=================
        public string 本地路劲 = Application.StartupPath;
        public string 插件路劲 = Application.StartupPath + "\\addons\\arcdps";
        public string 插件路劲B = Application.StartupPath + "\\addons\\sct";
        public string 下载路劲 = Application.StartupPath + "\\bin64";
        public string 版本检测网址 = "http://gw2sy.top/wp-content/uploads/1.txt";
        public string 更新说明 = "http://gw2sy.top/wp-content/uploads/2.txt";
        public string 信息检测网址 = "http://gw2sy.top/wp-content/uploads/11.txt";
        public string 信息说明 = "http://gw2sy.top/wp-content/uploads/22.txt";
        public string 官方网址 = "https://gw2sy.top";
        public int 项目个数 = 0;
        public int 完成个数 = 0;
        public bool 下载中 = false;
        public bool 不是更新期间 = false;
        public bool[] 勾选 = new bool[10];
        Thread[] thread = new Thread[12];
        //==============窗口=================
        public static Form1 form1;
        public Form1()    
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            form1 = this;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Properties.Settings.Default._1程序;
            checkBox2.Checked = Properties.Settings.Default._2切换;
            checkBox3.Checked = Properties.Settings.Default._3附加;
            checkBox4.Checked = Properties.Settings.Default._4流汉;
            checkBox5.Checked = Properties.Settings.Default._5流英;
            checkBox6.Checked = Properties.Settings.Default._6团力;
            checkBox7.Checked = Properties.Settings.Default._7团恩;
            checkBox8.Checked = Properties.Settings.Default._8坐骑;
            checkBox9.Checked = Properties.Settings.Default._9汉滤;
            checkBox10.Checked = Properties.Settings.Default._10全虑;
            checkBox11.Checked = Properties.Settings.Default._11自动;
            checkBox12.Checked = Properties.Settings.Default._12启动;
            checkBox13.Checked = Properties.Settings.Default._不更新;
            启用();
            初次运行();
            int 最新版本 = 版本();
            textBox1.AppendText("当前版本:V" + Application.ProductVersion + "\r\n");


            if (最新版本 == 0)
            {
                MessageBox.Show("1获取最新版本信息失败\r\n官网暂时无法连接\r\n请稍后再试或联系我\r\n");
                Close();
                Process.GetCurrentProcess().Kill();
            }
            textBox1.AppendText("最新版本:V" + 最新版本.ToString() + "\r\n");

            int.TryParse(Application.ProductVersion, out int 本地版本);
            if (本地版本 < 最新版本)
            {
                string 说明文档 = 说明();
                if (说明文档 == "")
                {
                    MessageBox.Show("2获取最新版本信息失败\r\n官网暂时无法连接\r\n请稍后再试或联系我\r\n");
                    Close();
                    Process.GetCurrentProcess().Kill();
                }
                MessageBox.Show("有最新版本V" + 最新版本.ToString() + ",请前往官网下载" + 说明文档);
            }//
            int 最新信息检测 = 信息检测();
            if (最新信息检测 != 0)
            {
                string 信息说明文档 = 获取信息说明();
                if (信息说明文档 == "")
                {
                    MessageBox.Show("3获取最新信息失败,\r\n官网暂时无法连接\r\n请稍后再试或联系我\r\n");
                    Close();
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    MessageBox.Show(信息说明文档, "重要信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // MessageBox.Show("","重要信息提示" + 信息说明文档, MessageBoxIcon.Question);
                }

            }
            
            if (检测环境())
            {
                
                bool 第一次使用 = Properties.Settings.Default.首次使用;
                if (第一次使用)
                {
                    Properties.Settings.Default.首次使用 = false;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("欢迎您使用激战2插件更新工具,\r\n" +
                        "第一次使用请勾选您需要的插件后点击更新按钮,\r\n" +
                        "您想使用滤镜插件的话需要勾选坐骑插件,\r\n" +
                        "不推荐:ReShade滤镜需要自己配置(建议高端玩家使用)\r\n" +
                        "推荐:SweetFX滤镜已经配置好了可以直接使用(需要游戏内关闭抗锯齿)\r\n" +
                        "遇到问题请先查看官网右上角菜单是否有解决方法,\r\n" +
                        "如果没有请联系我哦!");
                }
                if (Properties.Settings.Default._11自动)
                {
                    if (Properties.Settings.Default._不更新)
                    {
                        string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                        string week = Day[Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))].ToString();
                        if (!week.Equals(Day[3]) && !week.Equals(Day[4]))
                        {
                            textBox1.AppendText("今天是" + week + "可以正常更新\r\n");
                            读取后操作();
                        }
                        else
                        {
                            textBox1.AppendText("今天是" + week + "您设置了不更新,所以跳过\r\n");
                            不是更新期间 = true;
                        }
                    }
                    else
                    {
                        读取后操作();
                    }

                }
                
            }
        }
        private void 读取后操作()
        {
            更新();
            下载中 = true;
            禁用();
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
        //==============所有按钮==================
        private void 点击选项(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            
            switch (int.Parse(check.Tag.ToString()))
            {
                case 1:
                    Properties.Settings.Default._1程序 = check.Checked;
                    break;
                case 2:
                    Properties.Settings.Default._2切换 = check.Checked;
                    break;
                case 3:
                    Properties.Settings.Default._3附加 = check.Checked;
                    break;
                case 4:
                    Properties.Settings.Default._4流汉 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox5.Enabled = false;
                        checkBox6.Enabled = false;
                        checkBox7.Enabled = false;
                        
                    }
                    else
                    {
                        checkBox5.Enabled = true;
                        checkBox6.Enabled = true;
                        checkBox7.Enabled = true;
                    }
                    break;
                case 5:
                    Properties.Settings.Default._5流英 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox4.Enabled = false;
                        checkBox6.Enabled = false;
                        checkBox7.Enabled = false;

                    }
                    else
                    {
                        checkBox4.Enabled = true;
                        checkBox6.Enabled = true;
                        checkBox7.Enabled = true;
                    }
                    break;
                case 6:
                    Properties.Settings.Default._6团力 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox4.Enabled = false;
                        checkBox5.Enabled = false;
                    }
                    else
                    {
                        checkBox4.Enabled = true;
                        checkBox5.Enabled = true;
                    }
                    break;
                case 7:
                    Properties.Settings.Default._7团恩 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox4.Enabled = false;
                        checkBox5.Enabled = false;
                    }
                    else
                    {
                        checkBox4.Enabled = true;
                        checkBox5.Enabled = true;
                    }
                    break;
                case 8:
                    Properties.Settings.Default._8坐骑 = check.Checked;
                    break;
                case 9:
                    Properties.Settings.Default._9汉滤 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox10.Enabled = false;
                    }
                    else
                    {
                        checkBox10.Enabled = true;
                        if (File.Exists(下载路劲 + "\\ReShade64.dll"))
                        {
                            File.Delete(下载路劲 + "\\ReShade64.dll");
                        }
                        string didi3 = 下载路劲 + "\\reshade-shaders";
                        if (Directory.Exists(didi3))
                        {
                            删除目录(didi3);//ReShade.ini
                        }
                        if (File.Exists(下载路劲 + "\\ReShade.ini"))
                        {
                            File.Delete(下载路劲 + "\\ReShade.ini");
                        }
                    }
                    break;
                case 10:
                    Properties.Settings.Default._10全虑 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox9.Enabled = false;
                    }
                    else
                    {
                        checkBox9.Enabled = true;
                        string didi4 = 下载路劲 + "\\SweetFX";
                        if (Directory.Exists(didi4))
                        {
                            删除目录(didi4);
                        }
                        if (File.Exists(下载路劲 + "\\dxgi.dll"))
                        {
                            File.Delete(下载路劲 + "\\dxgi.dll");
                        }
                        if (File.Exists(下载路劲 + "\\d3d9_mchain.dll"))
                        {
                            File.Delete(下载路劲 + "\\d3d9_mchain.dll");
                        }
                        if (File.Exists(下载路劲 + "\\SweetFX readme.txt"))
                        {
                            File.Delete(下载路劲 + "\\SweetFX readme.txt");
                        }
                        if (File.Exists(下载路劲 + "\\SweetFX_preset.txt"))
                        {
                            File.Delete(下载路劲 + "\\SweetFX_preset.txt");
                        }
                        if (File.Exists(下载路劲 + "\\SweetFX_settings.txt"))
                        {
                            File.Delete(下载路劲 + "\\SweetFX_settings.txt");
                        }
                    }
                    break;
                case 11:
                    Properties.Settings.Default._11自动 = check.Checked;
                    break;
                case 12:
                    Properties.Settings.Default._12启动 = check.Checked;
                    break;
                case 13:
                    Properties.Settings.Default._不更新 = check.Checked;
                    break;
                default:
                    break;
            }
            string zhuang;
            if (check.Checked)
            {
                zhuang = "开启";
            }
            else
            {
                zhuang = "取消,记得点击更新哦!";
            }
            textBox1.AppendText(check.Text + zhuang + "-已保存\r\n");
            Properties.Settings.Default.Save();
        }



        private void 更新按钮(object sender, EventArgs e)
        {
            if (!下载中)
            {
                if (Properties.Settings.Default._不更新)
                {
                    string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                    ;
                    string week = Day[Convert.ToInt16(DateTime.Now.DayOfWeek.ToString("d"))];
                    if (!week.Equals(Day[3]) && !week.Equals(Day[4]))
                    {
                        textBox1.AppendText("今天是" + week + "可以正常更新\r\n");
                        读取后操作();
                    }
                    else
                    {
                        textBox1.AppendText("今天是"+ week+ "您设置了不更新,所以跳过\r\n");
                        不是更新期间 = true;
                    }
                }
                else
                {
                    读取后操作();
                }
            }
        }

        private void 卸载按钮(object sender, EventArgs e)
        {
            
            卸载();
        }

        private void 卸载()
        {
            string[] 所有文件名 = new string[14]
        {   "\\d3d9.dll",
            "\\d3d9_arcdps_buildtemplates.dll",
            "\\d3d9_arcdps_extras.dll",
            "\\d3d9_arcdps_mechanics.dll",
            "\\d3d9_chainload.dll",
            "\\d3d9_arcdps_tablechs.dll",
            "\\d3d9_arcdps_sct.dll",
            "\\ReShade64.dll",
            "\\ReShade.ini",
            "\\d3d9_mchain.dll",
            "\\SweetFX readme.txt",
            "\\SweetFX_preset.txt",
            "\\SweetFX_settings.txt",
            "\\dxgi.dll"
        };
            for (int i = 0; i < 所有文件名.Length; i++)
            {
                if (File.Exists(下载路劲 + 所有文件名[i]))
                {
                    File.Delete(下载路劲+ 所有文件名[i]);
                }
            }
            for (int i = 0; i < 所有文件名.Length; i++)
            {
                if (File.Exists(本地路劲 + 所有文件名[i]))
                {
                    File.Delete(本地路劲 + 所有文件名[i]);
                }
            }
            string didi1 = 下载路劲 + "\\SweetFX";
            if (Directory.Exists(didi1))
            {
                删除目录(didi1);
            }
            string didi2 = 下载路劲 + "\\reshade-shaders";
            if (Directory.Exists(didi2))
            {
                删除目录(didi2);//ReShade.ini
            }
        }

        private void 启动按钮(object sender, EventArgs e)
        {
            启动();
            
        }
        public void 启动()
        {
            string 启动代码 = "-maploadinfo";
            if (File.Exists(@".\\GW2Lanucher.exe"))
            {
                ProcessStartInfo info = new ProcessStartInfo { FileName = @".\\GW2Lanucher.exe", Arguments = 启动代码 };
                Process pro = new Process
                {
                    StartInfo = info
                };
                pro.Start();
                Close();
            }
            else if (File.Exists(@".\\Gw2-64.exe"))
            {
                ProcessStartInfo info = new ProcessStartInfo { FileName = @".\\Gw2-64.exe", Arguments = 启动代码 };
                Process pro = new Process
                {
                    StartInfo = info
                };
                pro.Start();
                Close();
            }
        }
        //============实现代码==================
        public static void 删除目录(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
                Directory.Delete(srcPath);
            }
            catch (Exception)
            {
                throw;
            }
        }


        private bool 解压文件(string 文件位置, string 解压路径, string 文件名)
        {
            bool a = false;
            textBox1.AppendText("开始解压:" + 文件名 + "\r\n");
            ZipFile.ExtractToDirectory(文件位置, 解压路径);
            textBox1.AppendText(文件名 + "解压完成\r\n");
            a = true;
            return a;
        }

        private int 版本()
        {
            int a = 0;
            var wc = new WebClient();
            try
            {

                var html = wc.DownloadString(版本检测网址);
                int.TryParse(html, out a);
                wc.Dispose();
            }
            catch (Exception)
            {
                a = 0;
            }
            finally
            {
                wc.Dispose();
            }
            return a;
        }

        private string 说明()
        {
            string a = "";
            try
            {
                var wc = new WebClient();
                string html = wc.DownloadString(更新说明);
                a = html;
                wc.Dispose();
            }
            catch (Exception)
            {
                a = "";
            }

            return a;
        }

        private int 信息检测()
        {
            int a = 0;
            try
            {
                var wc = new WebClient();
                var html = wc.DownloadString(信息检测网址);
                int.TryParse(html, out a);
                wc.Dispose();
            }
            catch (Exception)
            {
                a = 0;
            }
            return a;
        }

        private string 获取信息说明()
        {
            string a = "";
            try
            {
                var wc = new WebClient();
                string html = wc.DownloadString(信息说明);
                a = html;
                wc.Dispose();
            }
            catch (Exception)
            {
                a = "";
            }

            return a;
        }

        public bool 检测环境()
        {
            string path = @"./Gw2-64.exe";
            if (File.Exists(path))
            {
                if (Regex.IsMatch(本地路劲, @"[\u4e00-\u9fa5]") == true)
                {
                    MessageBox.Show("游戏目录含中文可能导致插件不识别中文请更换");
                    Process.GetCurrentProcess().Kill();
                }
            }
            else
            {
                MessageBox.Show("没有发现Gw2-64.exe请确认程序在游戏根目录或客户端程序名是否为Gw2-64.exe");
                Process.GetCurrentProcess().Kill();
            }
            int a = 0;
            if (!File.Exists(@"C:\Windows\System32\D3DX9_43.dll"))
            {
                a = 1; //安装dx
            }
            if (!File.Exists(@"C:\Windows\System32\vcamp120.dll"))
            {
                if (a == 1)
                {
                    a = 3; //安装2013 和dx
                }
                else
                {
                    a = 2;//安装2013
                }
            }
            if (!File.Exists(@"C:\Windows\System32\vcamp140.dll"))
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

            }
            if (a > 0)
            {
                string ass = "";
                if (a == 1)
                {
                    ass = "检测到你的电脑没有安装DX9.0c,点击确认开始安装";
                    if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
                else if (a == 2)
                {
                    ass = "检测到你的电脑没有安装VC++2013,点击确认开始安装";
                    if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
                else if (a == 3)
                {
                    ass = "检测到你的电脑没有安装DX9.0c和VC++2013,点击确认开始安装";
                    if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
                else if (a == 4)
                {
                    ass = "检测到你的电脑没有安装VC++2015,点击确认开始安装";
                    if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
                else if (a == 5)
                {
                    ass = "检测到你的电脑没有安装DX9.0c和VC++2015,点击确认开始安装";
                    if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
                else if (a == 6)
                {
                    ass = "检测到你的电脑没有安装VC++2015和VC++2013,点击确认开始安装";
                    if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
                else if (a == 7)
                {
                    ass = "检测到你的电脑没有安装DX9.0c和VC++2013和VC++2015,点击确认开始安装";
                    if (MessageBox.Show(ass, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Thread t31 = new Thread(delegate () { 安装运行库(a); });
                        t31.Start();
                        Application.DoEvents();
                    }
                }
                return false;
            }
            else
            {
                return true;
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
                    Process pro1 = Process.Start(info1);
                    break;
                case 2:
                    byte[] Save2 = Properties.Resources.vcredist_x64;
                    FileStream fsObj2 = new FileStream(本地路劲 + "\\vcredist_x64.exe", FileMode.CreateNew);
                    fsObj2.Write(Save2, 0, Save2.Length);
                    fsObj2.Close();
                    ProcessStartInfo info2 = new ProcessStartInfo { FileName = @".\\vcredist_x64.exe" };
                    Process pro2 = Process.Start(info2);
                    break;
                case 3:
                    byte[] Save3 = Properties.Resources.vcredist_x64;
                    FileStream fsObj3 = new FileStream(本地路劲 + "\\vcredist_x64.exe", FileMode.CreateNew);
                    fsObj3.Write(Save3, 0, Save3.Length);
                    fsObj3.Close();
                    ProcessStartInfo info3 = new ProcessStartInfo { FileName = @".\\vcredist_x64.exe" };
                    Process pro3 = Process.Start(info3);

                    byte[] Save4 = Properties.Resources.dxwebsetup;
                    FileStream fsObj4 = new FileStream(本地路劲 + "\\dxwebsetup.exe", FileMode.CreateNew);
                    fsObj4.Write(Save4, 0, Save4.Length);
                    fsObj4.Close();
                    ProcessStartInfo info4 = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                    Process pro4 = Process.Start(info4);
                    break;
                case 4:
                    byte[] Save5 = Properties.Resources.vcredist2015;
                    FileStream fsObj5 = new FileStream(本地路劲 + "\\vcredist2015.exe", FileMode.CreateNew);
                    fsObj5.Write(Save5, 0, Save5.Length);
                    fsObj5.Close();
                    ProcessStartInfo info5 = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                    Process pro5 = Process.Start(info5);
                    break;
                case 5:
                    //dx 2015
                    byte[] Save6 = Properties.Resources.dxwebsetup;
                    FileStream fsObj6 = new FileStream(本地路劲 + "\\dxwebsetup.exe", FileMode.CreateNew);
                    fsObj6.Write(Save6, 0, Save6.Length);
                    fsObj6.Close();
                    ProcessStartInfo info6 = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                    Process pro6 = Process.Start(info6);

                    byte[] Save7 = Properties.Resources.vcredist_x64;
                    FileStream fsObj7 = new FileStream(本地路劲 + "\\vcredist2015.exe", FileMode.CreateNew);
                    fsObj7.Write(Save7, 0, Save7.Length);
                    fsObj7.Close();
                    ProcessStartInfo info7 = new ProcessStartInfo { FileName = @".\\vcredist_x64.exe" };
                    Process pro7 = Process.Start(info7);
                    break;
                case 6:
                    //2013 2015
                    byte[] Save8 = Properties.Resources.vcredist_x64;
                    FileStream fsObj8 = new FileStream(本地路劲 + "\\vcredist_x64.exe", FileMode.CreateNew);
                    fsObj8.Write(Save8, 0, Save8.Length);
                    fsObj8.Close();
                    ProcessStartInfo info8 = new ProcessStartInfo { FileName = @".\\vcredist_x64.exe" };
                    Process pro8 = Process.Start(info8);

                    byte[] Save9 = Properties.Resources.vcredist2015;
                    FileStream fsObj9 = new FileStream(本地路劲 + "\\vcredist2015.exe", FileMode.CreateNew);
                    fsObj9.Write(Save9, 0, Save9.Length);
                    fsObj9.Close();
                    ProcessStartInfo info9 = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                    Process pro9 = Process.Start(info9);
                    break;
                case 7:
                    byte[] Save10 = Properties.Resources.vcredist_x64;
                    FileStream fsObj10 = new FileStream(本地路劲 + "\\vcredist_x64.exe", FileMode.CreateNew);
                    fsObj10.Write(Save10, 0, Save10.Length);
                    fsObj10.Close();
                    ProcessStartInfo info10 = new ProcessStartInfo { FileName = @".\\vcredist_x64.exe" };
                    Process pro10 = Process.Start(info10);

                    byte[] Save11 = Properties.Resources.vcredist2015;
                    FileStream fsObj11 = new FileStream(本地路劲 + "\\vcredist2015.exe", FileMode.CreateNew);
                    fsObj11.Write(Save11, 0, Save11.Length);
                    fsObj11.Close();
                    ProcessStartInfo info11 = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                    Process pro11 = Process.Start(info11);

                    byte[] Save12 = Properties.Resources.dxwebsetup;
                    FileStream fsObj12 = new FileStream(本地路劲 + "\\dxwebsetup.exe", FileMode.CreateNew);
                    fsObj12.Write(Save12, 0, Save12.Length);
                    fsObj12.Close();
                    ProcessStartInfo info12 = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                    Process pro12 = Process.Start(info12);
                    break;
                default:

                    break;
            }
            Application.DoEvents();
            Process.GetCurrentProcess().Kill();
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

        public void 解压(int a)
        {
            if (a == 1)
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

            }
            else if (a == 2)
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
            }
        }

        public ProgressBar 进度条(int a)
        {
            ProgressBar progressBar;
            switch (a)
            {
                case 0:
                    progressBar = progressBar1;
                    break;
                case 1:
                    progressBar = progressBar2;
                    break;
                case 2:
                    progressBar = progressBar3;
                    break;
                case 3:
                    progressBar = progressBar4;
                    break;
                case 4:
                    progressBar = progressBar5;
                    break;
                case 5:
                    progressBar = progressBar6;
                    break;
                case 6:
                    progressBar = progressBar7;
                    break;
                case 7:
                    progressBar = progressBar8;
                    break;
                case 8:
                    progressBar = progressBar9;
                    break;
                case 9:
                    progressBar = progressBar10;
                    break;
                default:
                    progressBar = null;
                    break;
            }
            return progressBar;
        }

        public void 下载(string 网站位置, bool 进度条是否开启, ProgressBar 进度条, string 储存位置, string 文件名)
        {
            Thread.BeginThreadAffinity();
            try
            {
                HttpWebRequest Myrq = (HttpWebRequest)WebRequest.Create(网站位置);
                HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
                Myrq.Timeout = 5000;
                long totalBytes = myrp.ContentLength;
                bool yum;
                if (File.Exists(储存位置 + 文件名))
                {
                    yum = totalBytes.ToString() == File.ReadAllBytes(储存位置 + 文件名).Length.ToString();
                }
                else
                {
                    yum = false;
                }
                if (!File.GetLastWriteTime(储存位置 + 文件名).DayOfYear.Equals(myrp.LastModified.DayOfYear) || yum == false)
                {
                    textBox1.AppendText("开始下载:" + 文件名 + "\r\n");
                    if (进度条是否开启)
                    {
                        if (进度条 != null)
                        {
                            进度条.Maximum = (int)totalBytes;
                        }
                    }

                    Stream st = myrp.GetResponseStream();
                    Stream so = new FileStream(储存位置 + 文件名, FileMode.Create);
                    long totalDownloadedByte = 0;
                    byte[] by = new byte[1024];
                    int osize = st.Read(by, 0, by.Length);
                    while (osize > 0)
                    {
                        totalDownloadedByte = osize + totalDownloadedByte;
                        so.Write(by, 0, osize);
                        if (进度条是否开启)
                        {
                            if (进度条 != null)
                            {
                                进度条.Value = (int)totalDownloadedByte;
                            }
                        }

                        osize = st.Read(by, 0, by.Length);
                    }
                    //so.Dispose();
                    //st.Dispose();
                    so.Close();
                    st.Close();
                    File.SetLastWriteTime(储存位置 + 文件名, myrp.LastModified);
                    textBox1.AppendText( 文件名 + "下载完成\r\n");
                    完成个数++;
                }
                else
                {
                    if (进度条是否开启)
                    {
                        for (int i = 0; i <= 100; i++)
                        {

                            进度条.Value = i;
                        }
                    }
                    textBox1.AppendText(文件名 + "无需更新\r\n");
                    完成个数++;
                }

                myrp.Close();
                //myrp.Dispose();
                Myrq.Abort();
            }
            catch (Exception)
            {
                完成个数++;
                textBox1.AppendText(文件名 + "下载出错,网络或者其他原因\r\n");
            }
            
            Thread.EndThreadAffinity();
        }


        public void 禁用()
        {
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
            checkBox5.Enabled = false;
            checkBox6.Enabled = false;
            checkBox7.Enabled = false;
            checkBox8.Enabled = false;
            checkBox9.Enabled = false;
            checkBox10.Enabled = false;
            checkBox11.Enabled = false;
            checkBox12.Enabled = false;
            checkBox13.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

        }

        public void 启用()
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            if (Properties.Settings.Default._4流汉)
            {
                checkBox4.Enabled = true;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
            }
            else
            {
                checkBox4.Enabled = false;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
                checkBox7.Enabled = true;
            }
            if (Properties.Settings.Default._5流英)
            {
                checkBox4.Enabled = false;
                checkBox5.Enabled = true;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
            }
            else
            {
                checkBox4.Enabled = true;
                if (Properties.Settings.Default._4流汉)
                {
                    checkBox5.Enabled = false;
                }
                else
                {
                    checkBox5.Enabled = true;
                }
            }
            if (Properties.Settings.Default._6团力)
            {
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = true;
                checkBox7.Enabled = true;
            }

            checkBox8.Enabled = true;
            if (Properties.Settings.Default._9汉滤)
            {
                checkBox9.Enabled = true;
                checkBox10.Enabled = false;
            }
            else
            {
                checkBox9.Enabled = true;
            }
            if (Properties.Settings.Default._10全虑)
            {
                checkBox9.Enabled = false;
                checkBox10.Enabled = true;
            }
            else
            {
                if (!Properties.Settings.Default._9汉滤)
                {
                    checkBox10.Enabled = true;
                }
                
            }
            checkBox11.Enabled = true;
            checkBox12.Enabled = true;
            checkBox13.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;

        }
        int tmp1 = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {

            
            if (下载中)
            {
                int tmp = 完成个数;
                if (tmp - tmp1 >1)
                {
                    tmp1 ++;
                    textBox1.AppendText(项目个数 + "个项目" + 完成个数 + "个完成\r\n");
                }
                if (项目个数 == 完成个数)
                {
                    启用();
                    下载中 = false;
                    tmp1 = 0;
                    if (Properties.Settings.Default._9汉滤)
                    {
                        if (!File.Exists(下载路劲 + "\\ReShade64.dll"))
                        {
                            if (File.Exists(下载路劲 + "\\d3d9_ReShade64.zip"))
                            {
                                解压文件(下载路劲 + "\\d3d9_ReShade64.zip", 下载路劲, "ReShade64滤镜");
                            }
                        }

                    }
                    if (Properties.Settings.Default._10全虑)
                    {
                        if (!File.Exists(下载路劲 + "\\dxgi.dll"))
                        {
                            if (File.Exists(下载路劲 + "\\SweetFX.zip"))
                            {
                                解压文件(下载路劲 + "\\SweetFX.zip", 下载路劲, "SweetFX滤镜");
                            }
                        }

                    }
                    textBox1.AppendText("全部完成开始游戏吧!!\r\n");
                }
               
            }
            else
            {
                tmp1 = 0;
                if (项目个数 == 完成个数 && 项目个数 > 0)
                {
                    if (Properties.Settings.Default._12启动 | checkBox12.CheckState == CheckState.Checked)
                    {
                        启动();
                    }
                }
                else if (项目个数 == 完成个数 && 完成个数 == 0 && 不是更新期间)
                {
                    if (Properties.Settings.Default._12启动 | checkBox12.CheckState == CheckState.Checked)
                    {
                        启动();
                    }
                }

            }
        }


        public void 更新()
        {
            完成个数 = 0;
            项目个数 = 0;
            勾选[0] = Properties.Settings.Default._1程序;
            勾选[1] = Properties.Settings.Default._2切换;
            勾选[2] = Properties.Settings.Default._3附加;
            勾选[3] = Properties.Settings.Default._4流汉;
            勾选[4] = Properties.Settings.Default._5流英;
            勾选[5] = Properties.Settings.Default._6团力;
            勾选[6] = Properties.Settings.Default._7团恩;
            勾选[7] = Properties.Settings.Default._8坐骑;
            勾选[8] = Properties.Settings.Default._9汉滤;
            勾选[9] = Properties.Settings.Default._10全虑;
            
            if (!Directory.Exists(下载路劲))
            {
                Directory.CreateDirectory(下载路劲);
            }

            if (!File.Exists(本地路劲 + "\\d3d9.dll"))
            {
                File.Delete(本地路劲 + "\\d3d9.dll");
            }
            //主程序
            if (勾选[0])
            {
                项目个数++;
                string a = @"https://www.deltaconnected.com/arcdps/x64/d3d9.dll";
                bool b = true;
                string c = 下载路劲;
                string d = "\\d3d9.dll";
                thread[0] = new Thread(new ParameterizedThreadStart(delegate { 下载(a, b, 进度条(0), c, d); }))
                {
                    IsBackground = true
                };
                thread[0].Start();
                项目个数++;
                string a1 = @"https://raw.githubusercontent.com/Snowy1794/Arcdps-translation-Chinese-simplified/master/arcdps_lang.ini";
                bool b1 = false;
                string c1 = 插件路劲;
                string d1 = "\\arcdps_lang.ini";
                thread[1] = new Thread(new ParameterizedThreadStart(delegate { 下载(a1, b1, null, c1, d1); }))
                {
                    IsBackground = true
                };
                thread[1].Start();
                Application.DoEvents();

            }
            else
            {

            }
            //都不切换
            if (勾选[1])
            {
                项目个数++;
                string a2 = @"https://www.deltaconnected.com/arcdps/x64/buildtemplates/d3d9_arcdps_buildtemplates.dll";
                bool b2 = true;
                string c2 = 下载路劲;
                string d2 = "\\d3d9_arcdps_buildtemplates.dll";
                thread[2] = new Thread(new ParameterizedThreadStart(delegate { 下载(a2, b2, 进度条(1), c2, d2); }))
                {
                    IsBackground = true
                };
                thread[2].Start();
                Application.DoEvents();
            }
            else
            {
                if (File.Exists(下载路劲 + "\\d3d9_arcdps_buildtemplates.dll"))
                {
                    File.Delete(下载路劲 + "\\d3d9_arcdps_buildtemplates.dll");
                }
            }
            //附加
            if (勾选[2])
            {
                项目个数++;
                string a3 = @"https://www.deltaconnected.com/arcdps/x64/extras/d3d9_arcdps_extras.dll";
                bool b3 = true;
                string c3 = 下载路劲;
                string d3 = "\\d3d9_arcdps_extras.dll";
                thread[3] = new Thread(new ParameterizedThreadStart(delegate { 下载(a3, b3, 进度条(2), c3, d3); }))
                {
                    IsBackground = true
                };
                thread[3].Start();
                Application.DoEvents();
            }
            else
            {
                if (File.Exists(下载路劲 + "\\d3d9_arcdps_extras.dll"))
                {
                    File.Delete(下载路劲 + "\\d3d9_arcdps_extras.dll");
                }
            }
            //流汗
            if (勾选[3])
            {
                项目个数++;
                string a4 = @"http://gw2sy.top//wp-content/uploads/d3d9_arcdps_sct.dll";
                bool b4 = true;
                string c4 = 下载路劲;
                string d4 = "\\d3d9_arcdps_sct.dll";
                thread[4] = new Thread(new ParameterizedThreadStart(delegate { 下载(a4, b4, 进度条(3), c4, d4); }))
                {
                    IsBackground = true
                };
                thread[4].Start();
                项目个数++;
                string a5 = @"http://gw2sy.top//wp-content/uploads/lang.ini";
                bool b5 = false;
                string c5 = 插件路劲B;
                string d5 = "\\lang.ini";
                thread[5] = new Thread(new ParameterizedThreadStart(delegate { 下载(a5, b5, null, c5, d5); }))
                {
                    IsBackground = true
                };
                thread[5].Start();
                Application.DoEvents();
            }
            else
            {
                if (!勾选[4])
                {
                    if (File.Exists(下载路劲 + "\\d3d9_arcdps_sct.dll"))
                    {
                        File.Delete(下载路劲 + "\\d3d9_arcdps_sct.dll");
                    }
                }

            }
            //流英
            if (勾选[4])
            {
                项目个数++;
                string a6 = @"http://gw2sy.top//wp-content/uploads/d3d9_arcdps_sct_en.dll";
                bool b6 = true;
                string c6 = 下载路劲;
                string d6 = "\\d3d9_arcdps_sct.dll";
                thread[6] = new Thread(new ParameterizedThreadStart(delegate { 下载(a6, b6, 进度条(4), c6, d6); }))
                {
                    IsBackground = true
                };
                thread[6].Start();
                Application.DoEvents();
            }
            else
            {
                if (!勾选[3])
                {
                    if (File.Exists(下载路劲 + "\\d3d9_arcdps_sct.dll"))
                    {
                        File.Delete(下载路劲 + "\\d3d9_arcdps_sct.dll");
                    }
                }
            }
            //团力
            if (勾选[5])
            {
                //d3d9_arcdps_mechanicschs.dll
                项目个数++;
                string a7 = @"http://gw2sy.top//wp-content/uploads/d3d9_arcdps_mechanicschs.dll";
                bool b7 = true;
                string c7 = 下载路劲;
                string d7 = "\\d3d9_arcdps_mechanicschs.dll";
                thread[7] = new Thread(new ParameterizedThreadStart(delegate { 下载(a7, b7, 进度条(5), c7, d7); }))
                {
                    IsBackground = true
                };
                thread[7].Start();
                Application.DoEvents();
            }
            else
            {
                if (File.Exists(下载路劲 + "\\d3d9_arcdps_mechanicschs.dll"))
                {
                    File.Delete(下载路劲 + "\\d3d9_arcdps_mechanicschs.dll");
                }
            }
            //团嗯
            if (勾选[6])
            {
                //d3d9_arcdps_tablechs.dll
                项目个数++;
                string a8 = @"http://gw2sy.top//wp-content/uploads/d3d9_arcdps_tablechs.dll";
                bool b8 = true;
                string c8 = 下载路劲;
                string d8 = "\\d3d9_arcdps_tablechs.dll";
                thread[8] = new Thread(new ParameterizedThreadStart(delegate { 下载(a8, b8, 进度条(6), c8, d8); }))
                {
                    IsBackground = true
                };
                thread[8].Start();
                Application.DoEvents();
            }
            else
            {
                if (File.Exists(下载路劲 + "\\d3d9_arcdps_tablechs.dll"))
                {
                    File.Delete(下载路劲 + "\\d3d9_arcdps_tablechs.dll");
                }
            }
            //坐骑
            if (勾选[7])
            {
                项目个数++;
                string a9 = @"http://gw2sy.top//wp-content/uploads/d3d9_chainload.dll";
                bool b9 = true;
                string c9 = 下载路劲;
                string d9 = "\\d3d9_chainload.dll";
                thread[9] = new Thread(new ParameterizedThreadStart(delegate { 下载(a9, b9, 进度条(7), c9, d9); }))
                {
                    IsBackground = true
                };
                thread[9].Start();
                Application.DoEvents();
            }
            else
            {
                if (File.Exists(下载路劲 + "\\d3d9_chainload.dll"))
                {
                    File.Delete(下载路劲 + "\\d3d9_chainload.dll");
                }
            }
            //汉滤镜
            if (勾选[8])
            {
                项目个数++;
                string a10 = @"http://gw2sy.top//wp-content/uploads/d3d9_ReShade64.zip";
                bool b10 = true;
                string c10 = 下载路劲;
                string d10 = "\\d3d9_ReShade64.zip";
                thread[10] = new Thread(new ParameterizedThreadStart(delegate { 下载(a10, b10, 进度条(8), c10, d10); }))
                {
                    IsBackground = true
                };
                thread[10].Start();
                Application.DoEvents();
            }
            else
            {

            }
            //滤镜
            if (勾选[9])
            {
                项目个数++;
                string a11 = @"http://gw2sy.top//wp-content/uploads/SweetFX.zip";
                bool b11 = true;
                string c11 = 下载路劲;
                string d11 = "\\SweetFX.zip";
                thread[11] = new Thread(new ParameterizedThreadStart(delegate { 下载(a11, b11, 进度条(9), c11, d11); }))
                {
                    IsBackground = true
                };
                thread[11].Start();
                Application.DoEvents();
            }
            else
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start(官方网址);
            //string httpUrl = @"http://gw2sy.top//wp-content/uploads/d3d9_chainload.dll";
            //string saveUrl = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + System.IO.Path.GetFileName(httpUrl);
            //int threadNumber = 8;
            
            //md = new MultiDownload(threadNumber, httpUrl, saveUrl);
            //md.Start();

        }
    }
}
