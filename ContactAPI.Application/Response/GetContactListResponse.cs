using ContactAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Application.Response
{
    public class GetContactListResponse: BaseResponse
    {
        public List<ContactAPI.Core.Entities.Contact> Contacts { get; set; }
    }
}
