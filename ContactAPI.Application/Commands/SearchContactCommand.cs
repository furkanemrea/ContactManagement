using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Application.Commands
{
    public class SearchContactCommand: BaseContactCommand
    {
        public string Keyword { get; set; }
    }
}
