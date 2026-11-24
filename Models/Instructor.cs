using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstructorInformationsSystem.Models
{
    public class Instructor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Instructor ID is required.")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Instructor ID must follow the format XX-XXX (e.g., 24-000).")]
        [Display(Name = "Instructor ID")]
        public string InstructorId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full Name is required.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Age is required.")]
        [Range(20, 60, ErrorMessage = "Age must be between 20 and 60 years old.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Designation is required.")]
        public string Designation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact Number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Majorship is required.")]
        public string Majorship { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bachelor Degree is required.")]
        [Display(Name = "Bachelor Degree")]
        public string BachelorDegree { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "E-mail Address")]
        public string EmailAddress { get; set; } = string.Empty;
    }
}