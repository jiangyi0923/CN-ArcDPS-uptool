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
                testui.BringToFront();
                log.WriteLogFile("首次使用");
            }
            else
            {
                Mgui mgui = new Mgui();
                if (mgui.有新版本() || mgui.有新提醒())
                {
                    Controls.Add(mgui);
                    mgui.BringToFront();
                }
            }
            log.WriteLogFile("载入部件");
            backgroundWorker1.RunWorkerAsync();
        }

        private readonly LogClass log =new LogClass();
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
                log.WriteLogFile("设置菜单已打开");
                return;
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
                log.WriteLogFile("打开了设置菜单");
            }

        }
        //启动
        private void Button3_Click(object sender, EventArgs e)
        {
            log.WriteLogFile("启动游戏");
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!下载中)
            {
                System.Threading.Thread.Sleep(10);
                shes(ref 设置完成_);
                if (设置完成_)
                {
                    log.WriteLogFile("设置界面关闭");
                    break;
                }
            }

        }
        //更新
        private void Button1_Click(object sender, EventArgs e)
        {
            下载中 = true;
            按钮开关(0);
            log.WriteLogFile("开始所有项目,关闭主界面按钮");
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
                    log.WriteLogFile("主界面界面按钮关闭");
                    break;
                case 1:
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    log.WriteLogFile("主界面界面按钮打开");
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
                log.WriteLogFile("创建了 主程序 控件");
                //汉化文本
                Jiheui settingui2 = new Jiheui();
                settingui2.赋值("汉化文本", 1);
                开始 += settingui2.更新;
                完成 += settingui2.下载完成__;
            }
            if (Properties.Settings.Default.db切换)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("DB切换", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                log.WriteLogFile("创建了 DB切换 控件");
            }
            if (Properties.Settings.Default.附加功能)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("附加功能", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                log.WriteLogFile("创建了 附加功能 控件");
            }
            if (Properties.Settings.Default.流动输出)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("流动输出", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                log.WriteLogFile("创建了 流动输出 控件");
            }
            if (Properties.Settings.Default.团队力学)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("团队力学", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                log.WriteLogFile("创建了 团队力学 控件");
            }
            if (Properties.Settings.Default.团队恩赐)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("团队恩赐", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                log.WriteLogFile("创建了 团队恩赐 控件");
            }
            if (Properties.Settings.Default.坐骑插件)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("坐骑插件", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                log.WriteLogFile("创建了 坐骑插件 控件");
            }
            if (Properties.Settings.Default.dx12)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("DX9TO12", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                log.WriteLogFile("创建了 DX9TO12 控件");
            }
            if (Properties.Settings.Default.r滤镜)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("ReShade滤镜", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                log.WriteLogFile("创建了 ReShade滤镜 控件");
            }
            if (Properties.Settings.Default.s滤镜)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("Sweet滤镜", 多线程());
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                log.WriteLogFile("创建了 Sweet滤镜 控件");
            }
            项目数 = 开始.GetInvocationList().Count();
            log.WriteLogFile("项目数:"+ 项目数);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            
            if (下载中)
            {
                完成个数 = 0;
                foreach (Func<bool> dd in 完成.GetInvocationList())
                {
                    if (dd())
                    {
                        完成个数++;
                        //Console.WriteLine(完成个数+"/"+项目数);
                        if ((项目数 == 完成个数) && (完成个数 > 0))
                        {
                            log.WriteLogFile("所有项目完成,重置必要参数,开启主界面按钮");
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
