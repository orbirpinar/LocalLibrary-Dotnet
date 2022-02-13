using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModel
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }
        
    }
}