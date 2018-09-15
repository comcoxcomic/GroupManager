using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace GroupManager.DataBase.Models
{
    public class Warning
    {
        [Key]
        public long Id { get; set; }
        public long GroupId { get; set; }
        public long QQ { get; set; }
        public long Count { get; set; }
    }
}
