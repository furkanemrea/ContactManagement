using ContactAPI.Core.Entities;
using ContactAPI.Core.Repositories.Query.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Core.Repositories.Query
{
    public interface IContactQueryRepository : IQueryRepository<Contact>
    {
        Task<IReadOnlyList<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(Int64 id);
    }
}
