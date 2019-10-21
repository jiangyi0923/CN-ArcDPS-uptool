using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlugIn_UpdateTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    
    //public class LogClass
    //{
    //    /**/
    //    /// <summary>
    //    /// 写入日志文件
    //    /// </summary>
    //    /// <param name="input"></param>
    //    public void WriteLogFile(string input)
    //    {
    //        string dateTimeNow = DateTime.Now.ToString("MM-dd@hh:mm:ss");
    //        string fname = Application.StartupPath + "\\插件更新工具日志.txt";//获取程序启动路径
    //        FileInfo finfo = new FileInfo(fname);
    //        if (!finfo.Exists)
    //        {
    //            FileStream fs;
    //            fs = File.Create(fname);
    //            fs.Close();
    //            finfo = new FileInfo(fname);
    //        }
    //        if (finfo.Length > 1024 * 1024 * 10)
    //        {
    //            File.Move(Application.StartupPath + "\\日志.txt", Directory.GetCurrentDirectory() + DateTime.Now.TimeOfDay + "\\日志.txt");
    //        }
    //        using (FileStream fs = finfo.OpenWrite())
    //        {
    //            StreamWriter w = new StreamWriter(fs);
    //            w.BaseStream.Seek(0, SeekOrigin.End);
    //            w.Write(dateTimeNow +":"+input + "\r");
    //            w.Flush();
    //            w.Close();
    //        }
    //    }
    //}
}
