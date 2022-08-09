using Contact.API.Infrastructure.Data;
using Contact.API.Infrastructure.Repository.Query.Base;
using ContactAPI.Core.Repositories.Query;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.API.Infrastructure.Repository.Query
{
    public class ContactQueryRepository : QueryRepository<ContactAPI.Core.Entities.Contact>, IContactQueryRepository
    {
        public ContactQueryRepository(ContactContext contactContext):base(contactContext)
        {
        }
        Task<IReadOnlyList<ContactAPI.Core.Entities.Contact>> IContactQueryRepository.GetAllAsync()
        {
            IReadOnlyList<ContactAPI.Core.Entities.Contact> contacts = _context.Contact.ToList();
            return Task.FromResult(contacts);
        }

        Task<ContactAPI.Core.Entities.Contact> IContactQueryRepository.GetByIdAsync(long id)
        {
            return Task.FromResult(_context.Contact.Find(id));
        }
    }
}
