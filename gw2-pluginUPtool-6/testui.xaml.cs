using Microsoft.Win32;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace gw2_pluginUPtool_6
{
    /// <summary>
    /// testui.xaml 的交互逻辑
    /// </summary>
    public partial class testui : UserControl
    {
        public string 本地路劲 = Directory.GetCurrentDirectory();
        public string 插件ad= Directory.GetCurrentDirectory() + "\\addons";
        public string 插件路劲 = Directory.GetCurrentDirectory() + "\\addons\\arcdps";
        public string 插件路劲B = Directory.GetCurrentDirectory() + "\\addons\\sct";
        public testui()
        {
            InitializeComponent();
            B_1.IsEnabled = false;
        }
        public Grid Home { get; set; }
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.首次运行检测_ = true;
            Properties.Settings.Default.Save();
            Home.Children.Remove(this);
        }



        private void 解压当前文件(string File_, string appPath)
        {
            using (ZipArchive archive = ZipFile.Open(File_, ZipArchiveMode.Read, Encoding.Default))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string fullPath = Path.Combine(appPath, entry.FullName);
                    if (String.IsNullOrEmpty(entry.Name))
                    {
                        Directory.CreateDirectory(fullPath);
                    }
                    else
                    {
                        entry.ExtractToFile(fullPath, true);
                    }
                }
            }

        }



        /// <summary>
        /// 重新检测按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            检测();
        }

        bool DX90 = false;
        bool VC2013 = false;
        bool VC2019 = false;



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
                if (!File.Exists(插件ad + "\\peizi.zip"))
                {
                    byte[] Save1 = Properties.Resources.peizi;
                    FileStream fsObj1 = new FileStream(插件ad + "\\peizi.zip", FileMode.CreateNew);
                    fsObj1.Write(Save1, 0, Save1.Length);
                    fsObj1.Close();
                    解压当前文件(插件ad + "\\peizi.zip", 插件ad);
                }
                
                if (!Directory.Exists(插件路劲B))
                {
                    Directory.CreateDirectory(插件路劲B);
                    Directory.CreateDirectory(插件路劲B + "\\fonts");
                }
                File.Copy(插件ad+ "\\arcdps_font.ttf", 插件路劲 + "\\arcdps_font.ttf", true);
                File.Copy(插件ad + "\\arcdps.ini", 插件路劲 + "\\arcdps.ini", true);
                File.Copy(插件ad + "\\arcdps_font.ttf", 插件路劲B + "\\fonts\\arcdps_font.ttf", true);
                File.Copy(插件ad + "\\sct.ini", 插件路劲B + "\\sct.ini", true);
                File.Copy(插件ad + "\\lang.ini", 插件路劲B + "\\lang.ini", true);
            }
            else if (aii == 2)
            {
                if (!Directory.Exists(插件路劲B))
                {
                    Directory.CreateDirectory(插件路劲B);
                    Directory.CreateDirectory(插件路劲B + "\\fonts");
                }
                if (!File.Exists(插件ad + "\\peizi.zip"))
                {
                    byte[] Save1 = Properties.Resources.peizi;
                    FileStream fsObj1 = new FileStream(插件ad + "\\peizi.zip", FileMode.CreateNew);
                    fsObj1.Write(Save1, 0, Save1.Length);
                    fsObj1.Close();
                    解压当前文件(插件ad + "\\peizi.zip", 插件ad);
                }
                if (!File.Exists(插件路劲 + "\\arcdps_font.ttf"))
                {
                    File.Copy(插件ad + "\\arcdps_font.ttf", 插件路劲 + "\\arcdps_font.ttf", true);
                }
                if (!File.Exists(插件路劲 + "\\arcdps.ini"))
                {
                    File.Copy(插件ad + "\\arcdps.ini", 插件路劲 + "\\arcdps.ini", true);
                }
                if (!File.Exists(插件路劲B + "\\fonts\\arcdps_font.ttf"))
                {
                    File.Copy(插件ad + "\\arcdps_font.ttf", 插件路劲B + "\\fonts\\arcdps_font.ttf", true);
                }
                if (!File.Exists(插件路劲B + "\\sct.ini"))
                {
                    File.Copy(插件ad + "\\sct.ini", 插件路劲B + "\\sct.ini", true);
                }
                if (!File.Exists(插件路劲B + "\\lang.ini"))
                {
                    File.Copy(插件ad + "\\lang.ini", 插件路劲B + "\\lang.ini", true);
                }
            }
            File.Delete(插件ad + "\\arcdps_font.ttf");
            File.Delete(插件ad + "\\arcdps.ini");
            File.Delete(插件ad + "\\sct.ini");
            File.Delete(插件ad + "\\lang.ini");
            File.Delete(插件ad + "\\peizi.zip");
        }

        private void 检测()
        {
            //检测目录名是否正确
            int mul = 0;
            int jishu = 0;
            if (Regex.IsMatch(Directory.GetCurrentDirectory(), @"[\u4e00-\u9fa5]") == false)
            {
                jishu++;
                mul++;
                M_d.IsChecked = true;
                M_d.Content = "目录名检测: 无中文";
                //L1.Content = Directory.GetCurrentDirectory();
                TX1.Text = Directory.GetCurrentDirectory();
                TX1.Foreground = M_d.Foreground = Brushes.Green;
            }
            else
            {
                M_d.IsChecked = false;
                M_d.Content = "目录名检测: 有中文";
                TX1.Text = Directory.GetCurrentDirectory();
                //L1.Content = Directory.GetCurrentDirectory();
                TX1.Foreground = M_d.Foreground = Brushes.Red;
            }
            
            //检测程序是否正确
            string path = @"./Gw2-64.exe";
            if (File.Exists(path))
            {
                jishu++;
                mul++;
                M_f.IsChecked = true;
                M_f.Content = "程序名检测: 完成";
                L2.Content = "√有Gw2-64.exe";
                L2.Foreground = M_f.Foreground = Brushes.Green;
            }
            else
            {
                M_f.IsChecked = false;
                M_f.Content = "程序名检测: 失败";
                L2.Content = "×无Gw2-64.exe,请关闭程序更改游戏程序名";
                L2.Foreground = M_f.Foreground = Brushes.Red;
            }
            //如果上面两个正确 - 解压配置和字体 否则 跳过
            if (mul == 2)
            {
                初次运行();
            }

            CheckCryptoKit();
            //检测dx运行库
            //如果拥有 按钮2关闭;否则开启
            if (DX90)
            {
                jishu++;
                M_dx.IsChecked = true;
                M_dx.Foreground = Brushes.Green;
                M_dx.Content = "DX9.0c已安装";
            }
            else
            {
                M_dx.IsChecked = false;
                M_dx.Foreground = Brushes.Red;
                M_dx.Content = "DX9.0c未安装,请点击安装";
            }
            //检测2013 
            //如果拥有 按钮3关闭;否则开启
            if (VC2013)
            {
                jishu++;
                M_3.IsChecked = true;
                M_3.Foreground = Brushes.Green;
                M_3.Content = "VC++2013已安装";
            }
            else
            {
                M_3.IsChecked = false;
                M_3.Foreground = Brushes.Red;
                M_3.Content = "VC++2013未安装,请点击安装";
            }
            //检测2015-2019
            //如果拥有 按钮4关闭;否则开启
            if (VC2019)
            {
                jishu++;
                M_59.IsChecked = true;
                M_59.Foreground = Brushes.Green;
                M_59.Content = "VC++2015-2019已安装";
                
            }
            else
            {
                M_59.IsChecked = false;
                M_59.Foreground = Brushes.Red;
                M_59.Content = "VC++2015-2019未安装,请点击安装";
            }
            //如果上面都正确 开启按钮1 否则呵呵
            if (jishu == 5)
            {
                B_2.IsEnabled = false;
                B_1.IsEnabled = true;
            }
            else
            {
                B_2.IsEnabled = true;
            }
            System.Windows.Forms.MessageBox.Show(
                "因为微软VC++2015,VC++2017,VC++2019共用一个文件,\r\n" +
                "导致VC++2015-2019的检测不一定准确,\r\n" +
                "如果检测失败或进游戏报错请自行查看系统\"应用与程序\"列表中\r\n" +
                "是否存在\"Microsoft Visual C++ 2015-2019\"\r\n" +
                "如果存在单独的2015,2017,2019请卸载后安装本工具提供的VC2015-2019\r\n" +
                "或者百度下载最新版本的VC2015-2019运行库\r\n" +
                "(本条提醒每次检测都会出现)");
        }
        /// <summary>
        /// 2013
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_3_Click(object sender, RoutedEventArgs e)
        {
            string 官方网址 = "http://dl.gw2sy.top/vcredist_x64.exe";
            //log.WriteLogFile("打开了官网");
            Process.Start(官方网址);
        }

        public void CheckCryptoKit()
        {
            String[] softwareList = null;
            ArrayList list = new ArrayList();

            //从注册表中获取控制面板“卸载程序”中的程序和功能列表
            RegistryKey Key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            if (Key != null) //如果系统禁止访问则返回null
            {
                foreach (String SubKeyName in Key.GetSubKeyNames())
                {
                    //打开对应的软件名称
                    RegistryKey SubKey = Key.OpenSubKey(SubKeyName);
                    if (SubKey != null)
                    {
                        String SoftwareName = SubKey.GetValue("DisplayName", "Nothing").ToString();
                        //如果没有取到，则不存入动态数组
                        if (SoftwareName != "Nothing")
                        {
                            list.Add(SoftwareName);
                        }
                    }
                }
                //强制转换成字符串数组，防止被修改数据溢出
                softwareList = (string[])list.ToArray(typeof(string));
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("获取程序列表失败,请关闭程序尝试使用管理员模式打开!");
            }

            //判断有无找到驱动中要找的关键字，有则返回true，无则返回false
            foreach (string software in softwareList)
            {
                if (software.IndexOf("Microsoft Visual C++ 2019 X64") > -1)
                {
                    VC2019 = true;
                }
                if (software.IndexOf("Microsoft Visual C++ 2013 x64") > -1)
                {
                    VC2013 = true;
                }
                if (software.IndexOf("DirectX") > -1)
                {
                    DX90 = true;
                }
            }
            if (!DX90)
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "//D3DX9_43.dll"))
                {
                    DX90 = true;
                }
            }
        }



            /// <summary>
            /// 2019
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        private void B_59_Click(object sender, RoutedEventArgs e)
        {
            string 官方网址 = "http://dl.gw2sy.top/vc_redist.x64.exe";
            Process.Start(官方网址);
        }
        /// <summary>
        /// dx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_dx_Click(object sender, RoutedEventArgs e)
        {
            string 官方网址 = "http://dl.gw2sy.top/dxwebsetup.exe";
            Process.Start(官方网址);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            检测();
        }
    }
}
