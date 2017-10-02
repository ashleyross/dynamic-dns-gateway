using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DynamicDnsGateway.Managers;
using System.Net;
using Amazon.Route53;

namespace DynamicDnsGateway.Controllers
{
    [Produces("application/json")]
    [Route("api/dyndns")]
    public class DynDnsController : Controller
    {
        private Updater _updater;

        // TODO: Move this injection to where it's actually needed
        public DynDnsController(IAmazonRoute53 amazonRoute53)
        {
            _updater = new Updater(amazonRoute53);
        }

        // GET: api/dyndns/update
        [HttpGet("update", Name = "Update")]
        public string Update(string hostname, string myip)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(myip);
                return _updater.Update(hostname, ipAddress) ? "good" : "nochg";
            }   catch (Exception ex)
            {
                // TODO: Log things nicely
                return "911";
            }
        }
    }
}
