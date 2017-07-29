using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Schedule.Models;

namespace Schedule.Services
{
    public class Item
    {
        public static List<ItemDetails> CopyOfInventory = new List<ItemDetails>();

        public void  storeList(List<ItemDetails> OriginalInventory)
        {
            CopyOfInventory = OriginalInventory;
        }

        public List<ItemDetails> getInventory()
        {
            return CopyOfInventory;
        }
    }
}