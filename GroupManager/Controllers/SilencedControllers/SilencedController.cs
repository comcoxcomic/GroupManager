using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coco.Framework.SDK;

namespace GroupManager.Controllers.SilencedControllers
{
    public class SilencedController : MainManagerController
    {
        protected internal SilencedQQEventArgs e;

        DataBase.Models.Config Config;
        Config Conf;

        public SilencedController(Plugin plugin, SilencedQQEventArgs e)
        {
            this.Client = plugin;
            this.e = e;

            Config = DataBase.db.GetClusterConfig(e.Cluster.ExternalId);
            Conf = PluginConfig.Init<Config>();
        }

        public void Manager()
        {
            //if (e.Time != 0xffffffff)
            //{
            //    foreach (var x in DataBase.db.GetSilencedConfig(e.Cluster.ExternalId).FindAll(x => x.EndTime < DateTime.Now.ToLocalTime()))
            //    {
            //        DataBase.db.SaveSilencedConfig(x, true);
            //    }
            //    if (e.Time == 0)
            //    {
            //        var member = DataBase.db.GetSilencedConfig(e.Cluster.ExternalId, e.ClusterMember.QQ);
            //        if (member != null)
            //            DataBase.db.SaveSilencedConfig(member, true);
            //    }
            //    else
            //    {
            //        DataBase.db.SaveSilencedConfig(new DataBase.Models.SilencedConfig()
            //        {
            //            GroupId = e.Cluster.ExternalId,
            //            QQ = e.ClusterMember.QQ,
            //            Time = e.Time,
            //            StartTime = DateTime.Now,
            //            EndTime = DateTime.Now.AddSeconds(e.Time).ToLocalTime()
            //        });
            //    }
            //}
        }
    }
}
