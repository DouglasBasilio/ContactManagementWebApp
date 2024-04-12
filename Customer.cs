using System.ComponentModel.DataAnnotations;

namespace Alfasoft
{
    public class Customer
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 255 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Contact number must be 9 digits.")]
        [UniqueContact(ErrorMessage = "Contact number must be unique.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [UniqueEmail(ErrorMessage = "Email address must be unique.")]
        public string EmailAddress { get; set; }

        public bool IsDeleted { get; set; }
    }
}
