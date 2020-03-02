using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Path = System.IO.Path;

namespace gw2_pluginUPtool_6
{
    
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

        public class 文件信息参数
        {
            public string 需下载文件名;
            public string 文件网址;
            public string 文件保存地址;
            public int 文件大小;
            public Double 下载完成大小;
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

        public void 解压当前文件(string File_, string appPath)
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
                通知标签(1, dwfileinfoset.需下载文件名 + "解压完成");
                if (获得标签() == "DX9TO12")
                {
                    //d912pxy\dll\release
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\d912pxy\\dll\\release\\d3d9.dll"))
                    {
                        File.Copy(Directory.GetCurrentDirectory() + "\\d912pxy\\dll\\release\\d3d9.dll",
                            bin64 + "\\d912pxy.dll", true);
                        通知标签(1, "d912pxy.dll复制完成");
                    }
                }

                if (!Properties.Settings.Default.dx12 && 获得标签() == "ReShade滤镜")
                {
                    if (File.Exists(bin64 + "\\dxgi.dll"))
                    {
                        File.Copy(bin64 + "\\dxgi.dll", bin64 + "\\ReShade64.dll", true);
                        File.Delete(bin64 + "\\dxgi.dll");
                        通知标签(3, "ReShade64.dll复制完成");
                    }
                }
                完成 = true;
            }
        }

        public bool 完成 = false;
        public int 线程数量;
        public 文件信息参数 dwfileinfoset = new 文件信息参数();
        private readonly string bin64 = Directory.GetCurrentDirectory() + "\\bin64";
        private DateTime _DateTime;

        public class HttpFile
        {
            public int threadh;//线程代号  
            public string filename;//文件名  
            public string strUrl;//接收文件的URL  
            public FileStream fs;
            public HttpWebRequest request;
            public Stream ns;
            public byte[] nbytes;//接收缓冲区  
            public int nreadsize;//接收字节数  
            public 文件信息参数 dwfileinfo;
            public HttpFile( 文件信息参数 Dwfilein, int threadhin)//构造方法  
            {
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
                        dwfileinfo.下载完成大小 += nreadsize;
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    if (request != null) request.Abort();
                    if (ns != null) ns.Close();
                    if (fs != null) fs.Close();
                }
                dwfileinfo.线程状态集合[threadh] = true;
            }
        }

        public void 通知标签(int 类型,string 内容) 
        {
            Label2.Dispatcher.Invoke(new Action(delegate
            {
                Label2.Content = 内容;
                if (类型 == 0) Label2.Foreground = Brushes.Red;
                if (类型 == 1) Label2.Foreground = Brushes.Green;
            }));
        }

        public string 获得标签()
        {
            string fanhui = "";
            Label1.Dispatcher.Invoke(new Action(delegate
            {
                fanhui = Label1.Content.ToString();
            }));
            return fanhui;
        }

        public void 通知进度条(Double 数值) 
        {
            ProgressBar1.Dispatcher.Invoke(new Action(delegate
            {
                ProgressBar1.Value =  数值;
            }));
        }

        public void 通知进度条最大值(int 数值)
        {
            ProgressBar1.Dispatcher.Invoke(new Action(delegate
            {
                ProgressBar1.Maximum = 数值;
            }));
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
                        通知进度条(Math.Round((Double)dwfileinfoset.下载完成大小 / (Double)dwfileinfoset.文件大小 * (Double)100, 2));
                        break;
                    }
                }
                if (完成 == true)
                {
                    通知标签(1, dwfileinfoset.需下载文件名 + "下载完成");
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
            Thread.Sleep(1000);

            File.SetLastWriteTime(dwfileinfoset.文件保存地址, _DateTime);

            if (File.ReadAllBytes(dwfileinfoset.文件保存地址).Length.ToString() != dwfileinfoset.文件大小.ToString())
            {
                通知标签(0, dwfileinfoset.需下载文件名 + "大小不一致,请重新更新");
            }

            通知进度条(100);
            通知标签(1, dwfileinfoset.需下载文件名 + "下载完成");

            if (获得标签() == "DX9TO12")
            {
                解压当前文件(dwfileinfoset.文件保存地址, Directory.GetCurrentDirectory());
            }
            else
            {
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
        }

        public void 新综合更新代码()
        {

            // 还要判断 addons目录 

            //赋值初始参数
            完成 = false;
            ProgressBar1.Value = 0;
            Label2.Foreground = Brushes.Green;

            switch (Label1.Content.ToString())
            {
                case "主程序":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.主程序_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    break;
                //汉化文本
                case "汉化文本":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.汉化文件地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = Directory.GetCurrentDirectory() + "\\addons\\arcdps\\" + dwfileinfoset.需下载文件名;
                    break;
                case "小提示":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.小提示_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    break;
                case "配置板":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.配置板_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    break;
                case "流动输出":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.流动输出_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    break;
                case "团队力学":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.团队力学_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    break;
                case "团队恩赐":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.团队恩赐_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    break;
                case "坐骑插件":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.坐骑插件_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    break;
                case "DX9TO12":
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
                    break;
                case "Sweet滤镜":
                    dwfileinfoset.文件网址 = Properties.Settings.Default.s滤镜_地址;
                    dwfileinfoset.需下载文件名 = Path.GetFileName(dwfileinfoset.文件网址);
                    dwfileinfoset.文件保存地址 = bin64 + "\\" + dwfileinfoset.需下载文件名;
                    dwfileinfoset.解压模式 = 2;
                    break;
                default:
                    break;
            }
            //根据文件类型选择下载方式
            if (Properties.Settings.Default.多线程下载)
            {
                if (Label1.Content.ToString() == "汉化文本")
                {
                    单线程下载();
                }
                else
                {
                    多线程下载();
                }
            }
            else
            {
                单线程下载();
            }

        }

        public void 单线程下载()
        {
            Task.Run(new Action(开始单线程下载));
        }

        public void 多线程下载()
        {
            Task.Run(new Action(开始多线程下载));
        }

        public void 开始多线程下载() 
        {
            Thread.BeginThreadAffinity();
            bool 可以下载;
            if (获得标签() == "主程序")
            {
                可以下载 = 是否允许下载(@"http://gw2sy.top/getitnow.txt");

            }
            else if (获得标签() == "DX9TO12")
            {
                可以下载 = 是否允许下载(@"http://gw2sy.top/getdxnow.txt");
            }
            else
            {
                可以下载 = true;
            }

            if (可以下载)
            {
                long totalBytes = 0;
                long totalBytes2 = 0;
                string 缓存 = dwfileinfoset.文件保存地址 + ".tmp";
                if (获得标签() == "DX9TO12")
                {
                    var wc = new WebClient();
                    try
                    {
                        string html = wc.DownloadString(@"http://gw2sy.top/dx12name6.txt");
                        if (html != "")
                        {
                            string[] 分段 = html.Split('@');
                            dwfileinfoset.需下载文件名 = 分段[0];
                            long.TryParse(分段[1], out totalBytes2);
                            dwfileinfoset.文件保存地址 = Directory.GetCurrentDirectory() + "//" + dwfileinfoset.需下载文件名;
                            缓存 = dwfileinfoset.文件保存地址 + ".tmp";
                            dwfileinfoset.文件网址 = @"http://gw2sy.top/" + dwfileinfoset.需下载文件名;
                            Properties.Settings.Default.dx12文件名 = dwfileinfoset.需下载文件名;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            通知标签(0, "获取服务器文件信息失败!");
                            完成 = true;
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        通知标签(0, "尝试获取文件名失败");
                        完成 = true;
                        return;
                    }
                    finally
                    {
                        wc.Dispose();
                    }
                }

                HttpWebRequest request = null;
                HttpWebResponse response = null;
                try
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    通知标签(1, dwfileinfoset.需下载文件名 + "尝试链接");
                    request = (HttpWebRequest)WebRequest.Create(dwfileinfoset.文件网址);
                    response = (HttpWebResponse)request.GetResponse();
                    totalBytes = response.ContentLength;
                    if (totalBytes > 0)
                    {
                        通知标签(1, dwfileinfoset.需下载文件名 + "读取成功");
                        _DateTime = response.LastModified;
                        通知进度条最大值(100);
                    }
                    else
                    {
                        通知标签(0, dwfileinfoset.需下载文件名 + "读取失败");
                        完成 = true;
                        return;
                    }

                    if (获得标签() == "DX9TO12" && totalBytes != totalBytes2)
                    {
                        通知标签(0, "服务端文件大小和源文件不匹配!请联系神油");
                        完成 = true;
                        return;
                    }
                    //
                    bool yum = false;
                    if (File.Exists(dwfileinfoset.文件保存地址))
                    {
                        yum = totalBytes.ToString() == File.ReadAllBytes(dwfileinfoset.文件保存地址).Length.ToString();
                        if (yum)
                        {
                            通知标签(1, dwfileinfoset.需下载文件名 + "文件大小相同");
                        }
                        else
                        {
                            通知标签(1, dwfileinfoset.需下载文件名 + "文件大小不同");
                        }
                    }
                    else
                    {
                        if (!File.Exists(dwfileinfoset.文件保存地址))
                        {
                            通知标签(1, dwfileinfoset.需下载文件名 + "文件不存在");
                        }
                        yum = false;
                    }

                    if (!File.GetLastWriteTime(dwfileinfoset.文件保存地址).DayOfYear.Equals(response.LastModified.DayOfYear) || yum == false)
                    {
                        线程数量 = 6;
                        dwfileinfoset.线程状态集合 = new bool[线程数量];
                        dwfileinfoset.文件线程名集合 = new string[线程数量];
                        dwfileinfoset.文件线程范围_起 = new int[线程数量];
                        dwfileinfoset.文件线程范围_终 = new int[线程数量];
                        dwfileinfoset.文件大小 = (int)totalBytes;
                        int filethread = (int)totalBytes / 线程数量;
                        for (int i = 0; i < 线程数量; i++)
                        {
                            dwfileinfoset.线程状态集合[i] = false;
                            dwfileinfoset.文件线程名集合[i] = dwfileinfoset.文件保存地址 + i.ToString() + ".tmp";
                            if (File.Exists(dwfileinfoset.文件线程名集合[i]))
                            {
                                try
                                {
                                    File.Delete(dwfileinfoset.文件线程名集合[i]);
                                }
                                catch (Exception)
                                {
                                    通知标签(0,  "缓存文件无法删除");
                                    完成 = true;
                                    return;
                                }
                            }
                            if (i < 线程数量 - 1)
                            {
                                dwfileinfoset.文件线程范围_起[i] = filethread * i;
                                dwfileinfoset.文件线程范围_终[i] = filethread * (i + 1) - 1;
                            }
                            else
                            {
                                dwfileinfoset.文件线程范围_起[i] = filethread * i;
                                dwfileinfoset.文件线程范围_终[i] = (int)totalBytes;
                            }
                        }

                        Thread[] threadk = new Thread[线程数量];
                        HttpFile[] httpfile = new HttpFile[线程数量];
                        for (int j = 0; j < 线程数量; j++)
                        {
                            httpfile[j] = new HttpFile( dwfileinfoset, j);
                            threadk[j] = new Thread(new ThreadStart(httpfile[j].Receive));
                            threadk[j].Start();
                            
                        }
                        通知标签(1, dwfileinfoset.需下载文件名 + "开始下载");
                        Thread hbth = new Thread(new ThreadStart(多线程文件合并));
                        hbth.Start();
                    }
                    else
                    {
                        通知进度条(100);
                        通知标签(1, dwfileinfoset.需下载文件名 + "无需更新");
                        if (获得标签() == "DX9TO12")
                        {
                            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\d912pxy") || !File.Exists(Directory.GetCurrentDirectory() + "\\bin64\\d912pxy.dll"))
                            {
                                解压当前文件(dwfileinfoset.文件保存地址, Directory.GetCurrentDirectory());
                            }
                        }
                        if (获得标签() == "Sweet滤镜")
                        {
                            if (!Directory.Exists(bin64 + "\\SweetFX"))
                            {
                                解压当前文件(dwfileinfoset.文件保存地址, bin64);
                            }
                        }
                        if (获得标签() == "ReShade滤镜")
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
                    通知标签(0, "网络读取过程中出错!");
                    完成 = true;
                }
                finally
                {
                    if (request != null) request.Abort();
                    if (response != null) response.Close();
                }
            }
            else
            {
                完成 = true;
            }
            Thread.EndThreadAffinity();

        }

        public void 开始单线程下载()
        {
            Thread.BeginThreadAffinity();
            bool 可以下载;
            if (获得标签() == "主程序")
            {
                可以下载 = 是否允许下载(@"http://gw2sy.top/getitnow.txt");

            }
            else if(获得标签() == "DX9TO12")
            {
                可以下载 = 是否允许下载(@"http://gw2sy.top/getdxnow.txt");
            }
            else
            {
                可以下载 = true;
            }

            if (可以下载)
            {
                //初始化参数
                HttpWebRequest request = null;
                HttpWebResponse response = null;
                Stream st = null;
                Stream so = null;
                long totalBytes;
                long totalBytes2 = 0;
                string 缓存 = dwfileinfoset.文件保存地址 + ".tmp";

                if (获得标签() == "DX9TO12")
                {
                    var wc = new WebClient();
                    try
                    {
                        string html = wc.DownloadString(@"http://gw2sy.top/dx12name6.txt");
                        if (html != "")
                        {
                            string[] 分段 = html.Split('@');
                            dwfileinfoset.需下载文件名 = 分段[0];
                            long.TryParse(分段[1], out totalBytes2);
                            dwfileinfoset.文件保存地址 = Directory.GetCurrentDirectory() + "//" + dwfileinfoset.需下载文件名;
                            缓存 = dwfileinfoset.文件保存地址 + ".tmp";
                            dwfileinfoset.文件网址 = @"http://gw2sy.top/" + dwfileinfoset.需下载文件名;
                            Properties.Settings.Default.dx12文件名 = dwfileinfoset.需下载文件名;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            通知标签(0, "获取服务器文件信息失败!");
                            完成 = true;
                            return;
                        }
                    }
                    catch (Exception )
                    {
                        通知标签(0, "尝试获取文件名失败");
                        完成 = true;
                        return;
                    }
                    finally
                    {
                        wc.Dispose();
                    }
                }
                
                try
                {
                    //读取文件大小
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    通知标签(1, dwfileinfoset.需下载文件名 + "尝试链接");
                    request = (HttpWebRequest)WebRequest.Create(dwfileinfoset.文件网址);
                    response = (HttpWebResponse)request.GetResponse();
                    totalBytes = response.ContentLength;
                    if (totalBytes > 0)
                    {
                        通知标签(1, dwfileinfoset.需下载文件名 + "读取成功");
                        _DateTime = response.LastModified;
                        通知进度条最大值(100);
                    }
                    else
                    {
                        通知标签(0, dwfileinfoset.需下载文件名 + "读取失败");
                        完成 = true;
                        return;
                    }

                    if (获得标签() == "DX9TO12" && totalBytes != totalBytes2)
                    {
                        通知标签(0, "服务端文件大小和源文件不匹配!请联系神油");
                        完成 = true;
                        return;
                    }
                    //判断是否需要更新
                    bool yum = false;
                    if (File.Exists(dwfileinfoset.文件保存地址))
                    {
                        yum = totalBytes.ToString() == File.ReadAllBytes(dwfileinfoset.文件保存地址).Length.ToString();
                        if (yum)
                        {
                            通知标签(1, dwfileinfoset.需下载文件名 + "文件大小相同");
                        }
                        else
                        {
                            通知标签(1, dwfileinfoset.需下载文件名 + "文件大小不同");
                        }
                    }
                    else
                    {
                        if (!File.Exists(dwfileinfoset.文件保存地址))
                        {
                            通知标签(1, dwfileinfoset.需下载文件名 + "文件不存在");
                        }
                        yum = false;
                    }
                    //根据判断开始下载
                    if (!File.GetLastWriteTime(dwfileinfoset.文件保存地址).DayOfYear.Equals(response.LastModified.DayOfYear) || yum == false)
                    {
                        try
                        {
                            st = response.GetResponseStream();
                            so = new FileStream(缓存, FileMode.Create);
                            long totalDownloadedByte = 0;
                            byte[] by = new byte[1024];
                            int osize = st.Read(by, 0, by.Length);
                            通知标签(1, dwfileinfoset.需下载文件名 + "开始下载");
                            while (osize > 0)
                            {
                                totalDownloadedByte = osize + totalDownloadedByte;
                                so.Write(by, 0, osize);

                                通知进度条(Math.Round((Double)totalDownloadedByte / (Double)totalBytes * (Double)100, 2) );
                                osize = st.Read(by, 0, by.Length);
                            }
                            if (so != null) so.Close();
                            if (st != null) st.Close();

                            File.Copy(缓存, dwfileinfoset.文件保存地址, true);
                            File.SetLastWriteTime(dwfileinfoset.文件保存地址, _DateTime);

                            通知进度条(100);
                            通知标签(1, dwfileinfoset.需下载文件名 + "下载完成");

                            if (获得标签() == "DX9TO12")
                            {
                                解压当前文件(dwfileinfoset.文件保存地址, Directory.GetCurrentDirectory());
                            }
                            else
                            {
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
                        }
                        catch (Exception)
                        {
                            通知标签(0, dwfileinfoset.需下载文件名 + "下载过程中出错");
                            完成 = true;
                        }

                    }
                    else
                    {
                        通知进度条(100);
                        通知标签(1, dwfileinfoset.需下载文件名 + "无需更新");
                        if (获得标签() == "DX9TO12")
                        {
                            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\d912pxy") || !File.Exists(Directory.GetCurrentDirectory() + "\\bin64\\d912pxy.dll"))
                            {
                                解压当前文件(dwfileinfoset.文件保存地址, Directory.GetCurrentDirectory());
                            }
                        }
                        if (获得标签() == "Sweet滤镜")
                        {
                            if (!Directory.Exists(bin64 + "\\SweetFX"))
                            {
                                解压当前文件(dwfileinfoset.文件保存地址, bin64);
                            }
                        }
                        if (获得标签() == "ReShade滤镜")
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

                    通知标签(0, "网络读取过程中出错!");
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
            }
            else
            {
                完成 = true;
            }
            Thread.EndThreadAffinity();
        }

        public bool 是否允许下载(string 检测地址) 
        {
            bool 允许下载 = false;
            int a = 0;
            var wc2 = new WebClient();
            try
            {
                var html = wc2.DownloadString(检测地址);
                int.TryParse(html, out a);
                wc2.Dispose();
            }
            catch (Exception)
            {
                a = 0;
                通知标签(0, "获取服务器状态失败");
            }
            finally
            {
                wc2.Dispose();
            }
            if (a == 0)
            {
                通知标签(0, "服务器更新中,请等会再来");
            }
            else
            {
                允许下载 = true;
            }
            return 允许下载;
        }

    }
}
