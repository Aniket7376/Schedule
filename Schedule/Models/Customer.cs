using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Schedule.Models;

namespace Schedule.Models
{
    public class Customer
    {
        public String Name { get; set; }
        //public List<ItemDetails> Order { get; set; }
        public String Lat { get; set; }
        public String Lon { get; set; }
    }
}