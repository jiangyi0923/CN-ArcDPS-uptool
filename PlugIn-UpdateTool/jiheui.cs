using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlugIn_UpdateTool
{
    public partial class Jiheui : UserControl
    {
        public Jiheui()
        {
            InitializeComponent();
        }
        public bool 完成 = false;
        public void 赋值(string 标签)
        {
            label1.Text = 标签;
        }
    }
}
