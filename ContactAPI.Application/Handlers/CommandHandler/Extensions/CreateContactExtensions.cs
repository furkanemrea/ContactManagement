using CommonLibrary;
using ContactAPI.Application.Commands;
using ContactAPI.Application.Mapper;
using ContactAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Application.Handlers.CommandHandler.Extensions
{
    public static class CreateContactExtensions
    {


        public static bool RequestIsValid(this CreateContactCommand createContactCommand)
        {
            return !string.IsNullOrEmpty(createContactCommand.FirstName) &&
                   !string.IsNullOrEmpty(createContactCommand.LastName) &&
                   createContactCommand.Phones != null &&
                   createContactCommand.Phones.Count > 0 &&
                   createContactCommand.Addresses != null &&
                   createContactCommand.Addresses.Count > 0;
        }
        public static void EmailChecker(this List<EmailCommand> emails, ContactAPI.Core.Entities.Contact contact, List<Exception> exceptions)
        {
            if (emails != null && emails.Count > 0)
            {
                foreach (var emailAddress in emails)
                {
                    // email valid check
                    if (!emailAddress.Value.EmailIsValid())
                    {
                        exceptions.Add(new Exception($"{emailAddress.Value} is not in expected format for mail "));
                    }
                    else
                    {
                        Email email = EmailMapper.Mapper.Map<Email>(emailAddress);
                        contact.Email.Add(email);
                    }
                }
            }
        }
        public static void UrlChecker(this List<UrlCommand> urls, ContactAPI.Core.Entities.Contact contact, List<Exception> exceptions)
        {
            if (urls != null && urls.Count > 0)
            {
                foreach (var tempUrl in urls)
                {
                    // email valid check
                    if (!tempUrl.Value.UrlIsValid())
                    {
                        exceptions.Add(new Exception($"{tempUrl.Value} is not in expected format for url"));
                    }
                    else
                    {
                        Url url = UrlMapper.Mapper.Map<Url>(tempUrl);
                        contact.Url.Add(url);
                    }
                }
            }

        }

        public static void PhoneChecker(this List<PhoneCommand> phones, ContactAPI.Core.Entities.Contact contact, List<Exception> exceptions)
        {
            if (phones != null && phones.Count > 0)
            {
                foreach (var tempPhone in phones)
                {
                    Phone phone = PhoneMapper.Mapper.Map<Phone>(tempPhone);
                    contact.Phone.Add(phone);
                }
            }

        }
        public static void AddressChecker(this List<AddressCommand> addresses, ContactAPI.Core.Entities.Contact contact, List<Exception> exceptions)
        {
            if (addresses != null && addresses.Count > 0)
            {
                foreach (var tempAddress in addresses)
                {
                    Address address = AddressMapper.Mapper.Map<Address>(tempAddress);
                    contact.Address.Add(address);
                }
            }

        }
        public static void InstantMessageChecker(this List<InstantMessageCommand> messages, ContactAPI.Core.Entities.Contact contact, List<Exception> exceptions)
        {
            if (messages != null && messages.Count > 0)
            {
                foreach (var tempInstantMessage in messages)
                {
                    InstantMessage instantMessage = InstantMessageMapper.Mapper.Map<InstantMessage>(tempInstantMessage);
                    contact.InstantMessage.Add(instantMessage);
                }
            }

        }
        public static void SocialProfileChecker(this List<SocialProfileCommand> profiles, ContactAPI.Core.Entities.Contact contact, List<Exception> exceptions)
        {
            if (profiles != null && profiles.Count>0)
            {
                foreach (var tempSocialProfile in profiles)
                {
                    SocialProfile socialProfile = SocialProfileMapper.Mapper.Map<SocialProfile>(tempSocialProfile);
                    contact.SocialProfile.Add(socialProfile);
                }
            }
        }
    }
}
