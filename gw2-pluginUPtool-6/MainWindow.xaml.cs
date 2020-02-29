using System;
using System.Collections.Generic;
using System.Diagnostics;
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
