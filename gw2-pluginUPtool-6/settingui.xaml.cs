using System.Windows;
using System.Windows.Controls;


namespace gw2_pluginUPtool_6
{
    /// <summary>
    /// settingui.xaml 的交互逻辑
    /// </summary>
    /// 
    
    public partial class settingui : UserControl
    {
        public settingui()
        {
            InitializeComponent();
            #region 控件赋值
            cc11.ToolTip = "shift + alt + t arcdps设置面板\r\nshift + alt + o 重载arcdps插件\r\nshift + alt + c 打开团队面板\r\nshift + alt + h 隐藏arcdps插件所有面板\r\n不要取消,取消也是勾选,呵呵";
            cc12.ToolTip = "在pvp和战场可能出现闪退的BUG,\r\n另外请不要开启技能图标设置容易闪退!";
            cc14.ToolTip = "在地图模式/角色选择模式/地图加载模式下显示游戏内的小提示";
            cc13.ToolTip = "不定期更新,个人觉得没多少用处";
            cc15.ToolTip = "shift + alt + L 打开团队力学插件日志 \r\n已经很久没更新了";
            cc16.ToolTip = "shift + alt + N 打开团队力学插件统计 \r\n已经很久没更新了";
            cc21.ToolTip = "shift + alt + m 坐骑插件设置面板+鼠标跟随设置\r\nF8 打开关闭的boss计时器\r\nF9 打开或关闭鼠标跟随模块\r\nF10 打开输出循环提示器设置\r\nF7 打开输出循环提示器(可在设置中更改)\r\n详细请看网页介绍";
            cc22.ToolTip = "2.0版本以后需要清洁安装\r\n首次使用会加载模型这使得第一次进某图缓慢\r\n并且模型缓慢加载是正常现象\r\n具体使用请自己去百度";
            cc23.ToolTip = "HOME 开启ReShade滤镜设置\r\n需要自行设置\r\n而且很容易报错哦";
            cc24.ToolTip = "INSERT 开启/关闭SweetFX滤镜\r\n无需设置懒人首选\r\n报错几率小";
            cc35.ToolTip = "多线程下载模式还不是很完善慎用!\r\n更新页面按钮无法点击的时候关程序会凉";
            cc32.ToolTip = "每当周三是美服固定更新时间\r\n每当周五是国服固定更新时间\r\n开启此功能避免同步美服插件更新而导致游戏报错";
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
            cc35.IsChecked = Properties.Settings.Default.多线程下载;
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
                if (cc21.IsChecked.Value)
                {
                    cc22.IsEnabled = cc23.IsEnabled = true;
                }
                else
                {
                    cc22.IsEnabled = cc23.IsEnabled = false;
                }
            }

            if (cc22.IsChecked.Value || cc23.IsChecked.Value)
            {
                cc24.IsEnabled = false;
            }
            else
            {
                if (cc21.IsChecked.Value)
                {
                    cc24.IsEnabled = true;
                }
                else
                {
                    cc24.IsEnabled = false;
                }
                
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

        public bool 完成 = false;
        public bool 设置完成__()
        {
            return 完成;
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            完成 = true;
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
        }
        /// <summary>
        /// 卸载按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Masgessui masgessui = new Masgessui
            {
                Home = Home,
                卸载按钮数值 = 1
            };
            masgessui.label1.Content = "确定卸载所有插件吗?";
            masgessui.Box_.AppendText("这将卸载游戏目录下所有插件\r\n");
            masgessui.Box_.AppendText("但会保留addons目录及文件!\r\n");
            masgessui.Box_.AppendText("此目录不影响你正常玩游戏!\r\n");
            Home.Children.Add(masgessui);
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
            Properties.Settings.Default.多线程下载 = cc35.IsChecked.Value;
            if (!cc11.IsChecked.Value && !cc21.IsChecked.Value)
            {
                Properties.Settings.Default.主程序 = true;
            }
            Properties.Settings.Default.Save();
            完成 = true;
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
                    if (checkBox.IsChecked.Value)
                    {
                        System.Windows.Forms.MessageBox.Show("请注意安装此插件不要打开技能图标选项!!");
                    }
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
                        if (!cc11.IsChecked.Value)
                        {
                            cc11.IsChecked = true;
                            cc12.IsEnabled = cc13.IsEnabled = cc14.IsEnabled = cc15.IsEnabled = cc16.IsEnabled = true;
                        }
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
                        cc32.IsChecked = false;
                    }
                    break;
                case "跳过周三周四":
                    break;
                case "更新完成启动游戏":
                    break;
                case "附加加载命令":
                    break;
                case "多线程下载":
                    break;
                default:
                    break;
            }
        }
        #endregion


    }
}
