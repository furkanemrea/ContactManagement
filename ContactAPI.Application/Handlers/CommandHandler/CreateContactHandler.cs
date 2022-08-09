using CommonLibrary;
using ContactAPI.Application.Commands;
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
            ContactAPI.Core.Entities.Contact contact = ContactMapper.Mapper.Map<ContactAPI.Core.Entities.Contact>(request);
            StringBuilder responseMessages = new StringBuilder();
            if (request != null)
            {
                ContactAPI.Core.Entities.Contact isAlreadyExistContact =  await _contactQueryRepository.GetContactByName(request.FirstName);
                if (isAlreadyExistContact != null)
                {
                    return EntityResponse<CreateContactResponse>.Builder().SetDuplicateStatus().SetMessage("User already exist in database").Build();
                }
                if (request.Emails != null && request.Emails.Count > 0)
                {
                    // Email foreach
                    foreach (var emailAddress in request.Emails)
                    {
                        // email valid check
                        if (!emailAddress.Value.EmailIsValid())
                        {
                            responseMessages.AppendLine($"{emailAddress.Value} is not in expected format for mail ||");
                        }
                        else
                        {
                            Email email = EmailMapper.Mapper.Map<Email>(emailAddress);
                            contact.Email.Add(email);
                        }
                    }
                }
                if(request.Urls != null && request.Urls.Count > 0)
                {
                    foreach (var tempUrl in request.Urls)
                    {
                        // email valid check
                        if (!tempUrl.Value.UrlIsValid())
                        {
                            responseMessages.AppendLine($"{tempUrl.Value} is not in expected format || ");
                        }
                        else
                        {
                            Url url = UrlMapper.Mapper.Map<Url>(tempUrl);
                            contact.Url.Add(url);
                        }
                    }
                }
                if(request.Phones != null && request.Phones.Count > 0)
                {
                    foreach (var tempPhone in request.Phones)
                    {
                        Phone phone = PhoneMapper.Mapper.Map<Phone>(tempPhone);
                        contact.Phone.Add(phone);
                    }
                }
                if(request.Addresses != null && request.Addresses.Count > 0)
                {
                    foreach (var tempAddress in request.Addresses)
                    {
                        Address address = AddressMapper.Mapper.Map<Address>(tempAddress);
                        contact.Address.Add(address);
                    }
                }
                if(request.InstantMessages != null && request.InstantMessages.Count > 0)
                {
                    foreach (var tempInstantMessage in request.InstantMessages)
                    {
                        InstantMessage instantMessage = InstantMessageMapper.Mapper.Map<InstantMessage>(tempInstantMessage);
                        contact.InstantMessage.Add(instantMessage);
                    }
                }
                if(request.SocialProfiles != null && request.SocialProfiles.Count>0)
                {
                    foreach (var tempSocialProfile in request.SocialProfiles)
                    {
                        SocialProfile socialProfile = SocialProfileMapper.Mapper.Map<SocialProfile>(tempSocialProfile);
                        contact.SocialProfile.Add(socialProfile);
                    }
                }
                if(request.Dates != null && request.Dates.Count > 0)
                {
                    foreach (var tempDate in request.Dates)
                    {
                        //Date date = DateMapper.Mapper.Map<Date>(tempDate);
                        //contact.Date.Add(date);
                    }
                }
            }
            var createdContactResponse = await _contactCommandRepository.Create(contact);
            if (createdContactResponse.IsSuccess())
            {
                return EntityResponse<CreateContactResponse>.Builder().SetSuccessStatus().SetMessage(responseMessages.ToString()).Build();
            }
            else
            {
                return EntityResponse<CreateContactResponse>.Builder().SetErrorStatus().SetMessage("Contact is not created").Build();
            }

        }
    }
}
