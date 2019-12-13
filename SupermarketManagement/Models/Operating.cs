using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models
{
    public class Operating
    {
        public int OperatingID { get; set; }
        public string Detail { get; set; }
        public virtual IEnumerable<RoleAllow> RoleAllows { get; set; }
    }
}
