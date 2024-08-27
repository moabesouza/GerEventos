using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GerEventos.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DecimalAttribute : ValidationAttribute, IClientModelValidator
    {
        public int MaxLength { get; set; }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-decimal", "true");

            if (MaxLength > 0)
            {
                MergeAttribute(context.Attributes, "maxlength", MaxLength.ToString());
            }
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                // Converte o valor para string
                var stringValue = value.ToString();

                // Substitui a vírgula pelo ponto
                stringValue = stringValue.Replace(',', '.');

                // Tenta converter a string para decimal
                if (decimal.TryParse(stringValue, out decimal decimalValue))
                {
                  
                    return ValidationResult.Success;
                }
                else
                {
              
                    return new ValidationResult(ErrorMessage ?? "O valor deve ser um número decimal válido.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
