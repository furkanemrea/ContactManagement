using ContactAPI.Core.Models.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Application.Response
{
    public class ContactResponse
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }
        public string RingTone { get; set; }
        public string TextTone { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Note { get; set; }
        public byte? RowStatusId { get; set; }
    }
}
