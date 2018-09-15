using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupManager.Controllers.ClusterControllers.Models
{
    public class Message
    {
        public long QQ { get; set; }
        public List<SendData> Data { get; set; } = new List<SendData>();
        public int RepeatCount { get; set; } = 1;
        public void Insert(SendData item)
        {
            this.Data.RemoveAll(x => x.SendTime <= DateTime.Now.AddMinutes(-5));
            if (Data.Count > 0)
            {
                var tmp = this.Data[this.Data.Count - 1];
                if (tmp.Content.Equals(item.Content))
                    RepeatCount++;
                else
                    this.RepeatCount = 1;
            }
            this.Data.Add(item);
        }

        public void Clear()
        {
            this.Data.Clear();
        }
    }

    public class SendData
    {
        public string Content { get; set; }
        public uint Sequence { get; set; }
        public uint MessageId { get; set; }
        public DateTime SendTime { get; set; }
    }
}
