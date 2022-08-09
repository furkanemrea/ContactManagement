using CommonLibrary;
using Contact.API.Infrastructure.Data;
using ContactAPI.Core.Repositories.Command.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.API.Infrastructure.Repository.Command.Base
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        protected readonly ContactContext _context;

        public CommandRepository(ContactContext context)
        {
            _context = context;
        }
        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            entity.GetType().GetProperty("RowStatusId").SetValue(entity, RowStatusValues.Delete);
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
