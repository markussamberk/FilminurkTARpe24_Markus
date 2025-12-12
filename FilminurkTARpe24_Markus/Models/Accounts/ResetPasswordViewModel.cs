using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FilminurkTARpe24_Markus.Models.Accounts
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Kirjuta oma uus parool uuesti:")]
        [Compare("Password", ErrorMessage = "Paroolid ei kattu, palun proovi uuesti.")]
        public string ConfirmNewPassword { get; set; }
        public string Token { get; set; }
    }
}
