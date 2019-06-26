using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcDPS_uptool
{
    public partial class Form1 : Form  
    {

        public string 目录 = Application.StartupPath;
        public string bin64 = Application.StartupPath + "//bin64";
        
        public string dw1 = Application.StartupPath + "//bin64";//主程序
        public string dw11 = Application.StartupPath + "//bin64";//汉化文件
        public string dw2 = Application.StartupPath + "//bin64";//db切换
        public string dw3 = Application.StartupPath + "//bin64";//附加功能
        public string dw4 = Application.StartupPath + "//bin64";//sct
        public string dw5 = Application.StartupPath + "//bin64";//团力
        public string dw6 = Application.StartupPath + "//bin64";//团恩
        public string dw7 = Application.StartupPath + "//bin64";//坐骑
        public string dw8 = Application.StartupPath + "//bin64";//汉化滤镜
        public string dw9 = Application.StartupPath + "//bin64";//自动滤镜

        
        DownloadOBGK md = new DownloadOBGK();
        DownloadOBGK md2 = new DownloadOBGK();
        bool ts1 = false;
        bool ts2 = false;
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            mainfrm = this;
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
            多选框检测();

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

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            //string httpUrl = @"https://www.deltaconnected.com/arcdps/x64/d3d9.dll";
            string httpUrl2 = @"https://www.deltaconnected.com/arcdps/x64/buildtemplates/d3d9_arcdps_buildtemplates.dll";
            //string saveUrl = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + System.IO.Path.GetFileName(httpUrl);
            string saveUrl2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + System.IO.Path.GetFileName(httpUrl2);
            ////System
            //var task = new Task(() =>
            //{
            //    ts1 = true;
            //    md = new DownloadOBGK(httpUrl, saveUrl);
            //    md.Start();
            //});
            //task.Start();
            var task2 = new Task(() =>
            {
                ts2 = true;
                md2 = new DownloadOBGK(httpUrl2, saveUrl2);
                md2.Start();
            });
            task2.Start();

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            //progressBar1.Maximum = (int)md.FileSize;
            //progressBar1.Value = md.DownloadSize;
            progressBar2.Maximum = (int)md2.FileSize;
            progressBar2.Value = md2.DownloadSize;

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
    }
}
