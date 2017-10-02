using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DynamicDnsGateway.Controllers
{
    [Route("api/ping")]
    public class PingController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "ready";
        }
    }
}
