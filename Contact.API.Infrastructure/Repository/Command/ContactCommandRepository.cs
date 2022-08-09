using CommonLibrary;
using Contact.API.Infrastructure.Data;
using Contact.API.Infrastructure.Repository.Command.Base;
using ContactAPI.Core.Models.Base;
using ContactAPI.Core.Repositories.Command;
using ContactAPI.Core.Repositories.Command.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Contact = ContactAPI.Core.Entities.Contact;

namespace Contact.API.Infrastructure.Repository.Command
{
    public class ContactCommandRepository : CommandRepository<ContactAPI.Core.Entities.Contact>, IContactCommandRepository
    {
        public ContactCommandRepository(ContactContext context) : base(context)
        {

        }
        public async Task<EntityResponse<ContactAPI.Core.Entities.Contact>> Create(ContactAPI.Core.Entities.Contact contact)
        {
            try
            {
                contact.RowStatusId = RowStatusValues.Active;
                await _context.Contact.AddAsync(contact);
                _context.SaveChanges();

                return EntityResponse<ContactAPI.Core.Entities.Contact>.Builder().SetSuccessStatus().SetResult(contact).Build();
            }
            catch (Exception ex)
            {
                return EntityResponse<ContactAPI.Core.Entities.Contact>.Builder().SetException(ex).SetErrorStatus().Build();
            }
        }
        public async Task<EntityResponse<List<ContactAPI.Core.Entities.Contact>>> GetContactByEmailKeyword(string emailKeyword)
        {
            List<ContactAPI.Core.Entities.Contact> contacts = await _context.Contact
                .Include(x => x.Email)
                .Where(x =>
                    x.Email.Any(x => x.Value.Contains(emailKeyword)) &&
                    x.RowStatusId == RowStatusValues.Active
                 ).ToListAsync();
            return EntityResponse<List<ContactAPI.Core.Entities.Contact>>.Builder().SetResult(contacts).SetSuccessStatus().Build();
        }
        public async Task<EntityResponse<List<ContactAPI.Core.Entities.Contact>>> GetContactByUrlKeyword(string urlKeyword)
        {
            List<ContactAPI.Core.Entities.Contact> contacts = await _context.Contact
                .Include(x => x.Url)
                .Where(x => 
                    x.Url.Any(x => x.Value.Contains(urlKeyword)) &&
                    x.RowStatusId == RowStatusValues.Active
                 ).ToListAsync();
            return EntityResponse<List<ContactAPI.Core.Entities.Contact>>.Builder().SetResult(contacts).SetSuccessStatus().Build();
        }
        public async Task<EntityResponse<List<ContactAPI.Core.Entities.Contact>>> GetContactByPhoneKeyword(string phoneKeyword)
        {
            List<ContactAPI.Core.Entities.Contact> contacts = await _context.Contact
                .Include(x => x.Phone)
                .Where(x => 
                    x.Phone.Any(x => x.Value.Contains(phoneKeyword)) &&
                    x.RowStatusId == RowStatusValues.Active)
                .ToListAsync();
            return EntityResponse<List<ContactAPI.Core.Entities.Contact>>.Builder().SetResult(contacts).SetSuccessStatus().Build();
        }
        public async Task<EntityResponse<List<ContactAPI.Core.Entities.Contact>>> GetContactByAddressKeyword(string addressKeyword)
        {
            List<ContactAPI.Core.Entities.Contact> contacts = await _context.Contact.Include(x => x.Address)
                .Where(x =>
                x.Address.Any(y => y.City.Contains(addressKeyword)) ||
                x.Address.Any(y => y.Country.Contains(addressKeyword)) ||
                x.Address.Any(y => y.Href.Contains(addressKeyword)) ||
                x.Address.Any(y => y.StreetPrefix.Contains(addressKeyword)) ||
                x.Address.Any(y => y.StreetSuffix.Contains(addressKeyword)) ||
                x.Address.Any(y => y.Type.Contains(addressKeyword))
                ).ToListAsync();
            return EntityResponse<List<ContactAPI.Core.Entities.Contact>>.Builder().SetResult(contacts).SetSuccessStatus().Build();
        }
        public async Task<EntityResponse<List<ContactAPI.Core.Entities.Contact>>> GetContactBySocialProfile(string socialProfileKeyword)
        {
            List<ContactAPI.Core.Entities.Contact> contacts = await _context.Contact.Include(x => x.Address)
               .Where(x =>
               x.SocialProfile.Any(y => y.Value.Contains(socialProfileKeyword))).ToListAsync();
            return EntityResponse<List<ContactAPI.Core.Entities.Contact>>.Builder().SetResult(contacts).SetSuccessStatus().Build();
        }

        public async Task<EntityResponse<List<ContactAPI.Core.Entities.Contact>>> GetContactByKeyword(string keyword)
        {
            List<ContactAPI.Core.Entities.Contact> contacts = await _context.Contact
                .Include(x => x.Address)
                .Include(x => x.Phone)
                .Include(x => x.SocialProfile)
                .Include(x => x.Url)
                .Include(x => x.Email)
                .Where(x =>
                    x.Address.Any(y => y.City.Contains(keyword)) ||
                    x.Address.Any(y => y.Country.Contains(keyword)) ||
                    x.Address.Any(y => y.Href.Contains(keyword)) ||
                    x.Address.Any(y => y.StreetPrefix.Contains(keyword)) ||
                    x.Address.Any(y => y.StreetSuffix.Contains(keyword)) ||
                    x.Address.Any(y => y.Type.Contains(keyword)) ||
                    x.Phone.Any(y => y.Value.Contains(keyword)) ||
                    x.Url.Any(y => y.Value.Contains(keyword)) ||
                    x.Email.Any(y => y.Value.Contains(keyword)) ||
                    x.SocialProfile.Any(y => y.Value.Contains(keyword))
                ).ToListAsync();
            return EntityResponse<List<ContactAPI.Core.Entities.Contact>>.Builder().SetResult(contacts).SetSuccessStatus().Build();
        }

    }
}
