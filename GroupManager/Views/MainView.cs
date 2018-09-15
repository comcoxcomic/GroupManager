using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Coco.Framework.SDK;
using Coco.Framework.Entities;

namespace GroupManager.Views
{
    public partial class MainView : Form
    {
        protected internal Plugin Client;
        Config Conf = PluginConfig.Init<Config>();
        public MainView(Plugin plugin)
        {
            this.Client = plugin;
            InitializeComponent();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            this.YunBlackList.Text = string.Join(",", Conf.YunBlackList);
            this.YunGroupCardKey.Text = string.Join("\r\n", Conf.YunGroupCardKey.Select(x => x.Content).ToList());
            this.YunMessageKey.Text = string.Join("\r\n", Conf.YunMessageKey.Select(x => x.Content).ToList());
            this.YunOcrKey.Text = string.Join("\r\n", Conf.YunOcrKey.Select(x => x.Content).ToList());
            this.YunQrCodeKey.Text = string.Join("\r\n", Conf.YunQrCodeKey.Select(x => x.Content).ToList());
            this.YunAudioKey.Text = string.Join("\r\n", Conf.YunAudioKey.Select(x => x.Content).ToList());
            this.YunRedBagKey.Text = string.Join("\r\n", Conf.YunRedBagKey.Select(x => x.Content).ToList());

            foreach (var x in this.Client.User.Clusters)
            {
                bool IsAdmin = x.members.Find(m => m.QQ == this.Client.User.QQ).IsAdmin();
                var item = new ListViewItem(new string[] { x.ExternalId.ToString(), x.Name, $"{x.members.Find(m => m.QQ == x.Creator).CardOrName}({x.Creator})", IsAdmin ? "是" : "否" });
                item.Tag = x;
                if (IsAdmin)
                    item.ForeColor = Color.Red;
                this.GroupList.Items.Add(item);
            }
            this.GroupList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conf.YunBlackList = this.YunBlackList.Text.Split(new string[] { ",", "，" }, StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
            Conf.YunGroupCardKey = this.YunGroupCardKey.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Models.MatchKey() { Content = x }).ToList();
            Conf.YunMessageKey = this.YunMessageKey.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Models.MatchKey { Content = x }).ToList();
            Conf.YunOcrKey = this.YunOcrKey.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Models.MatchKey { Content = x }).ToList();
            Conf.YunQrCodeKey = this.YunQrCodeKey.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Models.MatchKey { Content = x }).ToList();
            Conf.YunAudioKey = this.YunAudioKey.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Models.MatchKey { Content = x }).ToList();
            Conf.YunRedBagKey = this.YunRedBagKey.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Models.MatchKey { Content = x }).ToList();

            Conf.Save();
            MessageBox.Show("保存成功");
        }

        private void GroupList_DoubleClick(object sender, EventArgs e)
        {
            if (this.GroupList.SelectedItems.Count > 0)
                new GroupView(DataBase.db.GetClusterConfig((this.GroupList.SelectedItems[this.GroupList.SelectedItems.Count - 1].Tag as Cluster).ExternalId)).ShowDialog();
        }

        private void 恢复所有群默认权限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var x in this.Client.User.Clusters)
            {
                var item = DataBase.db.GetClusterConfig(x.ExternalId);

                item.Permission = new List<Permission.Permission>()
                {
                    new Permission.Permission(){ UUID = "开启GM", Content = "开启GM", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "关闭GM", Content = "关闭GM", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "一键开启", Content = "一键开启", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "一键关闭", Content = "一键关闭", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看GM设定", Content = "查看GM设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看系统设定", Content = "查看系统设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看腾讯设定", Content = "查看腾讯设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看百度设定", Content = "查看百度设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看红包设定", Content = "查看红包设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看加群设定", Content = "查看加群设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看退群设定", Content = "查看退群设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看刷屏设定", Content = "查看刷屏设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看黑名单设定", Content = "查看黑名单设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看复读姬设定", Content = "查看复读姬设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看关键词设定", Content = "查看关键词设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看群名片设定", Content = "查看群名片设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看二维码设定", Content = "查看二维码设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看重复发言设定", Content = "查看重复发言设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看Unicode设定", Content = "查看Unicode设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查看等级设定", Content = "查看等级设定", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },

                    new Permission.Permission(){ UUID = "加黑名单", Content = "^加黑名单\\s*\\[?@?(?<QQ>\\d{5,11})\\]?$", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "删黑名单", Content = "^删黑名单\\s*\\[?@?(?<QQ>\\d{5,11})\\]?$", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "加全局黑名单", Content = "^加全局黑名单\\s*\\[?@?(?<QQ>\\d{5,11})\\]?$", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "删全局黑名单", Content = "^删全局黑名单\\s*\\[?@?(?<QQ>\\d{5,11})\\]?$", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "删所有黑名单", Content = "^删所有群黑名单\\s*\\[?@?(?<QQ>\\d{5,11})\\]?$", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "查询黑名单", Content = "^查询黑名单\\s*\\[?@?(?<QQ>\\d{5,11})\\]?$", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },

                    new Permission.Permission(){ UUID = "禁言", Content = "^禁言\\s*\\[?@?(?<QQ>\\d{5,11})\\]?(?<Time>.*?)$", PermissionType = GroupManager.Permission.PermissionType.群主_管理员},
                    new Permission.Permission(){ UUID = "解禁", Content = "^解禁.*?(\\d{5,11})(.*?)$", PermissionType = GroupManager.Permission.PermissionType.群主_管理员},

                    new Permission.Permission(){ UUID = "添加等级检测白名单", Content = "^添加等级检测白名单\\s*\\[?@?(?<QQ>\\d{5,10})\\]?$", PermissionType = GroupManager.Permission.PermissionType.群主 },
                    new Permission.Permission(){ UUID = "删除等级检测白名单", Content = "^删除等级检测白名单\\s*\\[?@?(?<QQ>\\d{5,10})\\]?$", PermissionType = GroupManager.Permission.PermissionType.群主 },

                    new Permission.Permission(){ UUID = "加警告", Content = "^加警告\\s*\\[?@?(?<QQ>\\d{5,11})\\]?$", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "减警告", Content = "^减警告\\s*\\[?@?(?<QQ>\\d{5,11})\\]?$", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "清空警告", Content = "^清空警告\\s*\\[?@?(?<QQ>\\d{5,11})\\]?$", PermissionType = GroupManager.Permission.PermissionType.群主 },

                    new Permission.Permission(){ UUID = "查看禁言", Content = "查看禁言", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    new Permission.Permission(){ UUID = "一键解禁", Content = "一键解禁", PermissionType = GroupManager.Permission.PermissionType.群主_管理员 },
                    
                };
                DataBase.db.SaveClusterConfig(item);
            }
            
            MessageBox.Show("恢复成功");
        }
    }
}
