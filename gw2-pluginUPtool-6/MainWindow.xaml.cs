using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace gw2_pluginUPtool_6
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule.FileName);
            int.TryParse(myFileVersionInfo.FileVersion, out int 本地版本);
            Title = "激战2插件更新工具V6-" + 本地版本.ToString();
            if (!Properties.Settings.Default.首次运行检测)
            {
                testui testui1 = new testui
                {
                    Home = Home
                };
                Home.Children.Add(testui1);
            }
            else
            {

                Masgessui mgui = new Masgessui
                {
                    Home = Home
                };
                if (mgui.有新版本() || mgui.有新提醒())
                {
                    有提示_ = true;
                    Home.Children.Add(mgui);
                }
            }

            开始监听();

        }

        public void 开始监听() 
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            timer.Tick += 监听事件;
            timer.Start();
        }

        private void 监听事件(object sender, EventArgs e)
        {
            if (true)
            {
                执行界面事件();
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Panelpad.Children.Add(new module());向画板添加自定义控件集
            settingui sgui = new settingui
            {
                Home = Home
            };
            Home.Children.Add(sgui);

        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            有提示_ = false;
            开始更新();
        }

        public void 开始更新()
        {
            if (检测冲突() && !有提示_)
            {
                下载中 = true;
                //按钮开关(0);
                开始();
            }
        }

        private void 按钮开关(int 开关状态)
        {
            switch (开关状态)
            {
                case 0:
                    Updatebt.IsEnabled = false;
                    Settingbt.IsEnabled = false;
                    Runbt.IsEnabled = false;
                    ////log.WriteLogFile("主界面界面按钮关闭");
                    break;
                case 1:
                    Updatebt.IsEnabled = true;
                    Settingbt.IsEnabled = true;
                    Runbt.IsEnabled = true;
                    ////log.WriteLogFile("主界面界面按钮打开");
                    break;
                default:
                    break;
            }
        }

        public bool 检测冲突()
        {
            testui teui = new testui();


            if (!File.Exists(teui.插件路劲 + "//arcdps_font.ttf") || !File.Exists(teui.插件路劲B + "\\fonts\\arcdps_font.ttf"))
            {
                teui.初次运行();
            }

            if (File.Exists(bin64 + "//d3d9_arcdps_extras.dll"))
            {
                File.Delete(bin64 + "//d3d9_arcdps_extras.dll");
            }
            if (File.Exists(bin64 + "//d3d9_arcdps_buildtemplates.dll"))
            {
                File.Delete(bin64 + "//d3d9_arcdps_buildtemplates.dll");
            }

            if (!Properties.Settings.Default.小提示)
            {
                删除文件("d3d9_arcdps_ct.dll");
            }
            if (!Properties.Settings.Default.配置板)
            {
                删除文件("d3d9_arcdps_buildpad.dll");
            }
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
                if (File.Exists(bin64 + "\\dxgi.dll"))
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
            if (File.Exists(Directory.GetCurrentDirectory() + "\\" + 文件))
            {
                File.Delete(Directory.GetCurrentDirectory() + "\\" + 文件);
            }
            if (File.Exists(Directory.GetCurrentDirectory() + "\\bin64\\" + 文件))
            {
                File.Delete(Directory.GetCurrentDirectory() + "\\bin64\\" + 文件);
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


        private void 执行界面事件()
        {
            if (下载中)
            {
                return;
            }

            开始 = null;
            完成 = null;
            项目数 = 0;
            完成个数 = 0;
            Runbt.Content = "启动";
            Panelpad.Children.Clear();
            if (Properties.Settings.Default.主程序)
            {
                module settingui = new module();
                settingui.赋值("主程序");
                开始 += settingui.新综合更新代码;
                完成 += settingui.下载完成__;
                Panelpad.Children.Add(settingui);
                module settingui2 = new module();
                settingui2.赋值("汉化文本");
                开始 += settingui2.新综合更新代码;
                完成 += settingui2.下载完成__;
                Panelpad.Children.Add(settingui2);
            }
            if (Properties.Settings.Default.小提示)
            {
                module settingui = new module();
                settingui.赋值("小提示");
                开始 += settingui.新综合更新代码;
                完成 += settingui.下载完成__;
                Panelpad.Children.Add(settingui);
            }
            if (Properties.Settings.Default.配置板)
            {
                module settingui = new module();
                settingui.赋值("配置板");
                开始 += settingui.新综合更新代码;
                完成 += settingui.下载完成__;
                Panelpad.Children.Add(settingui);
            }
            if (Properties.Settings.Default.流动输出)
            {
                module settingui = new module();
                settingui.赋值("流动输出");
                开始 += settingui.新综合更新代码;
                完成 += settingui.下载完成__;
                Panelpad.Children.Add(settingui);
            }
            if (Properties.Settings.Default.团队力学)
            {
                module settingui = new module();
                settingui.赋值("团队力学");
                开始 += settingui.新综合更新代码;
                完成 += settingui.下载完成__;
                Panelpad.Children.Add(settingui);
            }
            if (Properties.Settings.Default.团队恩赐)
            {
                module settingui = new module();
                settingui.赋值("团队恩赐");
                开始 += settingui.新综合更新代码;
                完成 += settingui.下载完成__;
                Panelpad.Children.Add(settingui);
            }
            if (Properties.Settings.Default.坐骑插件)
            {
                module settingui = new module();
                settingui.赋值("坐骑插件");
                开始 += settingui.新综合更新代码;
                完成 += settingui.下载完成__;
                Panelpad.Children.Add(settingui);
            }
            if (Properties.Settings.Default.dx12)
            {
                module settingui = new module();
                settingui.赋值("DX9TO12");
                开始 += settingui.新综合更新代码;
                完成 += settingui.下载完成__;
                Panelpad.Children.Add(settingui);
            }
            if (Properties.Settings.Default.r滤镜)
            {
                module settingui = new module();
                settingui.赋值("ReShade滤镜");
                开始 += settingui.新综合更新代码;
                完成 += settingui.下载完成__;
                Panelpad.Children.Add(settingui);
            }
            if (Properties.Settings.Default.s滤镜)
            {
                module settingui = new module();
                settingui.赋值("Sweet滤镜");
                开始 += settingui.新综合更新代码;
                完成 += settingui.下载完成__;
                Panelpad.Children.Add(settingui);
            }
            项目数 = 开始.GetInvocationList().Count();
            if (Properties.Settings.Default.启动_)
            {
                //是否更新();
            }
        }


        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BingetoTimer();
        }

        /// <summary>
        /// 计时器计数
        /// </summary>
        private int Runtimer = 3;
        private bool 有提示_;
        private 开始下载 开始;
        private Func<bool> 完成;
        private bool 下载中 = false;
        private bool 设置完成_ = true;
        private int 项目数 = 0;
        private int 完成个数 = 0;
        private readonly string bin64 = Directory.GetCurrentDirectory() + "//bin64";
        private readonly string 目录 = Directory.GetCurrentDirectory();
        /// <summary>
        /// 计时器委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            Runtimer--;
            if (Runtimer < 0)
            {
                Panelpad.Children.Add(new module());
                //停止代码==>
                DispatcherTimer timer = (DispatcherTimer)sender;
                timer.Stop();
                //停止代码==<
            }
        }





        /// <summary>
        /// 开始计时器
        /// </summary>
        private void BingetoTimer()
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            timer.Tick += Timer_Tick;
            Runtimer = 3;
            timer.Start();
        }

    }
}
