using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlugIn_UpdateTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (!Properties.Settings.Default.首次运行检测)
            {
                Testui testui = new Testui();
                Controls.Add(testui);
                //testui.BringToFront();
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private 设置完成 shes;
        private 开始下载 开始;
        private Func<bool> 完成;
        //private 下载完成 完成;
        private bool 下载中 = false;
        private bool 设置完成_ = false;
        private int 项目数 = 0;
        private int 完成个数 = 0;
        private void Button2_Click(object sender, EventArgs e)
        {
            Settingui settingui = new Settingui();
            Controls.Add(settingui);
            settingui.BringToFront();
            shes = settingui.完成;
            if (backgroundWorker1.IsBusy)
            {
                return;
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
            }

        }
        //启动
        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!下载中)
            {
                System.Threading.Thread.Sleep(10);
                shes(ref 设置完成_);
                if (设置完成_)
                {
                    break;
                }
            }

        }
        //更新
        private void Button1_Click(object sender, EventArgs e)
        {
            下载中 = true;
            按钮开关(0);
            开始();
        }

        private void 按钮开关(int 开关状态)
        {
            switch (开关状态)
            {
                case 0:
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    break;
                case 1:
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private int 多线程()
        {
            if (Properties.Settings.Default.多线程下载)
            {
                return Properties.Settings.Default.多线程数;
            }
            else
            {
                return 1;
            }
        }


        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            开始 = null;
            完成 = null;
            项目数 = 0;
            完成个数 = 0;
            flowLayoutPanel1.Controls.Clear();
            if (Properties.Settings.Default.主程序)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("主程序", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                //汉化文本
                Jiheui settingui2 = new Jiheui();
                settingui2.赋值("汉化文本", 1);
                开始 += settingui2.更新;
            }
            if (Properties.Settings.Default.db切换)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("DB切换", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.附加功能)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("附加功能", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.流动输出)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("流动输出", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.团队力学)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("团队力学", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.团队恩赐)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("团队恩赐", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.坐骑插件)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("坐骑插件", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.dx12)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("DX9TO12", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.r滤镜)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("ReShade滤镜", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.s滤镜)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("Sweet滤镜", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
            }
            项目数 = 开始.GetInvocationList().Count();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (下载中)
            {
                foreach (Func<bool> dd in 完成.GetInvocationList())
                {
                    if (dd())
                    {
                        完成个数++;
                        if ((项目数 == 完成个数) && (完成个数 > 0))
                        {
                            //Console.WriteLine(完成个数);
                            下载中 = false;
                            完成个数 = 0;
                            按钮开关(1);
                        }
                    }
                }
            }
        }
    }
}
