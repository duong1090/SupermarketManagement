using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models
{
    public class Staff
    {
        public int StaffID { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Sex { get; set; }

        public string Image { get; set; }

        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        public int RoleID { get; set; }

        public string Address { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }

        public virtual User Users { get; set; }
        public virtual Role Role { get; set; }
        public virtual IEnumerable<Bill> Bills { get; set; }
        public virtual IEnumerable<ImportReceipt> ImportReceipts { get; set; }


    }
}
