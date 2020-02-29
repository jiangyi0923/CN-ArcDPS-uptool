using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        public TextBox Box_ { get; set; }


        private readonly string 版本检测网址 = "http://gw2sy.top/wp-content/uploads/1.txt";
        private readonly string 更新说明 = "http://gw2sy.top/wp-content/uploads/2.txt";
        private readonly string 信息检测网址 = "http://gw2sy.top/wp-content/uploads/11.txt";
        private readonly string 信息说明 = "http://gw2sy.top/wp-content/uploads/22.txt";

        public bool 有新版本()
        {
            bool a = false;
            int 最新版本 = 版本();

            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule.FileName);

            if (最新版本 == 0)
            {
                textBox1.AppendText("当前版本:V" + myFileVersionInfo.FileVersion + "\r\n");
                textBox1.Text = "获取最新版本说明失败,官网暂时无法连接";
            }
            int.TryParse(myFileVersionInfo.FileVersion, out int 本地版本);
            if (本地版本 < 最新版本)
            {
                textBox1.AppendText("当前版本:V" + myFileVersionInfo.FileVersion + "\r\n");
                string 说明文档 = 说明();
                a = true;
                if (说明文档 == "")
                {
                    textBox1.Text += "获取最新版本说明失败,官网暂时无法连接\r\n";
                }
                else
                {
                    string[] 分段 = 说明文档.Split('&');
                    for (int i = 0; i < 分段.Length; i++)
                    {
                        textBox1.AppendText(分段[i] + "\r\n");
                    }
                }
                label1.Content = "有最新版本V" + 最新版本.ToString();
            }//
            return a;
        }

        public bool 有新提醒()
        {
            bool a = false;
            int 最新信息检测 = 信息检测();
            if (最新信息检测 != 0)
            {
                a = true;
                string 信息说明文档 = 获取信息说明();
                if (信息说明文档 == "")
                {
                    ////log.WriteLogFile("获取最新提醒信息失败,官网暂时无法连接");
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
                    string[] 分段 = 信息说明文档.Split('&');
                    for (int i = 0; i < 分段.Length; i++)
                    {
                        textBox1.AppendText(分段[i] + "\r\n");
                    }
                }


            }
            return a;
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
                ////log.WriteLogFile("获取最新提醒失败,官网暂时无法连接");
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

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Home.Children.Remove(this);
            
        }
    }
}
