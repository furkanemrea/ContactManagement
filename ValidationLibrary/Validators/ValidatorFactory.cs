using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary.Validators
{
    public static class ValidatorFactory<T>
    {
        static Dictionary<string, IValidator<T>> validatorList = new();
        public static IValidator<T> GetValidator(Type attribute)
        {
            // will only execute once  becase the dictionary is static :)
            // I didnt use active.CreateInstance becase validator is generic type.
            if(validatorList.Count == 0)
            {
                validatorList.Add("StringData",new StringValidator<T>());
                validatorList.Add("EmailData",new EmailValidator<T>());
                validatorList.Add("DateData",new StringValidator<T>());
                validatorList.Add("DefaultData",new DefaultValidator<T>());  
            }
            return validatorList.ContainsKey(attribute.Name) ? validatorList[attribute.Name] : validatorList["Default"];
        }
    }
}
