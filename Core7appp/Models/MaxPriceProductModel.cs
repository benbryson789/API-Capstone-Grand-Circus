using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core7appp.Models
{
    public class MaxPriceProductModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int SupplierId { get; set; }

        public string QuantityPerUnit { get; set; }

        public double UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public string Category { get; set; }
        public string[] OrderDetails { get; set; }


    }
}
