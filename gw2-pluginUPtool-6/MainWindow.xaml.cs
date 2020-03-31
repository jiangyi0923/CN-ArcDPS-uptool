using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
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
            重置界面 = true;
            InitializeComponent();
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule.FileName);
            int.TryParse(myFileVersionInfo.FileVersion, out int 本地版本);
            Title = "激战2插件更新工具V6-" + 本地版本.ToString();
            if (!Properties.Settings.Default.首次运行检测_)
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
                 

                if (mgui.检测说明() == 1)
                {
                    bool mg1 = mgui.有新版本();
                    bool mg2 = mgui.有新提醒();
                    if (mg1)
                    {
                        有提示_ = true;
                        mgui.Showtext(0);
                        Home.Children.Add(mgui);
                    }
                    if (mg2 && !mg1)
                    {
                        有提示_ = true;
                        mgui.buttonquxi.IsEnabled = false;
                        mgui.Showtext(1);
                        Home.Children.Add(mgui);
                    }
                }
                else
                {
                    有提示_ = true;
                    mgui.buttonquxi.IsEnabled = false;
                    Home.Children.Add(mgui);
                }
            }
            
            

        }

        private bool 有提示_;
        private 开始下载 开始;
        private Func<bool> 完成;
        private Func<bool> 设置页面完成;
        private bool 下载中 = false;
        private bool 设置中 = false;
        private int 项目数 = 0;
        private int 完成个数 = 0;
        private readonly string bin64 = Directory.GetCurrentDirectory() + "//bin64";
        private readonly string 目录 = Directory.GetCurrentDirectory();
        private bool 重置界面 = false;
        private int 倒计时秒数 = 5;
        private bool 启动监听状态 = false;

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            settingui sgui = new settingui
            {
                Home = Home
            };
            设置页面完成 = null;
            设置页面完成 += sgui.设置完成__;
            设置中 = true;
            Home.Children.Add(sgui);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            开始更新();
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            启动游戏程序();
        }

        private void 开始监听()
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(2)
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
        private void 开始更新()
        {
            有提示_ = false;
            if (检测冲突() && !有提示_)
            {
                按钮开关(0);
                下载中 = true;
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

        private bool 检测冲突()
        {
            testui teui = new testui();

            //判断是前置文件夹是否存在
            if (!Directory.Exists(bin64))
            {
                Directory.CreateDirectory(bin64);
            }

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
                else
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

        private void 删除文件(string 文件)
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

            if (重置界面)
            {
                重置界面 = false;
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
                    if (Properties.Settings.Default.跳过_)
                    {
                        string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                        string week = Day[Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))].ToString();
                        if (!week.Equals(Day[3]) && !week.Equals(Day[4]))
                        {
                            this.Dispatcher.Invoke(new Action(delegate
                            {
                                this.开始更新(); 
                            }));
                        }
                        else
                        {
                            下载中 = false;
                            按钮开关(1);
                        }
                    }
                    else
                    {
                        this.Dispatcher.Invoke(new Action(delegate
                        {
                            this.开始更新();
                        }));
                    }
                }
                return;
            }

            if (下载中)
            {
                完成个数 = 0;
                foreach (Func<bool> dd in 完成.GetInvocationList())
                {
                    if (dd())
                    {
                        完成个数++;
                        if ((项目数 == 完成个数) && (完成个数 > 0))
                        {
                            下载中 = false;
                            重置界面 = false;
                            完成个数 = 0;
                            按钮开关(1);
                            if ( Properties.Settings.Default.开启_ && !设置中 && !启动监听状态)
                            {
                                启动监听();
                            }
                        }
                    }
                }
                return;
            }
            else
            {
                if (设置中)
                {
                    foreach (Func<bool> dd in 设置页面完成.GetInvocationList())
                    {
                        if (dd())
                        {
                            设置中 = false;
                            重置界面 = true;
                        }
                    }
                }
                if (Properties.Settings.Default.启动_ &&Properties.Settings.Default.开启_&& !设置中&& !启动监听状态)
                {
                    启动监听();
                }
            }

            
        }

        private void 启动游戏程序()
        {
            string 启动代码 = "-maploadinfo";
            if (!Properties.Settings.Default.附加_)
            {
                启动代码 = "";
            }
            if (File.Exists(@".\\GW2Lanucher.exe"))
            {
                if (File.Exists(bin64 + "\\d3d9.dll"))
                {
                    File.Copy(bin64 + "\\d3d9.dll", 目录 + "\\d3d9.dll", true);
                }
                ProcessStartInfo info = new ProcessStartInfo { FileName = @".\\GW2Lanucher.exe", Arguments = 启动代码 };
#pragma warning disable IDE0067 // 丢失范围之前释放对象
                Process pro = new Process
                {
                    StartInfo = info
                };
#pragma warning restore IDE0067 // 丢失范围之前释放对象
                pro.Start();
                Close();
            }
            else if (File.Exists(@".\\Gw2-64.exe"))
            {
                ProcessStartInfo info = new ProcessStartInfo { FileName = @".\\Gw2-64.exe", Arguments = 启动代码 };
#pragma warning disable IDE0067 // 丢失范围之前释放对象
                Process pro = new Process
                {
                    StartInfo = info
                };
#pragma warning restore IDE0067 // 丢失范围之前释放对象
                pro.Start();
                Close();
            }
        }

        private void 启动事件(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.开启_)
            {
                if (设置中 || 下载中)
                {
                    倒计时秒数 = 5;
                    return;
                }
                else
                {
                    if (倒计时秒数 < 1)
                    {
                        启动游戏程序();
                        倒计时秒数 = 5;
                    }
                    else
                    {
                        倒计时秒数--;
                        Runbt.Dispatcher.Invoke(new Action(delegate
                        {
                            Runbt.Content = "启动-" + 倒计时秒数;
                        }));
                    }
                }
            }
        }

        private void 启动监听()
        {
            启动监听状态 = true;
            DispatcherTimer timer2 = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            timer2.Tick += 启动事件;
            timer2.Start();
         
        }

        private void 加载(object sender, RoutedEventArgs e)
        {
            开始监听();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
