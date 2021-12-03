using System.ComponentModel.DataAnnotations;

namespace Frota.Carros.Api.ViewModels.Usuario
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Senha { get; set; }

        public LoginViewModel(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}
