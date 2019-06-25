using System;
using System.Windows.Forms;

namespace ArcDPS_uptool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Properties.Settings.Default.环境检测)
            {
                //Application.Run(new Form2());
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(new Form2());
                Application.Run(new Form1());
            }
        }
    }



}
