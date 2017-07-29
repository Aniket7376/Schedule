using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schedule.Models
{
    public class PickupAgent
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Lat { get; set; }
        public String Lon { get; set; }
        public Boolean IsFree { get; set; }
    }
}