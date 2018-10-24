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
        Thread[] thread = new Thread[6];
        public string 本地路劲 = Application.StartupPath;
        public string 插件路劲 = Application.StartupPath + "\\addons\\arcdps";
        public string 下载路劲 = Application.StartupPath + "\\bin64";
        public string 顺网路劲 = Application.StartupPath;
        //1 主程序;2 DB切换;3 附加功能;4团队力学;5 坐骑插件;6汉化文本 
        public string[] 文件名 = new string[6] 
        {   "\\d3d9.dll",
            "\\d3d9_arcdps_buildtemplates.dll",
            "\\d3d9_arcdps_extras.dll",
            "\\d3d9_arcdps_mechanics.dll",
            "\\d3d9_chainload.dll",
            "\\arcdps_lang.ini" };
        public string[] 网站 = new string[6] 
        {   @"https://www.deltaconnected.com/arcdps/x64/d3d9.dll",
            @"https://www.deltaconnected.com/arcdps/x64/buildtemplates/d3d9_arcdps_buildtemplates.dll",
            @"https://www.deltaconnected.com/arcdps/x64/extras/d3d9_arcdps_extras.dll",
            @"http://martionlabs.com/wp-content/uploads/d3d9_arcdps_mechanics.dll",
            @"http://q53809331.gz01.bdysite.com/wp-content/uploads/d3d9_chainload.dll",
            @"https://raw.githubusercontent.com/Snowy1794/Arcdps-translation-Chinese-simplified/master/arcdps_lang.ini" };
        public bool[] 勾选 = new bool[6];
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
        //主程序
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.主程序 = checkBox1.Checked;
            Properties.Settings.Default.Save();
        }
        //DB切换
        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DB切换 = checkBox2.Checked;
            Properties.Settings.Default.Save();
        }
        //附加功能
        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.附加功能 = checkBox3.Checked;
            Properties.Settings.Default.Save();
        }
        //团队力学
        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.团队力学 = checkBox4.Checked;
            Properties.Settings.Default.Save();
        }
        //坐骑插件
        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.坐骑插件 = checkBox5.Checked;
            Properties.Settings.Default.Save();
        }
        //汉化文本
        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.汉化文本 = checkBox6.Checked;
            Properties.Settings.Default.Save();
        }
        //自动更新
        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.自动更新 = checkBox7.Checked;
            Properties.Settings.Default.Save();
        }
        //自动启动
        private void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.自动启动 = checkBox8.Checked;
            Properties.Settings.Default.Save();

        }
        //顺网
        private void CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.顺网 = checkBox9.Checked;
            Properties.Settings.Default.Save();
            if (!下载中)
            {
                    禁用();
                    下载中 = true;
                    更新();
            }
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
        #endregion

        #region 主要代码

        public void 更新()
        {
            完成个数 = 0;
            项目个数 = 0;
            string 路劲;
            if (Properties.Settings.Default.顺网|| checkBox9.Checked)
            {
                路劲 = 顺网路劲;
                try
                {
                    for (int i = 0; i <= 5; i++)
                    {
                        if (File.Exists(下载路劲 + 文件名[i]))
                        {
                            File.Delete(下载路劲 + 文件名[i]);
                        }
                        
                    }
                }
                catch (Exception) { }
            }
            else
            {
                路劲 = 下载路劲;
                try
                {
                    for (int i = 0; i <= 5; i++)
                    {
                        if (File.Exists(顺网路劲 + 文件名[i]))
                        {
                            File.Delete(顺网路劲 + 文件名[i]);
                        }
                    }
                }
                catch (Exception){}
            }
            勾选[0] = Properties.Settings.Default.主程序;
            勾选[1] = Properties.Settings.Default.DB切换;
            勾选[2] = Properties.Settings.Default.附加功能;
            勾选[3] = Properties.Settings.Default.团队力学;
            勾选[4] = Properties.Settings.Default.坐骑插件;
            勾选[5] = Properties.Settings.Default.汉化文本;
            if (勾选[0])
            {
                项目个数++;
                thread[0] = new Thread(new ThreadStart(delegate { 下载(网站[0], 进度条(0), 路劲, 文件名[0]); }));
                thread[0].IsBackground = true;
                thread[0].Start();
                Application.DoEvents();
            }
            else
            {
                if (File.Exists(路劲 + 文件名[0]))
                {
                    File.Delete(路劲 + 文件名[0]);
                }
            }
            if (勾选[1])
            {
                项目个数++;
                thread[1] = new Thread(new ThreadStart(delegate { 下载(网站[1], 进度条(1), 路劲, 文件名[1]); }));
                thread[1].IsBackground = true;
                thread[1].Start();
                Application.DoEvents();
            }
            else
            {
                if (File.Exists(路劲 + 文件名[1]))
                {
                    File.Delete(路劲 + 文件名[1]);
                }
            }

            if (勾选[2])
            {
                项目个数++;
                thread[2] = new Thread(new ThreadStart(delegate { 下载(网站[2], 进度条(2), 路劲, 文件名[2]); }));
                thread[2].IsBackground = true;
                thread[2].Start();
                Application.DoEvents();
            }
            else
            {
                if (File.Exists(路劲 + 文件名[2]))
                {
                    File.Delete(路劲 + 文件名[2]);
                }
            }
            if (勾选[3])
            {
                项目个数++;
                thread[3] = new Thread(new ThreadStart(delegate { 下载(网站[3], 进度条(3), 路劲, 文件名[3]); }));
                thread[3].IsBackground = true;
                thread[3].Start();
                Application.DoEvents();
            }
            else
            {
                if (File.Exists(路劲 + 文件名[3]))
                {
                    File.Delete(路劲 + 文件名[3]);
                }
            }
            if (勾选[4])
            {
                if (勾选[0])
                {
                    项目个数++;
                    //thread[4] = new Thread(new ThreadStart(delegate { 坐骑(进度条(4), 路劲, 文件名[4]); }));
                    thread[4] = new Thread(new ThreadStart(delegate { 下载(网站[4], 进度条(4), 路劲, 文件名[4]); }));
                    thread[4].IsBackground = true;
                    thread[4].Start();
                    Application.DoEvents();
                }
                else
                {
                    MessageBox.Show("您现在选择单独使用坐骑插件,请注意其他插件将不会被加载");
                    项目个数++;
                    thread[4] = new Thread(new ThreadStart(delegate { 下载(网站[4], 进度条(4), 路劲, 文件名[0]); }));
                    thread[4].IsBackground = true;
                    thread[4].Start();
                    Application.DoEvents();
                }
                
            }
            else
            {
                if (File.Exists(路劲 + 文件名[4]))
                {
                    File.Delete(路劲 + 文件名[4]);
                }
            }
            if (勾选[5])
            {
                项目个数++;
                thread[5] = new Thread(new ThreadStart(delegate { 下载(网站[5], 进度条(5), 插件路劲, 文件名[5]); }));
                thread[5].IsBackground = true;
                thread[5].Start();
                Application.DoEvents();
            }
            else
            {
                if (File.Exists(插件路劲 + 文件名[5]))
                {
                    File.Delete(插件路劲 + 文件名[5]);
                }
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
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
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
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
        }

        public void 卸载()
        {
            try
            {
                for (int i = 0; i <= 5; i++)
                {
                    if (File.Exists(下载路劲 + 文件名[i]))
                    {
                        File.Delete(下载路劲 + 文件名[i]);
                    }
                    if (File.Exists(顺网路劲 + 文件名[i]))
                    {
                        File.Delete(顺网路劲 + 文件名[i]);
                    }
                }
            }
            catch (Exception) { }
        }

        public void 启动()
        {
            if (File.Exists(@".\\Gw2-64.exe"))
            {
                ProcessStartInfo info = new ProcessStartInfo { FileName = @".\\Gw2-64.exe", Arguments = "-maploadinfo" };
                Process pro = new Process
                {
                    StartInfo = info
                };
                pro.Start();
                Close();
            }
            else
            {
                MessageBox.Show("没有找到 Gw2-64.exe !");
                Close();
            }
        }

        //public void 坐骑(ProgressBar 进度条 ,string 存储位置,string 文件名 )
        //{
        //    if (!File.Exists(存储位置 + 文件名))
        //    {
        //        byte[] Save = Properties.Resources.d3d9_chainload;
        //        FileStream fsObj = new FileStream(存储位置 + 文件名, FileMode.CreateNew);
        //        fsObj.Write(Save, 0, Save.Length);
        //        fsObj.Close();
        //    }
        //    for (int i = 0; i <= 100; i++)
        //    {
        //        进度条.Value = i;
        //    }
        //    完成个数++;
        //}

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
                        Application.DoEvents();
                        so.Write(by, 0, osize);
                        if (进度条 != null)
                        {
                            进度条.Value = (int)totalDownloadedByte;
                        }
                        osize = st.Read(by, 0, by.Length);
                        Application.DoEvents();
                    }
                    so.Dispose();
                    st.Dispose();
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
                myrp.Dispose();
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
            if (!File.Exists(@"C:\Windows\System32\d3d9.dll"))
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
                byte[] Save = Properties.Resources.arcdps;
                FileStream fsObj = new FileStream(插件路劲 + "\\arcdps.ini", FileMode.CreateNew);
                fsObj.Write(Save, 0, Save.Length);
                fsObj.Close();
                byte[] Save1 = Properties.Resources.arcdps_font;
                FileStream fsObj1 = new FileStream(插件路劲 + "\\arcdps_font.ttf", FileMode.CreateNew);
                fsObj1.Write(Save1, 0, Save1.Length);
                fsObj1.Close();
            }
            else if (a == 2)
            {
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


    }
}
