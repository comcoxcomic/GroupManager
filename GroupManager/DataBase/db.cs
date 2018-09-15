using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coco.Framework.Utils;
using Dapper;
using Newtonsoft.Json;

namespace GroupManager.DataBase
{
    public class db : DbBase
    {
        internal static readonly object LockDb = new object();
        static string conn = $"Data Source={Util.MapFile("Plugin\\GroupManager2.db")};Pooling=true;FailIfMissing=false";
        public db() : base(conn) { }
        static List<Models.ClusterConfig> clusterConfigs = null;
        static List<Models.Judgement> judgements = null;
        static List<Models.Warning> warnings = null;
        static db()
        {
            lock (LockDb)
            {
                using (db d = new db())
                {
                    if (!d.Conn.TableExists("_history"))
                    {
                        d.Conn.Execute(@"
create table [_history] (
[Id] integer primary key not null,
[Ver] text not null
);

insert into [_history] (Ver) values('1.0.0.0');

create table [ClusterConfig] (
[Id] integer primary key not null,
[GroupId] integer not null,
[Config] text not null
);

create table [Warning] (
[Id] integer primary key not null,
[GroupId] integer not null,
[QQ] integer not null,
[Count] integer not null
);

create table [Judgement] (
[Id] integer primary key not null,
[Data] text not null
);");
                    }
                    try
                    {
                        if (!d.Conn.TableExists("Warning"))
                        {
                            d.Conn.Execute(@"create table [Warning] (
[Id] integer primary key not null,
[GroupId] integer not null,
[QQ] integer not null,
[Count] integer not null
);");
                        }
                    }
                    catch { }
                    clusterConfigs = d.Conn.GetAll<Models.ClusterConfig>().ToList();
                    warnings = d.Conn.GetAll<Models.Warning>().ToList();
                }
            }
        }

        internal static List<Models.Warning> GetWarning()
        {
            return warnings;
        }

        internal static Models.Warning GetWarning(long GroupId, long QQ)
        {
            return warnings.Find(x => x.GroupId == GroupId && x.QQ == QQ);
        }

        internal static Models.Warning SaveWarning(Models.Warning item, bool del = false)
        {
            lock (LockDb)
            {
                using (db d = new db())
                {
                    if (del)
                    {
                        warnings.Remove(item);
                        d.Conn.Delete(item);
                    }
                    else if (item.Id > 0)
                    {
                        warnings[warnings.FindIndex(x => x.Id == item.Id)] = item;
                        d.Conn.Update(item);
                    }
                    else
                    {
                        item.Id = d.Conn.Insert(item);
                        warnings.Add(item);
                    }
                    return item;
                }
            }
        }

        internal static List<Models.Judgement> GetJudgement()
        {
            return judgements;
        }

        internal static Models.JudgementData GetJudgement(long Id)
        {
            var tmp = judgements.Find(x => x.Id == Id);
            if (tmp == null)
                return null;
            return JsonConvert.DeserializeObject<Models.JudgementData>(tmp.Data);
        }

        internal static Models.Judgement SaveJudgement(Models.JudgementData item)
        {
            lock (LockDb)
            {
                using (db d = new db())
                {
                    var tmp = judgements.Find(x => x.Id == item.Id);
                    tmp.Data = JsonConvert.SerializeObject(item);
                    return SaveJudgement(tmp);
                }
            }
        }

        internal static Models.Judgement SaveJudgement(Models.Judgement item)
        {
            lock (LockDb)
            {
                using (db d = new db())
                {
                    if (item.Id > 0)
                    {
                        judgements[judgements.FindIndex(x => x.Id == item.Id)] = item;
                        d.Conn.Update(item);
                    }
                    else
                    {
                        item.Id = d.Conn.Insert(item);
                        judgements.Add(item);
                    }
                    return item;
                }
            }
        }

        internal static List<Models.ClusterConfig> GetClusterConfig()
        {
            return clusterConfigs;
        }

        internal static Models.Config GetClusterConfig(long GroupId)
        {
            var tmp = clusterConfigs.Find(x => x.GroupId == GroupId);
            if (tmp == null)
            {
                tmp = new DataBase.Models.ClusterConfig() { GroupId = GroupId };
                var Config = new DataBase.Models.Config();
                Config.GroupId = GroupId;
                tmp.Config = JSON.Serialize(Config);
                tmp.Id = SaveClusterConfig(tmp);
                return Config;
            }
            return JSON.Deserialize<Models.Config>(tmp.Config);
        }

        internal static Models.ClusterConfig SaveClusterConfig(Models.Config item)
        {
            var tmp = clusterConfigs.Find(x => x.GroupId == item.GroupId);
            tmp.Config = JsonConvert.SerializeObject(item);
            tmp.Id = SaveClusterConfig(tmp);
            return tmp;
        }

        internal static long SaveClusterConfig(Models.ClusterConfig item)
        {
            lock (LockDb)
            {
                using (db d = new db())
                {
                    if (item.Id > 0)
                    {
                        clusterConfigs[clusterConfigs.FindIndex(x => x.Id == item.Id)] = item;
                        d.Conn.Update(item);
                    }
                    else
                    {
                        item.Id = d.Conn.Insert(item);
                        clusterConfigs.Add(item);
                    }
                }
            }
            return item.Id;
        }
    }
}
