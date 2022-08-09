using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Core.Models.Base
{
    public abstract class BaseResponseModel 
    {
        public string Message { get; set; }
        public EntityResponseStatus Status { get; set; }
        public List<Exception> Exception { get; set; }
    }
}
