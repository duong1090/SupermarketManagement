using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models
{
    public class ImportReceipt
    {
        [Key]
        public int ImportReceiptID { get; set; }
        public int StaffID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Total { get; set; }

        public virtual Staff Staff { get; set; }
        

    }
}
