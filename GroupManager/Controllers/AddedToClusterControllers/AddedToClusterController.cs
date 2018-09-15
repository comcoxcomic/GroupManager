using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coco.Framework.SDK;

namespace GroupManager.Controllers.AddedToClusterControllers
{
    public class AddedToClusterController : MainManagerController
    {
        protected internal AddedToClusterQQEventArgs e;

        DataBase.Models.Config Config;
        Config Conf;

        public AddedToClusterController(Plugin plugin, AddedToClusterQQEventArgs e)
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
                if (Config.BlackListJoin && Config.BlackList)
                {
                    if (Config.QunBlackListUseYunBlackList)
                    {
                        if (Conf.YunBlackList.Contains(e.ClusterMember.QQ))
                        {
                            RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, 0, 0, Config.BlackListJoinSilencedTime, Config.BlackListJoinStep);
                            this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.BlackListJoinResult_N
                                .Replace("[黑名单类型]", "云黑名单"));
                            e.Cancel = true;
                            return;
                        }
                    }

                    if (Config.QunBlackList.Contains(e.ClusterMember.QQ))
                    {
                        RunStep(e.Cluster.ExternalId, e.ClusterMember.QQ, 0, 0, Config.BlackListJoinSilencedTime, Config.BlackListJoinStep);
                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.BlackListJoinResult_N
                            .Replace("[黑名单类型]", "本群黑名单"));
                        e.Cancel = true;
                        return;
                    }
                }

                if (Config.Welcome)
                {
                    if (Config.WelcomeSilenced)
                    {
                        this.Client.Silenced(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.WelcomeSilencedTime);
                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.WelcomeSilencedResult
                            .Replace("[禁言时间]", (Config.WelcomeSilencedTime / 60).ToString()));
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        this.Client.SendClusterMessage(e.Cluster.ExternalId, e.ClusterMember.QQ, Config.WelcomeResult);
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }
    }
}
