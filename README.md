# dynamic-dns-gateway

A gateway that exposes a dynamic DNS API to update non-dynamic DNS providers, such as AWS Route53

### Huhwhat?

Many consumer internet routers and modems support updating a dynamic DNS service with the device's current IP address, one of the most popular options being DynDNS. If you use a traditional DNS provider, however, you'd normally be out of luck if you want dynamic DNS updates.

This project aims to bridge this gap, exposing a DynDNS-like API to consumer devices while passing through the update to your traditional DNS provider in the background.

### Okay, tell me more

Written in C# and targeting ASP.NET Core 2.0, this project is currently in a barebones pre-alpha non-production state - but it works for Route53.

### That's... nice. How do I get it working?

Deploy it to your [ASP.NET Core](https://www.microsoft.com/net/download/core#/runtime)-compatible server, [set up your AWS API credentials](http://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config-creds.html), and then point your consumer device's dynamic DNS feature at `[your_server]/api/dyndns/update?hostname=[hostname]&myip=[ip]`
