using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models
{
    public class Bill
    {
        [Key]
        public int BillID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int StaffID { get; set; }
        public int? CustomerID { get; set; }
        public float TotalMoney { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual IEnumerable<Bill_Detail> Bill_Details { get; set; }
    }
}
