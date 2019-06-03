using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Net;
using System.Windows.Forms;

namespace GW2_Plug_Updatetool
{
    public class MultiDownload : Form1
    {
        #region 变量
        private int _threadNum;    //线程数量
        private long _fileSize;    //文件大小
        private string _fileUrl;   //文件地址
        private string _fileName;   //文件名
        private string _savePath;   //保存路径
        private short _threadCompleteNum; //线程完成数量
        private bool _isComplete = false;   //是否完成
        private volatile int _downloadSize; //当前下载大小(实时的)
        private Thread[] _thread;   //线程数组
        private List<string> _tempFiles = new List<string>();
        private object locker = new object();
        #endregion
        #region 属性
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize
        {
            get
            {
                return _fileSize;
            }
        }
        /// <summary>
        /// 当前下载大小(实时的)
        /// </summary>
        public int DownloadSize
        {
            get
            {
                return _downloadSize;
            }
        }
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsComplete
        {
            get
            {
                return _isComplete;
            }
        }
        /// <summary>
        /// 线程数量
        /// </summary>
        public int ThreadNum
        {
            get
            {
                return _threadNum;
            }
        }
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath
        {
            get
            {
                return _savePath;
            }
            set
            {
                _savePath = value;
            }
        }
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="threahNum">线程数量</param>
        /// <param name="fileUrl">文件Url路径</param>
        /// <param name="savePath">本地保存路径</param>
        public MultiDownload(int threahNum, string fileUrl, string savePath)
        {
            this._threadNum = threahNum;
            this._thread = new Thread[threahNum];
            this._fileUrl = fileUrl;
            this._savePath = savePath;
        }
        public MultiDownload()
        {
        }
        public void Start()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_fileUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            _fileSize = response.ContentLength;
            int singelNum = (int)(_fileSize / _threadNum);  //平均分配
            int remainder = (int)(_fileSize % _threadNum);  //获取剩余的
            request.Abort();
            response.Close();
            for (int i = 0; i < _threadNum; i++)
            {
                List<int> range = new List<int>();
                range.Add(i * singelNum);
                if (remainder != 0 && (_threadNum - 1) == i) //剩余的交给最后一个线程
                    range.Add(i * singelNum + singelNum + remainder - 1);
                else
                    range.Add(i * singelNum + singelNum - 1);
                //下载指定位置的数据
                int[] ran = new int[] { range[0], range[1] };
                _thread[i] = new Thread(new ParameterizedThreadStart(Download));
                _thread[i].Name = System.IO.Path.GetFileNameWithoutExtension(_fileUrl) + "_{0}".Replace("{0}", Convert.ToString(i + 1));
                _thread[i].Start(ran);
            }
        }
        private void Download(object obj)
        {
            Stream httpFileStream = null, localFileStram = null;
            try
            {
                int[] ran = obj as int[];
                string tmpFileBlock = System.IO.Path.GetTempPath() + Thread.CurrentThread.Name + ".tmp";
                _tempFiles.Add(tmpFileBlock);
                HttpWebRequest httprequest = (HttpWebRequest)WebRequest.Create(_fileUrl);
                httprequest.AddRange(ran[0], ran[1]);
                HttpWebResponse httpresponse = (HttpWebResponse)httprequest.GetResponse();
                httpFileStream = httpresponse.GetResponseStream();
                localFileStram = new FileStream(tmpFileBlock, FileMode.Create);
                byte[] by = new byte[5000];
                int getByteSize = httpFileStream.Read(by, 0, (int)by.Length); //Read方法将返回读入by变量中的总字节数
                while (getByteSize > 0)
                {
                    Thread.Sleep(20);
                    lock (locker) _downloadSize += getByteSize;
                    
                    localFileStram.Write(by, 0, getByteSize);
                    getByteSize = httpFileStream.Read(by, 0, (int)by.Length);
                }
                lock (locker) _threadCompleteNum++;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                if (httpFileStream != null) httpFileStream.Dispose();
                if (localFileStram != null) localFileStram.Dispose();
            }
            if (_threadCompleteNum == _threadNum)
            {
                Complete();
                _isComplete = true;
            }
        }
        /// <summary>
        /// 下载完成后合并文件块
        /// </summary>
        private void Complete()
        {
            Stream mergeFile = new FileStream(@_savePath, FileMode.Create);
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
            
        }
    }
}

