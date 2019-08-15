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
        private LogClass log = new LogClass();
        private bool 完成 = false;
        private int 多线程 = 1;
        private string 下载地址 = "";
        private string 文件名 = "";
        private string 储存位置 = "";
        private string bin64 = Application.StartupPath + "\\bin64";
        public void 赋值(string 标签,int _多线程)
        {
            label1.Text = 标签;
            多线程 = _多线程;
            this._thread = new Thread[多线程];
            
            log.WriteLogFile("赋值"+ 标签+ " 线程数"+_多线程);
            
        }
        public bool 下载完成__()
        {
            return 完成;
        }
        public void 更新()
        {
            完成 = false;
            progressBar1.Value = 0;
            _tempFiles.Clear();
            _threadCompleteNum = 0;
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
                case "DB切换":
                    下载地址 = Properties.Settings.Default.db切换_地址;
                    文件名 = Path.GetFileName(下载地址);
                    储存位置 = bin64 + "\\" + 文件名;
                    下载();
                    break;
                case "附加功能":
                    下载地址 = Properties.Settings.Default.附加功能_地址;
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
                    下载地址 = Properties.Settings.Default.主程序_地址;
                    文件名 = Path.GetFileName(下载地址);
                    储存位置 = bin64 + "\\" + 文件名;
                    下载();
                    break;
                case "ReShade滤镜":
                    下载地址 = Properties.Settings.Default.r滤镜_地址;
                    文件名 = Path.GetFileName(下载地址);
                    储存位置 = bin64 + "\\" + 文件名;
                    下载();
                    break;
                case "Sweet滤镜":
                    下载地址 = Properties.Settings.Default.s滤镜_地址;
                    文件名 = Path.GetFileName(下载地址);
                    储存位置 = bin64 + "\\" + 文件名;
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





        private long _fileSize;    //文件大小
        private short _threadCompleteNum; //线程完成数量
        private Thread[] _thread;   //线程数组
        private List<string> _tempFiles = new List<string>();
        private object locker = new object();
        private DateTime _DateTime;



        public void Start()
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                label2.Text = (文件名 + "尝试链接\r\n");
                request = (HttpWebRequest)WebRequest.Create(下载地址);
                response = (HttpWebResponse)request.GetResponse();
                _fileSize = response.ContentLength;
                if (_fileSize > 0)
                {
                    label2.Text=(文件名 + "读取成功\r\n");
                    _DateTime = response.LastModified;
                    progressBar1.Maximum = (int)_fileSize;
                }
                else
                {
                    label2.Text=(文件名 + "读取失败\r\n");
                }
                bool yum = false;
                if (File.Exists(储存位置))
                {
                    yum = _fileSize.ToString() == File.ReadAllBytes(储存位置).Length.ToString();
                    if (yum)
                    {
                        label2.Text=(文件名 + "文件大小相同\r\n");
                    }
                    else
                    {
                        label2.Text=(文件名 + "文件大小不同\r\n");
                    }
                }
                else
                {
                    if (!File.Exists(储存位置))
                    {
                        label2.Text=(文件名 + "文件不存在\r\n");
                    }
                    yum = false;
                }

                if (!File.GetLastWriteTime(储存位置).DayOfYear.Equals(response.LastModified.DayOfYear) || yum == false)
                {
                    int singelNum = (int)(_fileSize / 多线程);  //平均分配
                    int remainder = (int)(_fileSize % 多线程);  //获取剩余的
                    label2.Text=(文件名 + " - 开始下载\r\n");
                    for (int i = 0; i < 多线程; i++)
                    {
                        List<int> range = new List<int>();
                        range.Add(i * singelNum);
                        if (remainder != 0 && (多线程 - 1) == i) //剩余的交给最后一个线程
                            range.Add(i * singelNum + singelNum + remainder - 1);
                        else
                            range.Add(i * singelNum + singelNum - 1);
                        //下载指定位置的数据
                        int[] ran = new int[] { range[0], range[1] };
                        _thread[i] = new Thread(new ParameterizedThreadStart(Download));
                        _thread[i].Name = System.IO.Path.GetFileNameWithoutExtension(下载地址) + "_{0}".Replace("{0}", Convert.ToString(i + 1));
                        _thread[i].Start(ran);
                    }
                }
                else
                {
                    label2.Text=(文件名 + "修改时间相同无需更新\r\n");
                    log.WriteLogFile(label1.Text + " 无需更新");
                    progressBar1.Value = (int)_fileSize;
                    完成 = true;
                }
            }
            catch (Exception e)
            {
                log.WriteLogFile(e.StackTrace);
                label2.Text = (文件名 + "网络读取过程中出错\r\n");
                完成 = true;
            }
            finally
            {
                if (request != null) request.Abort();
                if (response != null) response.Close();
            }
        }
        private void Download(object obj)
        {
            Stream httpFileStream = null, localFileStram = null;
            HttpWebRequest httprequest = null;
            HttpWebResponse httpresponse = null;
            try
            {
                label2.Text = (文件名 + "尝试下载\r\n");
                int[] ran = obj as int[];
                string tmpFileBlock = bin64 + "\\" + Thread.CurrentThread.Name + ".tmp";
                _tempFiles.Add(tmpFileBlock);
                httprequest = (HttpWebRequest)WebRequest.Create(下载地址);
                httprequest.AddRange(ran[0], ran[1]);
                httpresponse = (HttpWebResponse)httprequest.GetResponse();
                httpFileStream = httpresponse.GetResponseStream();
                localFileStram = new FileStream(tmpFileBlock, FileMode.Create);
                byte[] by = new byte[4];
                int getByteSize = httpFileStream.Read(by, 0, (int)by.Length); //Read方法将返回读入by变量中的总字节数
                while (getByteSize > 0)
                {
                    //Thread.Sleep(10);
                    lock (locker) progressBar1.Value += getByteSize;
                    localFileStram.Write(by, 0, getByteSize);
                    getByteSize = httpFileStream.Read(by, 0, (int)by.Length);
                }
                lock (locker) _threadCompleteNum++;
            }
            catch (Exception e)
            {
                log.WriteLogFile(e.StackTrace);
                label2.Text=(文件名 + "下载过程中出错\r\n");
            }
            finally
            {
                if (httpFileStream != null) httpFileStream.Dispose();
                if (localFileStram != null) localFileStram.Dispose();
                if (httprequest != null) httprequest.Abort();
                if (httpresponse != null) httpresponse.Close();
            }
            if (_threadCompleteNum == 多线程)
            {
                Complete();
                label2.Text=(文件名 + " - 下载完成\r\n");
                log.WriteLogFile(label1.Text + " 下载完成");
                完成 = true;
            }
        }
        /// <summary>
        /// 下载完成后合并文件块
        /// </summary>
        private void Complete()
        {

            Stream mergeFile = new FileStream(储存位置, FileMode.Create);
            BinaryWriter AddWriter = new BinaryWriter(mergeFile);
            foreach (string file in _tempFiles)
            {
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    BinaryReader TempReader = new BinaryReader(fs);
                    AddWriter.Write(TempReader.ReadBytes((int)fs.Length));
                    TempReader.Close();
                }
                File.Delete(file);
            }
            AddWriter.Close();
            File.SetLastWriteTime(储存位置, _DateTime);
        }

    }
}
