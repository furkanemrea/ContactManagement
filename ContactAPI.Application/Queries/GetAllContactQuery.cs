using ContactAPI.Application.Response;
using ContactAPI.Core.Entities;
using ContactAPI.Core.Models.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Application.Queries
{
    public record GetAllContactQuery : IRequest<EntityResponse<IReadOnlyList<ContactResponse>>>
    {
        
    }
}
