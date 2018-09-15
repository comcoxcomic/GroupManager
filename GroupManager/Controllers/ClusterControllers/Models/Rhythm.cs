using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupManager.Controllers.ClusterControllers.Models
{
    public class Rhythm
    {
        public int Count { get; set; } = 1;
        public List<RhythmData> Data { get; set; } = new List<RhythmData>();
        public void Insert(RhythmData item)
        {
            if (this.Data.Count > 0)
            {
                var tmp = this.Data[this.Data.Count - 1];
                if (tmp.Message[0].Content == item.Message[0].Content)
                {
                    if (tmp.QQ == item.QQ)
                    {
                        tmp.Message.Add(item.Message[0]);
                        tmp.Count++;
                    }
                    else if (this.Data.FindIndex(x => x.QQ == item.QQ) != -1)
                    {
                        var idx = this.Data.FindIndex(x => x.QQ == item.QQ);
                        this.Data[idx].Message.Add(item.Message[0]);
                        this.Data[idx].Count++;
                    }
                    else
                    {
                        this.Data.Add(item);
                    }
                }
                else
                {
                    this.Clear();
                }
            }
            else
                this.Data.Add(item);
        }

        public void Clear()
        {
            this.Count = 0;
            this.Data.Clear();
        }
    }

    public class RhythmData
    {
        public long QQ { get; set; }
        public List<RhythmMessage> Message { get; set; } = new List<RhythmMessage>();
        public long Count { get; set; }
    }

    public class RhythmMessage
    {
        public string Content { get; set; }
        public uint Sequence { get; set; }
        public uint MessageId { get; set; }
        public DateTime SendTime { get; set; }
    }
}
