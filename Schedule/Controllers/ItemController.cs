using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Schedule.Models;
using Schedule.Services;

namespace Schedule.Controllers
{
    
    public class ItemController : ApiController
    {
        static List<ItemDetails> itemInventory = new List<ItemDetails>();
        static int ItemId=1;

        [HttpPost]
        [Route("api/item/additem")]
        public IHttpActionResult addItems(List<ItemDetails> newItems)
        {
            ItemDetails item = new ItemDetails();

            for(int i=0;i<newItems.Count;i++)
            {
                item = newItems.ElementAt(i);
                item.ItemId = ItemId++;
                itemInventory.Add(item);
            }

            Item Storeinventory = new Item();
            Storeinventory.storeList(itemInventory);

            return Content(HttpStatusCode.OK, itemInventory);
        }

        [HttpGet]
        [Route("api/item/viewinventory")]
        public IHttpActionResult viewInventory()
        {
            if(itemInventory.Count==0)
            {
                return Content(HttpStatusCode.BadRequest, "No Inventory added till Now");
            }
            else
            {
                return Content(HttpStatusCode.OK, itemInventory);
               
            }
        }

    }
}
