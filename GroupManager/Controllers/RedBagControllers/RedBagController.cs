using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Coco.Framework.SDK;

namespace GroupManager.Controllers.RedBagControllers
{
    public class RedBagController : MainManagerController
    {
        protected internal RedBagEventArgs e;

        DataBase.Models.Config Config;
        Config Conf;

        public RedBagController(Plugin plugin, RedBagEventArgs e)
        {
            this.Client = plugin;
            this.e = e;

            Config = DataBase.db.GetClusterConfig(e.ExternalId);
            Conf = PluginConfig.Init<Config>();

            if (Config.Status)
            {
                Config.RedBagProtect.Clear(Config.RedBagProtectTime);
                Config.RedBagProtect.Insert(new DataBase.Models.RedBagProtectData() { Content = e.Memo, SendTime = DateTime.Now });

                DataBase.db.SaveClusterConfig(Config);
            }
        }

        public void Manager()
        {
            if (IsAdmin(e.ExternalId, e.QQ))
                return;

            if (Config.Status)
            {
                if (Config.RedBag)
                {
                    if (Config.RedBag_S)
                    {
                        RunStep(e.ExternalId, e.QQ, 0, 0, Config.RedBagSilencedTime, Config.RedBagKeyStep);
                        this.Client.SendClusterMessage(e.ExternalId, e.QQ, Config.RedBagResult
                            .Replace("[违规类型]", "禁止发送红包")
                            .Replace("[执行操作]", Config.RedBagKeyStep));
                        e.Cancel = true;
                        return;
                    }

                    if (Config.RedBagAmount > 0 && e.amount < Config.RedBagAmount)
                    {
                        RunStep(e.ExternalId, e.QQ, 0, 0, Config.RedBagSilencedTime, Config.RedBagKeyStep);
                        this.Client.SendClusterMessage(e.ExternalId, e.QQ, Config.RedBagResult
                            .Replace("[违规类型]", $"红包金额小于{Math.Round((decimal)(Config.RedBagAmount / 100), 2)}元")
                            .Replace("[执行操作]", Config.RedBagKeyStep));
                        e.Cancel = true;
                        return;
                    }

                    if (Config.RedBagUseYunRedBagKey)
                    {
                        foreach (var x in Conf.YunRedBagKey)
                        {
                            var m = Regex.Match(e.Memo, x.Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                            if (m.Success)
                            {
                                RunStep(e.ExternalId, e.QQ, 0, 0, Config.RedBagSilencedTime, Config.RedBagKeyStep);
                                this.Client.SendClusterMessage(e.ExternalId, e.QQ, Config.RedBagResult
                                    .Replace("[违规类型]", $"红包违规内容【{m.Value}】")
                                    .Replace("[执行操作]", Config.RedBagKeyStep));
                                e.Cancel = true;
                                return;
                            }
                        }
                    }

                    foreach (var x in Config.RedBagKey)
                    {
                        var m = Regex.Match(e.Memo, x.Content, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        if (m.Success)
                        {
                            RunStep(e.ExternalId, e.QQ, 0, 0, Config.RedBagSilencedTime, Config.RedBagKeyStep);
                            this.Client.SendClusterMessage(e.ExternalId, e.QQ, Config.RedBagResult
                                .Replace("[违规类型]", $"红包违规内容【{m.Value}】")
                                .Replace("[执行操作]", Config.RedBagKeyStep));
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
        }
    }
}
