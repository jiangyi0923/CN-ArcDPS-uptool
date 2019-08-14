using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlugIn_UpdateTool
{
    public delegate bool 下载完成();
    public delegate void 开始下载();
    public partial class Jiheui : UserControl
    {
        public Jiheui()
        {
            InitializeComponent();
        }
        private LogClass log = new LogClass();
        private bool 完成 = false;
        private int 多线程 = 1;
        private string 下载地址 = "";
        private string 文件名 = "";
        private string 储存位置 = "";
        public void 赋值(string 标签,int _多线程)
        {
            label1.Text = 标签;
            多线程 = _多线程;
            log.WriteLogFile("赋值"+ 标签+ " 线程数"+_多线程);
        }
        public bool 下载完成__()
        {
            
            return 完成;
        }
        public void 更新()
        {
            完成 = false;
            switch (label1.Text)
            {
                case "主程序":
                    下载地址 = "主程序";
                    文件名 = "121.dll";
                    储存位置 = "cpan";
                    break;
                //汉化文本
                case "汉化文本":
                    下载地址 = "汉化文本";
                    文件名 = "121.dll";
                    储存位置 = "cpan";
                    break;
                case "DB切换":
                    下载地址 = "DB切换";
                    文件名 = "121.dll";
                    储存位置 = "cpan";
                    break;
                case "附加功能":
                    下载地址 = "附加功能";
                    文件名 = "121.dll";
                    储存位置 = "cpan";
                    break;
                case "流动输出":
                    下载地址 = "流动输出";
                    文件名 = "121.dll";
                    储存位置 = "cpan";
                    break;
                case "团队力学":
                    下载地址 = "团队力学";
                    文件名 = "121.dll";
                    储存位置 = "cpan";
                    break;
                case "团队恩赐":
                    下载地址 = "团队恩赐";
                    文件名 = "121.dll";
                    储存位置 = "cpan";
                    break;
                case "坐骑插件":
                    下载地址 = "坐骑插件";
                    文件名 = "121.dll";
                    储存位置 = "cpan";
                    break;
                case "DX9TO12":
                    下载地址 = "DX9TO12";
                    文件名 = "121.dll";
                    储存位置 = "cpan";
                    break;
                case "ReShade滤镜":
                    下载地址 = "ReShade滤镜";
                    文件名 = "121.dll";
                    储存位置 = "cpan";
                    break;
                case "Sweet滤镜":
                    下载地址 = "Sweet滤镜";
                    文件名 = "121.dll";
                    储存位置 = "cpan";
                    break;
                default:
                    break;
            }
            下载();
        }
        private void 下载()
        {
            label2.Text= "Test:" + 下载地址 + "--" + 文件名 + "--" + 储存位置;
            完成 = true;
            log.WriteLogFile(label1.Text + " 完成");
        }

    }
}
