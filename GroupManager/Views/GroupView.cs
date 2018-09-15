using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupManager.Views
{
    public partial class GroupView : Form
    {
        public DataBase.Models.Config Config;
        public GroupView(DataBase.Models.Config config)
        {
            this.Config = config;
            InitializeComponent();
        }

        private void GroupView_Load(object sender, EventArgs e)
        {
            LoadPermission();

            //----系统设定绑定数据----
            this.Status.DataBindings.Add("Checked", Config, "Status", true, DataSourceUpdateMode.OnPropertyChanged);
            this.ReplaceLine.DataBindings.Add("Checked", Config, "ReplaceLine", true, DataSourceUpdateMode.OnPropertyChanged);
            //----系统设定绑定数据----

            //----黑名单设定绑定数据----
            this.BlackList.DataBindings.Add("Checked", Config, "BlackList", true, DataSourceUpdateMode.OnPropertyChanged);
            this.QunBlackListUseYunBlackList.DataBindings.Add("Checked", Config, "QunBlackListUseYunBlackList", true, DataSourceUpdateMode.OnPropertyChanged);
            this.QunBlackList.Text = string.Join(",", Config.QunBlackList);//无绑定需要手动保存
            this.QunBlackListStep.DataBindings.Add("Text", Config, "QunBlackListStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.QunBlackListSilencedTime.DataBindings.Add("Value", Config, "QunBlackListSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.QunBlackListResult.DataBindings.Add("Text", Config, "QunBlackListResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----黑名单设定绑定数据----

            //----Unicode设定绑定数据----
            this.MessageUnicode.DataBindings.Add("Checked", Config, "MessageUnicode", true, DataSourceUpdateMode.OnPropertyChanged);
            this.MessageUnicodeStep.DataBindings.Add("Text", Config, "MessageUnicodeStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.MessageUnicodeSilencedTime.DataBindings.Add("Value", Config, "MessageUnicodeSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.MessageUnicodeResult.DataBindings.Add("Text", Config, "MessageUnicodeResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----Unicode设定绑定数据----

            //----群名片设定绑定数据
            this.GroupCard.DataBindings.Add("Checked", Config, "GroupCard", true, DataSourceUpdateMode.OnPropertyChanged);
            this.GroupCardUseYunKey.DataBindings.Add("Checked", Config, "GroupCardUseYunKey", true, DataSourceUpdateMode.OnPropertyChanged);
            this.GroupCardKey.Text = string.Join("\r\n", Config.GroupCardKey.Select(x => x.Content).ToList());//无绑定需要手动保存
            this.GroupCardStep.DataBindings.Add("Text", Config, "GroupCardStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.GroupCardSilencedTime.DataBindings.Add("Value", Config, "GroupCardSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.GroupCardResult.DataBindings.Add("Text", Config, "GroupCardResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----群名片设定绑定数据----

            //----Unicode群名片设定绑定数据----
            this.GroupCardUnicode.DataBindings.Add("Checked", Config, "GroupCardUnicode", true, DataSourceUpdateMode.OnPropertyChanged);
            this.GroupCardUnicodeStep.DataBindings.Add("Text", Config, "GroupCardUnicodeStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.GroupCardUnicodeSilencedTime.DataBindings.Add("Value", Config, "GroupCardUnicodeSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.GroupCardUnicodeResult.DataBindings.Add("Text", Config, "GroupCardUnicodeResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----Unicode群名片设定绑定数据----

            //----刷屏设定绑定数据----
            this.BrushScreen.DataBindings.Add("Checked", Config, "BrushScreen", true, DataSourceUpdateMode.OnPropertyChanged);
            this.BrushScreenSecond.DataBindings.Add("Value", Config, "BrushScreenSecond", true, DataSourceUpdateMode.OnPropertyChanged);
            this.BrushScreenCount.DataBindings.Add("Text", Config, "BrushScreenCount", true, DataSourceUpdateMode.OnPropertyChanged);
            this.BrushScreenStep.DataBindings.Add("Text", Config, "BrushScreenStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.BrushScreenSilencedTime.DataBindings.Add("Value", Config, "BrushScreenSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.BrushScreenResult.DataBindings.Add("Text", Config, "BrushScreenResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----刷屏设定绑定数据----

            //----重复发言设定绑定数据----
            this.Repeat.DataBindings.Add("Checked", Config, "Repeat", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RepeatCount.DataBindings.Add("Value", Config, "RepeatCount", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RepeatStep.DataBindings.Add("Text", Config, "RepeatStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RepeatSilencedTime.DataBindings.Add("Value", Config, "RepeatSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RepeatResult.DataBindings.Add("Text", Config, "RepeatResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----重复发言设定绑定数据----

            //----复读姬设定绑定数据----
            this.Rhythm.DataBindings.Add("Checked", Config, "Rhythm", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RhythmRandom.DataBindings.Add("Checked", Config, "RhythmRandom", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RhythmCount.DataBindings.Add("Value", Config, "RhythmCount", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RhythmStep.DataBindings.Add("Text", Config, "RhythmStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RhythmSilencedTime.DataBindings.Add("Value", Config, "RhythmSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RhythmRandomResult.DataBindings.Add("Text", Config, "RhythmRandomResult", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RhythmResult.DataBindings.Add("Text", Config, "RhythmResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----复读姬设定绑定数据----

            //----关键词设定绑定数据----
            this.MessageKey.DataBindings.Add("Checked", Config, "MessageKey", true, DataSourceUpdateMode.OnPropertyChanged);
            this.MessageKeyUseYunMessageKey.DataBindings.Add("Checked", Config, "MessageKeyUseYunMessageKey", true, DataSourceUpdateMode.OnPropertyChanged);
            this.MessageKeys.Text = string.Join("\r\n", Config.MessageKeys.Select(x => x.Content).ToList());
            this.MessageKeyStep.DataBindings.Add("Text", Config, "MessageKeyStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.MessageKeySilencedTime.DataBindings.Add("Value", Config, "MessageKeySilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.MessageKeyResult.DataBindings.Add("Text", Config, "MessageKeyResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----关键词设定绑定数据----

            //----腾讯API绑定数据----
            this.YoutuAppId.DataBindings.Add("Text", Config, "YoutuAppId", true, DataSourceUpdateMode.OnPropertyChanged);
            this.YoutuQQ.DataBindings.Add("Text", Config, "YoutuQQ", true, DataSourceUpdateMode.OnPropertyChanged);
            this.YoutuSecretId.DataBindings.Add("Text", Config, "YoutuSecretId", true, DataSourceUpdateMode.OnPropertyChanged);
            this.YoutuSecretKey.DataBindings.Add("Text", Config, "YoutuSecretKey", true, DataSourceUpdateMode.OnPropertyChanged);
            //----腾讯API绑定数据----

            //----OCR检测绑定数据----
            this.YoutuOcr.DataBindings.Add("Checked", Config, "YoutuOcr", true, DataSourceUpdateMode.OnPropertyChanged);
            this.YoutuOcrUseYunOcrKey.DataBindings.Add("Checked", Config, "YoutuOcrUseYunOcrKey", true, DataSourceUpdateMode.OnPropertyChanged);
            this.YoutuOcrKey.Text = string.Join("\r\n", Config.YoutuOcrKey.Select(x => x.Content).ToList());//无绑定需要手动保存
            this.YoutuOcrStep.DataBindings.Add("Text", Config, "YoutuOcrStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.YoutuOcrSilencedTime.DataBindings.Add("Value", Config, "YoutuOcrSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.YoutuOcrResult.DataBindings.Add("Text", Config, "YoutuOcrResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----OCR检测绑定数据

            //----鉴黄检测绑定数据----
            this.YoutuPorn.DataBindings.Add("Checked", Config, "YoutuPorn", true, DataSourceUpdateMode.OnPropertyChanged);
            this.YoutuPornValue.DataBindings.Add("Value", Config, "YoutuPornValue", true, DataSourceUpdateMode.OnPropertyChanged);
            this.YoutuPornStep.DataBindings.Add("Text", Config, "YoutuPornStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.YoutuPornSilencedTime.DataBindings.Add("Value", Config, "YoutuPornSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.YoutuPornResult.DataBindings.Add("Text", Config, "YoutuPornResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----鉴黄检测绑定数据----

            //----二维码检测绑定数据----
            this.QrCode.DataBindings.Add("Checked", Config, "QrCode", true, DataSourceUpdateMode.OnPropertyChanged);
            this.QrCode_S.DataBindings.Add("Checked", Config, "QrCode_S", true, DataSourceUpdateMode.OnPropertyChanged);
            this.QrCodeUseYunQrCodeKey.DataBindings.Add("Checked", Config, "QrCodeUseYunQrCodeKey", true, DataSourceUpdateMode.OnPropertyChanged);
            this.QrCodeKey.Text = string.Join("\r\n", Config.QrCodeKey.Select(x => x.Content).ToList());//无绑定需要手动保存
            this.QrCodeStep.DataBindings.Add("Text", Config, "QrCodeStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.QrCodeSilencedTime.DataBindings.Add("Value", Config, "QrCodeSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.QrCodeResult.DataBindings.Add("Text", Config, "QrCodeResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----二维码检测绑定数据----

            //----百度API绑定数据----
            this.BaiduApiKey.DataBindings.Add("Text", Config, "BaiduApiKey", true, DataSourceUpdateMode.OnPropertyChanged);
            this.BaiduSecretKey.DataBindings.Add("Text", Config, "BaiduSecretKey", true, DataSourceUpdateMode.OnPropertyChanged);
            //----百度API绑定数据----

            //----语音检测绑定数据----
            this.AudioKey.DataBindings.Add("Checked", Config, "AudioKey", true, DataSourceUpdateMode.OnPropertyChanged);
            this.AudioKeyUseYunAudioKey.DataBindings.Add("Checked", Config, "AudioKeyUseYunAudioKey", true, DataSourceUpdateMode.OnPropertyChanged);
            this.AudioKeys.Text = string.Join("\r\n", Config.AudioKeys.Select(x => x.Content).ToList());//无绑定需要手动保存
            this.AudioKeyStep.DataBindings.Add("Text", Config, "AudioKeyStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.AudioKeySilencedTime.DataBindings.Add("Value", Config, "AudioKeySilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.AudioKeyResult.DataBindings.Add("Text", Config, "AudioKeyResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----语音检测绑定数据----

            //----红包检测绑定数据----
            this.RedBag.DataBindings.Add("Checked", Config, "RedBag", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RedBagAmount.DataBindings.Add("Value", Config, "RedBagAmount", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RedBag_S.DataBindings.Add("Checked", Config, "RedBag_S", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RedBagUseYunRedBagKey.DataBindings.Add("Checked", Config, "RedBagUseYunRedBagKey", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RedBagKey.Text = string.Join("\r\n", Config.RedBagKey.Select(x => x.Content).ToList());//无绑定需要手动保存
            this.RedBagKeyStep.DataBindings.Add("Text", Config, "RedBagKeyStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RedBagSilencedTime.DataBindings.Add("Value", Config, "RedBagSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.RedBagResult.DataBindings.Add("Text", Config, "RedBagResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----红包检测绑定数据----

            //----退群加黑绑定数据----
            this.ExitClusterAddBlackList.DataBindings.Add("Checked", Config, "ExitClusterAddBlackList", true, DataSourceUpdateMode.OnPropertyChanged);
            this.ExitClusterAddYunBlackList.DataBindings.Add("Checked", Config, "ExitClusterAddYunBlackList", true, DataSourceUpdateMode.OnPropertyChanged);
            this.ExitClusterResult.DataBindings.Add("Text", Config, "ExitClusterResult", true, DataSourceUpdateMode.OnPropertyChanged);
            this.ExitAdminAddBlackList.DataBindings.Add("Checked", Config, "ExitAdminAddBlackList", true, DataSourceUpdateMode.OnPropertyChanged);
            this.ExitAdminAddYunBlackList.DataBindings.Add("Checked", Config, "ExitAdminAddYunBlackList", true, DataSourceUpdateMode.OnPropertyChanged);
            this.ExitAdminResult.DataBindings.Add("Text", Config, "ExitAdminResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----退群加黑绑定数据----

            //----进群黑名单检测绑定数据----
            this.BlackListJoin.DataBindings.Add("Checked", Config, "BlackListJoin", true, DataSourceUpdateMode.OnPropertyChanged);
            this.BlackListJoinReject.DataBindings.Add("Checked", Config, "BlackListJoinReject", true, DataSourceUpdateMode.OnPropertyChanged);
            this.BlackListJoinStep.DataBindings.Add("Text", Config, "BlackListJoinStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.BlackListJoinSilencedTime.DataBindings.Add("Value", Config, "BlackListJoinSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.BlackListJoinResult_S.DataBindings.Add("Text", Config, "BlackListJoinResult_N", true, DataSourceUpdateMode.OnPropertyChanged);
            this.BlackListJoinResult_R.DataBindings.Add("Text", Config, "BlackListJoinResult_R", true, DataSourceUpdateMode.OnPropertyChanged);
            //----进群黑名单检测绑定数据----

            //----红包保护时间绑定数据----
            this.RedBagProtectTime.DataBindings.Add("Value", Config, "RedBagProtectTime", true, DataSourceUpdateMode.OnPropertyChanged);
            //----红包保护时间绑定数据----

            //----加群欢迎绑定数据----
            this.Welcome.DataBindings.Add("Checked", Config, "Welcome", true, DataSourceUpdateMode.OnPropertyChanged);
            this.WelcomeSilenced.DataBindings.Add("Checked", Config, "WelcomeSilenced", true, DataSourceUpdateMode.OnPropertyChanged);
            this.WelcomeSilencedTime.DataBindings.Add("Value", Config, "WelcomeSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.WelcomeSilencedResult.DataBindings.Add("Text", Config, "WelcomeSilencedResult", true, DataSourceUpdateMode.OnPropertyChanged);
            this.WelcomeResult.DataBindings.Add("Text", Config, "WelcomeResult", true, DataSourceUpdateMode.OnPropertyChanged);
            //----加群欢迎绑定数据----

            //----等级设定绑定数据----
            this.Level.DataBindings.Add("Checked", Config, "Level", true, DataSourceUpdateMode.OnPropertyChanged);
            this.LevelReject.DataBindings.Add("Checked", Config, "LevelReject", true, DataSourceUpdateMode.OnPropertyChanged);
            this.LevelSilenced.DataBindings.Add("Checked", Config, "LevelSilenced", true, DataSourceUpdateMode.OnPropertyChanged);
            this.LevelMin.DataBindings.Add("Value", Config, "LevelMin", true, DataSourceUpdateMode.OnPropertyChanged);
            this.LevelStep.DataBindings.Add("Text", Config, "LevelStep", true, DataSourceUpdateMode.OnPropertyChanged);
            this.LevelSilencedTime.DataBindings.Add("Value", Config, "LevelSilencedTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.LevelResult_R.DataBindings.Add("Text", Config, "LevelResult_R", true, DataSourceUpdateMode.OnPropertyChanged);
            this.LevelResult_N.DataBindings.Add("Text", Config, "LevelResult_N", true, DataSourceUpdateMode.OnPropertyChanged);
            this.LevelWhiteList.Text = string.Join(",", Config.LevelWhiteList);
            //----等级设定绑定数据----

            //----警告设定绑定数据----
            this.Warning.DataBindings.Add("Checked", Config, "Warning", true, DataSourceUpdateMode.OnPropertyChanged);
            this.WarnSilenced.DataBindings.Add("Checked", Config, "WarnSilenced", true, DataSourceUpdateMode.OnPropertyChanged);
            this.WarningKey.Text = string.Join("\r\n", Config.WarningKey.Select(x => x.Content).ToList());
            this.WarningCount.DataBindings.Add("Value", Config, "WarningCount", true, DataSourceUpdateMode.OnPropertyChanged);
            this.WarningAddToBlack.DataBindings.Add("Checked", Config, "WarningAddToBlack", true, DataSourceUpdateMode.OnPropertyChanged);
            this.WarningAddToYunBlack.DataBindings.Add("Checked", Config, "WarningAddToYunBlack", true, DataSourceUpdateMode.OnPropertyChanged);
            this.WarningResult_N.DataBindings.Add("Text", Config, "WarningResult_N", true, DataSourceUpdateMode.OnPropertyChanged);
            //----警告设定绑定数据----
        }

        public void LoadPermission()
        {
            //----权限设定绑定数据----
            this.Permission.Items.Clear();
            foreach (var x in Config.Permission)
            {
                this.Permission.Items.Add(new ListViewItem(new string[] { x.UUID, x.Content, x.PermissionType.ToString() })).Tag = x;
            }
            this.Permission.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            //----权限设定绑定数据----
        }

        private void GroupView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.QunBlackList = this.QunBlackList.Text.Split(new string[] { ",", "，" }, StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
            Config.GroupCardKey = this.GroupCardKey.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Models.MatchKey { Content = x }).ToList();
            Config.YoutuOcrKey = this.YoutuOcrKey.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Models.MatchKey { Content = x }).ToList();
            Config.QrCodeKey = this.QrCodeKey.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Models.MatchKey { Content = x }).ToList();
            Config.AudioKeys = this.AudioKeys.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Models.MatchKey { Content = x }).ToList();
            Config.RedBagKey = this.RedBagKey.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Models.MatchKey { Content = x }).ToList();
            Config.MessageKeys = this.MessageKeys.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Models.MatchKey { Content = x }).ToList();
            Config.LevelWhiteList = this.LevelWhiteList.Text.Split(new string[] { ",", "，" }, StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
            Config.WarningKey = this.WarningKey.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Models.MatchKey { Content = x }).ToList();

            DataBase.db.SaveClusterConfig(Config);
        }

        private void Permission_DoubleClick(object sender, EventArgs e)
        {
            if (this.Permission.SelectedItems.Count > 0)
                new PermissionView(this.Permission.SelectedItems[this.Permission.SelectedItems.Count - 1].Tag as Permission.Permission, this).ShowDialog();
        }

        private void 恢复默认ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config.Permission = new List<Permission.Permission>()
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
            LoadPermission();
        }
    }
}
