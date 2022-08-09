using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary.Validators
{
    public class StringValidator<T> : IValidator<T>
    {
        public List<Exception> Validate(T value, int maxLength, string source, PropertyInfo propertyInfo, object obj)
        {
            List<Exception> errors = new();

            if(!typeof(T).IsValueType && typeof(T) != typeof(String))
            {
                throw new ArgumentException("T must be a value type and System.String");
            }

            if (!string.IsNullOrEmpty(value.ToString()))
            {
                if(source == "MaxLength")
                {
                    if (value.ToString().Length > maxLength)
                    {
                        errors.Add(new Exception($"{propertyInfo.Name} is not correct for => {value}"));
                    }
                }
         
            }
            return errors;
        }
    }
}
