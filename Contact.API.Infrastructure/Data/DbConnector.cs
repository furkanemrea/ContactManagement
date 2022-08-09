using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.API.Infrastructure.Data
{
    public class DbConnector
    {
        private readonly ContactContext _contactContext;

        public DbConnector()
        {
        }

        protected DbConnector(ContactContext contactContext)
        {
            _contactContext=contactContext;

        }
        public DbContext GetContext()
        {
            return _contactContext;
        }
    }
}
