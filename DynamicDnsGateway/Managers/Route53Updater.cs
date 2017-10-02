using Amazon.Route53;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Route53.Model;
using DynamicDnsGateway.Utils;

namespace DynamicDnsGateway.Managers
{
    public class Route53Updater
    {
        private IAmazonRoute53 route53Client;

        // This dependency-injected instance is populated from config
        public Route53Updater(IAmazonRoute53 amazonRoute53)
        {
            this.route53Client = amazonRoute53;
        }
        
        internal void Update(string hostname, IPAddress ipAddress)
        {
            var lhzTask = route53Client.ListHostedZonesAsync();

            ResourceRecord resourceRecord = new ResourceRecord(ipAddress.ToString());
            ResourceRecordSet resourceRecordSet = new ResourceRecordSet
            {
                Name = hostname,
                TTL = 30,
                Type = RRType.A,
                ResourceRecords = new List<ResourceRecord> { resourceRecord }
            };
            Change change = new Change
            {
                Action = ChangeAction.UPSERT,
                ResourceRecordSet = resourceRecordSet
            };
            ChangeBatch changeBatch = new ChangeBatch
            {
                Changes = new List<Change> { change },
                Comment = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture)
            };

            // This is wrong and does The Wrong Thing, I'm fairly certain
            string domain = DomainName.Parse(hostname).Domain;
            var zones = lhzTask.Result.HostedZones;
            string hostedZoneId = zones.Single(z => z.Name.StartsWith(domain)).Id;

            ChangeResourceRecordSetsRequest request = new ChangeResourceRecordSetsRequest
            {
                ChangeBatch = changeBatch,
                HostedZoneId = hostedZoneId
            };

            var crrsTask = route53Client.ChangeResourceRecordSetsAsync(request);
            var response = crrsTask.Result;
        }
    }
}
