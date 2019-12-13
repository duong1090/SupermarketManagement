using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models
{
    public class Bill_Detail
    {
        [Key]
        public int Bill_DetailID { get; set; }
        public int ProductID { get; set; }
        public int BillID { get; set; }
        public float TotalPrice { get; set; }
        public int ProductAmount { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Product Product {get; set; }


    }
}
