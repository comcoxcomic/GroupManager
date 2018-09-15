using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupManager.Models.Judgement
{
    public class Judgement
    {
        public long QQ { get; set; }
        //风纪委员等级
        public long Level { get; set; }
        //参与投票次数
        public long VoteCount { get; set; }
    }
}
