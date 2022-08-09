using Microsoft.Extensions.Configuration;
using ContactAPI.Core.Repositories.Query.Base;
using Contact.API.Infrastructure.Data;

namespace Contact.API.Infrastructure.Repository.Query.Base
{
    public class QueryRepository<T> : DbConnector, IQueryRepository<T> where T : class
    {
        protected readonly ContactContext _context;
        public QueryRepository(ContactContext context)
        {
            _context = context;
        }
    }
}
