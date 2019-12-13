using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public virtual IEnumerable<Staff> Staffs { get; set; }
        public virtual IEnumerable<RoleAllow> RoleAllows { get; set; }
    }
}
