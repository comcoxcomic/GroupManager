using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupManager.DataBase.Models
{
    public class RedBagProtect
    {
        public List<RedBagProtectData> Data { get; set; } = new List<RedBagProtectData>();
        public void Insert(RedBagProtectData item)
        {
            this.Data.Add(item);
        }

        public void Clear(int Second)
        {
            this.Data.RemoveAll(x => (DateTime.Now - x.SendTime).TotalSeconds > Second);
        }

        public void ClearAll()
        {
            this.Data.Clear();
        }
    }

    public class RedBagProtectData
    {
        public string Content { get; set; }
        public DateTime SendTime { get; set; }
    }
}
