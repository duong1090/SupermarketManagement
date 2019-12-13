using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models
{
    public class User
    {

        [Key]
        public int ID { get; set; }
        public int StaffID { get; set; }

        public string PassWord { get; set; }
        public virtual Staff Staff { get; set; }


    }
}
