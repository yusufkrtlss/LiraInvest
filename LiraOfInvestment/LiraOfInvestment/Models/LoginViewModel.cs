using System.ComponentModel.DataAnnotations;

namespace LiraOfInvestment.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Lütfen Kullanıcı Adı girin")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen Şifre Girin")]
        public string Password { get; set; }

    }
}
