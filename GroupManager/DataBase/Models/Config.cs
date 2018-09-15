using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupManager.DataBase.Models
{
    public class Config
    {
        public long GroupId { get; set; }

        public bool Status { get; set; } = false;
        public bool ReplaceLine { get; set; } = true;
        public List<Permission.Permission> Permission { get; set; } = new List<Permission.Permission>()
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


        public Models.RedBagProtect RedBagProtect { get; set; } = new RedBagProtect();
        public int RedBagProtectTime { get; set; } = 15;


        public bool BlackList { get; set; } = true;
        public bool QunBlackListUseYunBlackList { get; set; } = true;
        public List<long> QunBlackList { get; set; } = new List<long>();
        public string QunBlackListStep { get; set; } = @"禁言、撤回";
        public uint QunBlackListSilencedTime { get; set; } = 2592000;
        public string QunBlackListResult { get; set; } = @"黑名单成员警告:[换行]成员:[@[QQ]][换行]黑名单类型:[黑名单类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";


        public bool MessageUnicode { get; set; } = true;
        public string MessageUnicodeStep { get; set; } = @"禁言、撤回";
        public uint MessageUnicodeSilencedTime { get; set; } = 2592000;
        public string MessageUnicodeResult { get; set; } = @"违规行为警告:[换行]成员:[@[QQ]][换行]违规类型:[违规类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";


        public bool GroupCard { get; set; } = true;
        public bool GroupCardUseYunKey { get; set; } = true;
        public List<GroupManager.Models.MatchKey> GroupCardKey { get; set; } = new List<GroupManager.Models.MatchKey>() { new GroupManager.Models.MatchKey() { Content = "快手" }, new GroupManager.Models.MatchKey() { Content = "孙笑川" } };
        public string GroupCardStep { get; set; } = @"禁言";
        public uint GroupCardSilencedTime { get; set; } = 2592000;
        public string GroupCardResult { get; set; } = @"违规行为警告:[换行]成员:[@[QQ]][换行]违规类型:[违规类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";


        public bool GroupCardUnicode { get; set; } = true;
        public string GroupCardUnicodeStep { get; set; } = @"禁言";
        public uint GroupCardUnicodeSilencedTime { get; set; } = 2592000;
        public string GroupCardUnicodeResult { get; set; } = @"违规行为警告:[换行]成员:[@[QQ]][换行]违规类型:[违规类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";



        public bool BrushScreen { get; set; } = true;
        public List<Controllers.ClusterControllers.Models.Message> BrushScreenData { get; set; } = new List<Controllers.ClusterControllers.Models.Message>();
        public int BrushScreenSecond { get; set; } = 5;
        public int BrushScreenCount { get; set; } = 4;
        public string BrushScreenStep { get; set; } = @"禁言、撤回";
        public uint BrushScreenSilencedTime { get; set; } = 2592000;
        public string BrushScreenResult { get; set; } = @"违规行为警告:[换行]成员:[@[QQ]][换行]违规类型:[违规类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";


        public bool Repeat { get; set; } = true;
        public int RepeatCount { get; set; } = 4;
        public string RepeatStep { get; set; } = @"禁言、撤回";
        public uint RepeatSilencedTime { get; set; } = 2592000;
        public string RepeatResult { get; set; } = @"违规行为警告:[换行]成员:[@[QQ]][换行]违规类型:[违规类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";


        public bool Rhythm { get; set; } = true;
        public bool RhythmRandom { get; set; } = true;
        public Controllers.ClusterControllers.Models.Rhythm RhythmData { get; set; } = new Controllers.ClusterControllers.Models.Rhythm();
        public int RhythmCount { get; set; } = 4;
        public string RhythmStep { get; set; } = "禁言、撤回";
        public uint RhythmSilencedTime { get; set; } = 2592000;
        public string RhythmRandomResult { get; set; } = @"检测到大量复读姬,系统将会随机选取[数量]名复读姬进行拆除！";
        public string RhythmResult { get; set; } = @"违规行为警告:[换行]成员:[@[QQ]][换行]违规类型:[违规类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";


        public bool MessageKey { get; set; } = true;
        public bool MessageKeyUseYunMessageKey { get; set; } = true;
        public List<GroupManager.Models.MatchKey> MessageKeys { get; set; } = new List<GroupManager.Models.MatchKey>() { new GroupManager.Models.MatchKey() { Content = "快手" }, new GroupManager.Models.MatchKey() { Content = "孙笑川" } };
        public string MessageKeyStep { get; set; } = @"禁言、撤回";
        public uint MessageKeySilencedTime { get; set; } = 2592000;
        public string MessageKeyResult { get; set; } = @"违规行为警告:[换行]成员:[@[QQ]][换行]违规类型:[违规类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";


        public string YoutuAppId { get; set; } = "10108179";
        public string YoutuSecretId { get; set; } = "AKIDsspcKvWtPZuGh4KI8nInMzhVMGEA9EMY";
        public string YoutuSecretKey { get; set; } = "SzdMPWpCujdvEZwT3mcYZdm1NpUE0qhb";
        public string YoutuQQ { get; set; } = "656469762";


        public bool YoutuOcr { get; set; } = true;
        public bool YoutuOcrUseYunOcrKey { get; set; } = true;
        public List<GroupManager.Models.MatchKey> YoutuOcrKey { get; set; } = new List<GroupManager.Models.MatchKey>() { new GroupManager.Models.MatchKey() { Content = "快手" }, new GroupManager.Models.MatchKey() { Content = "孙笑川" } };
        public string YoutuOcrStep { get; set; } = @"禁言、撤回";
        public uint YoutuOcrSilencedTime { get; set; } = 2592000;
        public string YoutuOcrResult { get; set; } = @"违规行为警告:[换行]成员:[@[QQ]][换行]违规类型:[违规类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";


        public bool YoutuPorn { get; set; } = true;
        public int YoutuPornValue { get; set; } = 90;
        public string YoutuPornStep { get; set; } = @"禁言、撤回";
        public uint YoutuPornSilencedTime { get; set; } = 2592000;
        public string YoutuPornResult { get; set; } = @"违规行为警告:[换行]成员:[@[QQ]][换行]违规类型:[违规类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";


        public bool QrCode { get; set; } = true;
        public bool QrCode_S { get; set; } = true;
        public bool QrCodeUseYunQrCodeKey { get; set; } = true;
        public List<GroupManager.Models.MatchKey> QrCodeKey { get; set; } = new List<GroupManager.Models.MatchKey>() { new GroupManager.Models.MatchKey() { Content = "快手" }, new GroupManager.Models.MatchKey() { Content = "孙笑川" } };
        public string QrCodeStep { get; set; } = @"禁言、撤回";
        public uint QrCodeSilencedTime { get; set; } = 2592000;
        public string QrCodeResult { get; set; } = @"违规行为警告:[换行]成员:[@[QQ]][换行]违规类型:[违规类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";


        public string BaiduApiKey { get; set; } = "bLQ9Z18pMP3aSHLM84nXfakN";
        public string BaiduSecretKey { get; set; } = "db65325950fd9349e84a6f473cbacec1";


        public bool AudioKey { get; set; } = true;
        public bool AudioKeyUseYunAudioKey { get; set; } = true;
        public List<GroupManager.Models.MatchKey> AudioKeys { get; set; } = new List<GroupManager.Models.MatchKey>() { new GroupManager.Models.MatchKey() { Content = "快手" }, new GroupManager.Models.MatchKey() { Content = "孙笑川" } };
        public string AudioKeyStep { get; set; } = @"禁言、撤回";
        public uint AudioKeySilencedTime { get; set; } = 2592000;
        public string AudioKeyResult { get; set; } = @"违规行为警告:[换行]成员:[@[QQ]][换行]违规类型:[违规类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";


        public bool RedBag { get; set; } = true;
        public int RedBagAmount { get; set; } = 1000;
        public bool RedBag_S { get; set; } = false;
        public bool RedBagUseYunRedBagKey { get; set; } = true;
        public List<GroupManager.Models.MatchKey> RedBagKey { get; set; } = new List<GroupManager.Models.MatchKey>() { new GroupManager.Models.MatchKey() { Content = "快手" }, new GroupManager.Models.MatchKey() { Content = "孙笑川" } };
        public string RedBagKeyStep { get; set; } = @"禁言";
        public uint RedBagSilencedTime { get; set; } = 2592000;
        public string RedBagResult { get; set; } = @"违规行为警告:[换行]成员:[@[QQ]][换行]违规类型:[违规类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";


        public bool ExitClusterAddBlackList { get; set; } = true;
        public bool ExitAdminAddBlackList { get; set; } = true;
        public bool ExitClusterAddYunBlackList { get; set; } = true;
        public bool ExitAdminAddYunBlackList { get; set; } = true;
        public string ExitClusterResult { get; set; } = @"[昵称或名片]([QQ])退出了本群已被系统加入[黑名单类型]";
        public string ExitAdminResult { get; set; } = @"[昵称或名片]([QQ])被[@操作管理员]移出了本群已被系统加入[黑名单类型]";


        public bool BlackListJoin { get; set; } = true;
        public bool BlackListJoinReject { get; set; } = true;
        public string BlackListJoinStep { get; set; } = @"禁言";
        public uint BlackListJoinSilencedTime { get; set; } = 2592000;
        public string BlackListJoinResult_N { get; set; } = @"检测到黑名单:[换行]成员:[@[QQ]][换行]黑名单类型:[黑名单类型][换行]执行操作:[执行操作][换行][换行]如有疑问请进行申述";
        public string BlackListJoinResult_R { get; set; } = @"请联系群主、管理解除黑名单";


        public bool Welcome { get; set; } = true;
        public bool WelcomeSilenced { get; set; } = true;
        public uint WelcomeSilencedTime { get; set; } = 180;
        public string WelcomeSilencedResult { get; set; } = @"欢迎[@[QQ]]进群，请花[禁言时间]分钟时间观看群公告、群文件群规";
        public string WelcomeResult { get; set; } = @"欢迎[@[QQ]]加入了本群";


        public bool Level { get; set; } = true;
        public bool LevelReject { get; set; } = true;
        public bool LevelSilenced { get; set; } = true;
        public int LevelMin { get; set; } = 20;
        public string LevelStep { get; set; } = "禁言";
        public uint LevelSilencedTime { get; set; } = 2592000;
        public string LevelResult_R { get; set; } = @"QQ等级隐藏或低于[最低等级]级禁止入群";
        public string LevelResult_N { get; set; } = @"检测到疑似小号[换行]成员:[@[QQ]][换行]QQ等级:[QQ等级] 级[换行]入群最低等级:[最低等级] 级[换行][换行]如有疑问请进行申述";
        public List<long> LevelWhiteList { get; set; } = new List<long>();


        public bool Warning { get; set; } = true;
        public bool WarnSilenced { get; set; } = true;
        public List<GroupManager.Models.MatchKey> WarningKey { get; set; } = new List<GroupManager.Models.MatchKey>() { new GroupManager.Models.MatchKey() { Content = "快手" }, new GroupManager.Models.MatchKey() { Content = "孙笑川" } };
        public int WarningCount { get; set; } = 5;
        public bool WarningAddToBlack { get; set; } = true;
        public bool WarningAddToYunBlack { get; set; } = true;
        public string WarningResult_N { get; set; } = @"违规行为警告:[换行]成员:[@[QQ]][换行]违规类型:[违规类型][换行]执行操作:[执行操作][换行]已被警告:[警告次数] 次[换行][换行]达到[最大警告次数]将会被加入黑名单[换行]如有疑问请进行申述";
        public string WarningResult_S { get; set; } = @"[换行]已被警告:[警告次数] 次[换行]达到[最大警告次数]将会被加入黑名单";
    }
}
