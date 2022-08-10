using CommonLibrary;
using ContactAPI.Application.Commands;
using ContactAPI.Application.Handlers.CommandHandler.Extensions;
using ContactAPI.Application.Mapper;
using ContactAPI.Application.Mapper.Profiles;
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
using Contact = ContactAPI.Core.Entities.Contact;
namespace ContactAPI.Application.Handlers.CommandHandler
{
    public class CreateContactHandler : IRequestHandler<CreateContactCommand, EntityResponse<CreateContactResponse>>
    {
        private readonly IContactCommandRepository _contactCommandRepository;
        private readonly IContactQueryRepository _contactQueryRepository;

        public CreateContactHandler(IContactCommandRepository contactCommandRepository, IContactQueryRepository contactQueryRepository)
        {
            _contactCommandRepository=contactCommandRepository;
            _contactQueryRepository=contactQueryRepository;
        }
        public async Task<EntityResponse<CreateContactResponse>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            List<Exception> exceptions = new();
            ContactAPI.Core.Entities.Contact contact = new();
            EntityResponse<Core.Entities.Contact> createdContactResponse = new();
            if (request.IsNull())
            {
                return EntityResponse<CreateContactResponse>.Builder().SetErrorStatus().SetMessage("Contact is not created because request object is null").Build();
            }
            else
            {
                if (!request.RequestIsValid())
                {
                    return EntityResponse<CreateContactResponse>.Builder().SetErrorStatus().SetMessage("Name,LastName,Phone,Address are required.").Build();
                }
                contact = ContactMapper.Mapper.Map<ContactAPI.Core.Entities.Contact>(request);
                ContactAPI.Core.Entities.Contact isAlreadyExistContact = await _contactQueryRepository.GetContactByName(request.FirstName);
                if (isAlreadyExistContact != null)
                {
                    return EntityResponse<CreateContactResponse>.Builder().SetDuplicateStatus().SetMessage("User already exist in database").Build();
                }

                request.Emails.EmailChecker(contact, exceptions);
                request.Urls.UrlChecker(contact, exceptions);
                request.Phones.PhoneChecker(contact, exceptions);
                request.Addresses.AddressChecker(contact, exceptions);
                request.InstantMessages.InstantMessageChecker(contact, exceptions);
                request.SocialProfiles.SocialProfileChecker(contact, exceptions);
                createdContactResponse = await _contactCommandRepository.Create(contact);
            }
            if (createdContactResponse.IsSuccess())
            {
                return EntityResponse<CreateContactResponse>.Builder().SetExceptions(exceptions).SetSuccessStatus().Build();
            }
            else
            {
                return EntityResponse<CreateContactResponse>.Builder().SetErrorStatus().SetMessage("Contact is not created").Build();
            }

        }
    }
}
