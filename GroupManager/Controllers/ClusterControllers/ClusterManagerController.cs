using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Coco.Framework.SDK;
using GroupManager.API.Tencent;
using Coco.Framework.Utils;
using System.IO;
using Newtonsoft.Json;
using Baidu.Aip.Speech;
using Newtonsoft.Json.Linq;
using Coco.Framework.Entities;
using SufeiUtil;

namespace GroupManager.Controllers.ClusterControllers
{
    public class ClusterManagerController : MainManagerController
    {
        protected internal ReceiveClusterIMQQEventArgs e;
        DataBase.Models.Config Config;
        Config Conf;

        public ClusterManagerController(Plugin plugin, ReceiveClusterIMQQEventArgs e)
        {
            this.Client = plugin;
            this.e = e;

            Config = DataBase.db.GetClusterConfig(e.Cluster.ExternalId);
            Conf = PluginConfig.Init<Config>();

            if (Config.Status)
            {
                this.Client.GetImageFromMessage(this.Images, e.Message);
                if (this.Images.Count > 0)
                    this.Client.DownloadClusterImages(e.Cluster.ExternalId, e.Message);

                var m = Regex.Match(e.Message, @"\[声音=(?<audioName>.*?)(\/)?\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    this.Audio = Util.MapFile($"Audio\\{m.Groups["audioName"].Value}");
                    this.Client.DownloadClusterAudios(e.Cluster.ExternalId, e.Message);
                }

                if (!IsAdmin(e.Cluster.ExternalId, e.ClusterMember.QQ))
                {
                    var b_s_idx = Config.BrushScreenData.FindIndex(x => x.QQ == e.ClusterMember.QQ);
                    if (b_s_idx == -1)
                    {
                        Config.BrushScreenData.Add(new Models.Message() { QQ = e.ClusterMember.QQ });
                        b_s_idx = Config.BrushScreenData.FindIndex(x => x.QQ == e.ClusterMember.QQ);
                    }
                    Config.BrushScreenData[b_s_idx].Insert(new Models.SendData() { Content = e.Message, Sequence = e.Sequence, MessageId = e.MessageId, SendTime = e.SendTime });


                    Config.RhythmData.Insert(new Models.RhythmData() { QQ = e.ClusterMember.QQ, Count = 1, Message = new List<Models.RhythmMessage>() { new Models.RhythmMessage() { Content = e.Message, Sequence = e.Sequence, MessageId = e.MessageId, SendTime = e.SendTime } } });

                    DataBase.db.SaveClusterConfig(Config);
                }
            }
        }

        public void Manager()
        {
            if (!e.Cancel)
            {
                ManagerControl();
                //this.Client.OnLog("0");
            }
            if (Config.Status)
            {
                if (IsAdmin(e.Cluster.ExternalId, e.ClusterMember.QQ))
                    return;

                if (!e.Cancel)
                {
                    ManagerBlackList();
                    //this.Client.OnLog("1");
                }
                if (!e.Cancel)
                {
                    ManagerLevel();
                    //this.Client.OnLog("1.1");
                }
                if (!e.Cancel)
                {
                    ManagerBilibiliGroupCard();
                    //this.Client.OnLog("1.2");
                }
                if (!e.Cancel)
                {
                    ManagerMessageUnicode();
                    //this.Client.OnLog("2");
                }
                if (!e.Cancel)
                {
                    ManagerGroupCardKey();
                    //this.Client.OnLog("3");
                }
                if (!e.Cancel)
                {
                    ManagerGroupCardUnicode();
                    //this.Client.OnLog("4");
                }
                if (!e.Cancel)
                {
                    ManagerBrushSceen();
                    //this.Client.OnLog("5");
                }
                if (!e.Cancel)
                {
                    ManagerRepeat();
                    //this.Client.OnLog("6");
                }
                if (!e.Cancel)
                {
                    ManagerRhythm();
                    //this.Client.OnLog("7");
                }
                if (!e.Cancel)
                {
                    ManagerMessageKey();
                    //this.Client.OnLog("8");
                }
                if (!e.Cancel)
                {
                    ManagerWarning();
                    //this.Client.OnLog("8.1");
                }
                if (!e.Cancel)
                {
                    ManagerQrCode();
                    //this.Client.OnLog("9");
                }
                if (!e.Cancel)
                {
                    ManagerAudioKey();
                    //this.Client.OnLog("10");
                }
                if (!e.Cancel)
                {
                    ManagerTencentYoutu(this.Images);
                    //this.Client.OnLog("11");
                }

                foreach (var x in this.Images)
                {
                    try
                    {
                        File.Delete(Util.MapFile($"Image\\{x}"));
                    }
                    catch { continue; }
                }

                if (!string.IsNullOrWhiteSpace(this.Audio))
                {
                    try
                    {
                        File.Delete(this.Audio);
                    }
                    catch { }
                }
            }
        }

        private void ManagerControl()
        {
            if (HasPermission(GetPermission("开启GM")))
            {
                if (GetPermission("开启GM").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    Config.Status = true;
                    DataBase.db.SaveClusterConfig(Config);
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, "开启GM成功");
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("关闭GM")))
            {
                if (GetPermission("关闭GM").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    Config.Status = false;
                    DataBase.db.SaveClusterConfig(Config);
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, "关闭GM成功");
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("一键开启")))
            {
                if (GetPermission("一键开启").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    this.Control(true);
                    DataBase.db.SaveClusterConfig(Config);
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, "一键开启成功");
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("一键关闭")))
            {
                if (GetPermission("一键关闭").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    this.Control(false);
                    DataBase.db.SaveClusterConfig(Config);
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, "一键关闭成功");
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看GM设定")))
            {
                if (GetPermission("查看GM设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, @"请按照分类查看GM设定
系统设定->查看系统设定
腾讯->查看腾讯设定
百度->查看百度设定
红包->查看红包设定
加群->查看加群设定
退群->查看退群设定
刷屏->查看刷屏设定
等级->查看等级设定
黑名单->查看黑名单设定
复读姬->查看复读姬设定
关键词->查看关键词设定
群名片->查看群名片设定
二维码->查看二维码设定
重复发言->查看重复发言设定
Unicode->查看Unicode设定");
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看系统设定")))
            {
                if (GetPermission("查看系统设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
群开关=[群开关]
单行模式=[单行模式]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[群开关]", Config.Status ? "开" : "关")
                        .Replace("[单行模式]", Config.ReplaceLine ? "开" : "关"));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看腾讯设定")))
            {
                if (GetPermission("查看腾讯设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
OCR=[OCR]
OCR云词库=[OCR云词库]
OCR词库=[OCR词库]
OCR执行操作=[OCR执行操作]
OCR禁言时间=[OCR禁言时间]
OCR执行回复=[OCR执行回复]

鉴黄=[鉴黄]
鉴黄值=[鉴黄值]
鉴黄执行操作=[鉴黄执行操作]
鉴黄禁言时间=[鉴黄禁言时间]
鉴黄执行回复=[鉴黄执行回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[OCR]", Config.YoutuOcr ? "开" : "关")
                        .Replace("[OCR云词库]", Config.YoutuOcrUseYunOcrKey ? "开" : "关")
                        .Replace("[OCR词库]", string.Join("\r\n", Config.YoutuOcrKey.Select(x => x.Content).ToList()))
                        .Replace("[OCR执行操作]", Config.YoutuOcrStep)
                        .Replace("[OCR禁言时间]", Config.YoutuOcrSilencedTime.ToString())
                        .Replace("[OCR执行回复]", Config.YoutuOcrResult)

                        .Replace("[鉴黄]", Config.YoutuPorn ? "开" : "关")
                        .Replace("[鉴黄值]", Config.YoutuPornValue.ToString())
                        .Replace("[鉴黄执行操作]", Config.YoutuPornStep)
                        .Replace("[鉴黄禁言时间]", Config.YoutuPornSilencedTime.ToString())
                        .Replace("[鉴黄执行回复]", Config.YoutuPornResult));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看百度设定")))
            {
                if (GetPermission("查看百度设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
语音识别=[语音识别]
语音识别云词库=[语音识别云词库]
语音识别词库=[语音识别词库]
语音识别执行操作=[语音识别执行操作]
语音识别禁言时间=[语音识别禁言时间]
语音识别执行回复=[语音识别执行回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[语音识别]", Config.AudioKey ? "开" : "关")
                        .Replace("[语音识别云词库]", Config.AudioKeyUseYunAudioKey ? "开" : "关")
                        .Replace("[语音识别词库]", string.Join("\r\n", Config.AudioKeys.Select(x => x.Content).ToList()))
                        .Replace("[语音识别执行操作]", Config.AudioKeyStep)
                        .Replace("[语音识别禁言时间]", Config.AudioKeySilencedTime.ToString())
                        .Replace("[语音识别执行回复]", Config.AudioKeyResult));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看红包设定")))
            {
                if (GetPermission("查看红包设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
红包=[红包]
红包保护时间=[红包保护时间]
红包最低金额=[红包最低金额]
禁止发送红包=[禁止发送红包]
红包云词库=[红包云词库]
红包词库=[红包词库]
红包执行操作=[红包执行操作]
红包禁言时间=[红包禁言时间]
红包执行回复=[红包执行回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[红包]", Config.RedBag ? "开" : "关")
                        .Replace("[红包保护时间]", Config.RedBagProtectTime.ToString())
                        .Replace("[红包最低金额]", Config.RedBagAmount.ToString())
                        .Replace("[禁止发送红包]", Config.RedBag_S ? "开" : "关")
                        .Replace("[红包云词库]", Config.RedBagUseYunRedBagKey ? "开" : "关")
                        .Replace("[红包执行操作]", Config.RedBagKeyStep)
                        .Replace("[红包禁言时间]", Config.RedBagSilencedTime.ToString())
                        .Replace("[红包执行回复]", Config.RedBagResult));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看加群设定")))
            {
                if (GetPermission("查看加群设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
加群欢迎=[加群欢迎]
首次加群禁言=[首次加群禁言]
首次加群禁言时间=[首次加群禁言时间]
首次加群禁言回复=[首次加群禁言回复]
加群欢迎回复=[加群欢迎回复]

黑名单加群禁言=[黑名单加群禁言]
黑名单加群拒绝=[黑名单加群拒绝]
黑名单加群执行操作=[黑名单加群执行操作]
黑名单加群禁言时间=[黑名单加群禁言时间]
黑名单加群拒绝回复=[黑名单加群拒绝回复]
黑名单加群禁言回复=[黑名单加群禁言回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[加群欢迎]", Config.Welcome ? "开" : "关")
                        .Replace("[首次加群禁言]", Config.WelcomeSilenced ? "开" : "关")
                        .Replace("[首次加群禁言时间]", Config.WelcomeSilencedTime.ToString())
                        .Replace("[首次加群禁言回复]", Config.WelcomeSilencedResult)
                        .Replace("[加群欢迎回复]", Config.WelcomeResult)

                        .Replace("[黑名单的加群禁言]", Config.BlackListJoin ? "开" : "关")
                        .Replace("[黑名单加群拒绝]", Config.BlackListJoinReject ? "开" : "关")
                        .Replace("[黑名单加群执行操作]", Config.BlackListJoinStep)
                        .Replace("[黑名单加群禁言时间]", Config.BlackListJoinSilencedTime.ToString())
                        .Replace("[黑名单加群拒绝回复]", Config.BlackListJoinResult_R)
                        .Replace("[黑名单加群禁言回复]", Config.BlackListJoinResult_N));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看退群设定")))
            {
                if (GetPermission("查看退群设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
退群加黑=[退群加黑]
退群云黑=[退群云黑]
退群加黑回复=[退群加黑回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[退群加黑]", Config.ExitClusterAddBlackList ? "开" : "关")
                        .Replace("[退群云黑]", Config.ExitClusterAddYunBlackList ? "开" : "关")
                        .Replace("[退群加黑回复]", Config.ExitClusterResult));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看刷屏设定")))
            {
                if (GetPermission("查看刷屏设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
刷屏=[刷屏]
刷屏判定时间=[刷屏判定时间]
刷屏判定条数=[刷屏判定条数]
刷屏执行操作=[刷屏执行操作]
刷屏禁言时间=[刷屏禁言时间]
刷屏执行回复=[刷屏执行回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[刷屏]", Config.BrushScreen ? "开" : "关")
                        .Replace("[刷屏判定时间]", Config.BrushScreenSecond.ToString())
                        .Replace("[刷屏判定条数]", Config.BrushScreenCount.ToString())
                        .Replace("[刷屏执行操作]", Config.BrushScreenStep)
                        .Replace("[刷屏禁言时间]", Config.BrushScreenSilencedTime.ToString())
                        .Replace("[刷屏执行回复]", Config.BrushScreenResult));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看黑名单设定")))
            {
                if (GetPermission("查看黑名单设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
黑名单=[黑名单]
云黑名单=[云黑名单]
黑名单执行操作=[黑名单执行操作]
黑名单禁言时间=[黑名单禁言时间]
黑名单执行回复=[黑名单执行回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[黑名单]", Config.BlackList ? "开" : "关")
                        .Replace("[云黑名单]", Config.QunBlackListUseYunBlackList ? "开" : "关")
                        .Replace("[黑名单执行操作]", Config.BlackListJoinStep)
                        .Replace("[黑名单禁言时间]", Config.QunBlackListSilencedTime.ToString())
                        .Replace("[黑名单执行回复]", Config.QunBlackListResult));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看复读姬设定")))
            {
                if (GetPermission("查看复读姬设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
复读姬=[复读姬]
复读姬随机禁言=[复读姬随机禁言]
复读姬判定条数=[复读姬判定条数]
复读姬执行操作=[复读姬执行操作]
复读姬禁言时间=[复读姬禁言时间]
复读姬随机回复=[复读姬随机回复]
复读姬执行回复=[复读姬执行回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[复读姬]", Config.Rhythm ? "开" : "关")
                        .Replace("[复读姬随机禁言]", Config.RhythmRandom ? "开" : "关")
                        .Replace("[复读姬判定条数]", Config.RhythmCount.ToString())
                        .Replace("[复读姬执行操作]", Config.RhythmStep)
                        .Replace("[复读姬禁言时间]", Config.RhythmSilencedTime.ToString())
                        .Replace("[复读姬随机回复]", Config.RhythmRandomResult)
                        .Replace("[复读姬执行回复]", Config.RhythmResult));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看关键词设定")))
            {
                if (GetPermission("查看关键词设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
关键词=[关键词]
关键词云词库=[关键词云词库]
关键词词库=[关键词词库]
关键词执行操作=[关键词执行操作]
关键词禁言时间=[关键词禁言时间]
关键词执行回复=[关键词执行回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[关键词]", Config.MessageKey ? "开" : "关")
                        .Replace("[关键词云词库]", Config.MessageKeyUseYunMessageKey ? "开" : "关")
                        .Replace("[关键词词库]", string.Join("\r\n", Config.MessageKeys.Select(x => x.Content).ToList()))
                        .Replace("[关键词执行操作]", Config.MessageKeyStep)
                        .Replace("[关键词禁言时间]", Config.MessageKeySilencedTime.ToString())
                        .Replace("[关键词执行回复]", Config.MessageKeyResult));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看群名片设定")))
            {
                if (GetPermission("查看群名片设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
群名片=[群名片]
群名片云词库=[群名片云词库]
群名片词库=[群名片词库]
群名片执行操作=[群名片执行操作]
群名片禁言时间=[群名片禁言时间]
群名片执行回复=[群名片执行回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[群名片]", Config.GroupCard ? "开" : "关")
                        .Replace("[群名片云词库]", Config.GroupCardUseYunKey ? "开" : "关")
                        .Replace("[群名片词库]", string.Join("\r\n", Config.GroupCardKey.Select(x => x.Content).ToList()))
                        .Replace("[群名片执行操作]", Config.GroupCardStep)
                        .Replace("[群名片禁言时间]", Config.GroupCardSilencedTime.ToString())
                        .Replace("[群名片执行回复]", Config.GroupCardResult));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看二维码设定")))
            {
                if (GetPermission("查看二维码设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
二维码=[二维码]
禁止发送二维码=[禁止发送二维码]
二维码云词库=[二维码云词库]
二维码词库=[二维码词库]
二维码执行操作=[二维码执行操作]
二维码禁言时间=[二维码禁言时间]
二维码执行回复=[二维码执行回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[二维码]", Config.QrCode ? "开" : "关")
                        .Replace("[禁止发送二维码]", Config.QrCode_S ? "开" : "关")
                        .Replace("[二维码云词库]", Config.QrCodeUseYunQrCodeKey ? "开" : "关")
                        .Replace("[二维码词库]", string.Join("\r\n", Config.QrCodeKey.Select(x => x.Content).ToList()))
                        .Replace("[二维码执行操作]", Config.QrCodeStep)
                        .Replace("[二维码禁言时间]", Config.QrCodeSilencedTime.ToString())
                        .Replace("[二维码执行回复]", Config.QrCodeResult));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看重复发言设定")))
            {
                if (GetPermission("查看重复发言设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
重复禁言=[重复禁言]
重复禁言判定条数=[重复禁言判定条数]
重复禁言执行操作=[重复禁言执行操作]
重复禁言禁言时间=[重复禁言禁言时间]
重复禁言执行回复=[重复禁言执行回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[重复禁言]", Config.Repeat ? "开" : "关")
                        .Replace("[重复禁言判断条数]", Config.RepeatCount.ToString())
                        .Replace("[重复禁言执行操作]", Config.RepeatStep)
                        .Replace("[重复禁言禁言时间]", Config.RepeatSilencedTime.ToString())
                        .Replace("[重复禁言执行回复]", Config.RepeatResult));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看Unicode设定")))
            {
                if (GetPermission("查看Unicode设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
Unicode消息=[Unicode消息]
Unicode消息执行操作=[Unicode消息执行操作]
Unicode消息禁言时间=[Unicode消息禁言时间]
Unicode消息执行回复=[Unicode消息执行回复]

Unicode名片=[Unicode名片]
Unicode名片执行操作=[Unicode名片执行操作]
Unicode名片禁言时间=[Unicode名片禁言时间]
Unicode名片执行回复=[Unicode名片执行回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[Unicode消息]", Config.MessageUnicode ? "开" : "关")
                        .Replace("[Unicode消息执行操作]", Config.MessageUnicodeStep)
                        .Replace("[Unicode消息禁言时间]", Config.MessageUnicodeSilencedTime.ToString())
                        .Replace("[Unicode消息执行回复]", Config.MessageUnicodeResult)

                        .Replace("[Unicode名片]", Config.GroupCardUnicode ? "开" : "关")
                        .Replace("[Unicode名片执行操作]", Config.GroupCardUnicodeStep)
                        .Replace("[Unicode名片禁言时间]", Config.GroupCardUnicodeSilencedTime.ToString())
                        .Replace("[Unicode名片执行回复]", Config.GroupCardUnicodeResult));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看等级设定")))
            {
                if (GetPermission("查看等级设定").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    string result = @"设置GM
等级=[等级]
等级入群限制=[等级入群限制]
等级限制发言=[等级限制发言]
等级最低限制=[等级最低限制]
等级执行操作=[等级执行操作]
等级禁言时间=[等级禁言时间]
等级入群限制回复=[等级入群限制回复]
等级执行回复=[等级执行回复]";
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, result
                        .Replace("[等级]", Config.Level ? "开" : "关")
                        .Replace("[等级入群限制]", Config.LevelReject ? "开" : "关")
                        .Replace("[等级限制发言]", Config.LevelSilenced ? "开" : "关")
                        .Replace("[等级最低限制]", Config.LevelMin.ToString())
                        .Replace("[等级执行操作]", Config.LevelStep)
                        .Replace("[等级禁言时间]", Config.LevelSilencedTime.ToString())
                        .Replace("[等级入群限制回复]", Config.LevelResult_R)
                        .Replace("[等级执行回复]", Config.LevelResult_N));
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查看禁言")))
            {
                if (GetPermission("查看禁言").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("本群当前被禁言人员列表：");
                    foreach (var x in GMUtil.Util.GetAllSilencedMember(e.Cluster.ExternalId, this.Client.User.QQ, this.Client.User.skey, this.Client.GetBkn(this.Client.User.skey)))
                    {
                        sb.AppendLine($"[@{x}]");
                    }
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, sb.ToString());
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("一键解禁")))
            {
                if (GetPermission("一键解禁").Content.Equals(e.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (var x in GMUtil.Util.GetAllSilencedMember(e.Cluster.ExternalId, this.Client.User.QQ, this.Client.User.skey, this.Client.GetBkn(this.Client.User.skey)))
                    {
                        this.Client.Silenced(e.Cluster.ExternalId, (uint)x, 0);
                    }
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("加黑名单")))
            {
                var match = Regex.Match(e.Message, GetPermission("加黑名单").Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    uint QQ = uint.Parse(match.Groups["QQ"].Value.Trim());
                    Config.QunBlackList.Add(QQ);
                    DataBase.db.SaveClusterConfig(Config);
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, "添加黑名单完成");
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("删黑名单")))
            {
                var match = Regex.Match(e.Message, GetPermission("删黑名单").Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    uint QQ = uint.Parse(match.Groups["QQ"].Value.Trim());
                    Config.QunBlackList.RemoveAll(x => x == QQ);
                    DataBase.db.SaveClusterConfig(Config);
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, "删除黑名单完成");
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("加全局黑名单")))
            {
                var match = Regex.Match(e.Message, GetPermission("加全局黑名单").Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    uint QQ = uint.Parse(match.Groups["QQ"].Value.Trim());
                    Conf.YunBlackList.Add(QQ);
                    Conf.Save();
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, "添加全局黑名单完成");
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("删全局黑名单")))
            {
                var match = Regex.Match(e.Message, GetPermission("删全局黑名单").Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    uint QQ = uint.Parse(match.Groups["QQ"].Value.Trim());
                    Conf.YunBlackList.RemoveAll(x => x == QQ);
                    Conf.Save();
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, "删除全局黑名单完成");
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("删所有黑名单")))
            {
                var match = Regex.Match(e.Message, GetPermission("删所有黑名单").Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    uint QQ = uint.Parse(match.Groups["QQ"].Value.Trim());
                    Conf.YunBlackList.RemoveAll(x => x == QQ);
                    Conf.Save();

                    foreach (var x in DataBase.db.GetClusterConfig())
                    {
                        var item = DataBase.db.GetClusterConfig(x.GroupId);
                        if (item.QunBlackList.Contains(QQ))
                        {
                            item.QunBlackList.RemoveAll(m => m == QQ);
                            DataBase.db.SaveClusterConfig(item);
                        }
                    }
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, "删除所有黑名单完成");
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("查询黑名单")))
            {
                var match = Regex.Match(e.Message, GetPermission("查询黑名单").Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    uint QQ = uint.Parse(match.Groups["QQ"].Value.Trim());
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"成员：[@{QQ}]");
                    sb.AppendLine($"云黑名单：{(Conf.YunBlackList.Contains(QQ) ? "是" : "否")}");
                    sb.AppendLine($"群黑名单：{(Config.QunBlackList.Contains(QQ) ? "是" : "否")}");
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, sb.ToString());
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("禁言")))
            {
                var match = Regex.Match(e.Message, GetPermission("禁言").Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    uint QQ = uint.Parse(match.Groups["QQ"].Value.Trim());
                    DateTime EndTime = GetEndTime(match.Groups["Time"].Value.Trim());
                    uint Second = (uint)(EndTime - DateTime.Now.ToLocalTime()).TotalSeconds;
                    this.Client.Silenced(e.Cluster.ExternalId, QQ, Second);
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("解禁")))
            {
                var match = Regex.Match(e.Message, GetPermission("解禁").Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    Util.ToUintList(e.Message).ForEach(x =>
                    {
                        this.Client.Silenced(e.Cluster.ExternalId, x, 0);
                    });
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("添加等级检测白名单")))
            {
                var match = Regex.Match(e.Message, GetPermission("添加等级检测白名单").Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    uint QQ = uint.Parse(match.Groups["QQ"].Value.Trim());
                    if (!Config.LevelWhiteList.Contains(QQ))
                    {
                        Config.LevelWhiteList.Add(QQ);
                        DataBase.db.SaveClusterConfig(Config);
                    }
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, "添加等级检测白名单成功");
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("删除等级检测白名单")))
            {
                var match = Regex.Match(e.Message, GetPermission("删除等级检测白名单").Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    uint QQ = uint.Parse(match.Groups["QQ"].Value.Trim());
                    if (Config.LevelWhiteList.Contains(QQ))
                    {
                        Config.LevelWhiteList.RemoveAll(x => x == QQ);
                        DataBase.db.SaveClusterConfig(Config);
                    }
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, "删除等级检测白名单成功");
                    e.Cancel = true;
                }
            }


            if (HasPermission(GetPermission("加警告")))
            {
                var match = Regex.Match(e.Message, GetPermission("加警告").Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    uint QQ = uint.Parse(match.Groups["QQ"].Value.Trim());
                    var warn = DataBase.db.GetWarning(e.Cluster.ExternalId, QQ);
                    if (warn == null)
                    {
                        warn = new DataBase.Models.Warning()
                        {
                            GroupId = e.Cluster.ExternalId,
                            QQ = QQ,
                            Count = 0
                        };
                    }
                    warn.Count++;
                    DataBase.db.SaveWarning(warn);
                    if (warn.Count > Config.WarningCount)
                    {
                        var Conf = PluginConfig.Init<Config>();
                        if (Config.WarningAddToYunBlack)
                        {
                            Conf.YunBlackList.Add(QQ);
                            Conf.Save();
                            this.Client.SendClusterMessage(e.Cluster.ExternalId, QQ, Config.WarningResult_N
                                .Replace("[违规类型]", "手动警告")
                                .Replace("[执行操作]", "加入云黑名单")
                                .Replace("[警告次数]", warn.Count.ToString()));
                        }
                        else if (Config.WarningAddToBlack)
                        {
                            Config.QunBlackList.Add(QQ);
                            DataBase.db.SaveClusterConfig(Config);
                            this.Client.SendClusterMessage(e.Cluster.ExternalId, QQ, Config.WarningResult_N
                                .Replace("[违规类型]", "手动警告")
                                .Replace("[执行操作]", "加入黑名单")
                                .Replace("[警告次数]", warn.Count.ToString()));
                        }
                    }
                    else
                    {
                        this.Client.SendClusterMessage(e.Cluster.ExternalId, QQ, Config.WarningResult_N
                                .Replace("[违规类型]", "手动警告")
                                .Replace("[执行操作]", "警告")
                                .Replace("[警告次数]", warn.Count.ToString()));
                    }
                }
                e.Cancel = true;
            }

            if (HasPermission(GetPermission("减警告")))
            {
                var match = Regex.Match(e.Message, GetPermission("减警告").Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    uint QQ = uint.Parse(match.Groups["QQ"].Value.Trim());
                    var warn = DataBase.db.GetWarning(e.Cluster.ExternalId, QQ);
                    if (warn != null)
                    {
                        warn.Count--;
                        DataBase.db.SaveWarning(warn);
                    }
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, QQ, "[@[QQ]]警告次数-1");
                    e.Cancel = true;
                }
            }

            if (HasPermission(GetPermission("清空警告")))
            {
                var match = Regex.Match(e.Message, GetPermission("清空警告").Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    uint QQ = uint.Parse(match.Groups["QQ"].Value.Trim());
                    var warn = DataBase.db.GetWarning(e.Cluster.ExternalId, QQ);
                    if (warn != null)
                    {
                        warn.Count = 0;
                        DataBase.db.SaveWarning(warn);
                    }
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, QQ, "[@[QQ]]警告次数已清空");
                    e.Cancel = true;
                }
            }
        }

        private void ManagerBilibiliGroupCard()
        {
            if (e.Cluster.Name.StartsWith("bilibili"))
            {
                if (e.Cluster.ExternalId == 60781685 || e.Cluster.ExternalId == 7863996)
                {
                    this.Client.GetGroupInfo(e.Cluster.ExternalId);
                    var member_card = this.Client.User.Clusters.Find(x => x.ExternalId == e.Cluster.ExternalId).members.Find(x => x.QQ == e.ClusterMember.QQ).Card;
                    if (string.IsNullOrWhiteSpace(member_card))
                    {
                        //this.Client.Silenced(e.Cluster.ExternalId, e.ClusterMember.QQ, 2592000);
                        //this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, $@"[@[QQ]],系统未能获取到您的名片请使用最新版手机QQ修改。");
                        this.Client.ModifyMemberCard(e.Cluster.ExternalId, e.ClusterMember.QQ, $"【正式会员】{e.ClusterMember.Nick}");
                        e.Cancel = true;
                        return;
                    }

                    var match = Regex.Match(member_card, @"^【(?<v>.*?)】.*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    if (!match.Success)
                    {
                        //                        this.Client.Silenced(e.Cluster.ExternalId, e.ClusterMember.QQ, 2592000);
                        //                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, $@"[@[QQ]]
                        //名片：{member_card}
                        //判定：名片格式不符");
                        this.Client.ModifyMemberCard(e.Cluster.ExternalId, e.ClusterMember.QQ, $"【正式会员】{e.ClusterMember.Nick}");
                        e.Cancel = true;
                        return;
                    }

                    if (match.Success)
                    {
                        /*
                         * 视频区：【视频】
番剧区：【番剧】
国创区：【国创】
放映厅：【放映厅】
纪录片：【纪录片】
专栏区：【专栏】
音频区：【音频】
直播区：【主播】
动画区：【动画】
音乐区：【音乐】
舞蹈区：【舞蹈】
游戏区：【游戏】
科技区：【科技】
生活区：【生活】
鬼畜区：【鬼畜】
时尚区：【时尚】
广告区：—禁止使用否则被禁言—
娱乐区：【娱乐】
影视区：【影视】
电影区：【电影】
电视剧：【电视剧】
小视频：【小视频】
插画区：【插画】
漫画区：【漫画】
                         * */
                        List<string> allV = new List<string>()
                        {
                            "正式会员",
                            "视频",
                            "番剧",
                            "国创",
                            "放映厅",
                            "纪录片",
                            "专栏",
                            "音频",
                            "主播",
                            "动画",
                            "音乐",
                            "舞蹈",
                            "游戏",
                            "科技",
                            "生活",
                            "鬼畜",
                            "时尚",
                            "娱乐",
                            "影视",
                            "电影",
                            "电视剧",
                            "小视频",
                            "插画",
                            "漫画",
                            "UP"
                        };

                        bool verify = false;

                        foreach (var x in allV)
                        {
                            if (x.Equals(match.Groups["v"].Value))
                            {
                                verify = true;
                                break;
                            }
                        }

                        if (!verify)
                        {
                            //                            this.Client.Silenced(e.Cluster.ExternalId, e.ClusterMember.QQ, 2592000);
                            //                            this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, $@"[@[QQ]]
                            //名片：{member_card}
                            //判定：名片格式不符");

                            this.Client.ModifyMemberCard(e.Cluster.ExternalId, e.ClusterMember.QQ, $"【正式会员】{e.ClusterMember.Nick}");
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
        }

        private DateTime GetEndTime(string str)
        {
            DateTime now = DateTime.Now;
            DateTime result = new DateTime(2100, 1, 1);
            List<int> list = Util.ToIntList(str);
            if (list == null || list.Count == 0)
            {
                return result;
            }
            if (str.Contains("秒"))
            {
                return now.AddSeconds((double)list[0]);
            }
            if (str.Contains("分"))
            {
                return now.AddMinutes((double)list[0]);
            }
            if (str.Contains("时"))
            {
                return now.AddHours((double)list[0]);
            }
            if (str.Contains("天"))
            {
                return now.AddDays((double)list[0]);
            }
            if (str.Contains("月"))
            {
                return now.AddMonths(list[0]);
            }
            if (str.Contains("年"))
            {
                return now.AddYears(list[0]);
            }
            DateTime.TryParse(str, out result);
            return result;
        }

        private bool HasPermission(Permission.Permission permission)
        {
            if (permission == null)
                return false;
            if (this.Client.Config.AdminQQs.Contains(e.ClusterMember.QQ))
                return true;
            if (permission.PermissionType == Permission.PermissionType.机主)
                if (this.Client.Config.AdminQQs.Contains(e.ClusterMember.QQ))
                    return true;
            if (permission.PermissionType == Permission.PermissionType.群主)
                if (e.Cluster.Creator == e.ClusterMember.QQ)
                    return true;
            if (permission.PermissionType == Permission.PermissionType.群主_管理员)
                if (IsAdmin(e.Cluster.ExternalId, e.ClusterMember.QQ))
                    return true;
            if (permission.PermissionType == Permission.PermissionType.群主_管理员_风纪委员)
                if (IsAdmin(e.Cluster.ExternalId, e.ClusterMember.QQ))
                    return true;
            if (permission.PermissionType == Permission.PermissionType.风纪委员)
                return false;
            if (permission.PermissionType == Permission.PermissionType.群员)
                return true;
            return false;
        }

        private Permission.Permission GetPermission(string UUID)
        {
            return Config.Permission.Find(x => x.UUID.Equals(UUID, StringComparison.CurrentCultureIgnoreCase));
        }

        private void Control(bool Status)
        {
            Config.Status = Status;
            Config.BlackList = Status;
            Config.MessageUnicode = Status;
            Config.GroupCard = Status;
            Config.GroupCardUnicode = Status;
            Config.BrushScreen = Status;
            Config.Repeat = Status;
            Config.Rhythm = Status;
            Config.MessageKey = Status;
            Config.YoutuOcr = Status;
            Config.YoutuPorn = Status;
            Config.QrCode = Status;
            Config.AudioKey = Status;
            Config.RedBag = Status;
            Config.ExitClusterAddBlackList = Status;
            Config.BlackListJoin = Status;
            Config.Welcome = Status;
            Config.Level = Status;
        }

        private bool IsRedBag()
        {
            Config.RedBagProtect.Clear(Config.RedBagProtectTime);
            if (Config.RedBagProtect.Data.Count > 0 && Config.RedBagProtect.Data.FindIndex(x => x.Content.Equals(e.Message)) > -1)
                return true;
            return false;
        }

        public void ManagerWarning()
        {
            if (Config.Warning)
            {
                foreach (var x in Config.WarningKey)
                {
                    var match = Regex.Match(e.Message, x.Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        var warn = DataBase.db.GetWarning(e.Cluster.ExternalId, e.ClusterMember.QQ);
                        if (warn == null)
                        {
                            warn = new DataBase.Models.Warning()
                            {
                                GroupId = e.Cluster.ExternalId,
                                QQ = e.ClusterMember.QQ,
                                Count = 0
                            };
                        }
                        warn.Count++;
                        DataBase.db.SaveWarning(warn);
                        if (warn.Count > Config.WarningCount)
                        {
                            if (Config.WarningAddToYunBlack)
                            {
                                Conf.YunBlackList.Add(e.ClusterMember.QQ);
                                Conf.Save();
                                this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.WarningResult_N
                                    .Replace("[违规类型]", $"违规内容【{match.Value}】")
                                    .Replace("[执行操作]", "加入云黑名单")
                                    .Replace("[警告次数]", warn.Count.ToString())
                                    .Replace("[最大警告次数]", Config.WarningCount.ToString()));
                                e.Cancel = true;
                            }
                            else if (Config.WarningAddToBlack)
                            {
                                Config.QunBlackList.Add(e.ClusterMember.QQ);
                                DataBase.db.SaveClusterConfig(Config);
                                this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.WarningResult_N
                                    .Replace("[违规类型]", $"违规内容【{match.Value}】")
                                    .Replace("[执行操作]", "加入黑名单")
                                    .Replace("[警告次数]", warn.Count.ToString())
                                    .Replace("[最大警告次数]", Config.WarningCount.ToString()));
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.WarningResult_N
                                    .Replace("[违规类型]", $"违规内容【{match.Value}】")
                                    .Replace("[执行操作]", "警告")
                                    .Replace("[警告次数]", warn.Count.ToString())
                                    .Replace("[最大警告次数]", Config.WarningCount.ToString()));
                            e.Cancel = true;
                        }
                        return;
                    }
                }
            }
        }

        private void ManagerLevel()
        {
            if (Config.Level)
            {
                if (Config.LevelSilenced)
                {
                    if (!Config.LevelWhiteList.Contains(e.ClusterMember.QQ))
                    {
                        QQLevel qqlevel = this.Client.GetQQLevel(e.ClusterMember.QQ);
                        if (qqlevel != null && qqlevel.ret == 0 && int.Parse(qqlevel.qq_level) < Config.LevelMin)
                        {
                            RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.LevelSilencedTime, Config.LevelStep);
                            this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.LevelResult_N
                                .Replace("[QQ等级]", qqlevel.qq_level)
                                .Replace("[最低等级]", Config.LevelMin.ToString()));
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
        }

        private void ManagerAudioKey()
        {
            if (Config.AudioKey)
            {
                if (!string.IsNullOrWhiteSpace(this.Audio))
                {
                    var data = File.ReadAllBytes(this.Audio);
                    Asr _asrClient = new Asr(Config.BaiduApiKey, Config.BaiduSecretKey);
                    var result = _asrClient.Recognize(data, "amr", 8000);
                    if (result["err_no"].ToString() == "0")
                    {
                        JArray textArray = (JArray)result["result"];
                        StringBuilder sb = new StringBuilder();
                        foreach (JValue item in textArray)
                        {
                            sb.Append(item.ToString());
                        }
                        string audioText = sb.ToString();

                        if (!string.IsNullOrWhiteSpace(audioText))
                        {
                            if (Config.AudioKeyUseYunAudioKey)
                            {
                                foreach (var x in Conf.YunAudioKey)
                                {
                                    var m = Regex.Match(audioText, x.Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    if (m.Success)
                                    {
                                        RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.AudioKeySilencedTime, Config.AudioKeyStep);
                                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.AudioKeyResult
                                            .Replace("[关键词]", m.Value)
                                            .Replace("[违规类型]", $"语音违规内容【{m.Value}】")
                                            .Replace("[执行操作]", Config.AudioKeyStep));
                                        e.Cancel = true;
                                        return;
                                    }
                                }
                            }

                            foreach (var x in Config.AudioKeys)
                            {
                                var m = Regex.Match(audioText, x.Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                if (m.Success)
                                {
                                    RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.AudioKeySilencedTime, Config.AudioKeyStep);
                                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.AudioKeyResult
                                        .Replace("[关键词]", m.Value)
                                        .Replace("[违规类型]", $"语音违规内容【{m.Value}】")
                                        .Replace("[执行操作]", Config.AudioKeyStep));
                                    e.Cancel = true;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ManagerQrCode()
        {
            if (Config.QrCode && this.Images.Count > 0)
            {
                foreach (var x in Images)
                {
                    string current_path = Util.MapFile($"Image\\{x}");
                    if (File.Exists(current_path))
                    {
                        string result = DeQRCode(current_path);

                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            if (Config.QrCode_S)
                            {
                                RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.QrCodeSilencedTime, Config.QrCodeStep);
                                this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.QrCodeResult
                                    .Replace("[违规类型]", "禁止发送二维码")
                                    .Replace("[执行操作]", Config.QrCodeStep));
                                e.Cancel = true;
                                return;
                            }

                            if (Config.QrCodeUseYunQrCodeKey)
                            {
                                foreach (var xs in Conf.YunQrCodeKey)
                                {
                                    var m = Regex.Match(result, xs.Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    if (m.Success)
                                    {
                                        RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.QrCodeSilencedTime, Config.QrCodeStep);
                                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.QrCodeResult
                                            .Replace("[关键词]", m.Value)
                                            .Replace("[违规类型]", $"二维码违规内容【{m.Value}】")
                                            .Replace("[执行操作]", Config.QrCodeStep));
                                        e.Cancel = true;
                                        return;
                                    }
                                }
                            }

                            foreach (var xs in Config.QrCodeKey)
                            {
                                var m = Regex.Match(result, xs.Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                if (m.Success)
                                {
                                    RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.QrCodeSilencedTime, Config.QrCodeStep);
                                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.QrCodeResult
                                        .Replace("[关键词]", m.Value)
                                        .Replace("[违规类型]", $"二维码违规内容【{m.Value}】")
                                        .Replace("[执行操作]", Config.QrCodeStep));
                                    e.Cancel = true;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ManagerTencentYoutu(List<string> Images)
        {
            TencentYoutu.Conf.Instance().setAppInfo(Config.YoutuAppId, Config.YoutuSecretId, Config.YoutuSecretKey, Config.YoutuQQ, TencentYoutu.Conf.Instance().YOUTU_END_POINT);

            if (Config.YoutuOcr)
            {
                foreach (var x in Images)
                {
                    string current_path = $"{AppDomain.CurrentDomain.BaseDirectory}Image\\{x}";
                    if (File.Exists(current_path))
                    {
                        string str = TencentYoutu.generalocr(current_path);
                        API.Tencent.Models.OcrAPIResult.Result result = JsonConvert.DeserializeObject<API.Tencent.Models.OcrAPIResult.Result>(str);
                        StringBuilder sb = new StringBuilder();
                        result.items.ForEach(n =>
                        {
                            sb.Append(n.itemstring);
                        });

                        if (Config.YoutuOcrUseYunOcrKey)
                        {
                            foreach (var xs in Conf.YunOcrKey)
                            {
                                var m = Regex.Match(sb.ToString(), xs.Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                if (m.Success)
                                {
                                    RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.YoutuOcrSilencedTime, Config.YoutuOcrStep);
                                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.YoutuOcrResult
                                        .Replace("[关键词]", m.Value)
                                        .Replace("[违规类型]", $"图片违规内容【{m.Value}】")
                                        .Replace("[执行操作]", Config.YoutuOcrStep));
                                    e.Cancel = true;
                                    return;
                                }
                            }
                        }

                        foreach (var xs in Config.YoutuOcrKey)
                        {
                            var m = Regex.Match(sb.ToString(), xs.Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                            if (m.Success)
                            {
                                RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.YoutuOcrSilencedTime, Config.YoutuOcrStep);
                                this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.YoutuOcrResult
                                    .Replace("[关键词]", m.Value)
                                    .Replace("[违规类型]", $"图片违规内容【{m.Value}】")
                                    .Replace("[执行操作]", Config.YoutuOcrStep));
                                e.Cancel = true;
                                return;
                            }
                        }
                    }
                }
            }


            if (Config.YoutuPorn)
            {
                foreach (var x in Images)
                {
                    string current_path = $"{AppDomain.CurrentDomain.BaseDirectory}Image\\{x}";
                    var tencentGifResult = JsonConvert.DeserializeObject<API.Tencent.Models.PornAPIResult.Result>(TencentYoutu.imageporn(current_path));
                    var confidenceGif = tencentGifResult.confidence();
                    if (confidenceGif.porn >= Config.YoutuPornValue)
                    {
                        RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.YoutuPornSilencedTime, Config.YoutuPornStep);
                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.YoutuPornResult
                            .Replace("[违规类型]", "图片内容涉黄")
                            .Replace("[执行操作]", Config.YoutuPornStep));
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void ManagerMessageKey()
        {
            if (Config.MessageKey && !IsRedBag())
            {
                if (Config.MessageKeyUseYunMessageKey)
                {
                    foreach (var x in Conf.YunMessageKey)
                    {
                        var m = Regex.Match(e.Message, x.Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        if (m.Success)
                        {
                            RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.MessageKeySilencedTime, Config.MessageKeyStep);
                            this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.MessageKeyResult
                                .Replace("[关键词]", m.Value)
                                .Replace("[违规类型]", $"违规发送内容【{m.Value}】")
                                .Replace("[执行操作]", Config.MessageKeyStep));
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                foreach (var x in Config.MessageKeys)
                {
                    var m = Regex.Match(e.Message, x.Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    if (m.Success)
                    {
                        RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.MessageKeySilencedTime, Config.MessageKeyStep);
                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.MessageKeyResult
                            .Replace("[关键词]", m.Value)
                            .Replace("[违规类型]", $"违规发送内容【{m.Value}】")
                            .Replace("[执行操作]", Config.MessageKeyStep));
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void ManagerRhythm()
        {
            if (Config.Rhythm && !IsRedBag())
            {
                if (Config.RhythmData.Count >= Config.RhythmCount)
                {
                    StringBuilder sb = new StringBuilder();

                    if (Config.RhythmRandom)
                    {
                        var list = Config.RhythmData.Data;
                        var bear = Config.RhythmData.Data.FindAll(x => x.Count > 1);
                        if (bear.Count > 0)
                            list = bear;
                        Random r = new Random();
                        var lucky_member_count = r.Next(1, list.Count);
                        this.Client.SilencedALL(e.Cluster.ExternalId, true);
                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.RhythmRandomResult
                            .Replace("[数量]", lucky_member_count.ToString()));
                        Thread.Sleep(1500);

                        for (int i = 0; i < lucky_member_count; i++)
                        {
                            var l_m_idx = r.Next(0, list.Count);
                            RunStep(e.Cluster.ExternalId, (uint)list[l_m_idx].QQ, 0, 0, Config.RhythmSilencedTime, Config.RhythmStep);
                            if (Config.RhythmStep.Contains("撤回"))
                            {
                                foreach (var x in list[l_m_idx].Message)
                                {
                                    this.Client.Recall(e.Cluster.ExternalId, x.Sequence, x.MessageId);
                                }
                            }
                            sb.Append($"[@{Config.RhythmData.Data[l_m_idx].QQ}]");
                            Config.RhythmData.Data.RemoveAt(l_m_idx);
                        }

                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.RhythmResult
                            .Replace("[@[QQ]]", sb.ToString())
                            .Replace("[违规类型]", "复读姬")
                            .Replace("[执行操作]", Config.RhythmStep));

                        this.Client.SilencedALL(e.Cluster.ExternalId, false);
                    }
                    else
                    {
                        var list = Config.RhythmData.Data;
                        var bear = Config.RhythmData.Data.FindAll(x => x.Count > 1);
                        if (bear.Count > 0)
                            list = bear;
                        foreach (var x in list)
                        {
                            RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.RhythmSilencedTime, Config.RhythmStep);
                            if (Config.RhythmStep.Contains("撤回"))
                            {
                                foreach (var n in x.Message)
                                {
                                    this.Client.Recall(e.Cluster.ExternalId, n.Sequence, n.MessageId);
                                }
                            }
                            sb.Append($"[@{x.QQ}]");
                        }
                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.RhythmResult
                            .Replace("[@[QQ]]", sb.ToString())
                            .Replace("[违规类型]", "复读姬")
                            .Replace("[执行操作]", Config.RhythmStep));
                    }

                    Config.RhythmData.Clear();
                    DataBase.db.SaveClusterConfig(Config);
                    e.Cancel = true;
                }
            }
        }

        private void ManagerRepeat()
        {
            if (Config.Repeat)
            {
                var b_s_idx = Config.BrushScreenData.FindIndex(x => x.QQ == e.ClusterMember.QQ);
                if (Config.BrushScreenData[b_s_idx].RepeatCount >= Config.RepeatCount)
                {
                    RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, 0, 0, Config.RepeatSilencedTime, Config.RepeatStep);
                    foreach (var x in Config.BrushScreenData[b_s_idx].Data)
                    {
                        if (Config.RepeatStep.Contains("撤回"))
                            this.Client.Recall(e.Cluster.ExternalId, x.Sequence, x.MessageId);
                    }
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.RepeatResult
                        .Replace("[违规类型]", $"重复发送相同信息超过{Config.RepeatCount}次")
                        .Replace("[执行操作]", Config.RepeatStep));
                    Config.BrushScreenData[b_s_idx].Clear();
                    Config.BrushScreenData[b_s_idx].RepeatCount = 0;
                    DataBase.db.SaveClusterConfig(Config);
                    e.Cancel = true;
                }
            }
        }

        private void ManagerBrushSceen()
        {
            if (Config.BrushScreen)
            {
                var b_s_idx = Config.BrushScreenData.FindIndex(x => x.QQ == e.ClusterMember.QQ);
                var list = Config.BrushScreenData[b_s_idx].Data.FindAll(x => x.SendTime >= DateTime.Now.AddSeconds(-Config.BrushScreenSecond) && x.SendTime <= DateTime.Now);

                if (list.Count >= Config.BrushScreenCount)
                {
                    RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, 0, 0, Config.BrushScreenSilencedTime, Config.BrushScreenStep);
                    foreach (var x in list)
                    {
                        if (Config.BrushScreenStep.Contains("撤回"))
                            this.Client.Recall(e.Cluster.ExternalId, x.Sequence, e.MessageId);
                    }
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.BrushScreenResult
                        .Replace("[违规类型]", $"{Config.BrushScreenSecond}秒内发言超过{Config.BrushScreenCount}条")
                        .Replace("[执行操作]", Config.BrushScreenStep));

                    Config.BrushScreenData[b_s_idx].Clear();
                    DataBase.db.SaveClusterConfig(Config);
                    e.Cancel = true;
                }
            }
        }

        private void ManagerGroupCardUnicode()
        {
            if (Config.GroupCardUnicode)
            {
                this.Client.GetGroupInfo(e.Cluster.ExternalId);
                string CardOrName = this.Client.User.Clusters.Find(x => x.ExternalId == e.Cluster.ExternalId).members.Find(x => x.QQ == e.ClusterMember.QQ).CardOrName;
                if (IsUnicode(CardOrName))
                {
                    RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.GroupCardUnicodeSilencedTime, Config.GroupCardUnicodeStep);
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.GroupCardUnicodeResult
                        .Replace("[违规类型]", "违规使用特殊字符名片")
                        .Replace("[执行操作]", Config.GroupCardUnicodeStep));
                    e.Cancel = true;
                }
            }
        }

        private void ManagerGroupCardKey()
        {
            if (Config.GroupCard)
            {
                this.Client.GetGroupInfo(e.Cluster.ExternalId);
                string CardOrName = this.Client.User.Clusters.Find(x => x.ExternalId == e.Cluster.ExternalId).members.Find(x => x.QQ == e.ClusterMember.QQ).CardOrName;

                if (Config.GroupCardUseYunKey)
                {
                    foreach (var x in Conf.YunGroupCardKey)
                    {
                        var m = Regex.Match(CardOrName, x.Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        if (m.Success)
                        {
                            RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.GroupCardSilencedTime, Config.GroupCardStep);
                            this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.GroupCardResult
                                .Replace("[关键词]", m.Value)
                                .Replace("[违规类型]", $"违规名片【{m.Value}】")
                                .Replace("[执行操作]", Config.GroupCardStep));
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                foreach (var x in Config.GroupCardKey)
                {
                    var m = Regex.Match(CardOrName, x.Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    if (m.Success)
                    {
                        RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.GroupCardSilencedTime, Config.GroupCardStep);
                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.GroupCardResult
                            .Replace("[关键词]", m.Value)
                            .Replace("[违规类型]", $"违规名片【{m.Value}】")
                            .Replace("[执行操作]", Config.GroupCardStep));
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void ManagerMessageUnicode()
        {
            if (Config.MessageUnicode && IsUnicode(e.Message))
            {
                RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.MessageUnicodeSilencedTime, Config.MessageUnicodeStep);
                this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.MessageUnicodeResult
                    .Replace("[违规类型]", "违规发送特殊字符")
                    .Replace("[执行操作]", Config.MessageUnicodeStep));
                e.Cancel = true;
            }
        }

        private void ManagerBlackList()
        {
            if (Config.BlackList)
            {
                if (Config.QunBlackListUseYunBlackList && Conf.YunBlackList.Contains(e.ClusterMember.QQ))
                {
                    RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.QunBlackListSilencedTime, Config.QunBlackListStep, true);
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.QunBlackListResult
                        .Replace("[执行操作]", Config.QunBlackListStep)
                        .Replace("[黑名单类型]", "云黑名单"));
                    e.Cancel = true;
                    return;
                }

                if (Config.QunBlackList.Contains(e.ClusterMember.QQ))
                {
                    RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, e.Sequence, e.MessageId, Config.QunBlackListSilencedTime, Config.QunBlackListStep, true);
                    this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.QunBlackListResult
                        .Replace("[执行操作]", Config.QunBlackListStep)
                        .Replace("[黑名单类型]", "本群黑名单"));
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
