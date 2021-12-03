using System.ComponentModel.DataAnnotations;

namespace Frota.Carros.Api.ViewModels.Usuario
{
    public class CadastrarUsuarioViewModel
    {
        [Required]
        public string Nome { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Senha { get; set; }
    }
}
