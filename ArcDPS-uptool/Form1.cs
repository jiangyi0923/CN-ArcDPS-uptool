using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcDPS_uptool
{
    public partial class Form1 : Form  
    {
        public string 版本检测网址 = "http://gw2sy.top/wp-content/uploads/1.txt";
        public string 更新说明 = "http://gw2sy.top/wp-content/uploads/2.txt";
        public string 信息检测网址 = "http://gw2sy.top/wp-content/uploads/11.txt";
        public string 信息说明 = "http://gw2sy.top/wp-content/uploads/22.txt";
        public string 官方网址 = "https://gw2sy.top";
        public string 目录 = Application.StartupPath;
        public string bin64 = Application.StartupPath + "//bin64";
        
        public string dw1 = @"https://www.deltaconnected.com/arcdps/x64/d3d9.dll";//主程序
        public string dw11 = @"https://raw.githubusercontent.com/Snowy1794/Arcdps-translation-Chinese-simplified/master/arcdps_lang.ini";//汉化文件
        public string dw2 = @"https://www.deltaconnected.com/arcdps/x64/buildtemplates/d3d9_arcdps_buildtemplates.dll";//db切换
        public string dw3 = @"https://www.deltaconnected.com/arcdps/x64/extras/d3d9_arcdps_extras.dll";//附加功能
        public string dw4 = @"http://gw2sy.top//wp-content/uploads/d3d9_arcdps_sct.dll";//sct
        public string dw5 = @"http://gw2sy.top//wp-content/uploads/d3d9_arcdps_mechanicschs.dll";//团力
        public string dw6 = @"http://gw2sy.top//wp-content/uploads/d3d9_arcdps_tablechs.dll";//团恩
        public string dw7 = @"http://gw2sy.top//wp-content/uploads/d3d9_chainload.dll";//坐骑
        public string dw8 = @"http://gw2sy.top//wp-content/uploads/d3d9_ReShade64.zip";//汉化滤镜
        public string dw9 = @"http://gw2sy.top//wp-content/uploads/SweetFX.zip";//自动滤镜

        DownloadOBGK md1 = new DownloadOBGK();
        DownloadOBGK md11 = new DownloadOBGK();
        DownloadOBGK md2 = new DownloadOBGK();
        DownloadOBGK md3 = new DownloadOBGK();
        DownloadOBGK md4 = new DownloadOBGK();
        DownloadOBGK md5 = new DownloadOBGK();
        DownloadOBGK md6 = new DownloadOBGK();
        DownloadOBGK md7 = new DownloadOBGK();
        DownloadOBGK md8 = new DownloadOBGK();
        DownloadOBGK md9 = new DownloadOBGK();
        

        bool ts1 = false;
        bool ts11 = false;
        bool ts2 = false;
        bool ts3 = false;
        bool ts4 = false;
        bool ts5 = false;
        bool ts6 = false;
        bool ts7 = false;
        bool ts8 = false;
        bool ts9 = false;

        int 完成计数 = 0;
        int 项目计数 = 0;
        private bool 下载中 = false;
        private bool 完成 = false;

        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            mainfrm = this;
            if (Regex.IsMatch(Application.StartupPath, @"[\u4e00-\u9fa5]") == true)
            {
                MessageBox.Show("所在目录为:" + Application.StartupPath + "\r\n"+
                "×目录含中文会导致插件不识别中文请修改\r\n");
                Close();
            }
            string path = @"./Gw2-64.exe";
            if (!File.Exists(path))
            {
                MessageBox.Show("×没有发现Gw2-64.exe请确认程序在游戏根目录或客户端程序名是否为Gw2-64.exe\r\n"+
                    "×请确保您开启了文件后缀名显示\r\n");
                Close();
            }
        }

        private void Form1_Shown(object sender, System.EventArgs e)
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
                    "如果没有请联系我哦!QQ:375480856");
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            checkBox1.Checked = Properties.Settings.Default._1程序;
            checkBox2.Checked = Properties.Settings.Default._2切换;
            checkBox3.Checked = Properties.Settings.Default._3附加;
            checkBox4.Checked = Properties.Settings.Default._4流汉;
            checkBox5.Checked = Properties.Settings.Default._5团力;
            checkBox6.Checked = Properties.Settings.Default._6团恩;
            checkBox7.Checked = Properties.Settings.Default._7坐骑;
            checkBox8.Checked = Properties.Settings.Default._8汉滤;
            checkBox9.Checked = Properties.Settings.Default._9全滤;
            checkBox10.Checked = Properties.Settings.Default._自动更新;
            checkBox11.Checked = Properties.Settings.Default._自动启动;
            checkBox12.Checked = Properties.Settings.Default._跳过更新;
            checkBox13.Checked = Properties.Settings.Default._附加命令;
            Properties.Settings.Default.tmp = Application.StartupPath;
            Properties.Settings.Default.Save();
            多选框检测();


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

            if (Properties.Settings.Default._自动更新)
            {
                是否更新();
            }

        }

        

        private void 多选框检测()
        {
            if (checkBox8.Checked)
            {
                checkBox9.Enabled = false;
            }
            else
            {
                checkBox9.Enabled = true;
            }
            if (checkBox9.Checked)
            {
                checkBox8.Enabled = false;
            }
            else
            {
                checkBox8.Enabled = true;
            }
            if (checkBox4.Checked)
            {
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
            }
            else
            {
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
            }
            if (checkBox5.Checked || checkBox6.Checked)
            {
                checkBox4.Enabled = false;
            }
            else
            {
                checkBox4.Enabled = true;
            }
            if (checkBox10.Checked)
            {
                checkBox12.Enabled = true;
            }
            else
            {
                checkBox12.Enabled = false;
            }
        }

        private int 版本()
        {
            int a = 0;
            var wc2 = new WebClient();
            try
            {

                var html = wc2.DownloadString(版本检测网址);
                int.TryParse(html, out a);
                wc2.Dispose();
            }
            catch (Exception)
            {
                a = 0;
            }
            finally
            {
                wc2.Dispose();
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
        private void Button1_Click(object sender, EventArgs e)
        {
            更新();
        }

        private void 是否更新()
        {
            if (Properties.Settings.Default._跳过更新)
            {
                string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                string week = Day[Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))].ToString();
                if (!week.Equals(Day[3]) && !week.Equals(Day[4]))
                {
                    textBox1.AppendText("今天是" + week + "可以正常更新\r\n");
                    更新();
                }
                else
                {
                    textBox1.AppendText("今天是" + week + "您设置了不更新,所以跳过\r\n");
                    完成 = true;
                }
            }
            else
            {
                更新();
            }
        }

        private void 更新()
        {



            string saveUrl1 = bin64 + "//" + System.IO.Path.GetFileName(dw1);
            string saveUrl11 = 目录 + "//addons//arcdps//" + System.IO.Path.GetFileName(dw11);
            string saveUrl2 = bin64 + "//" + System.IO.Path.GetFileName(dw2);
            string saveUrl3 = bin64 + "//" + System.IO.Path.GetFileName(dw3);
            string saveUrl4 = bin64 + "//" + System.IO.Path.GetFileName(dw4);
            string saveUrl5 = bin64 + "//" + System.IO.Path.GetFileName(dw5);
            string saveUrl6 = bin64 + "//" + System.IO.Path.GetFileName(dw6);
            string saveUrl7 = bin64 + "//" + System.IO.Path.GetFileName(dw7);
            string saveUrl8 = bin64 + "//" + System.IO.Path.GetFileName(dw8);
            string saveUrl9 = bin64 + "//" + System.IO.Path.GetFileName(dw9);
            if (!Directory.Exists(bin64))
            {
                Directory.CreateDirectory(bin64);
            }
            完成 = false;
            下载中 = true;
            if (Properties.Settings.Default._1程序)
            {
                var task1 = new Task(() => { ts1 = true; md1 = new DownloadOBGK(dw1, saveUrl1); md1.Start(); });
                task1.Start();
                checkBox1.Enabled = false;
                项目计数++;
                var task2 = new Task(() => { ts11 = true; md11 = new DownloadOBGK(dw11, saveUrl11); md11.Start(); });
                task2.Start();
                项目计数++;
            }
            if (Properties.Settings.Default._2切换)
            {
                var task3 = new Task(() => { ts2 = true; md2 = new DownloadOBGK(dw2, saveUrl2); md2.Start(); });
                task3.Start();
                checkBox2.Enabled = false;
                项目计数++;
            }
            else
            {
                if (File.Exists(bin64 + "\\d3d9_arcdps_buildtemplates.dll"))
                {
                    File.Delete(bin64 + "\\d3d9_arcdps_buildtemplates.dll");
                }
            }
            if (Properties.Settings.Default._3附加)
            {
                var task4 = new Task(() => { ts3 = true; md3 = new DownloadOBGK(dw3, saveUrl3); md3.Start(); });
                task4.Start();
                checkBox3.Enabled = false;
                项目计数++;
            }
            else
            {
                if (File.Exists(bin64 + "\\d3d9_arcdps_extras.dll"))
                {
                    File.Delete(bin64 + "\\d3d9_arcdps_extras.dll");
                }
            }
            if (Properties.Settings.Default._4流汉)
            {
                var task5 = new Task(() => { ts4 = true; md4 = new DownloadOBGK(dw4, saveUrl4); md4.Start(); });
                task5.Start();
                checkBox4.Enabled = false;
                项目计数++;
            }
            else
            {
                if (File.Exists(bin64 + "\\d3d9_arcdps_sct.dll"))
                {
                    File.Delete(bin64 + "\\d3d9_arcdps_sct.dll");
                }
            }
            if (Properties.Settings.Default._5团力)
            {
                var task6 = new Task(() => { ts5 = true; md5 = new DownloadOBGK(dw5, saveUrl5); md5.Start(); });
                task6.Start();
                checkBox5.Enabled = false;
                项目计数++;
            }
            else
            {
                if (File.Exists(bin64 + "\\d3d9_arcdps_mechanicschs.dll"))
                {
                    File.Delete(bin64 + "\\d3d9_arcdps_mechanicschs.dll");
                }
            }
            if (Properties.Settings.Default._6团恩)
            {
                var task7 = new Task(() => { ts6 = true; md6 = new DownloadOBGK(dw6, saveUrl6); md6.Start(); });
                task7.Start();
                checkBox6.Enabled = false;
                项目计数++;
            }
            else
            {
                if (File.Exists(bin64 + "\\d3d9_arcdps_tablechs.dll"))
                {
                    File.Delete(bin64 + "\\d3d9_arcdps_tablechs.dll");
                }
            }
            if (Properties.Settings.Default._7坐骑)
            {
                var task8 = new Task(() => { ts7 = true; md7 = new DownloadOBGK(dw7, saveUrl7); md7.Start(); });
                task8.Start();
                checkBox7.Enabled = false;
                项目计数++;
            }
            else
            {
                if (File.Exists(bin64 + "\\d3d9_chainload.dll"))
                {
                    File.Delete(bin64 + "\\d3d9_chainload.dll");
                }
            }
            if (Properties.Settings.Default._8汉滤)
            {
                var task9 = new Task(() => { ts8 = true; md8 = new DownloadOBGK(dw8, saveUrl8); md8.Start(); });
                task9.Start();
                checkBox8.Enabled = false;
                项目计数++;
            }
            else
            {
                string[] 所有文件名 = new string[3] { "\\ReShade64.dll","\\ReShade.ini", "\\d3d9_ReShade64.zip" };
                for (int i = 0; i < 所有文件名.Length; i++)
                {
                    if (File.Exists(bin64 + 所有文件名[i]))
                    {
                        File.Delete(bin64 + 所有文件名[i]);
                    }
                }
                string didi2 = bin64 + "\\reshade-shaders";
                if (Directory.Exists(didi2))
                {
                    删除目录(didi2);
                }
            }
            if (Properties.Settings.Default._9全滤)
            {
                var task10 = new Task(() => { ts9 = true; md9 = new DownloadOBGK(dw9, saveUrl9); md9.Start(); });
                task10.Start();
                checkBox9.Enabled = false;
                项目计数++;
            }
            else
            {
                string[] 所有文件名 = new string[6] {"\\d3d9_mchain.dll", "\\SweetFX readme.txt", "\\SweetFX_preset.txt","\\SweetFX_settings.txt","\\dxgi.dll", "\\SweetFX.zip" };
                for (int i = 0; i < 所有文件名.Length; i++)
                {
                    if (File.Exists(bin64 + 所有文件名[i]))
                    {
                        File.Delete(bin64 + 所有文件名[i]);
                    }
                }
                string didi1 = bin64 + "\\SweetFX";
                if (Directory.Exists(didi1))
                {
                    删除目录(didi1);
                }
            }

        }

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
                //throw;
            }
        }
        private void 按钮点击事件(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            switch (int.Parse(check.Tag.ToString()))
            {
                case 1:
                    //Properties.Settings.Default._1程序 = check.Checked;
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
                    }
                    else
                    {
                        checkBox5.Enabled = true;
                        checkBox6.Enabled = true;
                    }
                    break;
                case 5:
                    Properties.Settings.Default._5团力 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox4.Enabled = false;
                    }
                    else
                    {
                        checkBox4.Enabled = true;
                    }
                    break;
                case 6:
                    Properties.Settings.Default._6团恩 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox4.Enabled = false;
                    }
                    else
                    {
                        checkBox4.Enabled = true;
                    }
                    break;
                case 7:
                    Properties.Settings.Default._7坐骑 = check.Checked;
                    break;
                case 8:
                    Properties.Settings.Default._8汉滤 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox9.Enabled = false;
                    }
                    else
                    {
                        checkBox9.Enabled = true;
                        if (File.Exists(bin64 + "\\ReShade64.dll"))
                        {
                            File.Delete(bin64 + "\\ReShade64.dll");
                        }
                        string didi3 = bin64 + "\\reshade-shaders";
                        if (Directory.Exists(didi3))
                        {
                            删除目录(didi3);//ReShade.ini
                        }
                        if (File.Exists(bin64 + "\\ReShade.ini"))
                        {
                            File.Delete(bin64 + "\\ReShade.ini");
                        }
                    }
                    break;
                case 9:
                    Properties.Settings.Default._9全滤 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox8.Enabled = false;
                    }
                    else
                    {
                        checkBox8.Enabled = true;
                        string didi4 = bin64 + "\\SweetFX";
                        if (Directory.Exists(didi4))
                        {
                            删除目录(didi4);
                        }
                        if (File.Exists(bin64 + "\\dxgi.dll"))
                        {
                            File.Delete(bin64 + "\\dxgi.dll");
                        }
                        if (File.Exists(bin64 + "\\d3d9_mchain.dll"))
                        {
                            File.Delete(bin64 + "\\d3d9_mchain.dll");
                        }
                        if (File.Exists(bin64 + "\\SweetFX readme.txt"))
                        {
                            File.Delete(bin64 + "\\SweetFX readme.txt");
                        }
                        if (File.Exists(bin64 + "\\SweetFX_preset.txt"))
                        {
                            File.Delete(bin64 + "\\SweetFX_preset.txt");
                        }
                        if (File.Exists(bin64 + "\\SweetFX_settings.txt"))
                        {
                            File.Delete(bin64 + "\\SweetFX_settings.txt");
                        }
                    }
                    break;
                case 10:
                    Properties.Settings.Default._自动更新 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox12.Enabled = true;
                    }
                    break;
                case 11:
                    Properties.Settings.Default._自动启动 = check.Checked;
                    break;
                case 12:
                    Properties.Settings.Default._跳过更新 = check.Checked;
                    break;
                case 13:
                    Properties.Settings.Default._附加命令 = check.Checked;
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
        private void Button2_Click(object sender, EventArgs e)
        {
            Process.Start(官方网址);
        }
        private void 卸载()
        {
            string[] 所有文件名 = new string[16]
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
            "\\dxgi.dll",
            "\\d3d9_ReShade64.zip",
            "\\SweetFX.zip"
        };
            for (int i = 0; i < 所有文件名.Length; i++)
            {
                if (File.Exists(bin64 + 所有文件名[i]))
                {
                    File.Delete(bin64 + 所有文件名[i]);
                }
            }
            for (int i = 0; i < 所有文件名.Length; i++)
            {
                if (File.Exists(目录 + 所有文件名[i]))
                {
                    File.Delete(目录 + 所有文件名[i]);
                }
            }
            string didi1 = bin64 + "\\SweetFX";
            if (Directory.Exists(didi1))
            {
                删除目录(didi1);
            }
            string didi2 = bin64 + "\\reshade-shaders";
            if (Directory.Exists(didi2))
            {
                删除目录(didi2);
            }
            textBox1.AppendText("bin64文件夹下插件文件删除完成!\r\n");
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            卸载();
        }
        public void 启动()
        {
            string 启动代码 = "-maploadinfo";
            if (!Properties.Settings.Default._附加命令)
            {
                启动代码 = "";
            }
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
        private void Button4_Click(object sender, EventArgs e)
        {
            启动();
        }

        //说明
        private void Button5_Click(object sender, EventArgs e)
        {

        }

        private bool 解压文件(string 文件位置, string 解压路径, string 文件名)
        {
            bool a = false;
            textBox1.AppendText("开始解压:" + 文件名 + "\r\n");
            try
            {
                ZipFile.ExtractToDirectory(文件位置, 解压路径);
            }
            catch (Exception)
            {
                textBox1.AppendText(文件名 + "解压出错请尝试删除bin64文件夹下"+文件名+"再次下载\r\n");
            }
            
            textBox1.AppendText(文件名 + "解压过程完成\r\n");
            a = true;
            return a;
        }

        int timrr = 0;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Maximum = (int)md1.FileSize;
            progressBar1.Value = md1.DownloadSize;
            progressBar2.Maximum = (int)md2.FileSize;
            progressBar2.Value = md2.DownloadSize;
            progressBar3.Maximum = (int)md3.FileSize;
            progressBar3.Value = md3.DownloadSize;
            progressBar4.Maximum = (int)md4.FileSize;
            progressBar4.Value = md4.DownloadSize;
            progressBar5.Maximum = (int)md5.FileSize;
            progressBar5.Value = md5.DownloadSize;
            progressBar6.Maximum = (int)md6.FileSize;
            progressBar6.Value = md6.DownloadSize;
            progressBar11.Maximum = (int)md7.FileSize;
            progressBar11.Value = md7.DownloadSize;
            progressBar12.Maximum = (int)md8.FileSize;
            progressBar12.Value = md8.DownloadSize;
            progressBar10.Maximum = (int)md9.FileSize;
            progressBar10.Value = md9.DownloadSize;

            if (完成计数 !=项目计数 && 下载中)
            {

                if (md1.IsComplete && ts1)
                {
                    ts1 = false;
                    完成计数++;
                    checkBox1.Enabled = true;
                }
                if (md11.IsComplete && ts11)
                {
                    ts11 = false;
                    完成计数++;
                }
                if (md2.IsComplete && ts2)
                {
                    ts2 = false;
                    完成计数++;
                    checkBox2.Enabled = true;
                }
                if (md3.IsComplete && ts3)
                {
                    ts3 = false;
                    完成计数++;
                    checkBox3.Enabled = true;
                }
                if (md4.IsComplete && ts4)
                {
                    ts4 = false;
                    完成计数++;
                    checkBox4.Enabled = true;
                }
                if (md5.IsComplete && ts5)
                {
                    ts5 = false;
                    完成计数++;
                    checkBox5.Enabled = true;
                }
                if (md6.IsComplete && ts6)
                {
                    ts6 = false;
                    完成计数++;
                    checkBox6.Enabled = true;
                }
                if (md7.IsComplete && ts7)
                {
                    ts7 = false;
                    完成计数++;
                    checkBox7.Enabled = true;
                }
                if (md8.IsComplete && ts8)
                {
                    ts8 = false;
                    完成计数++;
                    checkBox8.Enabled = true;
                }
                if (md9.IsComplete && ts9)
                {
                    ts9 = false;
                    完成计数++;
                    checkBox9.Enabled = true;
                }
            }
            else
            {
                
                if (完成计数 == 项目计数 && 项目计数 != 0)
                {
                    //textBox1.AppendText(完成计数.ToString()+"-"+ 项目计数.ToString() + "\r\n");
                    下载中 = false;
                    完成计数 = 0;
                    项目计数 = 0;
                    if (Properties.Settings.Default._8汉滤)
                    {
                        if (!File.Exists(bin64 + "\\ReShade64.dll"))
                        {
                            if (File.Exists(bin64 + "\\d3d9_ReShade64.zip"))
                            {
                                解压文件(bin64 + "\\d3d9_ReShade64.zip", bin64, "ReShade64滤镜");
                            }

                        }

                    }
                    if (Properties.Settings.Default._9全滤)
                    {
                        if (!File.Exists(bin64 + "\\dxgi.dll"))
                        {
                            if (File.Exists(bin64 + "\\SweetFX.zip"))
                            {
                                解压文件(bin64 + "\\SweetFX.zip", bin64, "SweetFX滤镜");
                            }
                        }

                    }
                    textBox1.AppendText("全部项目完成,开始游戏吧!\r\n");

                    完成 = true;

                }
                if (Properties.Settings.Default._自动启动&& 完成)
                {
                    if (timrr > 30)
                    {
                        启动();
                    }
                    else
                    {
                        if (timrr ==10)
                        {
                            textBox1.AppendText("启动倒计时2...\r\n");
                        }
                        if (timrr == 20)
                        {
                            textBox1.AppendText("启动倒计时1...\r\n");
                        }
                        if (timrr==30)
                        {
                            textBox1.AppendText("启动倒计时0...\r\n");
                        }
                    }
                    timrr++;
                }
            }


            //if (md.IsComplete && ts1)
            //{
            //    ts1 = false;
            //    GC.Collect();
            //}
            //if (md2.IsComplete && ts2)
            //{
            //    ts2 = false;
            //    GC.Collect();
            //}
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //numericUpDown
            //NumericUpDown numericUpDown = (NumericUpDown)sender;
            //Properties.Settings.Default.thnum = (int)numericUpDown.Value;
            //Properties.Settings.Default.Save();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(bin64);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {

                    }
                    else
                    {
                        
                        if (i.Extension ==".tmp")
                        {
                            File.Delete(i.FullName);      //删除指定文件
                        }
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }
    }
}
