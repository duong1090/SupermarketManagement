using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models.ViewModel
{
    public class ReceiptDetailModel
    {
        public Receipt_Detail Receipt_Detail { get; set; }
        public IEnumerable<Receipt_Detail> Receipt_Details { get; set; }
    }
}
