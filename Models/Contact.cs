using System.ComponentModel.DataAnnotations;

namespace ContactManagementWebApp.Models
{
    public class Contact
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 255 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact number is required.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Contact number must be 9 digits.")]
        public string ContactNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EmailAddress { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
    }
}
