using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models.ViewModel
{
    public class BillDetailsModel
    {
        public Bill_Detail bill_detail { get; set; }
        public IEnumerable<Bill_Detail> bill_details { get; set; }
    }
}
