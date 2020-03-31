using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace gw2_pluginUPtool_6
{
    /// <summary>
    /// masgessui.xaml 的交互逻辑
    /// </summary>
    public partial class Masgessui : UserControl
    {
        public Masgessui()
        {
            InitializeComponent();
        }
        public Grid Home { get; set; }
        //public Grid Home { get; set; }
        public TextBox Box_ { get { return textBox1; } }

        public bool 有新版本() 
        {
            return 新版本;
        }

        public bool 有新提醒()
        {
            return 新提醒;
        }

        public int 卸载按钮数值 { get; set; } = 0;
        
        private readonly string bin64 = Directory.GetCurrentDirectory() + "//bin64";
        private readonly string 目录 = Directory.GetCurrentDirectory();
        private readonly string 信息检测网址 = "http://gw2sy.top/wp-content/uploads/mgs.txt";
        private int 本地版本 = 0;
        private int 最新版本 = 0;
        private bool 新版本 = false;
        private bool 新提醒 = false;
        private int 最新信息检测 = 0;
        private string 版本说明 = "";
        private string 信息说明 = "";


        private void Gotu_Click(object sender, RoutedEventArgs e)
        {
            string 官方网址 = "https://gw2sy.top/wp-content/uploads/GW2-Plug-Updatetool.exe";
            Process.Start(官方网址);
        }


        public void Showtext(int dts)
        {
            switch (dts)
            {
                case 0:
                    if (版本说明 == "")
                    {
                        textBox1.Text += "获取最新版本说明失败,官网暂时无法连接\r\n";
                    }
                    else
                    {
                        textBox1.AppendText("当前版本:V" + 本地版本 + "\r\n");
                        string[] 分段 = 版本说明.Split('&');
                        for (int i = 0; i < 分段.Length; i++)
                        {
                            textBox1.AppendText(分段[i] + "\r\n");
                        }
                        buttonquxi.Content = "前往下载";
                        buttonquxi.Click += Gotu_Click;
                    }
                    break;
                case 1:
                    if (信息说明 == "")
                    {
                        textBox1.Text = "获取最新提醒信息失败,官网暂时无法连接\r\n";
                    }
                    else
                    {
                        label1.Content = "重要提醒!!";
                        if (最新信息检测 == 2)
                        {
                            label1.Foreground = Brushes.Green;
                        }
                        if (最新信息检测 == 3)
                        {
                            label1.Foreground = Brushes.Red;
                        }
                        string[] 分段 = 信息说明.Split('&');
                        
                        for (int i = 0; i < 分段.Length; i++)
                        {
                            textBox1.AppendText(分段[i] + "\r\n");
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public int 检测说明() 
        {
            int a速度去;
            string 获取说明 = 获取信息说明();
            if (获取说明 != "aaaaa")
            {
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule.FileName);
                int.TryParse(myFileVersionInfo.FileVersion, out 本地版本);
                string[] 分段1 = 获取说明.Split('#');
                int.TryParse(分段1[0], out 最新版本);
                if (本地版本 < 最新版本)
                {
                    新版本 = true;
                    label1.Content = "有最新版本V" + 最新版本.ToString();
                }
                string[] 分段2 = 分段1[1].Split('*');
                版本说明 = 分段2[0];
                string[] 分段3 = 分段2[1].Split('@');
                int.TryParse(分段3[0], out 最新信息检测);
                if (最新信息检测 > 0)
                {
                    新提醒 = true;
                }
                string[] 分段4 = 分段3[1].Split('%');
                信息说明 = 分段4[0];
                a速度去 = 1;
            }
            else
            {
                textBox1.Text = "获取最新信息失败,官网暂时无法连接\r\n";
                a速度去 = 2;
            }
            return a速度去;
        }

        private string 获取信息说明()
        {
            string a;
            try
            {
                var wc = new WebClient();
                string html = wc.DownloadString(信息检测网址);
                a = html;
                wc.Dispose();
            }
            catch (Exception)
            {
                a = "aaaaa";
            }
            return a;
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (卸载按钮数值 == 1)
            {
                卸载插件();
            }
            else
            {
                Home.Children.Remove(this);
            }
        }

        private void Buttonquxi_Click(object sender, RoutedEventArgs e)
        {
            Home.Children.Remove(this);
        }

        private void 卸载插件()
        {
            string[] 所有文件名 = new string[26]
            {   "d3d9.dll",
                "d3d9_arcdps_buildtemplates.dll",
                "d3d9_arcdps_extras.dll",
                "d3d9_arcdps_mechanicschs.dll",
                "d3d9_arcdps_buildpad.dll",
                "d3d9_arcdps_ct.dll",
                "d3d9_chainload.dll",
                "d3d9_arcdps_tablechs.dll",
                "d3d9_arcdps_sct.dll",
                "ReShade64.dll",
                "ReShade.ini",
                "DefaultPreset.ini",
                "d3d9_mchain.dll",
                "SweetFX readme.txt",
                "SweetFX_preset.txt",
                "SweetFX_settings.txt",
                "dxgi.dll",
                "d3d9_ReShade641.zip",
                "d912pxy.dll",
                "SweetFX.zip",
                "ReShade.fx",
                "Sweet.fx",
                "dxgi.log",
                "d3d9_mchain.log",
                "ReShade64.log",
                Properties.Settings.Default.dx12文件名
            };
            for (int i = 0; i < 所有文件名.Length; i++)
            {
                if (File.Exists(bin64 + "\\" + 所有文件名[i]))
                {
                    File.Delete(bin64 + "\\" + 所有文件名[i]);
                    textBox1.AppendText("删除" + 所有文件名[i] + "\r\n");
                }
            }
            for (int i = 0; i < 所有文件名.Length; i++)
            {
                if (File.Exists(目录 + "\\" + 所有文件名[i]))
                {
                    File.Delete(目录 + "\\" + 所有文件名[i]);
                    textBox1.AppendText("删除" + 所有文件名[i] + "\r\n");
                }
            }
            string didi1 = bin64 + "\\SweetFX";
            if (Directory.Exists(didi1))
            {
                删除目录(didi1);
                textBox1.AppendText("删除" + didi1 + "\r\n");
            }
            string didi2 = bin64 + "\\reshade-shaders";
            if (Directory.Exists(didi2))
            {
                删除目录(didi2);
                textBox1.AppendText("删除" + didi2 + "\r\n");
            }
            string didi3 = 目录 + "\\reshade-shaders";
            if (Directory.Exists(didi3))
            {
                删除目录(didi3);
                textBox1.AppendText("删除" + didi3 + "\r\n");
            }

            string didi4 = 目录 + "\\d912pxy";
            if (Directory.Exists(didi4))
            {
                删除目录(didi4);
                textBox1.AppendText("删除" + didi4 + "\r\n");
            }
            卸载按钮数值 = 0;
        }

        private static void 删除目录(string srcPath)
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
    }
}
