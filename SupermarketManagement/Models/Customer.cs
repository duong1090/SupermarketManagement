using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Sex { get; set; }
        public int Point { get; set; }
        public int Rate { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        public string Address { get; set; }
        public bool Status { get; set; }
        public virtual IEnumerable<Bill> Bills { get; set; }
    }
}
