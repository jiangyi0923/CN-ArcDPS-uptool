using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace PlugIn_UpdateTool
{
    public partial class Mgui : UserControl
    {
        public Mgui()
        {
            InitializeComponent();
        }
        private readonly string 版本检测网址 = "http://gw2sy.top/wp-content/uploads/1.txt";
        private readonly string 更新说明 = "http://gw2sy.top/wp-content/uploads/2.txt";
        private readonly string 信息检测网址 = "http://gw2sy.top/wp-content/uploads/11.txt";
        private readonly string 信息说明 = "http://gw2sy.top/wp-content/uploads/22.txt";
        //private readonly LogClass log = new LogClass();
        private void Button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        public bool 有新版本()
        {
            bool a = false;
            int 最新版本 = 版本();
            textBox1.AppendText("当前版本:V" + Application.ProductVersion + "\r\n");
            if (最新版本 == 0)
            {
                ////log.WriteLogFile("获取最新版本信息失败,官网暂时无法连接");
                textBox1.Text = "获取最新版本说明失败,官网暂时无法连接";
            }
            int.TryParse(Application.ProductVersion, out int 本地版本);
            if (本地版本 < 最新版本)
            {
                string 说明文档 = 说明();
                a = true;
                if (说明文档 == "")
                {
                    ////log.WriteLogFile("获取最新版本说明失败,官网暂时无法连接");
                    textBox1.Text += "获取最新版本说明失败,官网暂时无法连接\r\n";
                }
                else
                {
                    textBox1.Text += 说明文档;
                }
                label1.Text = "有最新版本V" + 最新版本.ToString();
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
                    textBox1.Text = 信息说明文档;
                }
                label1.Text = "重要提醒!!";
                label1.ForeColor = Color.Red;
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
    }
}
