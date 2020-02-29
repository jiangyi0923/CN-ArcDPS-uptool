using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
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
using Path = System.IO.Path;

namespace gw2_pluginUPtool_6
{
    public delegate bool 下载完成();
    public delegate void 开始下载();
    /// <summary>
    /// settingui.xaml 的交互逻辑
    /// </summary>
    public partial class module : UserControl
    {
        public module()
        {
            InitializeComponent();
        }

        //通用参数
        public class 文件信息参数
        {
            public string 需下载文件名;
            public string 文件网址;
            public string 文件保存地址;
            public int 文件大小;
            public string[] 文件线程名集合; 
            public int[] 文件线程范围_起;
            public int[] 文件线程范围_终;
            public bool[] 线程状态集合;
            public int 解压模式 = 0;
        }

        public void 赋值(string 标签)
        {
            Label1.Content = 标签;
        }

        public bool 下载完成__()
        {
            return 完成;
        }

        private void 解压当前文件(string File_, string appPath)
        {
            using (ZipArchive archive = ZipFile.Open(File_, ZipArchiveMode.Read, Encoding.Default))
            {

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string fullPath = Path.Combine(appPath, entry.FullName);
                    if (String.IsNullOrEmpty(entry.Name))
                    {
                        Directory.CreateDirectory(fullPath);
                    }
                    else
                    {
                        entry.ExtractToFile(fullPath, true);
                    }
                }
                Label2.Content = dwfileinfoset.需下载文件名 + "解压完成";
                Label2.Foreground = Brushes.Green;
                if (Label1.Content.ToString() == "DX9TO12")
                {
                    //d912pxy\dll\release
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\d912pxy\\dll\\release\\d3d9.dll"))
                    {
                        File.Copy(Directory.GetCurrentDirectory() + "\\d912pxy\\dll\\release\\d3d9.dll",
                            bin64 + "\\d912pxy.dll", true);
                        Label2.Content = "d912pxy.dll复制完成";
                    }
                }

                if (!Properties.Settings.Default.dx12 && Label1.Content.ToString() == "ReShade滤镜")
                {
                    if (File.Exists(bin64 + "\\dxgi.dll"))
                    {
                        File.Copy(bin64 + "\\dxgi.dll", bin64 + "\\ReShade64.dll", true);
                        File.Delete(bin64 + "\\dxgi.dll");
                        Label2.Content = "ReShade64.dll复制完成";
                    }
                }
                完成 = true;
            }
        }

        private bool 完成 = false;
        public int 线程数量;  
        public 文件信息参数 dwfileinfoset = new 文件信息参数();
        private readonly string bin64 = Directory.GetCurrentDirectory() + "\\bin64";
        private DateTime _DateTime;
        

        #region 单线程下载模式
        public void 更新()
        {
            完成 = false;
            ProgressBar1.Value = 0;
            Label2.Foreground = Brushes.Green;
            if (!Directory.Exists(bin64))
            {
                Directory.CreateDirectory(bin64);
            }
            switch (Label1.Content)
            {
                case "主程序":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.主程序_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    下载();
                    break;
                //汉化文本
                case "汉化文本":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.汉化文件地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = Directory.GetCurrentDirectory() + "\\addons\\arcdps\\" + dwfileinfoset.需下载文件名;
                    下载();
                    break;
                case "小提示":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.小提示_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    下载();
                    break;
                case "配置板":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.配置板_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    下载();
                    break;
                case "流动输出":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.流动输出_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    下载();
                    break;
                case "团队力学":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.团队力学_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    下载();
                    break;
                case "团队恩赐":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.团队恩赐_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    下载();
                    break;
                case "坐骑插件":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.坐骑插件_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    下载();
                    break;
                case "DX9TO12":
                    //下载4();
                    下载5();
                    break;
                case "ReShade滤镜":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.r滤镜_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);

                    if (Properties.Settings.Default.dx12)
                    {
                        dwfileinfoset.文件保存地址 = Directory.GetCurrentDirectory() + "\\" + dwfileinfoset.需下载文件名;
                        dwfileinfoset.解压模式 = 1;
                    }
                    else
                    {
                        dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                        dwfileinfoset.解压模式 = 2;
                    }
                    下载();
                    break;
                case "Sweet滤镜":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.s滤镜_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    dwfileinfoset.解压模式 = 2;
                    下载();
                    break;
                default:
                    break;
            }
        }

        private void 下载()
        {
            Task.Run(new Action(Start));
        }

        private void 下载5()
        {
            Task.Run(new Action(Start6));
        }

        private void Start()
        {
            Thread.BeginThreadAffinity();
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream st = null;
            Stream so = null;
            long totalBytes;
            string 缓存 = dwfileinfoset.文件保存地址 + ".tmp";

            try
            {

                if (dwfileinfoset.需下载文件名 == "d3d9.dll")
                {
                    int a = 0;
                    var wc2 = new WebClient();
                    try
                    {

                        var html = wc2.DownloadString(@"http://gw2sy.top/getitnow.txt");
                        int.TryParse(html, out a);
                        wc2.Dispose();
                    }
                    catch (Exception)
                    {
                        a = 0;
                        Label2.Content = "获取服务器状态失败";
                        Label2.Foreground = Brushes.Red;
                        完成 = true;
                        return;
                    }
                    finally
                    {
                        wc2.Dispose();
                    }
                    if (a == 0)
                    {
                        Label2.Content = "服务器更新中,请等会再来";
                        完成 = true;
                        return;
                    }
                }


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                Label2.Content = dwfileinfoset.需下载文件名 + "尝试链接";
                request = (HttpWebRequest)WebRequest.Create(dwfileinfoset.文件网址);
                response = (HttpWebResponse)request.GetResponse();
                totalBytes = response.ContentLength;
                if (totalBytes > 0)
                {
                    Label2.Content = dwfileinfoset.需下载文件名 + "读取成功";
                    _DateTime = response.LastModified;
                    ProgressBar1.Maximum = (int)totalBytes;
                }
                else
                {
                    Label2.Content = dwfileinfoset.需下载文件名 + "读取失败";
                    Label2.Foreground = Brushes.Red;
                    完成 = true;
                    return;
                }
                bool yum = false;
                if (File.Exists(dwfileinfoset.文件保存地址))
                {
                    yum = totalBytes.ToString() == File.ReadAllBytes(dwfileinfoset.文件保存地址).Length.ToString();
                    if (yum)
                    {
                        Label2.Content = dwfileinfoset.需下载文件名 + "文件大小相同";
                    }
                    else
                    {
                        Label2.Content = dwfileinfoset.需下载文件名 + "文件大小不同";
                    }
                }
                else
                {
                    if (!File.Exists(dwfileinfoset.文件保存地址))
                    {
                        Label2.Content = dwfileinfoset.需下载文件名 + "文件不存在";
                    }
                    yum = false;
                }

                if (!File.GetLastWriteTime(dwfileinfoset.文件保存地址).DayOfYear.Equals(response.LastModified.DayOfYear) || yum == false)
                {
                    try
                    {
                        st = response.GetResponseStream();
                        so = new FileStream(缓存, FileMode.Create);
                        long totalDownloadedByte = 0;
                        byte[] by = new byte[1024];
                        int osize = st.Read(by, 0, by.Length);
                        Label2.Content = dwfileinfoset.需下载文件名 + "开始下载";
                        while (osize > 0)
                        {
                            totalDownloadedByte = osize + totalDownloadedByte;
                            so.Write(by, 0, osize);
                            ProgressBar1.Value = (int)totalDownloadedByte;
                            osize = st.Read(by, 0, by.Length);
                        }
                        if (so != null) so.Close();
                        if (st != null) st.Close();
                        File.Copy(缓存, dwfileinfoset.文件保存地址, true);
                        File.SetLastWriteTime(dwfileinfoset.文件保存地址, _DateTime);

                        ProgressBar1.Value = (int)totalBytes;
                        Label2.Content = dwfileinfoset.需下载文件名 + "下载完成\r\n";
                        Label2.Foreground = Brushes.Green;
                        if (dwfileinfoset.解压模式 > 0)
                        {
                            if (dwfileinfoset.解压模式 == 2)
                            {
                                解压当前文件(dwfileinfoset.文件保存地址, bin64);
                            }
                            else
                            {
                                解压当前文件(dwfileinfoset.文件保存地址, Directory.GetCurrentDirectory());
                            }
                        }
                        else
                        {
                            完成 = true;
                        }

                    }
                    catch (Exception)
                    {
                        Label2.Content = dwfileinfoset.需下载文件名 + "下载过程中出错";
                        Label2.Foreground = Brushes.Red;
                        完成 = true;
                    }

                }
                else
                {
                    ProgressBar1.Value = (int)totalBytes;
                    Label2.Content = dwfileinfoset.需下载文件名 + "无需更新";
                    Label2.Foreground = Brushes.Green;
                    if (Label1.Content.ToString() == "Sweet滤镜")
                    {
                        if (!Directory.Exists(bin64 + "\\SweetFX"))
                        {
                            解压当前文件(dwfileinfoset.文件保存地址, bin64);
                        }
                    }
                    if (Label1.Content.ToString() == "ReShade滤镜")
                    {
                        if (dwfileinfoset.解压模式 == 2 && !Directory.Exists(bin64 + "\\reshade-shaders"))
                        {
                            解压当前文件(dwfileinfoset.文件保存地址, bin64);
                        }
                        if (dwfileinfoset.解压模式 == 1 && !Directory.Exists(Directory.GetCurrentDirectory() + "\\reshade-shaders"))
                        {
                            解压当前文件(dwfileinfoset.文件保存地址, Directory.GetCurrentDirectory());
                        }
                    }

                    完成 = true;
                }
            }
            catch (Exception)
            {
                Label2.Content = dwfileinfoset.需下载文件名 + "网络读取过程中出错";
                Label2.Foreground = Brushes.Red;
                完成 = true;
            }
            finally
            {
                if (request != null) request.Abort();
                if (response != null) response.Close();
                if (so != null) so.Close();
                if (st != null) st.Close();
                if (File.Exists(缓存)) File.Delete(缓存);
            }
            Thread.EndThreadAffinity();
        }

        private void Start6()
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream st = null;
            Stream so = null;
            long totalBytes;
            long totalBytes2;
            string 缓存 = "";
            try
            {
                Label2.Content = "尝试获取服务器状态";
                int azt = 0;
                var wc2zt = new WebClient();
                try
                {

                    var htmztl = wc2zt.DownloadString(@"http://gw2sy.top/getdxnow.txt");
                    int.TryParse(htmztl, out azt);
                    wc2zt.Dispose();
                }
                catch (Exception)
                {
                    azt = 0;
                    Label2.Content = "获取服务器状态失败";
                    Label2.Foreground = Brushes.Red;
                    完成 = true;
                    return;
                }
                finally
                {
                    wc2zt.Dispose();
                }
                if (azt == 0)
                {
                    Label2.Content = "服务器正在更新中,请等会再来";
                    Label2.Foreground = Brushes.Red;
                    完成 = true;
                    return;
                }
                Label2.Content = "尝试获取文件名";

                var wc = new WebClient();
                try
                {
                    string html = wc.DownloadString(@"http://gw2sy.top/dx12name.txt");
                    if (html != "")
                    {
                        string[] 分段 = html.Split('@');
                        Label2.Content = dwfileinfoset.需下载文件名 = 分段[0];
                        long.TryParse(分段[1], out totalBytes2);
                        dwfileinfoset.文件保存地址 = Directory.GetCurrentDirectory() + "//" + dwfileinfoset.需下载文件名;
                        缓存 = dwfileinfoset.文件保存地址 + ".tmp";
                        dwfileinfoset.文件网址 = @"http://gw2sy.top/" + dwfileinfoset.需下载文件名;
                        Properties.Settings.Default.dx12文件名 = dwfileinfoset.需下载文件名;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Label2.Content = "获取服务器文件信息失败!";
                        Label2.Foreground = Brushes.Red;
                        完成 = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    Label2.Content = "尝试获取文件名失败";
                    Label2.Foreground = Brushes.Red;
                    完成 = true;
                    return;
                }
                finally
                {
                    wc.Dispose();
                }

                Label2.Content = dwfileinfoset.需下载文件名 + "尝试链接";
                request = (HttpWebRequest)WebRequest.Create(dwfileinfoset.文件网址);
                request.Timeout = 5000;
                response = (HttpWebResponse)request.GetResponse();
                totalBytes = response.ContentLength;

                if (totalBytes != totalBytes2)
                {
                    Label2.Content = "服务端文件大小和源文件不匹配!请联系神油";
                    Label2.Foreground = Brushes.Red;
                    完成 = true;
                    return;
                }

                if (totalBytes > 0)
                {
                    Label2.Content = dwfileinfoset.需下载文件名 + "读取成功";
                    _DateTime = response.LastModified;
                    ProgressBar1.Maximum = (int)totalBytes;
                }
                else
                {
                    Label2.Content = dwfileinfoset.需下载文件名 + "读取失败";
                    Label2.Foreground = Brushes.Red;
                    完成 = true;
                    return;
                }
                bool yum = false;
                if (File.Exists(dwfileinfoset.文件保存地址))
                {
                    yum = totalBytes.ToString() == File.ReadAllBytes(dwfileinfoset.文件保存地址).Length.ToString();
                    if (yum)
                    {
                        Label2.Content = dwfileinfoset.需下载文件名 + "文件大小相同";
                    }
                    else
                    {
                        Label2.Content = dwfileinfoset.需下载文件名 + "文件大小不同";
                    }
                }
                else
                {
                    if (!File.Exists(dwfileinfoset.文件保存地址))
                    {
                        Label2.Content = dwfileinfoset.需下载文件名 + "文件不存在";
                    }
                    yum = false;
                }
                if (!File.GetLastWriteTime(dwfileinfoset.文件保存地址).DayOfYear.Equals(response.LastModified.DayOfYear) || yum == false)
                {
                    try
                    {
                        st = response.GetResponseStream();
                        so = new FileStream(缓存, FileMode.Create);
                        long totalDownloadedByte = 0;
                        byte[] by = new byte[1024];
                        int osize = st.Read(by, 0, by.Length);
                        Label2.Content = dwfileinfoset.需下载文件名 + "开始下载";
                        while (osize > 0)
                        {
                            totalDownloadedByte = osize + totalDownloadedByte;
                            so.Write(by, 0, osize);
                            ProgressBar1.Value = (int)totalDownloadedByte;
                            osize = st.Read(by, 0, by.Length);
                        }
                        if (so != null) so.Close();
                        if (st != null) st.Close();
                        File.Copy(缓存, dwfileinfoset.文件保存地址, true);
                        File.SetLastWriteTime(dwfileinfoset.文件保存地址, _DateTime);
                        ProgressBar1.Value = (int)totalBytes;
                        Label2.Content = dwfileinfoset.需下载文件名 + "下载完成\r\n";
                        Label2.Foreground = Brushes.Green;
                        解压当前文件(dwfileinfoset.文件保存地址, Directory.GetCurrentDirectory());
                    }
                    catch (Exception)
                    {
                        Label2.Content = dwfileinfoset.需下载文件名 + "下载过程中出错";
                        Label2.Foreground = Brushes.Red;
                        完成 = true;
                    }
                }
                else
                {
                    ProgressBar1.Value = (int)totalBytes;
                    Label2.Content = dwfileinfoset.需下载文件名 + "无需更新";
                    Label2.Foreground = Brushes.Green;
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\d912pxy") || !File.Exists(Directory.GetCurrentDirectory() + "\\bin64\\d912pxy.dll"))
                    {
                        解压当前文件(dwfileinfoset.文件保存地址, Directory.GetCurrentDirectory());
                    }
                    完成 = true;
                }
            }
            catch (Exception)
            {
                Label2.Content = dwfileinfoset.需下载文件名 + "网络读取过程中出错";
                Label2.Foreground = Brushes.Red;
                完成 = true;
            }
            finally
            {
                if (request != null) request.Abort();
                if (response != null) response.Close();
                if (so != null) so.Close();
                if (st != null) st.Close();
                if (缓存 != "")
                {
                    if (File.Exists(缓存)) File.Delete(缓存);
                }
            }
            Thread.EndThreadAffinity();
        }
        #endregion

        #region 多线程下载

        public void 多线程更新()
        {
            完成 = false;
            ProgressBar1.Value = 0;
            Label2.Foreground = Brushes.Green;
            if (!Directory.Exists(bin64))
            {
                Directory.CreateDirectory(bin64);
            }
            switch (Label1.Content)
            {
                case "主程序":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.主程序_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    多线程下载();
                    break;
                //汉化文本
                case "汉化文本":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.汉化文件地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = Directory.GetCurrentDirectory() + "\\addons\\arcdps\\" + dwfileinfoset.需下载文件名;
                    下载();//文件太小 单线程下载
                    break;
                case "小提示":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.小提示_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    多线程下载();
                    break;
                case "配置板":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.配置板_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    多线程下载();
                    break;
                case "流动输出":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.流动输出_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    多线程下载();
                    break;
                case "团队力学":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.团队力学_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    多线程下载();
                    break;
                case "团队恩赐":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.团队恩赐_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    多线程下载();
                    break;
                case "坐骑插件":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.坐骑插件_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    多线程下载();
                    break;
                case "DX9TO12":
                    //下载4();
                    多线程下载();  //
                    break;
                case "ReShade滤镜":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.r滤镜_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);

                    if (Properties.Settings.Default.dx12)
                    {
                        dwfileinfoset.文件保存地址 = Directory.GetCurrentDirectory() + "\\" + dwfileinfoset.需下载文件名;
                        dwfileinfoset.解压模式 = 1;
                    }
                    else
                    {
                        dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                        dwfileinfoset.解压模式 = 2;
                    }
                    多线程下载();
                    break;
                case "Sweet滤镜":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.s滤镜_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    dwfileinfoset.解压模式 = 2;
                    多线程下载();
                    break;
                default:
                    break;
            }
        }

        public class HttpFile
        {
            public ProgressBar PBar;
            public int threadh;//线程代号  
            public string filename;//文件名  
            public string strUrl;//接收文件的URL  
            public FileStream fs;
            public HttpWebRequest request;
            public Stream ns;
            public byte[] nbytes;//接收缓冲区  
            public int nreadsize;//接收字节数  
            public 文件信息参数 dwfileinfo;
            public HttpFile(ProgressBar PBarin, 文件信息参数 Dwfilein, int threadhin)//构造方法  
            {
                PBar = PBarin;
                dwfileinfo = Dwfilein;
                threadh = threadhin;
            }
            public void Receive()//接收线程  
            {
                filename = dwfileinfo.文件线程名集合[threadh];
                strUrl = dwfileinfo.文件网址;
                ns = null;
                nbytes = new byte[512];
                nreadsize = 0;
                fs = new FileStream(filename, System.IO.FileMode.Create);
                try
                {
                    request = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                    request.AddRange(dwfileinfo.文件线程范围_起[threadh], dwfileinfo.文件线程范围_终[threadh]);
                    ns = request.GetResponse().GetResponseStream();
                    nreadsize = ns.Read(nbytes, 0, nbytes.Length);
                    while (nreadsize > 0)
                    {
                        fs.Write(nbytes, 0, nreadsize);
                        nreadsize = ns.Read(nbytes, 0, nbytes.Length);
                        PBar.Dispatcher.Invoke(new Action(delegate
                        {
                            PBar.Value += nreadsize;
                        }));
                    }
                    fs.Close();
                    ns.Close();

                }
                catch (Exception)
                {
                    fs.Close();
                }
                dwfileinfo.线程状态集合[threadh] = true;
            }
        }

        public void 多线程下载()
        {

            if (dwfileinfoset.需下载文件名 == "d3d9.dll")
            {
                int a = 0;
                var wc2 = new WebClient();
                try
                {
                    var html = wc2.DownloadString(@"http://gw2sy.top/getitnow.txt");
                    int.TryParse(html, out a);
                    wc2.Dispose();
                }
                catch (Exception)
                {
                    a = 0;
                    Label2.Content = "获取服务器状态失败";
                    Label2.Foreground = Brushes.Red;
                    完成 = true;
                    return;
                }
                finally
                {
                    wc2.Dispose();
                }
                if (a == 0)
                {
                    Label2.Content = "服务器更新中,请等会再来";
                    完成 = true;
                    return;
                }
            }



            HttpWebRequest request;
            long filesize = 0;
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(dwfileinfoset.文件网址);
                filesize = request.GetResponse().ContentLength;//取得目标文件的长度  
                ProgressBar1.Maximum = (int)filesize;
                request.Abort();
                Label2.Content = dwfileinfoset.需下载文件名 + "远程大小:" + filesize.ToString();
            }
            catch (Exception)
            {
                Label2.Content = dwfileinfoset.需下载文件名 + "获取文件失败";
            } 
            线程数量 = 6;
            dwfileinfoset.线程状态集合 = new bool[线程数量];
            dwfileinfoset.文件线程名集合 = new string[线程数量];
            dwfileinfoset.文件线程范围_起 = new int[线程数量];
            dwfileinfoset.文件线程范围_终 = new int[线程数量];

            int filethread = (int)filesize / 线程数量;
            for (int i = 0; i < 线程数量; i++)
            {
                dwfileinfoset.线程状态集合[i] = false;  
                dwfileinfoset.文件线程名集合[i] = dwfileinfoset.文件保存地址 + i.ToString() + ".tmp";
                if (i < 线程数量 - 1)
                {
                    dwfileinfoset.文件线程范围_起[i] = filethread * i;
                    dwfileinfoset.文件线程范围_终[i] = filethread * (i + 1) - 1;  
                }
                else
                {
                    dwfileinfoset.文件线程范围_起[i] = filethread * i;
                    dwfileinfoset.文件线程范围_终[i] = (int)filesize;
                }
            }
             
            Thread[] threadk = new Thread[线程数量];
            HttpFile[] httpfile = new HttpFile[线程数量];
            for (int j = 0; j < 线程数量; j++)
            {
                httpfile[j] = new HttpFile(ProgressBar1, dwfileinfoset, j);
                threadk[j] = new Thread(new ThreadStart(httpfile[j].Receive));
                threadk[j].Start();
                Label2.Content = dwfileinfoset.需下载文件名 + "开始下载";
            }
            
            Thread hbth = new Thread(new ThreadStart(多线程文件合并));
            hbth.Start();
        }

        public void 多线程文件合并()
        {
            while (true) 
            {
                完成 = true;
                for (int i = 0; i < 线程数量; i++)
                {
                    if (dwfileinfoset.线程状态集合[i] == false) 
                    {
                        完成 = false;
                        Thread.Sleep(100);
                        break;
                    }
                }
                if (完成 == true) 
                {
                    Label2.Dispatcher.Invoke(new Action(delegate
                    {
                        Label2.Content = dwfileinfoset.需下载文件名 + "下载线程全部结束";
                    }));
                    break;
                }
            }
            FileStream fs;//开始合并  
            FileStream fstemp;
            int readfile;
            byte[] bytes = new byte[512];
            fs = new FileStream(dwfileinfoset.文件保存地址, System.IO.FileMode.Create);
            for (int k = 0; k < 线程数量; k++)
            {
                fstemp = new FileStream(dwfileinfoset.文件线程名集合[k], System.IO.FileMode.Open);
                while (true)
                {
                    readfile = fstemp.Read(bytes, 0, bytes.Length);
                    if (readfile > 0)
                    {
                        fs.Write(bytes, 0, readfile);
                    }
                    else
                    {
                        break;
                    }
                }
                fstemp.Close();
            }
            fs.Close();
            for (int i = 0; i < 线程数量; i++)
            {
                if (File.Exists(dwfileinfoset.文件线程名集合[i]))
                {
                    File.Delete(dwfileinfoset.文件线程名集合[i]);
                }
            }

            Label2.Dispatcher.Invoke(new Action(delegate
            {
                Label2.Content = dwfileinfoset.需下载文件名 + "下载完成";
            }));
            ProgressBar1.Dispatcher.Invoke(new Action(delegate
            {
                ProgressBar1.Value = ProgressBar1.Maximum = 100;
            }));
        }

        #endregion




        ////=======新代码
        ///








        #region 废弃代码
        //private void 下载4()
        //{
        //    Task.Run(new Action(Start4));
        //}

        //private void Start4()
        //{
        //    Thread.BeginThreadAffinity();
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    HttpWebRequest request = null;
        //    HttpWebResponse response = null;
        //    Stream ResStream = null;
        //    StreamReader streamReader = null;
        //    try
        //    {
        //        Label2.Content = Label1.Content + "尝试获取文件地址";
        //        request = (HttpWebRequest)WebRequest.Create(@"https://api.github.com/repos/megai2/d912pxy/releases/latest");
        //        request.Method = "GET";
        //        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
        //        request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
        //        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.26 Safari/537.36 Core/1.63.5977.400 LBBROWSER/10.1.3752.400";
        //        //request.UnsafeAuthenticatedConnectionSharing = true;
        //        response = (HttpWebResponse)request.GetResponse();
        //        ResStream = response.GetResponseStream();
        //        streamReader = new StreamReader(ResStream);
        //        string str = string.Empty;
        //        //循环读取从指定网站获得的数据
        //        while ((str = streamReader.ReadLine()) != null)
        //        {
        //            if (str.IndexOf("browser_download_url") > 0)
        //            {
        //                str = str.Replace("browser_download_url\":", "");
        //                str = str.Replace("\"", "");
        //                str = str.Replace(" ", "");
        //                str.Trim();
        //                下载地址 = str;
        //                文件名 = Path.GetFileName(str);
        //                Label2.Content = "尝试获取到:" + 文件名;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Label2.Content = Label1.Content + "尝试获取最新文件失败";
        //    }
        //    finally
        //    {
        //        if (request != null) request.Abort();
        //        if (response != null) response.Close();
        //        if (ResStream != null) ResStream.Close();
        //        if (streamReader != null) streamReader.Close();
        //    }

        //    if (文件名 != "" && 下载地址 != "")
        //    {
        //        dwfileinfoset.文件保存地址 = Directory.GetCurrentDirectory() + "//" + 文件名;

        //        if (File.Exists(Directory.GetCurrentDirectory() + "//" + 文件名))
        //        {
        //            Properties.Settings.Default.dx12文件名 = 文件名;
        //            Properties.Settings.Default.Save();
        //            ProgressBar1.Value = ProgressBar1.Maximum;
        //            Label2.Content = 文件名 + "发现现存文件";
        //            Label2.Foreground = Brushes.Green;

        //            解压当前文件(dwfileinfoset.文件保存地址, Directory.GetCurrentDirectory());
        //            完成 = true;
        //            Thread.EndThreadAffinity();

        //        }
        //        else
        //        {
        //            Start5();
        //        }
        //    }
        //}

        //private void Start5()
        //{

        //    HttpWebRequest request = null;
        //    HttpWebResponse response = null;
        //    Stream st = null;
        //    Stream so = null;
        //    long totalBytes;
        //    string 缓存 = dwfileinfoset.文件保存地址 + ".tmp";
        //    try
        //    {

        //        Label2.Content = 文件名 + "尝试链接";
        //        request = (HttpWebRequest)WebRequest.Create(下载地址);
        //        request.Timeout = 5000;
        //        response = (HttpWebResponse)request.GetResponse();
        //        totalBytes = response.ContentLength;
        //        if (totalBytes > 0)
        //        {
        //            Label2.Content = 文件名 + "读取成功";
        //            _DateTime = response.LastModified;
        //            ProgressBar1.Maximum = (int)totalBytes;
        //        }
        //        else
        //        {
        //            Label2.Content = 文件名 + "读取失败";
        //            Label2.Foreground = Brushes.Red;
        //            完成 = true;
        //            return;
        //        }
        //        bool yum = false;
        //        if (File.Exists(Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.dx12文件名))
        //        {
        //            yum = true;
        //        }
        //        if (!Properties.Settings.Default.dx12文件名.Equals(文件名))
        //        {
        //            File.Delete(Directory.GetCurrentDirectory() + "\\" + Properties.Settings.Default.dx12文件名);
        //            Properties.Settings.Default.dx12文件名 = 文件名;
        //            Properties.Settings.Default.Save();
        //            yum = false;
        //        }
        //        if (!File.Exists(dwfileinfoset.文件保存地址) || yum == false)
        //        {
        //            try
        //            {
        //                st = response.GetResponseStream();
        //                so = new FileStream(缓存, FileMode.Create);
        //                long totalDownloadedByte = 0;
        //                byte[] by = new byte[1024];
        //                int osize = st.Read(by, 0, by.Length);
        //                Label2.Content = 文件名 + "开始下载";
        //                while (osize > 0)
        //                {
        //                    totalDownloadedByte = osize + totalDownloadedByte;
        //                    so.Write(by, 0, osize);
        //                    ProgressBar1.Value = (int)totalDownloadedByte;
        //                    osize = st.Read(by, 0, by.Length);
        //                }
        //                ////log.WriteLogFile(Label1.Content + " 关闭缓存流");
        //                if (so != null) so.Close();
        //                if (st != null) st.Close();
        //                ////log.WriteLogFile(Label1.Content + " 拷贝缓存到老文件");
        //                File.Copy(缓存, dwfileinfoset.文件保存地址, true);
        //                File.SetLastWriteTime(dwfileinfoset.文件保存地址, _DateTime);
        //                ////log.WriteLogFile(Label1.Content + " 下载完成");
        //                ProgressBar1.Value = (int)totalBytes;
        //                Label2.Content = 文件名 + "下载完成\r\n";
        //                Label2.Foreground = Brushes.Green;
        //                解压当前文件(dwfileinfoset.文件保存地址, Directory.GetCurrentDirectory());
        //            }
        //            catch (Exception)
        //            {
        //                ////log.WriteLogFile(Label1.Content + "下载过程中出错");
        //                Label2.Content = 文件名 + "下载过程中出错";
        //                Label2.Foreground = Brushes.Red;
        //                完成 = true;
        //            }

        //        }
        //        else
        //        {
        //            ////log.WriteLogFile(Label1.Content + " 无需更新");
        //            ProgressBar1.Value = (int)totalBytes;
        //            Label2.Content = 文件名 + "无需更新";
        //            Label2.Foreground = Brushes.Green;
        //            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\d912pxy"))
        //            {
        //                解压当前文件(dwfileinfoset.文件保存地址, Directory.GetCurrentDirectory());
        //            }
        //            完成 = true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ////log.WriteLogFile(Label1.Content + "网络读取过程中出错");
        //        Label2.Content = 文件名 + "网络读取过程中出错";
        //        Label2.Foreground = Brushes.Red;
        //        完成 = true;
        //    }
        //    finally
        //    {
        //        if (request != null) request.Abort();
        //        if (response != null) response.Close();
        //        if (so != null) so.Close();
        //        if (st != null) st.Close();
        //        if (File.Exists(缓存)) File.Delete(缓存);
        //    }

        //    Thread.EndThreadAffinity();
        //}
        #endregion
    }
}
