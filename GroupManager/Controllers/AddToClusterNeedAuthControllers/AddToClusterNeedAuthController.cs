using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coco.Framework.Entities;
using Coco.Framework.SDK;

namespace GroupManager.Controllers.AddToClusterNeedAuthControllers
{
    public class AddToClusterNeedAuthController : MainManagerController
    {
        protected internal AddToClusterNeedAuthQQEventArgs e;

        DataBase.Models.Config Config;
        Config Conf;

        public AddToClusterNeedAuthController(Plugin plugin, AddToClusterNeedAuthQQEventArgs e)
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
                if (Config.BlackListJoin && Config.BlackListJoinReject)
                {
                    if (Config.QunBlackListUseYunBlackList)
                    {
                        if (Conf.YunBlackList.Contains(e.QQ))
                        {
                            this.Client.AgreeJoinCluster(e.Cluster.ExternalId, e.QQ, false, Config.BlackListJoinResult_R);
                            e.Cancel = true;
                            return;
                        }
                    }

                    if (Config.QunBlackList.Contains(e.QQ))
                    {
                        this.Client.AgreeJoinCluster(e.Cluster.ExternalId, e.QQ, false, Config.BlackListJoinResult_R);
                        e.Cancel = true;
                        return;
                    }
                }

                if (Config.Level)
                {
                    if (Config.LevelReject)
                    {
                        QQLevel qqlevel = this.Client.GetQQLevel(e.QQ);
                        if (qqlevel != null && qqlevel.ret == 0 && !Config.LevelWhiteList.Contains(e.QQ) && int.Parse(qqlevel.qq_level) < Config.LevelMin)
                        {
                            this.Client.AgreeJoinCluster(e.Cluster.ExternalId, e.QQ, false, Config.LevelResult_R
                            .Replace("[最低等级]", Config.LevelMin.ToString()));
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
        }
    }
}
