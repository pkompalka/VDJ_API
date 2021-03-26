using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VDJAPI.Models;

namespace VDJAPI.Controllers
{
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly RequestContext requestContext;

        public RequestsController(RequestContext context)
        {
            requestContext = context;
        }

        // GET: api/Requests/{int}
        [HttpGet]
        [Route("api/Requests/{fromWhichNumber:int}")]
        public ActionResult<IEnumerable<Request>> Get(int fromWhichNumber)
        {
            List<Request> requestList = requestContext.Requests.OrderBy(q => q.Id).ToList();
            List <Request> requestListToSend = new List<Request>();

            for(int i = fromWhichNumber; i < requestList.Count; i++)
            {
                requestListToSend.Add(requestList[i]);
            }
            return requestListToSend;
        }

        // POST: api/Requests
        [HttpPost]
        [Route("api/Requests")]
        public ActionResult Post([FromBody] Request request)
        {
            Request requestToAdd = request;
            int idToAdd = (from n in requestContext.Requests orderby n.Id descending select n.Id).FirstOrDefault();
            requestToAdd.Id = idToAdd + 1;
            requestContext.Requests.Add(requestToAdd);
            requestContext.SaveChanges();
            return Ok();
        }

        // DELETE: api/Requests
        [HttpDelete]
        [Route("api/Requests")]
        public ActionResult Delete()
        {
            requestContext.Requests.RemoveRange(requestContext.Requests.ToList());
            requestContext.SaveChanges();
            return Ok();
        }
    }
}
