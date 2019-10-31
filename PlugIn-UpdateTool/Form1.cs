using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
            int.TryParse(Application.ProductVersion, out int 本地版本);
            this.Text = "激战2插件更新工具V5-" + 本地版本.ToString();
            if (!Properties.Settings.Default.首次运行检测)
            {
                Testui testui = new Testui();
                Controls.Add(testui);
                testui.BringToFront();
                ////log.WriteLogFile("首次使用");
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
            ////log.WriteLogFile("载入部件");
            backgroundWorker1.RunWorkerAsync();
        }

        //private readonly LogClass log =new LogClass();
        private 设置完成 shes;
        private 开始下载 开始;
        private Func<bool> 完成;
        //private 下载完成 完成;
        private bool 下载中 = false;
        private bool 设置完成_ = false;
        private int 项目数 = 0;
        private int 完成个数 = 0;
        private readonly string bin64 = Application.StartupPath + "//bin64";
        private readonly string 目录 = Application.StartupPath;
        private void Button2_Click(object sender, EventArgs e)
        {
            Settingui settingui = new Settingui();
            Controls.Add(settingui);
            settingui.BringToFront();
            shes = settingui.完成;
            if (backgroundWorker1.IsBusy)
            {
                ////log.WriteLogFile("设置菜单已打开");
                return;
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
                ////log.WriteLogFile("打开了设置菜单");
            }

        }
        //启动
        private void Button3_Click(object sender, EventArgs e)
        {
            ////log.WriteLogFile("启动游戏");
            启动yx();
        }

        public void 启动yx()
        {
            string 启动代码 = "-maploadinfo";
            if (!Properties.Settings.Default.附加地图)
            {
                启动代码 = "";
            }
            if (File.Exists(@".\\GW2Lanucher.exe"))
            {
                if (File.Exists(bin64 +"\\d3d9.dll"))
                {
                    File.Copy(bin64 + "\\d3d9.dll", 目录 + "\\d3d9.dll",true);
                }
                ProcessStartInfo info = new ProcessStartInfo { FileName = @".\\GW2Lanucher.exe", Arguments = 启动代码 };
                Process pro = new Process
                {
                    StartInfo = info
                };
                pro.Start();
                Close();
            }
            else if (File.Exists(@".\\Gw2-64.exe"))
            {
                ProcessStartInfo info = new ProcessStartInfo { FileName = @".\\Gw2-64.exe", Arguments = 启动代码 };
                Process pro = new Process
                {
                    StartInfo = info
                };
                pro.Start();
                Close();
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!下载中)
            {
                System.Threading.Thread.Sleep(10);
                shes(ref 设置完成_);
                if (设置完成_)
                {
                    ////log.WriteLogFile("设置界面关闭");
                    break;
                }
            }

        }
        //更新
        private void Button1_Click(object sender, EventArgs e)
        {
            开始更新();
        }
        public void 开始更新()
        {
            下载中 = true;
            按钮开关(0);
            if (检测冲突())
            {
                开始();
            }
        }

        public bool 检测冲突()
        {
            //if (!Properties.Settings.Default.附加功能)
            //{
            //    删除文件("d3d9_arcdps_extras.dll");
            //}
            //if (!Properties.Settings.Default.db切换)
            //{
             //   删除文件("d3d9_arcdps_buildtemplates.dll");
                
            //}
            if (!Properties.Settings.Default.团队力学)
            {
                删除文件("d3d9_arcdps_mechanicschs.dll");
            }
            if (!Properties.Settings.Default.团队恩赐)
            {
                删除文件("d3d9_arcdps_tablechs.dll");
            }
            if (!Properties.Settings.Default.流动输出)
            {
                删除文件("d3d9_arcdps_sct.dll");
            }
            if (!Properties.Settings.Default.坐骑插件)
            {
                删除文件("d3d9_chainload.dll");
            }
            if (!Properties.Settings.Default.r滤镜)
            {
                string[] vs = new string[4]
                {
                "ReShade64.dll",
                "ReShade.ini",
                "DefaultPreset.ini",
                "d3d9_ReShade641.zip",
                };

                for (int i = 0; i < vs.Length; i++)
                {
                    删除文件(vs[i]);
                }

                if (Properties.Settings.Default.s滤镜)
                {
                    if (File.Exists(目录 + "\\dxgi.dll"))
                    {
                        File.Delete(目录 + "\\dxgi.dll");
                    }
                }


                string didi2 = bin64 + "\\reshade-shaders";
                if (Directory.Exists(didi2))
                {
                    删除目录(didi2);
                }
                string didi3 = 目录 + "\\reshade-shaders";
                if (Directory.Exists(didi3))
                {
                    删除目录(didi3);
                }
            }
            else
            {
                if (!Properties.Settings.Default.dx12)
                {
                    string didi3 = 目录 + "\\reshade-shaders";
                    if (Directory.Exists(didi3))
                    {
                        删除目录(didi3);
                    }
                    string[] vs = new string[5]
                    {
                        "ReShade64.dll",
                        "ReShade.ini",
                        "DefaultPreset.ini",
                        "d3d9_ReShade641.zip",
                        "dxgi.dll"
                    };
                    for (int i = 0; i < vs.Length; i++)
                    {
                        if (File.Exists(目录 + "\\" + vs[i]))
                        {
                            File.Delete(目录 + "\\" + vs[i]);
                        }
                    }
                }
            }
            if (!Properties.Settings.Default.s滤镜)
            {
                string didi1 = bin64 + "\\SweetFX";
                if (Directory.Exists(didi1))
                {
                    删除目录(didi1);
                }
                string[] vs = new string[5]
                {
                "d3d9_mchain.dll",
                "SweetFX readme.txt",
                "SweetFX_preset.txt",
                "SweetFX_settings.txt",
                "SweetFX.zip"
                };
                for (int i = 0; i < vs.Length; i++)
                {
                    删除文件(vs[i]);
                }
                if (File.Exists(bin64 +"\\dxgi.dll"))
                {
                    File.Delete(bin64 + "\\dxgi.dll");
                }
            }
            if (!Properties.Settings.Default.dx12)
            {
                string didi1 = 目录 + "\\d912pxy";
                if (Directory.Exists(didi1))
                {
                    删除目录(didi1);
                }
                删除文件("d912pxy.dll");
            }
            else
            {
                if (Properties.Settings.Default.r滤镜)
                {
                    string[] vs = new string[5]
                    {
                        "ReShade64.dll",
                        "ReShade.ini",
                        "DefaultPreset.ini",
                        "d3d9_ReShade641.zip",
                        "dxgi.dll"
                    };
                    for (int i = 0; i < vs.Length; i++)
                    {
                        if (File.Exists(bin64 + "\\" + vs[i]))
                        {
                            File.Delete(bin64 + "\\" + vs[i]);
                        }
                    }
                    string didi2 = bin64 + "\\reshade-shaders";
                    if (Directory.Exists(didi2))
                    {
                        删除目录(didi2);
                    }
                }
            }
            return true;
        }

        public void 删除文件(string 文件)
        {
            if (File.Exists(Application.StartupPath+"\\"+文件))
            {
                File.Delete(Application.StartupPath + "\\" + 文件);
            }
            if (File.Exists(Application.StartupPath + "\\bin64\\" + 文件))
            {
                File.Delete(Application.StartupPath + "\\bin64\\" + 文件);
            }
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
        private void 按钮开关(int 开关状态)
        {
            switch (开关状态)
            {
                case 0:
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    ////log.WriteLogFile("主界面界面按钮关闭");
                    break;
                case 1:
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    ////log.WriteLogFile("主界面界面按钮打开");
                    break;
                default:
                    break;
            }
        }


        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            开始 = null;
            完成 = null;
            项目数 = 0;
            完成个数 = 0;
            button3.Text = "启动";
            flowLayoutPanel1.Controls.Clear();
            if (Properties.Settings.Default.主程序)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("主程序");
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                ////log.WriteLogFile("创建了 主程序 控件");
                //汉化文本
                Jiheui settingui2 = new Jiheui();
                settingui2.赋值("汉化文本");
                开始 += settingui2.更新;
                完成 += settingui2.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui2);
            }
            //if (Properties.Settings.Default.db切换)
            //{
            //    Jiheui settingui = new Jiheui();
            //    settingui.赋值("BD切换");
            //    开始 += settingui.更新;
            //    完成 += settingui.下载完成__;
            //    flowLayoutPanel1.Controls.Add(settingui);
            //    ////log.WriteLogFile("创建了 DB切换 控件");
            //}
            //if (Properties.Settings.Default.附加功能)
            //{
            //    Jiheui settingui = new Jiheui();
            //    settingui.赋值("附加功能");
            //    开始 += settingui.更新;
            //    完成 += settingui.下载完成__;
            //    flowLayoutPanel1.Controls.Add(settingui);
            //    ////log.WriteLogFile("创建了 附加功能 控件");
            //}
            if (Properties.Settings.Default.流动输出)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("流动输出");
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                ////log.WriteLogFile("创建了 流动输出 控件");
            }
            if (Properties.Settings.Default.团队力学)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("团队力学");
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                ////log.WriteLogFile("创建了 团队力学 控件");
            }
            if (Properties.Settings.Default.团队恩赐)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("团队恩赐");
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                ////log.WriteLogFile("创建了 团队恩赐 控件");
            }
            if (Properties.Settings.Default.坐骑插件)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("坐骑插件");
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                ////log.WriteLogFile("创建了 坐骑插件 控件");
            }
            if (Properties.Settings.Default.dx12)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("DX9TO12");
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                ////log.WriteLogFile("创建了 DX9TO12 控件");
            }
            if (Properties.Settings.Default.r滤镜)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("ReShade滤镜");
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                ////log.WriteLogFile("创建了 ReShade滤镜 控件");
            }
            if (Properties.Settings.Default.s滤镜)
            {
                Jiheui settingui = new Jiheui();
                settingui.赋值("Sweet滤镜");
                开始 += settingui.更新;
                完成 += settingui.下载完成__;
                flowLayoutPanel1.Controls.Add(settingui);
                ////log.WriteLogFile("创建了 Sweet滤镜 控件");
            }
            项目数 = 开始.GetInvocationList().Count();
            ////log.WriteLogFile("项目数:"+ 项目数);
            if (Properties.Settings.Default.启动更新)
            {
                是否更新();
            }
        }

        bool 是否开始计时 = false;
        private void 是否更新()
        {
            if (Properties.Settings.Default.跳过更新)
            {
                string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                string week = Day[Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))].ToString();
                if (!week.Equals(Day[3]) && !week.Equals(Day[4]))
                {
                    label1.Text = "今天是" + week + "可以正常更新";
                    开始更新();
                }
                else
                {
                    label1.Text = "今天是" + week + "您设置了不更新,所以跳过 ";
                    下载中 = false;
                    按钮开关(1);
                    if (Properties.Settings.Default.自动启动)
                    {
                        timer2.Enabled =  是否开始计时 = true;
                        //启动yx();
                    }
                }
            }
            else
            {
                开始更新();
            }
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
                            ////log.WriteLogFile("所有项目完成,重置必要参数,开启主界面按钮");
                            下载中 = false;
                            完成个数 = 0;
                            按钮开关(1);
                            if (Properties.Settings.Default.自动启动)
                            {
                                timer2.Enabled = 是否开始计时 = true;
                                //启动yx();
                            }
                        }
                    }
                }
            }
        }

        int 计时器_ = 5;
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (是否开始计时)
            {
                计时器_--;
                
                if (计时器_ < 0)
                {
                    timer2.Enabled = 是否开始计时 = false;
                    计时器_ = 5;
                    if (设置完成_ == true)
                    {
                        启动yx();
                    }
                    
                }
                button3.Text = "启动 -" + 计时器_.ToString();
            }

        }
    }
}
