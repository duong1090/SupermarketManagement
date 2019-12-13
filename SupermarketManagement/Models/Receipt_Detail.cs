using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models
{
    public class Receipt_Detail
    {
        [Key]
        public int Receipt_DetailID { get; set; }
        public int ImportReceiptID { get; set; }
        public int ProductID { get; set; }
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ManufactureDate { get; set; }
        public int Amount { get; set; }
        public virtual Product Product { get; set; }
    }
}
