using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupManager.Permission
{
    public enum PermissionType
    {
        机主 = 0,
        群主 = 1,
        群主_管理员 = 2,
        群主_管理员_风纪委员 = 3,
        风纪委员 = 4,
        群员 = 5
    }

    public class Permission
    {
        public string UUID { get; set; }
        public string Content { get; set; }
        public PermissionType PermissionType { get; set; }
    }
}
