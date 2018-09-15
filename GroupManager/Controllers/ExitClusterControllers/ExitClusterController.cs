using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coco.Framework.SDK;

namespace GroupManager.Controllers.ExitClusterControllers
{
    public class ExitClusterController : MainManagerController
    {
        protected internal MemberExitClusterQQEventArgs e;

        DataBase.Models.Config Config;
        Config Conf;

        public ExitClusterController(Plugin plugin, MemberExitClusterQQEventArgs e)
        {
            this.Client = plugin;
            this.e = e;

            Config = DataBase.db.GetClusterConfig(e.Cluster.ExternalId);
            Conf = PluginConfig.Init<Config>();
        }

        public void Manager()
        {
            if (Config.Status)
            {
                var admin_member = e.ClusterMemberAdmin;
                if (admin_member == null)
                {
                    if (Config.ExitClusterAddBlackList)
                    {
                        if (Config.ExitClusterAddYunBlackList)
                        {
                            Conf.YunBlackList.Add(e.ClusterMember.QQ);
                            Conf.Save();
                            this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.ExitClusterResult
                                .Replace("[昵称或名片]", e.ClusterMember.CardOrName)
                                .Replace("[QQ]", e.ClusterMember.QQ.ToString())
                                .Replace("[黑名单类型]", "云黑名单"));
                            e.Cancel = true;
                            return;
                        }

                        Config.QunBlackList.Add(e.ClusterMember.QQ);
                        DataBase.db.SaveClusterConfig(Config);
                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.ExitClusterResult
                            .Replace("[昵称或名片]", e.ClusterMember.CardOrName)
                            .Replace("[QQ]", e.ClusterMember.QQ.ToString())
                            .Replace("[黑名单类型]", "本群黑名单"));
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    if (Config.ExitAdminAddBlackList)
                    {
                        if (Config.ExitAdminAddYunBlackList)
                        {
                            Conf.YunBlackList.Add(e.ClusterMember.QQ);
                            Conf.Save();
                            this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.ExitAdminResult
                                .Replace("[昵称或名片]", e.ClusterMember.CardOrName)
                                .Replace("[QQ]", e.ClusterMember.QQ.ToString())
                                .Replace("[黑名单类型]", "云黑名单")
                                .Replace("[操作管理员昵称或名片]", admin_member.CardOrName)
                                .Replace("[操作管理员QQ]", admin_member.QQ.ToString())
                                .Replace("[@操作管理员]", $"[@{admin_member.QQ}]"));
                            e.Cancel = true;
                            return;
                        }

                        Config.QunBlackList.Add(e.ClusterMember.QQ);
                        DataBase.db.SaveClusterConfig(Config);
                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.ExitAdminResult
                            .Replace("[昵称或名片]", e.ClusterMember.CardOrName)
                            .Replace("[QQ]", e.ClusterMember.QQ.ToString())
                            .Replace("[黑名单类型]", "本群黑名单")
                            .Replace("[操作管理员昵称或名片]", admin_member.CardOrName)
                            .Replace("[操作管理员QQ]", admin_member.QQ.ToString())
                            .Replace("[@操作管理员]", $"[@{admin_member.QQ}]"));
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }
    }
}
