using System.ComponentModel.DataAnnotations;

namespace Alfasoft
{
    public class UniqueContactAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            if (dbContext != null)
            {
                var customer = (Customer)validationContext.ObjectInstance;

                // Verifica se já existe um contato com o mesmo número
                if (dbContext.Customers.Any(c => c.ID != customer.ID && c.ContactNumber == (string)value))
                {
                    return new ValidationResult("Contact number must be unique.");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            if (dbContext != null)
            {
                var customer = (Customer)validationContext.ObjectInstance;

                // Verifica se já existe um contato com o mesmo endereço de e-mail
                if (dbContext.Customers.Any(c => c.ID != customer.ID && c.EmailAddress == (string)value))
                {
                    return new ValidationResult("Email address must be unique.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
