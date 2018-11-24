using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace ArcDPS更新工具
{
    public partial class Form1 : Form
    {
        #region 参数
        Thread[] thread = new Thread[8];
        public string 本地路劲 = Application.StartupPath;
        public string 插件路劲 = Application.StartupPath + "\\addons\\arcdps";
        public string 插件路劲B = Application.StartupPath + "\\addons\\sct";
        public string 下载路劲 = Application.StartupPath + "\\bin64";
        //1 主程序;2 DB切换;3 附加功能;4团队力学;5 坐骑插件;6汉化文本;7流动插件;8流动插件汉化;
        public string 版本检测网址 = "http://gw2sy.gz01.bdysite.com/wp-content/uploads/1.txt";
        public string 更新说明 = "http://gw2sy.gz01.bdysite.com/wp-content/uploads/2.txt";
        public string 官方网址 = "https://www.syupdatetool.top";
        public string[] 文件名 = new string[8]
        {   "\\d3d9.dll",
            "\\d3d9_arcdps_buildtemplates.dll",
            "\\d3d9_arcdps_extras.dll",
            "\\d3d9_arcdps_mechanics.dll",
            "\\d3d9_chainload.dll",
            "\\arcdps_lang.ini",
            "\\d3d9_arcdps_sct.dll",
            "\\lang.ini"};
        public string[] 网站 = new string[8] 
        {   @"https://www.deltaconnected.com/arcdps/x64/d3d9.dll",
            @"https://www.deltaconnected.com/arcdps/x64/buildtemplates/d3d9_arcdps_buildtemplates.dll",
            @"https://www.deltaconnected.com/arcdps/x64/extras/d3d9_arcdps_extras.dll",
            @"http://martionlabs.com/wp-content/uploads/d3d9_arcdps_mechanics.dll",
            @"http://gw2sy.gz01.bdysite.com/wp-content/uploads/d3d9_chainload.dll",
            @"https://raw.githubusercontent.com/Snowy1794/Arcdps-translation-Chinese-simplified/master/arcdps_lang.ini",
            @"http://gw2sy.gz01.bdysite.com/wp-content/uploads/d3d9_arcdps_sct.dll",
            @"http://gw2sy.gz01.bdysite.com/wp-content/uploads/lang.txt"};
        public bool[] 勾选 = new bool[8];
        public bool 下载中 = false;
        public int 项目个数 = 0;
        public int 完成个数 = 0;

        #endregion

        #region 窗口加载

        public Form1()
        {
            
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Properties.Settings.Default.主程序;
            checkBox2.Checked = Properties.Settings.Default.DB切换;
            checkBox3.Checked = Properties.Settings.Default.附加功能;
            checkBox4.Checked = Properties.Settings.Default.团队力学;
            checkBox5.Checked = Properties.Settings.Default.坐骑插件;
            checkBox6.Checked = Properties.Settings.Default.汉化文本;
            checkBox7.Checked = Properties.Settings.Default.自动更新;
            checkBox8.Checked = Properties.Settings.Default.自动启动;
            checkBox9.Checked = Properties.Settings.Default.顺网;
            checkBox10.Checked = Properties.Settings.Default.流动输出;
            checkBox11.Checked = Properties.Settings.Default.快捷启动;
            textBox1.Text = Properties.Settings.Default.用户名;
            textBox2.Text = Properties.Settings.Default.密码;
            
            int 最新版本 = 版本();
            label4.Text = "当前版本:V"+ Application.ProductVersion;
            if (最新版本 == 0)
            {
                MessageBox.Show("获取最新版本信息失败,官网暂时无法连接");
                Close();
            }
            linkLabel1.Text = "最新版本:V"+ 最新版本.ToString();

            int.TryParse(Application.ProductVersion, out int 本地版本);
            if (本地版本 < 最新版本)
            {
                string 说明文档 = 说明();
                if (说明文档 == "")
                {
                    MessageBox.Show("获取最新版本信息失败");
                }
                MessageBox.Show("有最新版本V" + 最新版本.ToString() + ",请前往官网下载"+ 说明文档);
            }//
            
            if (检测环境())
            {
                初次运行();
                if (Properties.Settings.Default.自动更新)
                {
                    更新();
                    下载中 = true;
                    禁用();
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        #endregion

        #region 按钮

        private void 选择事件(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            switch (check.Text)
            {
                case "主程序":
                    Properties.Settings.Default.主程序 = check.Checked;
                    break;
                case "DB切换":
                    Properties.Settings.Default.DB切换 = check.Checked;
                    break;
                case "附加功能":
                    Properties.Settings.Default.附加功能 = check.Checked;
                    break;
                case "团队力学":
                    Properties.Settings.Default.团队力学 = check.Checked;
                    break;
                case "坐骑插件":
                    Properties.Settings.Default.坐骑插件 = check.Checked;
                    break;
                case "汉化文本":
                    Properties.Settings.Default.汉化文本 = check.Checked;
                    break;
                case "流动输出":
                    Properties.Settings.Default.流动输出 = check.Checked;
                    break;
                case "启动后自动更新":
                    Properties.Settings.Default.自动更新 = check.Checked;
                    break;
                case "更新完成启动游戏":
                    Properties.Settings.Default.自动启动 = check.Checked;
                    break;
                case "快捷启动(需已经正常登陆过的网络)":
                    Properties.Settings.Default.快捷启动 = check.Checked;
                    break;
                case "顺网(快捷启动无效)":
                    Properties.Settings.Default.顺网 = check.Checked;
                    break;
                default:
                    break;
            }
            Properties.Settings.Default.Save();
        }
        //更新
        private void Button1_Click(object sender, EventArgs e)
        {
            if (!下载中)
            {
                禁用();
                下载中 = true;
                更新();
            }
        }
        //卸载
        private void Button2_Click(object sender, EventArgs e)
        {
            卸载();
        }
        //启动
        private void Button3_Click(object sender, EventArgs e)
        {
            启动();
        }
        private void 用户名改变(object sender, EventArgs e)
        {
            Properties.Settings.Default.用户名 = textBox1.Text;
            Properties.Settings.Default.Save();
        }
        private void 密码改变(object sender, EventArgs e)
        {
            Properties.Settings.Default.密码 = textBox2.Text;
            Properties.Settings.Default.Save();
        }
        #endregion

        #region 主要代码

        public void 更新()
        {
            完成个数 = 0;
            项目个数 = 0;
            勾选[0] = Properties.Settings.Default.主程序;
            勾选[1] = Properties.Settings.Default.DB切换;
            勾选[2] = Properties.Settings.Default.附加功能;
            勾选[3] = Properties.Settings.Default.团队力学;
            勾选[4] = Properties.Settings.Default.坐骑插件;
            勾选[5] = Properties.Settings.Default.汉化文本;
            勾选[6] = Properties.Settings.Default.流动输出;

            if (File.Exists(下载路劲 + "\\d3d9_chainload.dll"))
            {
                File.Delete(下载路劲 + "\\d3d9_chainload.dll");
            }
            if (File.Exists(下载路劲 + "\\d3d9_chainload_noex.dll"))
            {
                File.Delete(下载路劲 + "\\d3d9_chainload_noex.dll");
            }
            string d3d9 = "";
            string 坐骑 = "";
            string arc路径 = "";
            string 坐骑路径 = "";
            if (勾选[0] && 勾选[4])
            {
                d3d9 = "\\d3d9_mchain.dll";
                坐骑 = 文件名[0];
                arc路径 = 下载路劲;
                坐骑路径 = 本地路劲;

            }
            else
            {
                if (勾选[0])
                {
                    d3d9 = 文件名[0];
                    arc路径 = 本地路劲;
                    
                }
                if (勾选[4])
                {
                    MessageBox.Show("您现在选择的是当坐骑插件模式,请注意其他插件将不会被加载");
                    坐骑 = 文件名[0];
                    坐骑路径 = 本地路劲;
                }
                if (File.Exists(下载路劲 + "\\d3d9_mchain.dll"))
                {
                    File.Delete(下载路劲 + "\\d3d9_mchain.dll");
                }
            }
            if (d3d9 != "")
            {
                项目个数++;
                thread[0] = new Thread(new ParameterizedThreadStart(delegate { 下载(网站[0], 进度条(0), arc路径, d3d9); }));
                thread[0].IsBackground = true;
                thread[0].Start();
                Application.DoEvents();
            }
            if (坐骑 != "")
            {
                项目个数++;
                thread[4] = new Thread(new ParameterizedThreadStart(delegate { 下载(网站[4], 进度条(4), 坐骑路径, 坐骑); }));
                thread[4].IsBackground = true;
                thread[4].Start();
                Application.DoEvents();
            }
            if (勾选[6])
            {
                项目个数++;
                thread[6] = new Thread(new ParameterizedThreadStart(delegate { 下载(网站[6], 进度条(6), 下载路劲, 文件名[6]); }));
                thread[6].IsBackground = true;
                thread[6].Start();
                Application.DoEvents();
                项目个数++;
                thread[7] = new Thread(new ParameterizedThreadStart(delegate { 下载(网站[7], 进度条(7), 插件路劲B, 文件名[7]); }));
                thread[7].IsBackground = true;
                thread[7].Start();
                Application.DoEvents();
            }
            if (勾选[1])
            {
                项目个数++;
                thread[1] = new Thread(new ParameterizedThreadStart(delegate { 下载(网站[1], 进度条(1), 下载路劲, 文件名[1]); }));
                thread[1].IsBackground = true;
                thread[1].Start();
                Application.DoEvents();
            }
            if (勾选[2])
            {
                项目个数++;
                thread[2] = new Thread(new ParameterizedThreadStart(delegate { 下载(网站[2], 进度条(2), 下载路劲, 文件名[2]); }));
                thread[2].IsBackground = true;
                thread[2].Start();
                Application.DoEvents();
            }
            if (勾选[3])
            {
                项目个数++;
                thread[3] = new Thread(new ParameterizedThreadStart(delegate { 下载(网站[3], 进度条(3), 下载路劲, 文件名[3]); }));
                thread[3].IsBackground = true;
                thread[3].Start();
                Application.DoEvents();
            }
            if (勾选[5])
            {
                项目个数++;
                thread[5] = new Thread(new ParameterizedThreadStart(delegate { 下载(网站[5], 进度条(5), 插件路劲, 文件名[5]); }));
                thread[5].IsBackground = true;
                thread[5].Start();
                Application.DoEvents();
            }
        }

        public ProgressBar 进度条(int a )
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
                default:
                    progressBar = null;
                    break;
            }
            return progressBar;
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
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            //textBox1.Enabled = false;
            //textBox2.Enabled = false;
        }

        public void 启用()
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox5.Enabled = true;
            checkBox6.Enabled = true;
            checkBox7.Enabled = true;
            checkBox8.Enabled = true;
            checkBox9.Enabled = true;
            checkBox10.Enabled = true;
            checkBox11.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            //textBox1.Enabled = true;
            //textBox2.Enabled = true;
        }

        public void 卸载()
        {
            try
            {
                for (int i = 0; i <= 7; i++)
                {
                    if (File.Exists(下载路劲 + 文件名[i]))
                    {
                        File.Delete(下载路劲 + 文件名[i]);
                    }
                    if (File.Exists(本地路劲 + 文件名[i]))
                    {
                        File.Delete(本地路劲 + 文件名[i]);
                    }
                }
                if (Directory.Exists(Application.StartupPath + "\\addons"))
                {
                    Directory.Delete(Application.StartupPath + "\\addons");
                }
            }
            catch (Exception) { }
        }

        public void 启动()
        {
            string 启动代码 = "-maploadinfo";
            string 程序位置 = @".\\Gw2-64.exe";
            if (Properties.Settings.Default.顺网)
            {
                MessageBox.Show("顺网用户快捷登录无效哦!");
                程序位置 = @".\\GW2Lanucher.exe";
                if (File.Exists(程序位置))
                {
                    ProcessStartInfo info = new ProcessStartInfo { FileName = 程序位置, Arguments = 启动代码 };
                    Process pro = new Process
                    {
                        StartInfo = info
                    };
                    pro.Start();
                    Close();
                }
                else
                {
                    MessageBox.Show("没有找到 GW2Lanucher.exe !");
                    Close();
                }

            }
            else
            {
                if (File.Exists(程序位置))
                {
                    if (Properties.Settings.Default.快捷启动)
                    {
                        if (textBox1.Text != "" && textBox1.Text != "")
                        {
                            string argument1 = "\"" + "-email" + "\"";
                            string argument2 = "\"" + textBox1.Text + "\"";
                            string argument3 = "\"" + "-password" + "\"";
                            string argument4 = "\"" + textBox2.Text + "\"";
                            string argument5 = "\"" + "-nopatchui" + "\"";
                            string argument6 = "\"" + "-maploadinfo" + "\"";
                            Process process = new Process();
                            process.StartInfo.FileName = System.Environment.CurrentDirectory + "//Gw2-64.exe";
                            process.StartInfo.Arguments = argument1 + " " + argument2 + " " + argument3 + " " + argument4 + " " + argument5 + " " + argument6;
                            process.Start();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("你没有输入正确的帐号和密码");
                            return;
                        }

                    }
                    else
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
                else
                {
                    MessageBox.Show("没有找到 Gw2-64.exe !");
                    Close();
                }
            }

        }

        public int 版本()
        {
            int a =0;
            try
            {
                var wc = new WebClient();
                var html = wc.DownloadString(版本检测网址);
                int.TryParse(html, out a);
                wc.Dispose();
            }
            catch (Exception)
            {
                 a = 0;
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

        public void 下载(string 网站位置, ProgressBar 进度条, string 储存位置, string 文件名)
        {
            Thread.BeginThreadAffinity();
            try
            {
                HttpWebRequest Myrq = (HttpWebRequest)HttpWebRequest.Create(网站位置);
                HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
                Myrq.Timeout = 50000;
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
                    if (进度条 != null)
                    {
                        进度条.Maximum = (int)totalBytes;
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
                        if (进度条 != null)
                        {
                            进度条.Value = (int)totalDownloadedByte;
                        }
                        osize = st.Read(by, 0, by.Length);
                    }
                    //so.Dispose();
                    //st.Dispose();
                    so.Close();
                    st.Close();
                    File.SetLastWriteTime(储存位置 + 文件名, myrp.LastModified);
                    完成个数++;
                }
                else
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        进度条.Value = i;
                    }
                    完成个数++;
                }

                myrp.Close();
                //myrp.Dispose();
                Myrq.Abort();
            }
            catch (Exception)
            {
                完成个数++;          
            }
            Thread.EndThreadAffinity();
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
                a = 1;
            }
            if (!File.Exists(@"C:\Windows\System32\vcamp120.dll"))
            {
                if (a == 1)
                {
                    a = 3;
                }
                else
                {
                    a = 2;
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
                    ProcessStartInfo info = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                    Process pro = Process.Start(info);
                    break;
                case 2:
                    byte[] Save2 = Properties.Resources.vcredist_x64;
                    FileStream fsObj2 = new FileStream(本地路劲 + "\\vcredist_x64.exe", FileMode.CreateNew);
                    fsObj2.Write(Save2, 0, Save2.Length);
                    fsObj2.Close();
                    ProcessStartInfo info1 = new ProcessStartInfo { FileName = @".\\vcredist_x64.exe" };
                    Process pro1 = Process.Start(info1);
                    byte[] Save3 = Properties.Resources.vcredist_x86;
                    FileStream fsObj3 = new FileStream(本地路劲 + "\\vcredist_x86.exe", FileMode.CreateNew);
                    fsObj3.Write(Save3, 0, Save3.Length);
                    fsObj3.Close();
                    ProcessStartInfo info2 = new ProcessStartInfo { FileName = @".\\vcredist_x86.exe" };
                    Process pro2 = Process.Start(info2);
                    break;
                case 3:
                    byte[] Save4 = Properties.Resources.vcredist_x64;
                    FileStream fsObj4 = new FileStream(本地路劲 + "\\vcredist_x64.exe", FileMode.CreateNew);
                    fsObj4.Write(Save4, 0, Save4.Length);
                    fsObj4.Close();
                    ProcessStartInfo info3 = new ProcessStartInfo { FileName = @".\\vcredist_x64.exe" };
                    Process pro3 = Process.Start(info3);
                    byte[] Save5 = Properties.Resources.vcredist_x86;
                    FileStream fsObj5 = new FileStream(本地路劲 + "\\vcredist_x86.exe", FileMode.CreateNew);
                    fsObj5.Write(Save5, 0, Save5.Length);
                    fsObj5.Close();
                    ProcessStartInfo info4 = new ProcessStartInfo { FileName = @".\\vcredist_x86.exe" };
                    Process pro4 = Process.Start(info4);
                    byte[] Save6 = Properties.Resources.dxwebsetup;
                    FileStream fsObj6 = new FileStream(本地路劲 + "\\dxwebsetup.exe", FileMode.CreateNew);
                    fsObj6.Write(Save6, 0, Save6.Length);
                    fsObj6.Close();
                    ProcessStartInfo info5 = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                    Process pro5 = Process.Start(info5);
                    break;
                default:
                    break;
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
                    Directory.CreateDirectory(插件路劲B+ "\\fonts");
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

                //byte[] Save3 = Properties.Resources.lang;
                //FileStream fsObj3 = new FileStream(插件路劲B + "\\lang.ini", FileMode.CreateNew);
                //fsObj3.Write(Save3, 0, Save3.Length);
                //fsObj3.Close();
                //Application.DoEvents();
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
                //if (!File.Exists(插件路劲B + "\\lang.ini"))
                //{
                //    byte[] Save3 = Properties.Resources.lang;
                //    FileStream fsObj3 = new FileStream(插件路劲B + "\\lang.ini", FileMode.CreateNew);
                //    fsObj3.Write(Save3, 0, Save3.Length);
                //    fsObj3.Close();
                //}
            }
        }

        #endregion

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (下载中)
            {
                if (项目个数 == 完成个数)
                {
                    下载中 = false;
                    启用();
                }
            }
            else
            {
                if (项目个数 == 完成个数 && 项目个数 >0)
                {
                    if (Properties.Settings.Default.自动启动 | checkBox8.CheckState == CheckState.Checked)
                    {
                        启动();
                    }
                }

            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(官方网址);
        }

    }
}
