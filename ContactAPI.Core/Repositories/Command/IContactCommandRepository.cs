using ContactAPI.Core.Entities;
using ContactAPI.Core.Models.Base;
using ContactAPI.Core.Repositories.Command.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = ContactAPI.Core.Entities.Contact;
namespace ContactAPI.Core.Repositories.Command
{
    public interface IContactCommandRepository:ICommandRepository<Contact>
    {
        Task<EntityResponse<Contact>> Create(Contact contact);
        Task<EntityResponse<List<Contact>>> GetContactByEmailKeyword(string emailKeyword);
        Task<EntityResponse<List<ContactAPI.Core.Entities.Contact>>> GetContactByUrlKeyword(string urlKeyword);
        Task<EntityResponse<List<ContactAPI.Core.Entities.Contact>>> GetContactByPhoneKeyword(string phoneKeyword);
        Task<EntityResponse<List<ContactAPI.Core.Entities.Contact>>> GetContactByAddressKeyword(string addressKeyword);
        Task<EntityResponse<List<ContactAPI.Core.Entities.Contact>>> GetContactBySocialProfile(string socialProfileKeyword);
        Task<EntityResponse<List<ContactAPI.Core.Entities.Contact>>> GetContactByKeyword(string keyword);
    }
}
