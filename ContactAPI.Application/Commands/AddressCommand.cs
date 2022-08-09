using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Application.Commands
{
    public class AddressCommand : BaseContactCommand
    {
        public string Href { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string StateOrProvince { get; set; }
        public string StreetPrefix { get; set; }
        public string StreetSuffix { get; set; }
        public string Type { get; set; }
    }
}
