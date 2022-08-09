using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ValidationLibrary.Models;

namespace ValidationLibrary.Validators
{
    public static class ValidateClassProperties
    {
        public static ValidateResponseModel GetValidateResult(object model)
        {
            ValidateResponseModel validateResponseModel = new ValidateResponseModel();
            var errorList = new List<Exception>();
            bool isError = false;
            PropertyInfo[] properties = model.GetType().GetProperties(); // select all properties
            foreach (PropertyInfo property in properties)
            {
                //      * execute for all custom attributes
                for (int i = 0; i < property.GetCustomAttributes(true).Length; i++)
                {
                    Type type = property.GetCustomAttributes(true)[i].GetType();
                    var validator = ValidatorFactory<string>.GetValidator(type);
                    string propertyValue = property.GetValue(model).ToString();
                    List<int> attributeValues = new();
                    foreach (var validationProperty in type.GetProperties())
                    {
                        if (validationProperty.Name != "TypeId")
                        {
                            string value = property.GetCustomAttributesData()[i].ConstructorArguments[i].Value.ToString();
                            List<Exception> exceptions = validator.Validate(propertyValue, Convert.ToInt32(value), validationProperty.Name, property, model);
                            foreach (var exception in exceptions)
                            {
                                validateResponseModel.IsError = true;
                                validateResponseModel.SetError(exception);
                            }
                        }
                    }
                    if (validateResponseModel.IsError) break;
                }
            }
            return validateResponseModel;
        }
    }
}
