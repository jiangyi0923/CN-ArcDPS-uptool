using System;
using System.Collections.Generic;
using System.Linq;
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
    /// settingui.xaml 的交互逻辑
    /// </summary>
    public partial class settingui : UserControl
    {
        public settingui()
        {
            InitializeComponent();
            #region 控件赋值
            cc11.ToolTip = "已经很久没更新了";
            cc12.ToolTip = "已经很久没更新了";
            cc13.ToolTip = "已经很久没更新了";
            cc14.ToolTip = "已经很久没更新了";
            cc15.ToolTip = "已经很久没更新了";
            cc16.ToolTip = "已经很久没更新了";
            cc21.ToolTip = "已经很久没更新了";
            cc22.ToolTip = "已经很久没更新了";
            cc23.ToolTip = "已经很久没更新了";
            cc24.ToolTip = "已经很久没更新了";
            cc11.IsChecked = Properties.Settings.Default.主程序;
            cc12.IsChecked = Properties.Settings.Default.流动输出;
            cc13.IsChecked = Properties.Settings.Default.配置板;
            cc14.IsChecked = Properties.Settings.Default.小提示;
            cc15.IsChecked = Properties.Settings.Default.团队力学;
            cc16.IsChecked = Properties.Settings.Default.团队恩赐;
            cc21.IsChecked = Properties.Settings.Default.坐骑插件;
            cc22.IsChecked = Properties.Settings.Default.dx12;
            cc23.IsChecked = Properties.Settings.Default.r滤镜;
            cc24.IsChecked = Properties.Settings.Default.s滤镜;
            cc31.IsChecked = Properties.Settings.Default.启动_;
            cc32.IsChecked = Properties.Settings.Default.跳过_;
            cc33.IsChecked = Properties.Settings.Default.开启_;
            cc34.IsChecked = Properties.Settings.Default.附加_;
            #endregion
            #region 控件解决冲突
            if (cc11.IsChecked.Value)
            {
                cc12.IsEnabled = cc13.IsEnabled = cc14.IsEnabled = cc15.IsEnabled = cc16.IsEnabled = true;
            }
            else
            {
                cc12.IsEnabled = cc13.IsEnabled = cc14.IsEnabled = cc15.IsEnabled = cc16.IsEnabled = false;
            }

            if (cc21.IsChecked.Value)
            {
                cc22.IsEnabled = cc23.IsEnabled = cc24.IsEnabled = true;
            }
            else
            {
                cc22.IsEnabled = cc23.IsEnabled = cc24.IsEnabled = false;
            }

            if (cc24.IsChecked.Value)
            {
                cc22.IsEnabled = cc23.IsEnabled = false;
            }
            else
            {
                cc22.IsEnabled = cc23.IsEnabled = true;
            }

            if (cc22.IsChecked.Value || cc23.IsChecked.Value)
            {
                cc24.IsEnabled = false;
            }
            else
            {
                cc24.IsEnabled = true;
            }

            if (cc31.IsChecked.Value)
            {
                cc32.IsEnabled = true;
            }
            else
            {
                cc32.IsEnabled = false;
            }
            #endregion

        }
        /// <summary>
        /// 托管主页面
        /// </summary>
        public Grid Home { get; set; }

        //public WrapPanel HomeWrapP { get; set; }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Home.Children.Remove(this);
        }
        /// <summary>
        /// 检测按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            testui tesui = new testui
            {
                Home = Home
            };
            Home.Children.Add(tesui);
            Home.Children.Remove(this);
        }
        /// <summary>
        /// 卸载按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Masgessui ms = new Masgessui();
            //for (int i = 0; i < 100; i++)
            //{
            //    //ms.BOX.AppendText(i+ " dfdwsf\r\n");
            //}
            //ms.Home = Home;
            //Home.Children.Add(ms);
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.主程序 = cc11.IsChecked.Value;
            Properties.Settings.Default.流动输出 = cc12.IsChecked.Value;
            Properties.Settings.Default.配置板 = cc13.IsChecked.Value;
            Properties.Settings.Default.小提示 = cc14.IsChecked.Value;
            Properties.Settings.Default.团队力学 = cc15.IsChecked.Value;
            Properties.Settings.Default.团队恩赐 = cc16.IsChecked.Value;
            Properties.Settings.Default.坐骑插件 = cc21.IsChecked.Value;
            Properties.Settings.Default.dx12 = cc22.IsChecked.Value;
            Properties.Settings.Default.r滤镜 = cc23.IsChecked.Value;
            Properties.Settings.Default.s滤镜 = cc24.IsChecked.Value;
            Properties.Settings.Default.启动_ = cc31.IsChecked.Value;
            Properties.Settings.Default.跳过_ = cc32.IsChecked.Value;
            Properties.Settings.Default.开启_ = cc33.IsChecked.Value;
            Properties.Settings.Default.附加_ = cc34.IsChecked.Value;
            Properties.Settings.Default.Save();
            Home.Children.Remove(this);
        }

        #region 控件检测
        private void Dpsck(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            switch (checkBox.Content.ToString())
            {
                case "主程序":
                    if (checkBox.IsChecked.Value)
                    {
                        cc12.IsEnabled = cc13.IsEnabled = cc14.IsEnabled = cc15.IsEnabled = cc16.IsEnabled = true;
                    }
                    else
                    {
                        cc12.IsEnabled = cc13.IsEnabled = cc14.IsEnabled = cc15.IsEnabled = cc16.IsEnabled = false;
                        cc12.IsChecked = cc13.IsChecked = cc14.IsChecked = cc15.IsChecked = cc16.IsChecked = false;
                    }
                    break;
                case "SCT流动输出":
                    break;
                case "配置板":
                    break;
                case "小提示":
                    break;
                case "团队力学":
                    break;
                case "团队恩赐":
                    break;
                default:
                    break;
            }
        }

        private void Othck(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            switch (checkBox.Content.ToString())
            {
                case "坐骑插件":
                    if (checkBox.IsChecked.Value)
                    {
                        cc22.IsEnabled = cc23.IsEnabled = cc24.IsEnabled = true;
                    }
                    else
                    {
                        cc22.IsEnabled = cc23.IsEnabled = cc24.IsEnabled = false;
                        cc22.IsChecked = cc23.IsChecked = cc24.IsChecked = false;
                    }
                    break;
                case "DX9TO12":
                    if (checkBox.IsChecked.Value)
                    {
                        cc23.IsEnabled = true;
                        cc24.IsEnabled = false;
                        cc24.IsChecked = false;
                    }
                    else
                    {
                        cc23.IsEnabled = cc24.IsEnabled = true;
                    }
                    break;
                case "ReShade滤镜":
                    if (checkBox.IsChecked.Value)
                    {
                        cc24.IsEnabled = false;
                        cc24.IsChecked = false;
                    }
                    else
                    {
                        if (cc22.IsChecked.Value)
                        {
                            cc24.IsEnabled = false;
                            cc24.IsChecked = false;
                        }
                        else
                        {
                            cc24.IsEnabled = true;
                        }

                    }
                    break;
                case "Sweet滤镜":
                    if (checkBox.IsChecked.Value)
                    {
                        cc22.IsEnabled = cc23.IsEnabled = false;
                        cc22.IsChecked = cc23.IsChecked = false;
                    }
                    else
                    {
                        cc22.IsEnabled = cc23.IsEnabled = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void Tthck(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            switch (checkBox.Content.ToString())
            {
                case "启动更新":
                    if (checkBox.IsChecked.Value)
                    {
                        cc32.IsEnabled = true;
                    }
                    else
                    {
                        cc32.IsEnabled = false;
                    }
                    break;
                case "跳过更新":
                    break;
                case "更新完成启动游戏":
                    break;
                case "附加加载命令":
                    break;
                default:
                    break;
            }
        }
        #endregion


    }
}
