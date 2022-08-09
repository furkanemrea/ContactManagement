using ContactAPI.Application.Response;
using ContactAPI.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Application.Handlers.QueryHandler
{
    public class GetContactByQueryHandler
    {
        public GetContactByQueryHandler()
        {

        }

        // queryType => phone
        // queryType => email
        // queryType => any
        // queryType => city
        // queryType => url
        // queryType => instantMessage
        public async Task Handle(string queryType,string keyword)
        {
            EntityResponse<IReadOnlyList<List<ContactResponse>>> response = new();



        }
    }
}
