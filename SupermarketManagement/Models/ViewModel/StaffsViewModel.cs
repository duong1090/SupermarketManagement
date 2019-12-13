using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models.ViewModel
{
    public class StaffsViewModel
    {
        public Staff Staffs { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
