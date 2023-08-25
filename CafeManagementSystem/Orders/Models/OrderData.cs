using System;
using System.Collections.Generic;
using System.Text;

namespace CafeManagementSystem.Orders.Models
{
    public class OrderData
    {
        public OrderData()
        {
            Initialize();
        }


        public int Num { get; set; }
        public int Flag { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal Sum { get; set; }
        public string Item { get; set; }
        public string Category { get; set; }

        private void Initialize()
        {
            Num = 1;
            Flag = 0;
            Price = 0;
            Total = 0;
            Sum = 0;
            Item = string.Empty;
            Category = string.Empty;
        }
    }
}

