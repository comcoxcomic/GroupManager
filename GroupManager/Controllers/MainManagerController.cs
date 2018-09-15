using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Coco.Framework.SDK;
using ZXing;

namespace GroupManager.Controllers
{
    public class MainManagerController
    {
        protected internal Plugin Client;

        protected internal List<string> Images = new List<string>();

        protected internal string Audio;

        public bool IsAdmin(uint ExternalId, uint QQ)
        {
            var Cluster = this.Client.User.Clusters.Find(x => x.ExternalId == ExternalId);

            if (this.Client.Config.AdminQQs.Contains(QQ))
                return true;
            else if (Cluster != null && Cluster.Creator == QQ)
                return true;
            else if (Cluster != null)
            {
                var Member = Cluster.members.Find(x => x.QQ == QQ);
                if (Member != null)
                    return Member.IsAdmin();
            }

            return false;
        }

        public bool IsUnicode(string str)
        {
            List<byte> b = Encoding.Unicode.GetBytes(str).ToList();
            int uCount = 0;
            while (b.FindIndex(x => x == 46) > -1 && b.FindIndex(x => x == 46) + 1 < b.Count && b[b.FindIndex(x => x == 46) + 1] == 32)
            {
                int i = b.FindIndex(x => x == 46);
                if (i > -1)
                {
                    if (i + 1 < b.Count && b[i] == 46 && b[i + 1] == 32)
                    {
                        b.RemoveAt(i + 1);
                        b.RemoveAt(i);
                        uCount++;
                    }
                }
            }
            return uCount > 0;
        }

        private bool IsXML(string Message)
        {
            int i = Message.LastIndexOf("</msg>");

            if (i != -1)
            {
                i += 6;
                Message = Message.Substring(0, i);
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(Message);
                return true;
            }
            catch { }
            return false;
        }

        public string DeQRCode(string QRCodePath)
        {
            try
            {
                Bitmap bmp = new Bitmap(QRCodePath);
                IBarcodeReader reader = new BarcodeReader();
                var result = reader.Decode(bmp);
                if (result != null)
                {
                    return result.Text;
                }
                else return null;
            }
            catch (Exception) { return null; }
        }

        public void RunStep(uint ExternalId, uint QQ, uint Sequence, uint MessageId, uint SilencedTime, string Step, bool IsBlackStep = false)
        {
            if (Step.Contains("移除"))
            {
                this.Client.RemoveMember(ExternalId, QQ);
            }
            if (Step.Contains("禁言") && SilencedTime > 0)
            {
                var Config = DataBase.db.GetClusterConfig(ExternalId);
                if (Config.WarnSilenced && !IsBlackStep)
                {
                    var warn = DataBase.db.GetWarning(ExternalId, QQ);
                    if (warn == null)
                    {
                        warn = new DataBase.Models.Warning()
                        {
                            GroupId = ExternalId,
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
                            this.Client.SendClusterMessage(ExternalId, QQ, Config.WarningResult_N
                                .Replace("[违规类型]", "禁言警告")
                                .Replace("[执行操作]", "加入云黑名单")
                                .Replace("[警告次数]", warn.Count.ToString())
                                .Replace("[最大警告次数]", Config.WarningCount.ToString()));
                        }
                        else if (Config.WarningAddToBlack)
                        {
                            Config.QunBlackList.Add(QQ);
                            DataBase.db.SaveClusterConfig(Config);
                            this.Client.SendClusterMessage(ExternalId, QQ, Config.WarningResult_N
                                .Replace("[违规类型]", "禁言警告")
                                .Replace("[执行操作]", "加入黑名单")
                                .Replace("[警告次数]", warn.Count.ToString())
                                .Replace("[最大警告次数]", Config.WarningCount.ToString()));
                        }
                    }
                    else
                    {
                        this.Client.SendClusterMessage(ExternalId, QQ, Config.WarningResult_N
                                .Replace("[违规类型]", "禁言警告")
                                .Replace("[执行操作]", "警告")
                                .Replace("[警告次数]", warn.Count.ToString())
                                .Replace("[最大警告次数]", Config.WarningCount.ToString()));
                    }
                }
                int count = 0;
                var silenced_member = GMUtil.Util.GetAllSilencedMember(ExternalId, this.Client.User.QQ, this.Client.User.skey, this.Client.GetBkn(this.Client.User.skey));
                while (!this.Client.Silenced(ExternalId, QQ, SilencedTime).Contains("禁言成功"))
                {
                    if (count < silenced_member.Count)
                    {
                        this.Client.Silenced(ExternalId, (uint)silenced_member[count], 0);
                    }
                    else
                    {
                        this.Client.OnLog($"群【{this.Client.User.Clusters.Find(x => x.ExternalId == ExternalId).Name}】成员【{this.Client.GetFriend(QQ).NickName}({QQ})】禁言失败");
                        break;
                    }
                    count++;
                }
            }
            if (Step.Contains("撤回") && Sequence != 0 && MessageId != 0)
            {
                this.Client.Recall(ExternalId, Sequence, MessageId);
            }
        }
    }
}
