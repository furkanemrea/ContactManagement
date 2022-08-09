using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary.Models
{
    public class ValidateResponseModel
    {
        public List<Exception> Errors { get; set; } = new();
        public bool IsError { get; set; }
        public void SetError(Exception ex)
        {
            this.Errors.Add(ex);
        }
    }
}
