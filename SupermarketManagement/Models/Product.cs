using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public float Price { get; set; }
        public string Producer { get; set; }
        public int Amount { get; set; }
        public int CategoryID { get; set; }

        [Required]
        public string Name { get; set; }
        public bool Status { get; set; }
        public virtual IEnumerable<Bill_Detail> Bill_Detail { get; set; }
        public virtual Category Category { get; set; }
        public virtual IEnumerable<Receipt_Detail> Receipt_Detail { get; set; }
    }
}
