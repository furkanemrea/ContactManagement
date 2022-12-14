using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary.Validators
{
    public interface IValidator<T>
    {
        public List<Exception> Validate(T value, int type, string source, PropertyInfo propertyInfo, object obj);
    }
}
