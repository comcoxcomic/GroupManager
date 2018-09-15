using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace GroupManager.DataBase.Models
{
    public class Judgement
    {
        [Key]
        public long Id { get; set; }
        public string Data { get; set; }
    }
}
