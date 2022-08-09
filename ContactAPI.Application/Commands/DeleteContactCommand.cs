using ContactAPI.Application.Response;
using ContactAPI.Core.Models.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Application.Commands
{
    public class DeleteContactCommand:IRequest<EntityResponse<ContactResponse>>
    {
        public Int64 Id { get; set; }
        public DeleteContactCommand(Int64 id)
        {
            this.Id = id;
        }
    }
}
