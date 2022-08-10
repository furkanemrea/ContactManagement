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
    public class UpdateContactCommand: BaseContactCommand, IRequest<EntityResponse<UpdateContactResponse>>
    {

        /// <summary>
        /// Related Contact Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Contact First Name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Contact Last Name
        /// </summary>
        public string LastName { get; set; }
    }
}
