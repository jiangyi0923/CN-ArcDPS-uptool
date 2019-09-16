using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace PlugIn_UpdateTool
{
    public delegate void 设置完成(ref bool ssu);
    public partial class Settingui : UserControl
    {
        public Settingui()
        {
            InitializeComponent();
            控件赋值();
        }
        private readonly LogClass log = new LogClass();
        private bool _设置完成 = false;
        private readonly string bin64 = Application.StartupPath + "//bin64";
        private readonly string 目录 = Application.StartupPath;
        public void 完成(ref bool ssu)
        {
            ssu = _设置完成;
        }

        //取消按钮
        private void Button1_Click(object sender, EventArgs e)
        {
            _设置完成 = true;
            log.WriteLogFile("取消了设置");
            Dispose();
        }
        //保存按钮
        private void Button4_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            log.WriteLogFile("保存了设置");
            _设置完成 = true;
            Dispose();
        }
        //官网按钮
        private void Button2_Click(object sender, EventArgs e)
        {
            string 官方网址 = "https://gw2sy.top";
            log.WriteLogFile("打开了官网");
            Process.Start(官方网址);
        }
        //卸载按钮
        private void Button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "这将卸载有戏目录下所有插件但会保留addons目录及文件!(此目录不影响你正常玩游戏)", 
                "确定卸载所以插件吗?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                log.WriteLogFile("开始卸载");
                卸载插件();
            }
            else
            {
                log.WriteLogFile("取消卸载");
            }


        }

        private void 卸载插件()
        {
            string[] 所有文件名 = new string[24]
            {   "d3d9.dll",
                "d3d9_arcdps_buildtemplates.dll",
                "d3d9_arcdps_extras.dll",
                "d3d9_arcdps_mechanics.dll",
                "d3d9_chainload.dll",
                "d3d9_arcdps_tablechs.dll",
                "d3d9_arcdps_sct.dll",
                "ReShade64.dll",
                "ReShade.ini",
                "DefaultPreset.ini",
                "d3d9_mchain.dll",
                "SweetFX readme.txt",
                "SweetFX_preset.txt",
                "SweetFX_settings.txt",
                "dxgi.dll",
                "d3d9_ReShade641.zip",
                "d912pxy.dll",
                "SweetFX.zip",
                "ReShade.fx",
                "Sweet.fx",
                "dxgi.log",
                "d3d9_mchain.log",
                "ReShade64.log",
                Properties.Settings.Default.dx12文件名
            };
            for (int i = 0; i < 所有文件名.Length; i++)
            {
                if (File.Exists(bin64 + "\\" + 所有文件名[i]))
                {
                    File.Delete(bin64 + "\\" + 所有文件名[i]);
                }
            }
            for (int i = 0; i < 所有文件名.Length; i++)
            {
                if (File.Exists(目录 + "\\" + 所有文件名[i]))
                {
                    File.Delete(目录 + "\\" + 所有文件名[i]);
                }
            }
            string didi1 = bin64 + "\\SweetFX";
            if (Directory.Exists(didi1))
            {
                删除目录(didi1);
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

            string didi4 = 目录 + "\\d912pxy";
            if (Directory.Exists(didi4))
            {
                删除目录(didi4);
            }
            log.WriteLogFile("卸载完成");
            MessageBox.Show("卸载完成");
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
        //检测环境
        private void Button5_Click(object sender, EventArgs e)
        {
            log.WriteLogFile("从设置界面开启了检测界面");
            Testui testui = new Testui();
            Controls.Add(testui);
            testui.BringToFront();
        }

        private void 控件赋值()
        {
            checkBox1.Checked = Properties.Settings.Default.主程序;
            checkBox2.Checked = Properties.Settings.Default.db切换;
            checkBox3.Checked = Properties.Settings.Default.附加功能;
            checkBox4.Checked = Properties.Settings.Default.流动输出;
            checkBox5.Checked = Properties.Settings.Default.团队力学;
            checkBox6.Checked = Properties.Settings.Default.团队恩赐;
            checkBox7.Checked = Properties.Settings.Default.坐骑插件;
            checkBox8.Checked = Properties.Settings.Default.dx12;
            checkBox9.Checked = Properties.Settings.Default.r滤镜;
            checkBox10.Checked = Properties.Settings.Default.s滤镜;
            checkBox12.Checked = Properties.Settings.Default.启动更新;
            checkBox13.Checked = Properties.Settings.Default.自动启动;
            checkBox14.Checked = Properties.Settings.Default.跳过更新;
            checkBox15.Checked = Properties.Settings.Default.附加地图;
            if (checkBox12.Checked)
            {
                checkBox14.Enabled = true;
            }
            else
            {
                
                if (checkBox14.Checked)
                {
                    Properties.Settings.Default.跳过更新 = checkBox14.Checked = checkBox14.Enabled = false;
                }
                else
                {
                    checkBox14.Enabled = false;
                }
            }
            if (checkBox7.Checked)
            {
                checkBox8.Enabled = true;
                checkBox9.Enabled = true;
                checkBox10.Enabled = true;
                if (checkBox8.Checked)
                {
                    checkBox10.Enabled = false;
                    Properties.Settings.Default.s滤镜 = checkBox10.Checked = false;
                }
                if (checkBox9.Checked)
                {
                    checkBox10.Enabled = false;
                    Properties.Settings.Default.s滤镜 = checkBox10.Checked = false;
                }
                if (checkBox10.Checked)
                {
                    checkBox8.Enabled = false;
                    checkBox9.Enabled = false;
                    Properties.Settings.Default.dx12 = checkBox8.Checked = false;
                    Properties.Settings.Default.r滤镜 = checkBox9.Checked = false;
                }
            }
            else
            {
                Properties.Settings.Default.s滤镜 = checkBox10.Checked = checkBox10.Enabled = false;
                Properties.Settings.Default.dx12 = checkBox8.Checked = checkBox8.Enabled = false;
                Properties.Settings.Default.r滤镜 = checkBox9.Checked = checkBox9.Enabled = false;
            }
            Properties.Settings.Default.Save();
            log.WriteLogFile("设置界面控件赋值完成");
        }

        private void 按键事件(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            
            switch (int.Parse(check.Tag.ToString()))
            {
                case 2:
                    Properties.Settings.Default.db切换 = check.Checked;
                    log.WriteLogFile("db切换"+ check.Checked);
                    break;
                case 3:
                    Properties.Settings.Default.附加功能 = check.Checked;
                    log.WriteLogFile("附加功能" + check.Checked);
                    break;
                case 4:
                    Properties.Settings.Default.流动输出 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox5.Enabled = false;
                        checkBox6.Enabled = false;
                    }
                    else
                    {
                        checkBox5.Enabled = true;
                        checkBox6.Enabled = true;
                    }
                    log.WriteLogFile("流动输出" + check.Checked);
                    break;
                case 5:
                    Properties.Settings.Default.团队力学 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox4.Enabled = false;
                    }
                    else
                    {
                        checkBox4.Enabled = true;
                    }
                    log.WriteLogFile("团队力学" + check.Checked);
                    break;
                case 6:
                    Properties.Settings.Default.团队恩赐 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox4.Enabled = false;
                    }
                    else
                    {
                        checkBox4.Enabled = true;
                    }
                    log.WriteLogFile("团队恩赐" + check.Checked);
                    break;
                case 7:
                    Properties.Settings.Default.坐骑插件 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox8.Enabled = true;
                        checkBox9.Enabled = true;
                        checkBox10.Enabled = true;
                    }
                    else
                    {
                        Properties.Settings.Default.s滤镜 = checkBox10.Checked = checkBox10.Enabled = false;
                        Properties.Settings.Default.dx12 = checkBox8.Checked = checkBox8.Enabled = false;
                        Properties.Settings.Default.r滤镜 = checkBox9.Checked = checkBox9.Enabled = false;
                    }
                    log.WriteLogFile("坐骑插件" + check.Checked);
                    break;
                case 8:
                    
                    if (checkBox7.Checked)
                    {
                        Properties.Settings.Default.dx12 = check.Checked;
                        if (check.Checked)
                        {
                            checkBox10.Enabled = false;
                        }
                        else
                        {
                            if (!checkBox9.Checked)
                            {
                                checkBox10.Enabled = true;
                            }
                        }
                    }
                    log.WriteLogFile("dx12" + check.Checked);
                    break;
                case 9:
                    if (checkBox7.Checked)
                    {
                        Properties.Settings.Default.r滤镜 = check.Checked;
                        if (check.Checked)
                        {
                            checkBox10.Enabled = false;
                        }
                        else
                        {
                            if (!checkBox8.Checked)
                            {
                                checkBox10.Enabled = true;
                            }
                            
                        }
                    }
                    log.WriteLogFile("r滤镜" + check.Checked);
                    break;
                case 10:
                    if (checkBox7.Checked)
                    {
                        Properties.Settings.Default.s滤镜 = check.Checked;
                        if (check.Checked)
                        {
                            checkBox8.Enabled = false;
                            checkBox9.Enabled = false;
                        }
                        else
                        {
                            checkBox8.Enabled = true;
                            checkBox9.Enabled = true;
                        }
                    }
                    log.WriteLogFile("s滤镜" + check.Checked);
                    break;
                case 12:
                    Properties.Settings.Default.启动更新 = check.Checked;
                    if (check.Checked)
                    {
                        checkBox14.Enabled = true;
                    }
                    else
                    {
                        if (checkBox14.Checked)
                        {
                            Properties.Settings.Default.跳过更新 = checkBox14.Checked = checkBox14.Enabled = false;
                        }
                        else
                        {
                            checkBox14.Enabled = false;
                        }
                    }
                    log.WriteLogFile("启动更新" + check.Checked);
                    break;
                case 13:
                    Properties.Settings.Default.自动启动 = check.Checked;
                    log.WriteLogFile("自动启动" + check.Checked);
                    break;
                case 14:
                    Properties.Settings.Default.跳过更新 = check.Checked;
                    log.WriteLogFile("跳过更新" + check.Checked);
                    break;
                case 15:
                    Properties.Settings.Default.附加地图 = check.Checked;
                    log.WriteLogFile("附加地图" + check.Checked);
                    break;
                default:
                    break;
            }

        }


    }
}
