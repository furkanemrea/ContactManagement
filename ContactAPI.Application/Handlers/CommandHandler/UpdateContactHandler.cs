using ContactAPI.Application.Commands;
using ContactAPI.Application.Response;
using ContactAPI.Core.Models.Base;
using ContactAPI.Core.Repositories.Command;
using ContactAPI.Core.Repositories.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAPI.Application.Handlers.CommandHandler
{
    public class UpdateContactHandler : IRequestHandler<UpdateContactCommand, EntityResponse<UpdateContactResponse>>
    {
        private readonly IContactQueryRepository _contactQueryRepository;
        private readonly IContactCommandRepository _contactCommandRepository;

        public UpdateContactHandler(IContactQueryRepository contactQueryRepository, IContactCommandRepository contactCommandRepository)
        {
            _contactQueryRepository=contactQueryRepository;
            _contactCommandRepository=contactCommandRepository;
        }

        public async Task<EntityResponse<UpdateContactResponse>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            ContactAPI.Core.Entities.Contact currentContact =await  _contactQueryRepository.GetByIdAsync(request.Id);
            
            if(currentContact == null)
            {
                return EntityResponse<UpdateContactResponse>.Builder().SetErrorStatus().SetMessage("Contact is not found").Build();
            }
            else
            {
                var contactIsAlreadyExist = await _contactQueryRepository.GetContactByName(request.FirstName);
                if(contactIsAlreadyExist != null)
                {
                    return EntityResponse<UpdateContactResponse>.Builder().SetErrorStatus().SetMessage("Contact is already exist").Build();
                }
                else
                {
                    currentContact.FirstName = request.FirstName;
                    currentContact.LastName = request.LastName;
                    await _contactCommandRepository.UpdateAsync(currentContact);
                    return EntityResponse<UpdateContactResponse>.Builder().SetSuccessStatus().Build();

                }

            }
        }
    }
}
