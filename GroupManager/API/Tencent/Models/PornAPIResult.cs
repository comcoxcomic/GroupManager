using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupManager.API.Tencent.Models
{
    public class PornAPIResult
    {
        public class Tag
        {
            public string tag_name { get; set; }
            public int tag_confidence { get; set; }
            public double tag_confidence_f { get; set; }
        }

        public class Feas
        {
            public string name { get; set; }
            public string feature { get; set; }
        }

        public class Result
        {
            public int errorcode { get; set; }
            public string errormsg { get; set; }
            public List<Tag> tags { get; set; }
            public List<object> faces { get; set; }
            public Feas feas { get; set; }
            public Confidence confidence()
            {
                var _confidence = new Confidence();
                _confidence.normal = this.tags.Find(n => n.tag_name == "normal").tag_confidence;
                _confidence.hot = this.tags.Find(n => n.tag_name == "hot").tag_confidence;
                _confidence.porn = this.tags.Find(n => n.tag_name == "porn").tag_confidence;
                return _confidence;
            }

            public class Confidence
            {
                public int normal { get; set; } = 0;
                public int hot { get; set; }
                public int porn { get; set; }
            }
        }
    }
}
