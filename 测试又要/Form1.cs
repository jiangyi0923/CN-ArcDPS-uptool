using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 测试又要
{
    public partial class Form1 : Form
    {
        DownloadOBGK md = new DownloadOBGK();
        DownloadOBGK md2 = new DownloadOBGK();
        bool ts1 = false;
        bool ts2 = false;
        public Form1()
        {
            InitializeComponent();
            textBox1.AppendText(Environment.GetFolderPath(Environment.SpecialFolder.System));
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string httpUrl = @"https://www.deltaconnected.com/arcdps/x64/d3d9.dll";
            string httpUrl2 = @"https://www.deltaconnected.com/arcdps/x64/buildtemplates/d3d9_arcdps_buildtemplates.dll";
            string saveUrl = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + System.IO.Path.GetFileName(httpUrl);
            string saveUrl2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + System.IO.Path.GetFileName(httpUrl2);
            //System
            var task = new Task(() =>
            {
                ts1 = true;
                md = new DownloadOBGK(httpUrl, saveUrl);
                md.Start();
            }
            );
            task.Start();
            var task2 = new Task(() =>
            {
                ts2 = true;
                md2 = new DownloadOBGK(httpUrl2, saveUrl2);
                md2.Start();
            }
            );
            task2.Start();

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            progressBar1.Maximum = (int)md.FileSize;
            progressBar1.Value = md.DownloadSize;
            progressBar2.Maximum = (int)md2.FileSize;
            progressBar2.Value = md2.DownloadSize;

            if (md.IsComplete&& ts1)
            {
                ts1 = false;
                GC.Collect();
            }
            if (md2.IsComplete&& ts2)
            {
                ts2 = false;
                GC.Collect();
            }
        }
    }
}
