namespace ArcDPS_uptool
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public static Form1 mainfrm = null;
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.progressBar6 = new System.Windows.Forms.ProgressBar();
            this.progressBar5 = new System.Windows.Forms.ProgressBar();
            this.progressBar4 = new System.Windows.Forms.ProgressBar();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.progressBar10 = new System.Windows.Forms.ProgressBar();
            this.progressBar11 = new System.Windows.Forms.ProgressBar();
            this.progressBar12 = new System.Windows.Forms.ProgressBar();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.checkBox13 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(141, 20);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(181, 16);
            this.progressBar1.TabIndex = 0;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(141, 42);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(181, 16);
            this.progressBar2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 41);
            this.button1.TabIndex = 2;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox6);
            this.groupBox1.Controls.Add(this.checkBox5);
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.progressBar6);
            this.groupBox1.Controls.Add(this.progressBar5);
            this.groupBox1.Controls.Add(this.progressBar4);
            this.groupBox1.Controls.Add(this.progressBar3);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.progressBar2);
            this.groupBox1.Location = new System.Drawing.Point(11, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 155);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DPS系列";
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(10, 130);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(108, 16);
            this.checkBox6.TabIndex = 11;
            this.checkBox6.Tag = "6";
            this.checkBox6.Text = "团队BUFF(汉化)";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.Click += new System.EventHandler(this.按钮点击事件);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(10, 108);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(108, 16);
            this.checkBox5.TabIndex = 10;
            this.checkBox5.Tag = "5";
            this.checkBox5.Text = "团队机制(汉化)";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.Click += new System.EventHandler(this.按钮点击事件);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(10, 86);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(126, 16);
            this.checkBox4.TabIndex = 9;
            this.checkBox4.Tag = "4";
            this.checkBox4.Text = "SCT流动输出(汉化)";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.Click += new System.EventHandler(this.按钮点击事件);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(10, 64);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 16);
            this.checkBox3.TabIndex = 8;
            this.checkBox3.Tag = "3";
            this.checkBox3.Text = "附加功能";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.Click += new System.EventHandler(this.按钮点击事件);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(10, 42);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(60, 16);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Tag = "2";
            this.checkBox2.Text = "DB切换";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Click += new System.EventHandler(this.按钮点击事件);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoCheck = false;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(10, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 16);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Tag = "1";
            this.checkBox1.Text = "主程序";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.按钮点击事件);
            // 
            // progressBar6
            // 
            this.progressBar6.Location = new System.Drawing.Point(141, 130);
            this.progressBar6.Name = "progressBar6";
            this.progressBar6.Size = new System.Drawing.Size(181, 16);
            this.progressBar6.TabIndex = 5;
            // 
            // progressBar5
            // 
            this.progressBar5.Location = new System.Drawing.Point(141, 108);
            this.progressBar5.Name = "progressBar5";
            this.progressBar5.Size = new System.Drawing.Size(181, 16);
            this.progressBar5.TabIndex = 4;
            // 
            // progressBar4
            // 
            this.progressBar4.Location = new System.Drawing.Point(141, 86);
            this.progressBar4.Name = "progressBar4";
            this.progressBar4.Size = new System.Drawing.Size(181, 16);
            this.progressBar4.TabIndex = 3;
            // 
            // progressBar3
            // 
            this.progressBar3.Location = new System.Drawing.Point(141, 64);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(181, 16);
            this.progressBar3.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox9);
            this.groupBox2.Controls.Add(this.checkBox8);
            this.groupBox2.Controls.Add(this.checkBox7);
            this.groupBox2.Controls.Add(this.progressBar10);
            this.groupBox2.Controls.Add(this.progressBar11);
            this.groupBox2.Controls.Add(this.progressBar12);
            this.groupBox2.Location = new System.Drawing.Point(12, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(328, 86);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "其他插件";
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Location = new System.Drawing.Point(9, 64);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(90, 16);
            this.checkBox9.TabIndex = 14;
            this.checkBox9.Tag = "9";
            this.checkBox9.Text = "SweetFX滤镜";
            this.checkBox9.UseVisualStyleBackColor = true;
            this.checkBox9.Click += new System.EventHandler(this.按钮点击事件);
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(9, 42);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(126, 16);
            this.checkBox8.TabIndex = 13;
            this.checkBox8.Tag = "8";
            this.checkBox8.Text = "ReShade滤镜(汉化)";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.Click += new System.EventHandler(this.按钮点击事件);
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(9, 20);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(72, 16);
            this.checkBox7.TabIndex = 12;
            this.checkBox7.Tag = "7";
            this.checkBox7.Text = "坐骑插件";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.Click += new System.EventHandler(this.按钮点击事件);
            // 
            // progressBar10
            // 
            this.progressBar10.Location = new System.Drawing.Point(141, 64);
            this.progressBar10.Name = "progressBar10";
            this.progressBar10.Size = new System.Drawing.Size(181, 16);
            this.progressBar10.TabIndex = 2;
            // 
            // progressBar11
            // 
            this.progressBar11.Location = new System.Drawing.Point(141, 20);
            this.progressBar11.Name = "progressBar11";
            this.progressBar11.Size = new System.Drawing.Size(181, 16);
            this.progressBar11.TabIndex = 0;
            // 
            // progressBar12
            // 
            this.progressBar12.Location = new System.Drawing.Point(141, 42);
            this.progressBar12.Name = "progressBar12";
            this.progressBar12.Size = new System.Drawing.Size(181, 16);
            this.progressBar12.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 405);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 41);
            this.button2.TabIndex = 7;
            this.button2.Text = "官网";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(257, 405);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 41);
            this.button3.TabIndex = 8;
            this.button3.Text = "卸载";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(11, 452);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(329, 41);
            this.button4.TabIndex = 9;
            this.button4.Text = "启动游戏";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Location = new System.Drawing.Point(21, 265);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(84, 16);
            this.checkBox10.TabIndex = 10;
            this.checkBox10.Tag = "10";
            this.checkBox10.Text = "启动时更新";
            this.checkBox10.UseVisualStyleBackColor = true;
            this.checkBox10.Click += new System.EventHandler(this.按钮点击事件);
            // 
            // checkBox11
            // 
            this.checkBox11.AutoSize = true;
            this.checkBox11.Location = new System.Drawing.Point(220, 265);
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size(120, 16);
            this.checkBox11.TabIndex = 11;
            this.checkBox11.Tag = "11";
            this.checkBox11.Text = "更新完成启动游戏";
            this.checkBox11.UseVisualStyleBackColor = true;
            this.checkBox11.Click += new System.EventHandler(this.按钮点击事件);
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Location = new System.Drawing.Point(21, 287);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(108, 16);
            this.checkBox12.TabIndex = 12;
            this.checkBox12.Tag = "12";
            this.checkBox12.Text = "周三周四不更新";
            this.checkBox12.UseVisualStyleBackColor = true;
            this.checkBox12.Click += new System.EventHandler(this.按钮点击事件);
            // 
            // checkBox13
            // 
            this.checkBox13.AutoSize = true;
            this.checkBox13.Location = new System.Drawing.Point(220, 287);
            this.checkBox13.Name = "checkBox13";
            this.checkBox13.Size = new System.Drawing.Size(120, 16);
            this.checkBox13.TabIndex = 13;
            this.checkBox13.Tag = "13";
            this.checkBox13.Text = "附加地图加载命令";
            this.checkBox13.UseVisualStyleBackColor = true;
            this.checkBox13.Click += new System.EventHandler(this.按钮点击事件);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.HideSelection = false;
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox1.Location = new System.Drawing.Point(12, 309);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Size = new System.Drawing.Size(328, 90);
            this.textBox1.TabIndex = 14;
            this.textBox1.TabStop = false;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(175, 405);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(82, 41);
            this.button5.TabIndex = 15;
            this.button5.Text = "说明";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(352, 505);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox13);
            this.Controls.Add(this.checkBox12);
            this.Controls.Add(this.checkBox11);
            this.Controls.Add(this.checkBox10);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(368, 544);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(368, 544);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "激战2插件更新工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ProgressBar progressBar6;
        private System.Windows.Forms.ProgressBar progressBar5;
        private System.Windows.Forms.ProgressBar progressBar4;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.ProgressBar progressBar10;
        private System.Windows.Forms.ProgressBar progressBar11;
        private System.Windows.Forms.ProgressBar progressBar12;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.CheckBox checkBox11;
        private System.Windows.Forms.CheckBox checkBox12;
        private System.Windows.Forms.CheckBox checkBox13;
        private System.Windows.Forms.Button button5;
        public System.Windows.Forms.TextBox textBox1;
    }
}

