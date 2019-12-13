using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupermarketManagement.Models;

namespace SupermarketManagement.Models.ViewModel
{
    public class StatisticViewModel
    {
        public int categoryID { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public int NumberOfMonth { get; set; }

        public Receipt_Detail receipt_Detail { get; set; }

        public IEnumerable<Receipt_Detail> receipt_Details { get; set; }
    }
}
