namespace GroupManager.Views
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.GroupList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.YunRedBagKey = new System.Windows.Forms.RichTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.YunAudioKey = new System.Windows.Forms.RichTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.YunQrCodeKey = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.YunOcrKey = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.YunMessageKey = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.YunGroupCardKey = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.YunBlackList = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.恢复所有群默认权限ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1427, 851);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GroupList);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1411, 804);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "群设定";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // GroupList
            // 
            this.GroupList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.GroupList.ContextMenuStrip = this.contextMenuStrip1;
            this.GroupList.FullRowSelect = true;
            this.GroupList.GridLines = true;
            this.GroupList.Location = new System.Drawing.Point(7, 7);
            this.GroupList.MultiSelect = false;
            this.GroupList.Name = "GroupList";
            this.GroupList.Size = new System.Drawing.Size(1398, 791);
            this.GroupList.TabIndex = 0;
            this.GroupList.UseCompatibleStateImageBehavior = false;
            this.GroupList.View = System.Windows.Forms.View.Details;
            this.GroupList.DoubleClick += new System.EventHandler(this.GroupList_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "群号";
            this.columnHeader1.Width = 226;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "群名";
            this.columnHeader2.Width = 464;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "群主";
            this.columnHeader3.Width = 414;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "机器人是否管理员";
            this.columnHeader4.Width = 226;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1411, 804);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "云设定";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(620, 3406);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(205, 51);
            this.button1.TabIndex = 3;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.YunRedBagKey);
            this.groupBox7.Location = new System.Drawing.Point(6, 2920);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(1342, 480);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "云 - 红包规词库";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(3, 443);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(898, 24);
            this.label7.TabIndex = 2;
            this.label7.Text = "打开使用违规云词库时优先判断云词库中的关键词，支持正则，请使用回车换行分隔";
            // 
            // YunRedBagKey
            // 
            this.YunRedBagKey.BackColor = System.Drawing.Color.White;
            this.YunRedBagKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.YunRedBagKey.Location = new System.Drawing.Point(7, 35);
            this.YunRedBagKey.Name = "YunRedBagKey";
            this.YunRedBagKey.Size = new System.Drawing.Size(1329, 390);
            this.YunRedBagKey.TabIndex = 0;
            this.YunRedBagKey.Text = "";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.YunAudioKey);
            this.groupBox6.Location = new System.Drawing.Point(7, 2434);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1342, 480);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "云 - 语音违规词库";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(3, 442);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(898, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "打开使用违规云词库时优先判断云词库中的关键词，支持正则，请使用回车换行分隔";
            // 
            // YunAudioKey
            // 
            this.YunAudioKey.BackColor = System.Drawing.Color.White;
            this.YunAudioKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.YunAudioKey.Location = new System.Drawing.Point(7, 35);
            this.YunAudioKey.Name = "YunAudioKey";
            this.YunAudioKey.Size = new System.Drawing.Size(1329, 390);
            this.YunAudioKey.TabIndex = 0;
            this.YunAudioKey.Text = "";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.YunQrCodeKey);
            this.groupBox5.Location = new System.Drawing.Point(7, 1948);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1342, 480);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "云 - 二维码违规词库";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(3, 442);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(898, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "打开使用违规云词库时优先判断云词库中的关键词，支持正则，请使用回车换行分隔";
            // 
            // YunQrCodeKey
            // 
            this.YunQrCodeKey.BackColor = System.Drawing.Color.White;
            this.YunQrCodeKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.YunQrCodeKey.Location = new System.Drawing.Point(7, 35);
            this.YunQrCodeKey.Name = "YunQrCodeKey";
            this.YunQrCodeKey.Size = new System.Drawing.Size(1329, 390);
            this.YunQrCodeKey.TabIndex = 0;
            this.YunQrCodeKey.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.YunOcrKey);
            this.groupBox4.Location = new System.Drawing.Point(7, 1462);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1342, 480);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "云 - OCR违规词库";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(3, 443);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(898, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "打开使用违规云词库时优先判断云词库中的关键词，支持正则，请使用回车换行分隔";
            // 
            // YunOcrKey
            // 
            this.YunOcrKey.BackColor = System.Drawing.Color.White;
            this.YunOcrKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.YunOcrKey.Location = new System.Drawing.Point(7, 35);
            this.YunOcrKey.Name = "YunOcrKey";
            this.YunOcrKey.Size = new System.Drawing.Size(1329, 390);
            this.YunOcrKey.TabIndex = 0;
            this.YunOcrKey.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.YunMessageKey);
            this.groupBox3.Location = new System.Drawing.Point(7, 979);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1342, 480);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "云 - 群消息违规词库";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(3, 444);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(898, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "打开使用违规云词库时优先判断云词库中的关键词，支持正则，请使用回车换行分隔";
            // 
            // YunMessageKey
            // 
            this.YunMessageKey.BackColor = System.Drawing.Color.White;
            this.YunMessageKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.YunMessageKey.Location = new System.Drawing.Point(7, 35);
            this.YunMessageKey.Name = "YunMessageKey";
            this.YunMessageKey.Size = new System.Drawing.Size(1329, 390);
            this.YunMessageKey.TabIndex = 0;
            this.YunMessageKey.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.YunGroupCardKey);
            this.groupBox2.Location = new System.Drawing.Point(7, 493);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1342, 480);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "云 - 群名片违规词库";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(2, 442);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(898, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "打开使用违规云词库时优先判断云词库中的关键词，支持正则，请使用回车换行分隔";
            // 
            // YunGroupCardKey
            // 
            this.YunGroupCardKey.BackColor = System.Drawing.Color.White;
            this.YunGroupCardKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.YunGroupCardKey.Location = new System.Drawing.Point(7, 35);
            this.YunGroupCardKey.Name = "YunGroupCardKey";
            this.YunGroupCardKey.Size = new System.Drawing.Size(1329, 390);
            this.YunGroupCardKey.TabIndex = 0;
            this.YunGroupCardKey.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.YunBlackList);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1342, 480);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "云 - 黑名单";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(2, 439);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1258, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "打开使用云黑名单群判断黑名单时优先判断云黑名单内的内容之后才会进行分群内的黑名单判断，请使用英文逗号分隔";
            // 
            // YunBlackList
            // 
            this.YunBlackList.BackColor = System.Drawing.Color.White;
            this.YunBlackList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.YunBlackList.Location = new System.Drawing.Point(7, 35);
            this.YunBlackList.Name = "YunBlackList";
            this.YunBlackList.Size = new System.Drawing.Size(1329, 390);
            this.YunBlackList.TabIndex = 0;
            this.YunBlackList.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.恢复所有群默认权限ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(305, 84);
            // 
            // 恢复所有群默认权限ToolStripMenuItem
            // 
            this.恢复所有群默认权限ToolStripMenuItem.Name = "恢复所有群默认权限ToolStripMenuItem";
            this.恢复所有群默认权限ToolStripMenuItem.Size = new System.Drawing.Size(304, 36);
            this.恢复所有群默认权限ToolStripMenuItem.Text = "恢复所有群默认权限";
            this.恢复所有群默认权限ToolStripMenuItem.Click += new System.EventHandler(this.恢复所有群默认权限ToolStripMenuItem_Click);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1452, 876);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainView";
            this.Text = "MainView";
            this.Load += new System.EventHandler(this.MainView_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox YunBlackList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox YunGroupCardKey;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox YunMessageKey;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox YunOcrKey;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox YunQrCodeKey;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RichTextBox YunAudioKey;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RichTextBox YunRedBagKey;
        private System.Windows.Forms.ListView GroupList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 恢复所有群默认权限ToolStripMenuItem;
    }
}