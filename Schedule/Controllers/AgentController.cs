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

   
    public class AgentController : ApiController
    {
        //global vars
        static List<PickupAgent> agentList = new List<PickupAgent>();
        static int id = 1;

        //for viewing cuurent agents
        [HttpGet]
        [Route("api/agent/viewagent")]
        public IHttpActionResult viewAgents()
        {
            if (agentList.Count == 0)
            {
                return Content(HttpStatusCode.NoContent, "No Agents");
            }
            else
            {
                return Content(HttpStatusCode.OK, agentList);
            }
        }

        [HttpPost]
        [Route("api/agent/addagent")]
        public IHttpActionResult addAgent(PickupAgent agentDetails)
        {

            Boolean Found = false;
            PickupAgent compareAgent = new PickupAgent();
            LatAndLong generator = new LatAndLong();

            int Size = agentList.Count;
            agentDetails.Id = id++;
            agentDetails.Lat = generator.GetLat();
            agentDetails.Lon = generator.GetLong();

            //Base case
            if (Size == 0)
            {
                agentList.Add(agentDetails);
                return Content(HttpStatusCode.OK, agentList);
            }

            //Check if Exists
            for (int i = 0; i < Size; i++)
            {
                compareAgent = agentList.ElementAt(i);

                if (compareAgent.Id == agentDetails.Id)
                {
                    Found = true;
                }
            }

            if (!Found)
            {

                agentList.Add(agentDetails);

                return Content(HttpStatusCode.OK, agentList);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Agent Already Exists");
            }

            
        }



    }
}
