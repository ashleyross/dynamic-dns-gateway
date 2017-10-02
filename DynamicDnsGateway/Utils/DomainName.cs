/*
Copyright 2012 Bespoke Industries http://www.bespokeindustries.com/

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDnsGateway.Utils
{
    public class DomainName
    {
        public string Host { get; set; }

        /// <summary>
        /// Domain Name with the TLD included.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Top Level Domain Name
        /// </summary>
        public string Tld { get; set; }

        public static DomainName Parse(string domainNameString)
        {
            if (string.IsNullOrWhiteSpace(domainNameString))
            {
                throw new ArgumentException("domainName cannot be null or empty.");
            }

            domainNameString = domainNameString.TrimEnd('.');

            var segments = domainNameString.Count(character => character == '.') + 1;

            var domainName = new DomainName();

            //What about domain names such as .co.uk?
            if (segments == 1)
            {
                domainName.Tld = domainNameString;
            }
            else if (segments == 2)
            {
                //Domain + TLD
                domainName.Domain = domainNameString;
                domainName.Tld = domainNameString.Substring(domainNameString.LastIndexOf('.') + 1);
            }
            else if (segments == 3)
            {
                domainName.Host = domainNameString.Substring(0, domainNameString.IndexOf('.'));
                domainName.Domain = domainNameString.Substring(domainNameString.IndexOf('.') + 1);
                domainName.Tld = domainNameString.Substring(domainNameString.LastIndexOf('.') + 1);
            }
            else
            {
                //TODO: Support domain names with more segments.
                throw new ArgumentException("Invalid domain name");
            }

            return domainName;
        }
    }
}
