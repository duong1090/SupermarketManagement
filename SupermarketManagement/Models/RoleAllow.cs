using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models
{
    public class RoleAllow
    {
        [Key]
        [Column(Order = 1)]
        public int RoleID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int OperatingID { get; set; }
  
        public bool Alow { get; set; }
        public virtual Role Role { get; set; }
        public virtual Operating Operating { get; set; }
    }
}
