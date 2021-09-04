using System.ComponentModel.DataAnnotations;

namespace Frota.Carros.ViewModels.Usuario
{
    public class CadastrarUsuarioViewModel
    {
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Senha { get; set; }
    }
}
