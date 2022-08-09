using ContactAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ContactAPI.Application.Response
{
    public class CreateContactResponse
    {
        public ContactAPI.Core.Entities.Contact Contact { get; set; }
        public List<Email> Email { get; set; }
        public List<Address> Address { get; set; }
        public List<Date> Dates { get; set; }
        public List<InstantMessage> InstantMessages { get; set; }
        public List<Phone> Phones { get; set; }
        public List<SocialProfile> SocialProfiles { get; set; }
        public List<Url> Urls { get; set; }

    }
}
