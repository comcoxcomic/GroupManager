using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace GroupManager.DataBase.Models
{
    public class ClusterConfig
    {
        [Key]
        public long Id { get; set; }
        public long GroupId { get; set; }
        public string Config { get; set; }
    }
}
