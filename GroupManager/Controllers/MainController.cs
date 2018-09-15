using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coco.Framework.SDK;

namespace GroupManager
{
    public class MainController : Plugin
    {
        public MainController()
        {
            this.Author = "神崎H亚里亚";
            this.Description = "";
            this.PluginName = "QQ群管";
        }

        public override bool Start()
        {
            Plugin.OutputReplace += Plugin_OutputReplace;
            this.ReceiveClusterIM += MainController_ReceiveClusterIM;
            this.RedBagEvent += MainController_RedBagEvent;
            this.MemberExitCluster += MainController_MemberExitCluster;
            this.AddedToCluster += MainController_AddedToCluster;
            this.AddToClusterNeedAuth += MainController_AddToClusterNeedAuth;
            this.SilencedQQEvent += MainController_SilencedQQEvent;
            return base.Start();
        }

        private void MainController_SilencedQQEvent(object sender, SilencedQQEventArgs e)
        {

        }

        private void MainController_AddToClusterNeedAuth(object sender, AddToClusterNeedAuthQQEventArgs e)
        {
            Controllers.AddToClusterNeedAuthControllers.AddToClusterNeedAuthController anc = new Controllers.AddToClusterNeedAuthControllers.AddToClusterNeedAuthController(this, e);
            anc.Manager();
        }

        private void MainController_AddedToCluster(object sender, AddedToClusterQQEventArgs e)
        {
            Controllers.AddedToClusterControllers.AddedToClusterController ac = new Controllers.AddedToClusterControllers.AddedToClusterController(this, e);
            ac.Manager();
        }

        private void MainController_MemberExitCluster(object sender, MemberExitClusterQQEventArgs e)
        {
            Controllers.ExitClusterControllers.ExitClusterController ec = new Controllers.ExitClusterControllers.ExitClusterController(this, e);
            ec.Manager();
        }

        private void MainController_RedBagEvent(object sender, RedBagEventArgs e)
        {
            //[QQ红包]我发了一个“语音口令红包”，请使用手机QQ7.3.0及以上版本查收红包。
            Controllers.RedBagControllers.RedBagController rc = new Controllers.RedBagControllers.RedBagController(this, e);
            rc.Manager();
        }

        private void MainController_ReceiveClusterIM(object sender, ReceiveClusterIMQQEventArgs e)
        {
            Controllers.ClusterControllers.ClusterManagerController cm = new Controllers.ClusterControllers.ClusterManagerController(this, e);
            cm.Manager();
        }

        public override bool ShowForm()
        {
            new Views.MainView(this).Show();
            return base.ShowForm();
        }

        private void Plugin_OutputReplace(object sender, OutputReplaceEventArgs e)
        {
            if (e.Type == 1)
            {
                var Config = DataBase.db.GetClusterConfig(e.ExternalId);
                if (Config.Status)
                {
                    if (Config.ReplaceLine)
                    {
                        e.Message = e.Message.Replace("\r\n", "\n");
                    }
                }
            }
        }

        public override bool UnInstall()
        {
            Plugin.OutputReplace -= Plugin_OutputReplace;
            return base.UnInstall();
        }
    }
}
