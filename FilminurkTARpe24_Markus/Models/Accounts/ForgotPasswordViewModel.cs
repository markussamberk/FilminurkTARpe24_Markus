using System.ComponentModel.DataAnnotations;

namespace FilminurkTARpe24_Markus.Models.Accounts
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }   
    }
}
