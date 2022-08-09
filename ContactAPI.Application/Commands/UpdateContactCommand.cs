using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Application.Commands
{
    public class UpdateContactCommand: BaseContactCommand
    {
        public int ContactId { get; set; }
    }
}
