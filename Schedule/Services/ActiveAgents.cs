using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Schedule.Models;
using System.Net.Http;

namespace Schedule.Services
{
    public class ActiveAgents
    {
        public static List<PickupAgent> CurrentAgents = new List<PickupAgent>();
        HttpClient client = new HttpClient();
        String agentLat, agentLon;
        String custLat, custLon;
        int dist; 

        public void GetCopyOfOrder(List<PickupAgent> CopyOfList)
        {
            CurrentAgents = CopyOfList;
        }
        public List<PickupAgent> GetOrderList()
        {
            return CurrentAgents;
        }

        public PickupAgent NextFreeAgent(string lat, string lon)
        {
            
            int minDistance = 10000;
            custLat = lat;custLon = lon;
            PickupAgent NextAgent = new PickupAgent();
            int index=0;Boolean isFree = false;
            for(int i=0;i<CurrentAgents.Count;i++)
            {
                if(CurrentAgents.ElementAt(i).IsFree==true)
                {
                    agentLat = CurrentAgents.ElementAt(i).Lat;
                    agentLon = CurrentAgents.ElementAt(i).Lon;

                    if (dist < minDistance) { 
                        minDistance = dist;
                        index = i;
                        isFree = true;
                    }

                }
            }
            NextAgent = CurrentAgents.ElementAt(index);
            CurrentAgents.ElementAt(index).IsFree = false;
            return NextAgent;
        }

        public async void FreeUtil()
        {
            string url = "https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=" + custLat + "," + custLon + "&destination=";
            url += agentLat + "," + agentLon;
            var res = await client.GetAsync(url);
            string response = await res.Content.ReadAsStringAsync();
            dynamic jsonObj = JsonConvert.DesrializeObject<object>(response);
            dist = jsonObj['rows'][0]['elements'][0]['distance']['value'];
            //var json = await res.Content.ReadAsStringAsync();
            //JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        }

        
    }
}