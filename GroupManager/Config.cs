using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coco.Framework.SDK;

namespace GroupManager
{
    public class Config : PluginConfig
    {
        public List<long> YunBlackList { get; set; } = new List<long>();

        public List<Models.MatchKey> YunGroupCardKey { get; set; } = new List<Models.MatchKey>() { new Models.MatchKey() { Content = "快手" }, new Models.MatchKey() { Content = "孙笑川" } };

        public List<Models.MatchKey> YunMessageKey { get; set; } = new List<Models.MatchKey>() { new Models.MatchKey() { Content = "快手" }, new Models.MatchKey() { Content = "孙笑川" } };

        public List<Models.MatchKey> YunOcrKey { get; set; } = new List<Models.MatchKey>() { new Models.MatchKey() { Content = "快手" }, new Models.MatchKey() { Content = "孙笑川" } };

        public List<Models.MatchKey> YunQrCodeKey { get; set; } = new List<Models.MatchKey>() { new Models.MatchKey() { Content = "快手" }, new Models.MatchKey() { Content = "孙笑川" } };

        public List<Models.MatchKey> YunAudioKey { get; set; } = new List<Models.MatchKey>() { new Models.MatchKey() { Content = "快手" }, new Models.MatchKey() { Content = "孙笑川" } };

        public List<Models.MatchKey> YunRedBagKey { get; set; } = new List<Models.MatchKey>() { new Models.MatchKey() { Content = "快手" }, new Models.MatchKey() { Content = "孙笑川" } };

        public long SilencedMaxMember { get; set; } = 30;
    }
}
