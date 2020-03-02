using System;
using System.Diagnostics;
using System.IO;
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
        public string 插件路劲 = Directory.GetCurrentDirectory() + "\\addons\\arcdps";
        public string 插件路劲B = Directory.GetCurrentDirectory() + "\\addons\\sct";
        public testui()
        {
            InitializeComponent();
            B_1.IsEnabled = false;
            检测();
        }
        public Grid Home { get; set; }
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.首次运行检测 = true;
            Properties.Settings.Default.Save();
            Home.Children.Remove(this);
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
                if (!Directory.Exists(插件路劲B))
                {
                    Directory.CreateDirectory(插件路劲B);
                    Directory.CreateDirectory(插件路劲B + "\\fonts");
                }

                byte[] Save1 = Properties.Resources.arcdps_font;
                FileStream fsObj1 = new FileStream(插件路劲 + "\\arcdps_font.ttf", FileMode.CreateNew);
                fsObj1.Write(Save1, 0, Save1.Length);
                fsObj1.Close();

                byte[] Save2 = Properties.Resources.arcdps_font;
                FileStream fsObj2 = new FileStream(插件路劲B + "\\fonts\\arcdps_font.ttf", FileMode.CreateNew);
                fsObj2.Write(Save2, 0, Save2.Length);
                fsObj2.Close();

                byte[] Save3 = Properties.Resources.sct;
                FileStream fsObj3 = new FileStream(插件路劲B + "\\sct.ini", FileMode.CreateNew);
                fsObj3.Write(Save3, 0, Save3.Length);
                fsObj3.Close();

                byte[] Save4 = Properties.Resources.lang;
                FileStream fsObj4 = new FileStream(插件路劲B + "\\lang.ini", FileMode.CreateNew);
                fsObj4.Write(Save4, 0, Save4.Length);
                fsObj4.Close();


            }
            else if (aii == 2)
            {
                if (!Directory.Exists(插件路劲B))
                {
                    Directory.CreateDirectory(插件路劲B);
                    Directory.CreateDirectory(插件路劲B + "\\fonts");
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
            //检测dx运行库
            //如果拥有 按钮2关闭;否则开启
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "//D3DX9_43.dll"))
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
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "//vccorlib120.dll"))
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
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "//vcamp140.dll"))
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
        /// <summary>
        /// 2019
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_59_Click(object sender, RoutedEventArgs e)
        {
            string 官方网址 = "http://dl.gw2sy.top/vc_redist.x64.exe";
            //log.WriteLogFile("打开了官网");
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
            //log.WriteLogFile("打开了官网");
            Process.Start(官方网址);
        }
    }
}
