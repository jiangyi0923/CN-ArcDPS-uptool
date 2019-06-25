using System.Windows.Forms;

namespace ArcDPS_uptool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, System.EventArgs e)
        {
            bool 第一次使用 = Properties.Settings.Default.首次使用;
            if (第一次使用)
            {
                Properties.Settings.Default.首次使用 = false;
                Properties.Settings.Default.Save();
                MessageBox.Show("欢迎您使用激战2插件更新工具,\r\n" +
                    "第一次使用请勾选您需要的插件后点击更新按钮,\r\n" +
                    "您想使用滤镜插件的话需要勾选坐骑插件,\r\n" +
                    "不推荐:ReShade滤镜需要自己配置(建议高端玩家使用)\r\n" +
                    "推荐:SweetFX滤镜已经配置好了可以直接使用(需要游戏内关闭抗锯齿)\r\n" +
                    "遇到问题请先查看官网右上角菜单是否有解决方法,\r\n" +
                    "如果没有请联系我哦!QQ:375480856");
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {

        }
    }
}
