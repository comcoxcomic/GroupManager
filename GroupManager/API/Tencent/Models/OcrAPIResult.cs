using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupManager.API.Tencent.Models
{
    public class OcrAPIResult
    {
        public class Itemcoord
        {
            public int x { get; set; }
            public int y { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Word
        {
            public string character { get; set; }
            public double confidence { get; set; }
        }

        public class Parag
        {
            public int word_size { get; set; }
            public int parag_no { get; set; }
        }

        public class Item
        {
            public Itemcoord itemcoord { get; set; }
            public double itemconf { get; set; }
            public string itemstring { get; set; }
            public List<object> coords { get; set; }
            public List<Word> words { get; set; }
            public List<object> candword { get; set; }
            public Parag parag { get; set; }
        }

        public class Result
        {
            public int errorcode { get; set; }
            public string errormsg { get; set; }
            public List<Item> items { get; set; }
            public string session_id { get; set; }
            public double angle { get; set; }
            public List<object> _class { get; set; }
        }
    }
}
