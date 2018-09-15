using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SufeiUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupManager.GMUtil
{
    public class Util
    {
        public static int GetBkn(string skey)
        {
            int num = 5381;
            int length = skey.Length;
            for (int i = 0; i < length; i++)
            {
                num += (num << 5) + (int)skey[i];
            }
            return num & 2147483647;
        }

        public static long GetFirstSilencedMember(long GroupId, long RobotQQ, string skey, int bkn)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = $"https://qinfo.clt.qq.com/cgi-bin/qun_info/get_group_setting_v2?src=qinfo_v3&gc={GroupId}&bkn={bkn}",
                Cookie = $"skey={skey};uin=o{RobotQQ.ToString().PadLeft(10, '0')}"
            };
            string result = http.GetHtml(item).Html;
            JObject j_result = (JObject)JsonConvert.DeserializeObject(result);
            JToken j_shutup = j_result["shutup"];
            JArray j_list = (JArray)j_shutup["list"];
            if (j_list.Count == 0)
                return -1;
            return long.Parse(j_list[0]["uin"].ToString());
        }

        public static List<long> GetAllSilencedMember(long GroupId, long RobotQQ, string skey, int bkn)
        {
            List<long> list = new List<long>();
            try
            {
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = $"https://qinfo.clt.qq.com/cgi-bin/qun_info/get_group_setting_v2?src=qinfo_v3&gc={GroupId}&bkn={bkn}",
                    Cookie = $"skey={skey};uin=o{RobotQQ.ToString().PadLeft(10, '0')}"
                };
                string result = http.GetHtml(item).Html;
                JObject j_result = (JObject)JsonConvert.DeserializeObject(result);
                JToken j_shutup = j_result["shutup"];
                JArray j_list = (JArray)j_shutup["list"];
                foreach (var x in j_list)
                {
                    list.Add(long.Parse(x["uin"].ToString()));
                }
            }
            catch { }
            return list;
        }

        public static string GetMemberCardXHR(long GroupId, long memberCount, long RobotQQ, string skey, string p_skey, int bkn)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = "https://qun.qq.com/cgi-bin/qun_mgr/search_group_members",
                Method = "POST",
                Postdata = $"gc={GroupId}&st=0&end={memberCount}&sort=0&bkn={bkn}",
                Cookie = $"uin=o{RobotQQ.ToString().PadLeft(10, '0')};skey={skey};p_skey={p_skey}"
            };
            string html = http.GetHtml(item).Html;
            return html;
        }

        public static string GetMemberCard(long GroupId, long memberCount, long RobotQQ, string skey, string p_skey, int bkn, long uin)
        {
            string html = GetMemberCardXHR(GroupId, memberCount, RobotQQ, skey, p_skey, bkn);
            if (string.IsNullOrWhiteSpace(html))
                return null;

            try
            {
                JObject jObject = (JObject)JsonConvert.DeserializeObject(html);
                JArray mems = (JArray)jObject["mems"];
                JToken mem = mems.FirstOrDefault(x => x["uin"].ToString() == uin.ToString());
                if (mem == null)
                    return null;

                if (!mem.Contains("card"))
                    return null;

                return mem["card"].ToString();
            }
            catch { return null; }
        }
    }
}
