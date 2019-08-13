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

        private readonly 设置完成[] shes = new 设置完成[1];
        public bool 下载中 = false;
        public bool 设置完成_ = false;
        private void Button2_Click(object sender, EventArgs e)
        {
            Settingui settingui = new Settingui();
            Controls.Add(settingui);
            settingui.BringToFront();
            shes[0] = settingui.完成;
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
                shes[0](ref 设置完成_);
                if (设置完成_)
                {
                    break;
                }
            }

        }
        //更新
        private void Button1_Click(object sender, EventArgs e)
        {

            
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            if (Properties.Settings.Default.主程序)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("主程序");
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.db切换)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("DB切换");
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.附加功能)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("附加功能");
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.流动输出)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("流动输出");
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.团队力学)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("团队力学");
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.团队恩赐)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("团队恩赐");
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.坐骑插件)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("坐骑插件");
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.dx12)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("DX9TO12");
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.r滤镜)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("ReShade滤镜");
                flowLayoutPanel1.Controls.Add(settingui);
            }
            if (Properties.Settings.Default.s滤镜)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("Sweet滤镜");
                flowLayoutPanel1.Controls.Add(settingui);
            }
        }
    }
}
