using ContactAPI.Application.Response;
using ContactAPI.Core.Entities;
using ContactAPI.Core.Models.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValidationLibrary.Attributes;
using System.Threading.Tasks;

namespace ContactAPI.Application.Commands
{
    public class CreateContactCommand: IRequest<EntityResponse<CreateContactResponse>>
    {
        public long Id { get; set; }
        [StringData(maxLength:100)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }
        public string RingTone { get; set; }
        public string TextTone { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Note { get; set; }
        public List<EmailCommand> Emails { get; set; }
        public List<UrlCommand> Urls { get; set; }
        public List<PhoneCommand> Phones { get; set; }
        public List<AddressCommand> Addresses { get; set; }
        public List<InstantMessageCommand> InstantMessages { get; set; }
        public List<SocialProfileCommand> SocialProfiles { get; set; }
        public List<DateCommand> Dates { get; set; }
    }
}
