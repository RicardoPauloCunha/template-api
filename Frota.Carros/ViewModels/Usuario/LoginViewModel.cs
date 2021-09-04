using System.ComponentModel.DataAnnotations;

namespace Frota.Carros.ViewModels.Usuario
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Senha { get; set; }
    }
}
