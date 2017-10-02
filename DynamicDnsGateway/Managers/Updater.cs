using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Route53;

namespace DynamicDnsGateway.Managers
{
    public class Updater
    {
        private static Dictionary<string, IPAddress> _updates = new Dictionary<string, IPAddress>();

        private Route53Updater _route53Updater;

        // TODO: Move this injection to where it's actually needed
        public Updater(IAmazonRoute53 amazonRoute53)
        {
             _route53Updater = new Route53Updater(amazonRoute53);
        }

        public bool Update(string hostname, IPAddress ipAddress)
        { 
            if (_updates.ContainsKey(hostname))
            {
                if (_updates[hostname].Equals(ipAddress))
                {
                    return false;
                }
            }

            _route53Updater.Update(hostname, ipAddress);
            _updates[hostname] = ipAddress;
            return true;
        }
    }
}
