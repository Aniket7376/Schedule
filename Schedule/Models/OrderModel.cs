using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Schedule.Models
{
    public class OrderModel
    {
        public Customer CustomerOrdering;
        public ItemDetails ItemOrdered;
        public int Quantity;
        public int OrderId;
    }
}