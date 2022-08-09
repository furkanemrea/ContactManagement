using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ValidationLibrary.Validators
{
    internal class EmailValidator<T> : IValidator<T>
    {
        public List<Exception> Validate(T value, int type, string source, PropertyInfo propertyInfo, object obj)
        {
            List<Exception> errors = new();

            if (!typeof(T).IsValueType && typeof(T) != typeof(String))
            {
                throw new ArgumentException("T must be a value type and System.String");
            }

            if (!string.IsNullOrEmpty(value.ToString()))
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(value.ToString());
                if (!match.Success) errors.Add(new Exception($"{value} is not Email"));
            }
            return errors;
        }
    }
}
