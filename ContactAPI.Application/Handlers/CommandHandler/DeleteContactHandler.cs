using CommonLibrary;
using ContactAPI.Application.Commands;
using ContactAPI.Application.Mapper;
using ContactAPI.Application.Response;
using ContactAPI.Core.Entities;
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
    public class DeleteContactHandler : IRequestHandler<DeleteContactCommand, EntityResponse<ContactResponse>>
    {
        private readonly IContactCommandRepository _contactCommandRepository;
        private readonly IContactQueryRepository _contactQueryRepository;

        public DeleteContactHandler(IContactCommandRepository contactCommandRepository, IContactQueryRepository contactQueryRepository)
        {
            _contactCommandRepository=contactCommandRepository;
            _contactQueryRepository=contactQueryRepository;
        }

        public async Task<EntityResponse<ContactResponse>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            ContactResponse deleteContactResponse = new ContactResponse();
            ContactAPI.Core.Entities.Contact existingContact = await _contactQueryRepository.GetByIdAsync(request.Id);

            if(existingContact == null)
            {
                return EntityResponse<ContactResponse>.Builder().SetErrorStatus().SetMessage("Contact is not found").Build();
            }
            await _contactCommandRepository.DeleteAsync(existingContact);
            deleteContactResponse = ContactMapper.Mapper.Map<ContactResponse>(existingContact);
            //deleteContactResponse.Status = EntityResponseStatus.Success;
            return EntityResponse<ContactResponse>.Builder().SetResult(deleteContactResponse).SetSuccessStatus().Build();
        }
    }
}
