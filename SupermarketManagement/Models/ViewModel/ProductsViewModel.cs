using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Models.ViewModel
{
    public class ProductsViewModel
    {
        public Product Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
