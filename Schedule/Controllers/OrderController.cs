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
    public class OrderController : ApiController
    {
        static List<Customer> customers = new List<Customer>();
        static List<ItemDetails> Inventory = new List<ItemDetails>();
        static List<OrderModel> CurrentOrders = new List<OrderModel>();
        static int OrderNumber = 1;

        [HttpGet]
        [Route("api/order/inventory")]
        public IHttpActionResult ViewInventory()
        {
            Item ToCopy = new Item();
            Inventory = ToCopy.getInventory();
            return Content(HttpStatusCode.OK, Inventory);
        }


        //To Place a New Order
        [HttpPost]
        [Route("api/order/neworder")]
        public IHttpActionResult PlaceNewOrder(OrderModel newItemOrder)
        {
            Item ToCopy = new Item();
            Inventory = ToCopy.getInventory();
            LatAndLong generator = new LatAndLong();
            int index =0;
            Boolean found = false;
            for(int i=0;i<customers.Count;i++)
            {
                if(customers[i].Name == newItemOrder.CustomerOrdering.Name)
                {
                    found = true;
                    index = i;
                    break;
                }
            }
            if(!found)
            {
                newItemOrder.CustomerOrdering.Lat = generator.GetLat();
                newItemOrder.CustomerOrdering.Lon = generator.GetLong();
                customers.Add(newItemOrder.CustomerOrdering);
            }
            else
            {
                newItemOrder.CustomerOrdering.Lat = customers.ElementAt(index).Lat;
                newItemOrder.CustomerOrdering.Lon = customers.ElementAt(index).Lon;
            }

            Boolean present = false;
            for(int i=0;i<Inventory.Count;i++)
            {
                if (newItemOrder.ItemOrdered.Name == Inventory.ElementAt(i).Name)
                {
                    present = true;
                }
            }
            newItemOrder.ItemOrdered.ItemId = OrderNumber++;
            if(!present)
            {
                return Content(HttpStatusCode.BadRequest, "No such Item Exists In Our Inventory");
            }
            else
            {
                CurrentOrders.Add(newItemOrder);
                return Content(HttpStatusCode.OK, "Order Successfully Placed");
            }
           
        }

        [HttpGet]
        [Route("api/order/vieworder/{name}")]
        public IHttpActionResult ViewOrder(String name)
        {
            List<OrderModel> orderOfOnePerson = new List<OrderModel>();
            for(int i=0;i<CurrentOrders.Count;i++)
            {
                if(CurrentOrders[i].CustomerOrdering.Name == name)
                {
                    orderOfOnePerson.Add(CurrentOrders[i]);
                }
            }
            return Content(HttpStatusCode.OK, orderOfOnePerson);
        }


        //To cancel an existing order
        [HttpPost]
        [Route("api/order/cancelOrder")]
        public IHttpActionResult CancelOrder(int orderId)
        {
            ActiveAgents GetNextAgent = new ActiveAgents();
            PickupAgent agent = new PickupAgent();
            Boolean exists = false;
            OrderModel orderCancelled=new OrderModel();
            for(int i=0;i<CurrentOrders.Count;i++)
            {
                if(orderId == CurrentOrders.ElementAt(i).OrderId)
                {
                    exists = true;
                    orderCancelled =CurrentOrders.ElementAt(i);
                    break;
                }
            }

            if(exists)
            {
                CurrentOrders.Remove(orderCancelled);
                agent= GetNextAgent.NextFreeAgent(orderCancelled.CustomerOrdering.Lat,orderCancelled.CustomerOrdering.Lon);
                return Content(HttpStatusCode.OK, agent);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "No such order exists");
            }

        }

    }
}
