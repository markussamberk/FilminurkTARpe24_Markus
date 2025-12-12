using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FilminurkTARpe24_Markus.Models.Accounts
{
    public class AddPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Sisesta oma uus parool:")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Kirjuta oma uus parool uuesti:")]
        [Compare("NewPassword", ErrorMessage="Paroolid ei kattu, palun proovi uuesti.")]
        public string ConfirmNewPassword { get; set; }
    }
}
