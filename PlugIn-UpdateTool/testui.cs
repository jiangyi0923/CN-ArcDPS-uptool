using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace PlugIn_UpdateTool
{
    public partial class Testui : UserControl
    {
        public string 本地路劲 = Application.StartupPath;
        public string 插件路劲 = Application.StartupPath + "\\addons\\arcdps";
        public string 插件路劲B = Application.StartupPath + "\\addons\\sct";
        private LogClass log = new LogClass();
        public Testui()
        {
            InitializeComponent();
            button1.Enabled = false;
            
        }

        public void 初次运行()
        {
            if (!Directory.Exists(插件路劲))
            {
                解压(1);
            }
            else
            {
                解压(2);
            }
        }
        public void 解压(int aii)
        {
            if (aii == 1)
            {
                Directory.CreateDirectory(插件路劲);
                Application.DoEvents();
                if (!Directory.Exists(插件路劲B))
                {
                    Directory.CreateDirectory(插件路劲B);
                    Application.DoEvents();
                    Directory.CreateDirectory(插件路劲B + "\\fonts");
                    Application.DoEvents();
                }

                byte[] Save = Properties.Resources.arcdps;
                FileStream fsObj = new FileStream(插件路劲 + "\\arcdps.ini", FileMode.CreateNew);
                fsObj.Write(Save, 0, Save.Length);
                fsObj.Close();
                Application.DoEvents();

                byte[] Save1 = Properties.Resources.arcdps_font;
                FileStream fsObj1 = new FileStream(插件路劲 + "\\arcdps_font.ttf", FileMode.CreateNew);
                fsObj1.Write(Save1, 0, Save1.Length);
                fsObj1.Close();
                Application.DoEvents();

                byte[] Save2 = Properties.Resources.arcdps_font;
                FileStream fsObj2 = new FileStream(插件路劲B + "\\fonts\\arcdps_font.ttf", FileMode.CreateNew);
                fsObj2.Write(Save2, 0, Save2.Length);
                fsObj2.Close();
                Application.DoEvents();

                byte[] Save3 = Properties.Resources.sct;
                FileStream fsObj3 = new FileStream(插件路劲B + "\\sct.ini", FileMode.CreateNew);
                fsObj3.Write(Save3, 0, Save3.Length);
                fsObj3.Close();
                Application.DoEvents();

                byte[] Save4 = Properties.Resources.lang;
                FileStream fsObj4 = new FileStream(插件路劲B + "\\lang.ini", FileMode.CreateNew);
                fsObj4.Write(Save4, 0, Save4.Length);
                fsObj4.Close();
                Application.DoEvents();


            }
            else if (aii == 2)
            {
                if (!Directory.Exists(插件路劲B))
                {
                    Directory.CreateDirectory(插件路劲B);
                    Application.DoEvents();
                    Directory.CreateDirectory(插件路劲B + "\\fonts");
                    Application.DoEvents();
                }
                if (!File.Exists(插件路劲 + "\\arcdps.ini"))
                {
                    byte[] Save = Properties.Resources.arcdps;
                    FileStream fsObj = new FileStream(插件路劲 + "\\arcdps.ini", FileMode.CreateNew);
                    fsObj.Write(Save, 0, Save.Length);
                    fsObj.Close();
                }
                if (!File.Exists(插件路劲 + "\\arcdps_font.ttf"))
                {
                    byte[] Save1 = Properties.Resources.arcdps_font;
                    FileStream fsObj1 = new FileStream(插件路劲 + "\\arcdps_font.ttf", FileMode.CreateNew);
                    fsObj1.Write(Save1, 0, Save1.Length);
                    fsObj1.Close();
                }
                if (!File.Exists(插件路劲B + "\\fonts\\arcdps_font.ttf"))
                {
                    byte[] Save2 = Properties.Resources.arcdps_font;
                    FileStream fsObj2 = new FileStream(插件路劲B + "\\fonts\\arcdps_font.ttf", FileMode.CreateNew);
                    fsObj2.Write(Save2, 0, Save2.Length);
                    fsObj2.Close();
                }
                if (!File.Exists(插件路劲B + "\\sct.ini"))
                {
                    byte[] Save3 = Properties.Resources.sct;
                    FileStream fsObj3 = new FileStream(插件路劲B + "\\sct.ini", FileMode.CreateNew);
                    fsObj3.Write(Save3, 0, Save3.Length);
                    fsObj3.Close();
                }
                if (!File.Exists(插件路劲B + "\\lang.ini"))
                {
                    byte[] Save4 = Properties.Resources.lang;
                    FileStream fsObj4 = new FileStream(插件路劲B + "\\lang.ini", FileMode.CreateNew);
                    fsObj4.Write(Save4, 0, Save4.Length);
                    fsObj4.Close();
                }
            }
            
        }
        private void 检测()
        {
            //检测目录名是否正确
            int mul = 0;
            int jishu = 0;
            if (Regex.IsMatch(Application.StartupPath, @"[\u4e00-\u9fa5]") == false)
            {
                jishu++;
                mul++;
                label3.Text = "目录名检测: √无中文";
                label4.Text =  Application.StartupPath;
                label3.ForeColor = label4.ForeColor = Color.Green;
            }
            else
            {
                label3.Text = "目录名检测: ×有中文,请关闭程序更改目录名";
                label4.Text =   Application.StartupPath;
                label3.ForeColor = label4.ForeColor = Color.Red;
            }
            log.WriteLogFile(Application.StartupPath);
            //检测程序是否正确
            string path = @"./Gw2-64.exe";
            if (File.Exists(path))
            {
                jishu++;
                mul++;
                label5.Text = "程序名检测: ";
                label6.Text = "√有Gw2-64.exe";
                log.WriteLogFile("有Gw2-64.exe");
                label5.ForeColor = label6.ForeColor = Color.Green;
            }
            else
            {
                label5.Text = "程序名检测: ";
                label6.Text = "×无Gw2-64.exe,请关闭程序更改游戏程序名";
                log.WriteLogFile("没有Gw2-64.exe");
                label5.ForeColor = label6.ForeColor = Color.Red;
            }
            //如果上面两个正确 - 解压配置和字体 否则 跳过
            if (mul == 2)
            {
                初次运行();
            }
            //检测dx运行库
            //如果拥有 按钮2关闭;否则开启
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "//D3DX9_43.dll"))
            {
                jishu++;
                button2.Enabled = false;
                label7.ForeColor = Color.Green;
                log.WriteLogFile("有D3DX9_43.dll");
            }
            else
            {
                button2.Enabled = true;
                label7.ForeColor = Color.Red;
                log.WriteLogFile("没有D3DX9_43.dll");
            }
            //检测2013 
            //如果拥有 按钮3关闭;否则开启
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "//vccorlib120.dll"))
            {
                jishu++;
                button3.Enabled = false;
                label8.ForeColor = Color.Green;
                log.WriteLogFile("有vccorlib120.dll");
            }
            else
            {
                button3.Enabled = true;
                label8.ForeColor = Color.Red;
                log.WriteLogFile("没有vccorlib120.dll");
            }
            //检测2015-2019
            //如果拥有 按钮4关闭;否则开启
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "//vcamp140.dll"))
            {
                jishu++;
                button4.Enabled = false;
                label9.ForeColor = Color.Green;
                log.WriteLogFile("有vcamp140.dll");
            }
            else
            {
                button4.Enabled = true;
                label9.ForeColor = Color.Red;
                log.WriteLogFile("没有vcamp140.dll");
            }
            //如果上面都正确 开启按钮1 否则呵呵
            if (jishu == 5)
            {
                button5.Enabled = false;
                button1.Enabled = true;
            }
            else
            {
                button5.Enabled = true;
            }
        }


        private void 完成检测(object sender, EventArgs e)
        {
            Properties.Settings.Default.首次运行检测 = true;
            Properties.Settings.Default.Save();
            log.WriteLogFile("完成检测");
            Dispose();
        }
        //安装dx
        private void Button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@".\\dxwebsetup.exe"))
            {
                byte[] Save1 = Properties.Resources.dxwebsetup;
                FileStream fsObj1 = new FileStream(本地路劲 + "\\dxwebsetup.exe", FileMode.CreateNew);
                fsObj1.Write(Save1, 0, Save1.Length);
                fsObj1.Close();
                ProcessStartInfo info1 = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                _ = Process.Start(info1);
            }
            else
            {
                ProcessStartInfo info1 = new ProcessStartInfo { FileName = @".\\dxwebsetup.exe" };
                _ = Process.Start(info1);
            }

        }
        //安装2013
        private void Button3_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@".\\vcredist_x64.exe"))
            {
                byte[] Save1 = Properties.Resources.vcredist_x64;
                FileStream fsObj1 = new FileStream(本地路劲 + "\\vcredist_x64.exe", FileMode.CreateNew);
                fsObj1.Write(Save1, 0, Save1.Length);
                fsObj1.Close();
                ProcessStartInfo info1 = new ProcessStartInfo { FileName = @".\\vcredist_x64.exe" };
                _ = Process.Start(info1);
            }
            else
            {
                ProcessStartInfo info1 = new ProcessStartInfo { FileName = @".\\vcredist_x64.exe" };
                _ = Process.Start(info1);
            }

        }
        //安装2015
        private void Button4_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@".\\vc_redist_x64.exe"))
            {
                byte[] Save1 = Properties.Resources.vc_redist_x64;
                FileStream fsObj1 = new FileStream(本地路劲 + "\\vc_redist_x64.exe", FileMode.CreateNew);
                fsObj1.Write(Save1, 0, Save1.Length);
                fsObj1.Close();
                ProcessStartInfo info1 = new ProcessStartInfo { FileName = @".\\vc_redist_x64.exe" };
                _ = Process.Start(info1);
            }
            else
            {
                ProcessStartInfo info1 = new ProcessStartInfo { FileName = @".\\vc_redist_x64.exe" };
                _ = Process.Start(info1);
            }

        }
        //重新检测
        private void Button5_Click(object sender, EventArgs e)
        {
            log.WriteLogFile("重新检测");
            检测();
        }

        private void Testui_Load(object sender, EventArgs e)
        {
            检测();
        }
    }
}
