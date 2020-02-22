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
using System.Threading;
using System.Net;
using System.IO.Compression;

namespace PlugIn_UpdateTool
{
    public delegate bool 下载完成();
    public delegate void 开始下载();
    public partial class Jiheui : UserControl
    {
        public Jiheui()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        //private readonly LogClass log = new LogClass();
        private bool 完成 = false;
        //private int 多线程 = 1;
        private string 下载地址 = "";
        private string 文件名 = "";
        private string 储存位置 = "";
        private readonly string bin64 = Application.StartupPath + "\\bin64";
        private DateTime _DateTime;
        private int 解压模式 = 0;
        public void 赋值(string 标签)
        {
            label1.Text = 标签;
            ////log.WriteLogFile("赋值"+ 标签);
            
        }
        public bool 下载完成__()
        {
            return 完成;
        }
        public void 更新()
        {
            完成 = false;
            progressBar1.Value = 0;
            label2.ForeColor = Color.Green;
            if (!Directory.Exists(bin64))
            {
                Directory.CreateDirectory(bin64);
            }
            switch (label1.Text)
            {
                case "主程序":
                    下载地址 = Properties.Settings.Default.主程序_地址;
                    文件名 = Path.GetFileName(下载地址);
                    储存位置 = bin64+"\\"+ 文件名;
                    下载();
                    break;
                //汉化文本
                case "汉化文本":
                    下载地址 = Properties.Settings.Default.汉化文件地址;
                    文件名 = Path.GetFileName(下载地址);
                    储存位置 = Application.StartupPath + "\\addons\\arcdps\\" + 文件名;
                    下载();
                    break;
                case "小提示":
                    下载地址 = Properties.Settings.Default.小提示_地址;
                    文件名 = Path.GetFileName(下载地址);
                    储存位置 = bin64 + "\\" + 文件名;
                    下载();
                    break;
                case "配置板":
                    下载地址 = Properties.Settings.Default.配置板_地址;
                    文件名 = Path.GetFileName(下载地址);
                    储存位置 = bin64 + "\\" + 文件名;
                    下载();
                    break;
                case "流动输出":
                    下载地址 = Properties.Settings.Default.流动输出_地址;
                    文件名 = Path.GetFileName(下载地址);
                    储存位置 = bin64 + "\\" + 文件名;
                    下载();
                    break;
                case "团队力学":
                    下载地址 = Properties.Settings.Default.团队力学_地址;
                    文件名 = Path.GetFileName(下载地址);
                    储存位置 = bin64 + "\\" + 文件名;
                    下载();
                    break;
                case "团队恩赐":
                    下载地址 = Properties.Settings.Default.团队恩赐_地址;
                    文件名 = Path.GetFileName(下载地址);
                    储存位置 = bin64 + "\\" + 文件名;
                    下载();
                    break;
                case "坐骑插件":
                    下载地址 = Properties.Settings.Default.坐骑插件_地址;
                    文件名 = Path.GetFileName(下载地址);
                    储存位置 = bin64 + "\\" + 文件名;
                    下载();
                    break;
                case "DX9TO12":
                    //下载4();
                    下载5();
                    break;
                case "ReShade滤镜":
                    下载地址 = Properties.Settings.Default.r滤镜_地址;
                    文件名 = Path.GetFileName(下载地址);
                    
                    if (Properties.Settings.Default.dx12)
                    {
                        储存位置 = Application.StartupPath + "\\" + 文件名;
                        解压模式 = 1;
                    }
                    else
                    {
                        储存位置 = bin64 + "\\" + 文件名;
                        解压模式 = 2;
                    }
                    下载();
                    break;
                case "Sweet滤镜":
                    下载地址 = Properties.Settings.Default.s滤镜_地址;
                    文件名 = Path.GetFileName(下载地址);
                    储存位置 = bin64 + "\\" + 文件名;
                    解压模式 = 2;
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
        private void 下载4()
        {
            Task.Run(new Action(Start4));
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
            string 缓存 = 储存位置 + ".tmp";
            ////log.WriteLogFile(储存位置 + ".tmp");
            try
            {

                if (文件名 == "d3d9.dll")
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
                        label2.Text = "获取服务器状态失败";
                        完成 = true;
                        return;
                    }
                    finally
                    {
                        wc2.Dispose();
                    }
                    if (a == 0)
                    {
                        label2.Text = "服务器更新中,请等会再来";
                        完成 = true;
                        return;
                    }
                }


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                label2.Text = 文件名 + "尝试链接";
                request = (HttpWebRequest)WebRequest.Create(下载地址);
                response = (HttpWebResponse)request.GetResponse();
                totalBytes = response.ContentLength;
                if (totalBytes > 0)
                {
                    label2.Text = 文件名 + "读取成功";
                    _DateTime = response.LastModified;
                    progressBar1.Maximum = (int)totalBytes;
                }
                else
                {
                    label2.Text = 文件名 + "读取失败";
                    label2.ForeColor = Color.Red;
                    完成 = true;
                    return ;
                }
                bool yum = false;
                if (File.Exists(储存位置))
                {
                    yum = totalBytes.ToString() == File.ReadAllBytes(储存位置).Length.ToString();
                    if (yum)
                    {
                        label2.Text = 文件名 + "文件大小相同";
                    }
                    else
                    {
                        label2.Text = 文件名 + "文件大小不同";
                    }
                }
                else
                {
                    if (!File.Exists(储存位置))
                    {
                        label2.Text = 文件名 + "文件不存在";
                    }
                    yum = false;
                }

                if (!File.GetLastWriteTime(储存位置).DayOfYear.Equals(response.LastModified.DayOfYear) || yum == false)
                {
                    try
                    {
                        st = response.GetResponseStream();
                        so = new FileStream(缓存, FileMode.Create);
                        long totalDownloadedByte = 0;
                        byte[] by = new byte[1024];
                        int osize = st.Read(by, 0, by.Length);
                        label2.Text = 文件名 + "开始下载";
                        while (osize > 0)
                        {
                            totalDownloadedByte = osize + totalDownloadedByte;
                            so.Write(by, 0, osize);
                            progressBar1.Value = (int)totalDownloadedByte;
                            osize = st.Read(by, 0, by.Length);
                        }
                        ////log.WriteLogFile(label1.Text + " 关闭缓存流");
                        if (so != null) so.Close();
                        if (st != null) st.Close();
                        ////log.WriteLogFile(label1.Text + " 拷贝缓存到老文件");
                        File.Copy(缓存, 储存位置, true);
                        File.SetLastWriteTime(储存位置, _DateTime);

                        ////log.WriteLogFile(label1.Text + " 下载完成");
                        progressBar1.Value = (int)totalBytes;
                        label2.Text = 文件名 + "下载完成\r\n";
                        label2.ForeColor = Color.Green;
                        if (解压模式 > 0)
                        {
                            if (解压模式 == 2)
                            {
                                Ungzip(储存位置, bin64);
                            }
                            else
                            {
                                Ungzip(储存位置, Application.StartupPath);
                            }
                        }
                        else
                        {
                            完成 = true;
                        }
                        
                    }
                    catch (Exception)
                    {
                         
                        ////log.WriteLogFile(label1.Text + "下载过程中出错");
                        label2.Text = 文件名 + "下载过程中出错";
                        label2.ForeColor = Color.Red;
                        完成 = true;
                    }

                }
                else
                {
                    ////log.WriteLogFile(label1.Text + " 无需更新");
                    progressBar1.Value = (int)totalBytes;
                    label2.Text = 文件名 + "无需更新";
                    label2.ForeColor = Color.Green;
                    if (label1.Text == "Sweet滤镜")
                    {
                        if (!Directory.Exists(bin64 + "\\SweetFX"))
                        {
                            Ungzip(储存位置, bin64);
                        }
                    }
                    if (label1.Text == "ReShade滤镜")
                    {
                        if (解压模式 == 2 && !Directory.Exists(bin64 + "\\reshade-shaders"))
                        {
                            Ungzip(储存位置, bin64);
                        }
                        if (解压模式 == 1 && !Directory.Exists(Application.StartupPath + "\\reshade-shaders"))
                        {
                            Ungzip(储存位置, Application.StartupPath);
                        }
                    }

                    完成 = true;
                }
            }
            catch (Exception)
            {
                ////log.WriteLogFile(label1.Text + "网络读取过程中出错");
                ////log.WriteLogFile(e.StackTrace);
                label2.Text = 文件名 + "网络读取过程中出错";
                label2.ForeColor = Color.Red;
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
        private void Ungzip(string File_, string appPath)
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
                label2.Text = 文件名 + "解压完成";
                label2.ForeColor = Color.Green;
                if (label1.Text == "DX9TO12")
                {
                    //d912pxy\dll\release
                    if (File.Exists(Application.StartupPath+ "\\d912pxy\\dll\\release\\d3d9.dll"))
                    {
                        File.Copy(Application.StartupPath + "\\d912pxy\\dll\\release\\d3d9.dll",
                            bin64+ "\\d912pxy.dll",true);
                        label2.Text = "d912pxy.dll复制完成";
                    }
                }

                if (!Properties.Settings.Default.dx12 && label1.Text == "ReShade滤镜")
                {
                    if (File.Exists(bin64 + "\\dxgi.dll"))
                    {
                        File.Copy(bin64 + "\\dxgi.dll", bin64 + "\\ReShade64.dll", true);
                        File.Delete(bin64 + "\\dxgi.dll");
                        label2.Text = "ReShade64.dll复制完成";
                    }
                }
                完成 = true;
            }
        }

        private void Start4()
        {
            Thread.BeginThreadAffinity();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream ResStream = null;
            StreamReader streamReader = null;
            try
            {
                label2.Text = label1.Text + "尝试获取文件地址";
                request = (HttpWebRequest)WebRequest.Create(@"https://api.github.com/repos/megai2/d912pxy/releases/latest");
                request.Method = "GET";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.26 Safari/537.36 Core/1.63.5977.400 LBBROWSER/10.1.3752.400";
                //request.UnsafeAuthenticatedConnectionSharing = true;
                response = (HttpWebResponse)request.GetResponse();
                ResStream = response.GetResponseStream();
                streamReader = new StreamReader(ResStream);
                string str = string.Empty;
                //循环读取从指定网站获得的数据
                while ((str = streamReader.ReadLine()) != null)
                {
                    if (str.IndexOf("browser_download_url") > 0)
                    {
                        str = str.Replace("browser_download_url\":", "");
                        str = str.Replace("\"", "");
                        str = str.Replace(" ", "");
                        str.Trim();
                        下载地址 = str;
                        文件名 = Path.GetFileName(str);
                        label2.Text = "尝试获取到:"+ 文件名;
                    }
                }
            }
            catch (Exception)
            {
                label2.Text = label1.Text + "尝试获取最新文件失败";
            }
            finally
            {
                if (request != null) request.Abort();
                if (response != null) response.Close();
                if (ResStream != null) ResStream.Close();
                if (streamReader != null) streamReader.Close();
            }
            
            if (文件名 != "" && 下载地址 !="")
            {
                储存位置 = Application.StartupPath + "//" + 文件名;

                if (File.Exists(Application.StartupPath + "//" + 文件名))
                {
                    Properties.Settings.Default.dx12文件名 = 文件名;
                    Properties.Settings.Default.Save();
                    progressBar1.Value = progressBar1.Maximum;
                    label2.Text = 文件名 + "发现现存文件";
                    label2.ForeColor = Color.Green;

                        Ungzip(储存位置, Application.StartupPath);
                        完成 = true;
                        Thread.EndThreadAffinity();

                }
                else
                {
                    Start5();
                }
            }
        }

        private void Start5()
        {
            
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream st = null;
            Stream so = null;
            long totalBytes;
            string 缓存 = 储存位置 + ".tmp";
            try
            {
                
                label2.Text = 文件名 + "尝试链接";
                request = (HttpWebRequest)WebRequest.Create(下载地址);
                request.Timeout = 5000;
                response = (HttpWebResponse)request.GetResponse();
                totalBytes = response.ContentLength;
                if (totalBytes > 0)
                {
                    label2.Text = 文件名 + "读取成功";
                    _DateTime = response.LastModified;
                    progressBar1.Maximum = (int)totalBytes;
                }
                else
                {
                    label2.Text = 文件名 + "读取失败";
                    label2.ForeColor = Color.Red;
                    完成 = true;
                    return;
                }
                bool yum = false;
                if (File.Exists(Application.StartupPath +"\\"+ Properties.Settings.Default.dx12文件名))
                {
                    yum = true;
                }
                if (!Properties.Settings.Default.dx12文件名.Equals(文件名))
                {
                    File.Delete(Application.StartupPath+"\\"+ Properties.Settings.Default.dx12文件名);
                    Properties.Settings.Default.dx12文件名 = 文件名;
                    Properties.Settings.Default.Save();
                    yum = false;
                }
                if (!File.Exists(储存位置) || yum == false)
                {
                    try
                    {
                        st = response.GetResponseStream();
                        so = new FileStream(缓存, FileMode.Create);
                        long totalDownloadedByte = 0;
                        byte[] by = new byte[1024];
                        int osize = st.Read(by, 0, by.Length);
                        label2.Text = 文件名 + "开始下载";
                        while (osize > 0)
                        {
                            totalDownloadedByte = osize + totalDownloadedByte;
                            so.Write(by, 0, osize);
                            progressBar1.Value = (int)totalDownloadedByte;
                            osize = st.Read(by, 0, by.Length);
                        }
                        ////log.WriteLogFile(label1.Text + " 关闭缓存流");
                        if (so != null) so.Close();
                        if (st != null) st.Close();
                        ////log.WriteLogFile(label1.Text + " 拷贝缓存到老文件");
                        File.Copy(缓存, 储存位置, true);
                        File.SetLastWriteTime(储存位置, _DateTime);
                        ////log.WriteLogFile(label1.Text + " 下载完成");
                        progressBar1.Value = (int)totalBytes;
                        label2.Text = 文件名 + "下载完成\r\n";
                        label2.ForeColor = Color.Green;
                        Ungzip(储存位置, Application.StartupPath);
                    }
                    catch (Exception)
                    {
                        ////log.WriteLogFile(label1.Text + "下载过程中出错");
                        label2.Text = 文件名 + "下载过程中出错";
                        label2.ForeColor = Color.Red;
                        完成 = true;
                    }

                }
                else
                {
                    ////log.WriteLogFile(label1.Text + " 无需更新");
                    progressBar1.Value = (int)totalBytes;
                    label2.Text = 文件名 + "无需更新";
                    label2.ForeColor = Color.Green;
                    if (!Directory.Exists(Application.StartupPath+ "\\d912pxy"))
                    {
                        Ungzip(储存位置, Application.StartupPath);
                    }
                    完成 = true;
                }
            }
            catch (Exception)
            {
                ////log.WriteLogFile(label1.Text + "网络读取过程中出错");
                label2.Text = 文件名 + "网络读取过程中出错";
                label2.ForeColor = Color.Red;
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

        private void Start6(){
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream st = null;
            Stream so = null;
            long totalBytes;
            long totalBytes2;
            string 缓存 = "";
            try
            {
                label2.Text = "尝试获取服务器状态";
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
                    label2.Text = "获取服务器状态失败";
                    label2.ForeColor = Color.Red;
                    完成 = true;
                    return;
                }
                finally
                {
                    wc2zt.Dispose();
                }
                if (azt == 0)
                {
                    label2.Text = "服务器正在更新中,请等会再来";
                    label2.ForeColor = Color.Red;
                    完成 = true;
                    return;
                }
                label2.Text = "尝试获取文件名";

                var wc = new WebClient();
                try
                {
                    string html = wc.DownloadString(@"http://gw2sy.top/dx12name.txt");
                    if (html != "")
                    {
                        string[] 分段 = html.Split('@');
                        label2.Text = 文件名 = 分段[0];
                        long.TryParse(分段[1], out totalBytes2);
                        储存位置 = Application.StartupPath + "//" + 文件名;
                        缓存 = 储存位置 + ".tmp";
                        下载地址 = @"http://gw2sy.top/" + 文件名;
                        Properties.Settings.Default.dx12文件名 = 文件名;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        label2.Text = "获取服务器文件信息失败!";
                        label2.ForeColor = Color.Red;
                        完成 = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    label2.Text = "尝试获取文件名失败";
                    label2.ForeColor = Color.Red;
                    完成 = true;
                    return;
                }
                finally
                {
                    wc.Dispose();
                }

                label2.Text = 文件名 + "尝试链接";
                request = (HttpWebRequest)WebRequest.Create(下载地址);
                request.Timeout = 5000;
                response = (HttpWebResponse)request.GetResponse();
                totalBytes = response.ContentLength;
                if (totalBytes != totalBytes2) {
                    label2.Text = "服务端文件大小和源文件不匹配!请联系神油";
                    label2.ForeColor = Color.Red;
                    完成 = true;
                    return;
                }

                if (totalBytes > 0)
                {
                    label2.Text = 文件名 + "读取成功";
                    _DateTime = response.LastModified;
                    progressBar1.Maximum = (int)totalBytes;
                }
                else
                {
                    label2.Text = 文件名 + "读取失败";
                    label2.ForeColor = Color.Red;
                    完成 = true;
                    return;
                }
                bool yum = false;
                if (File.Exists(储存位置))
                {
                    yum = totalBytes.ToString() == File.ReadAllBytes(储存位置).Length.ToString();
                    if (yum)
                    {
                        label2.Text = 文件名 + "文件大小相同";
                    }
                    else
                    {
                        label2.Text = 文件名 + "文件大小不同";
                    }
                }
                else
                {
                    if (!File.Exists(储存位置))
                    {
                        label2.Text = 文件名 + "文件不存在";
                    }
                    yum = false;
                }
                if (!File.GetLastWriteTime(储存位置).DayOfYear.Equals(response.LastModified.DayOfYear) || yum == false)
                {
                    try
                    {
                        st = response.GetResponseStream();
                        so = new FileStream(缓存, FileMode.Create);
                        long totalDownloadedByte = 0;
                        byte[] by = new byte[1024];
                        int osize = st.Read(by, 0, by.Length);
                        label2.Text = 文件名 + "开始下载";
                        while (osize > 0)
                        {
                            totalDownloadedByte = osize + totalDownloadedByte;
                            so.Write(by, 0, osize);
                            progressBar1.Value = (int)totalDownloadedByte;
                            osize = st.Read(by, 0, by.Length);
                        }
                        ////log.WriteLogFile(label1.Text + " 关闭缓存流");
                        if (so != null) so.Close();
                        if (st != null) st.Close();
                        ////log.WriteLogFile(label1.Text + " 拷贝缓存到老文件");
                        File.Copy(缓存, 储存位置, true);
                        File.SetLastWriteTime(储存位置, _DateTime);
                        ////log.WriteLogFile(label1.Text + " 下载完成");
                        progressBar1.Value = (int)totalBytes;
                        label2.Text = 文件名 + "下载完成\r\n";
                        label2.ForeColor = Color.Green;
                        Ungzip(储存位置, Application.StartupPath);
                    }
                    catch (Exception)
                    {
                        ////log.WriteLogFile(label1.Text + "下载过程中出错");
                        label2.Text = 文件名 + "下载过程中出错";
                        label2.ForeColor = Color.Red;
                        完成 = true;
                    }
                }
                else
                {
                    ////log.WriteLogFile(label1.Text + " 无需更新");
                    progressBar1.Value = (int)totalBytes;
                    label2.Text = 文件名 + "无需更新";
                    label2.ForeColor = Color.Green;
                    if (!Directory.Exists(Application.StartupPath + "\\d912pxy")|| !File.Exists(Application.StartupPath + "\\bin64\\d912pxy.dll"))
                    {
                        Ungzip(储存位置, Application.StartupPath);
                    }
                    完成 = true;
                }
            }
            catch (Exception)
            {
                label2.Text = 文件名 + "网络读取过程中出错";
                label2.ForeColor = Color.Red;
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
    }
}
