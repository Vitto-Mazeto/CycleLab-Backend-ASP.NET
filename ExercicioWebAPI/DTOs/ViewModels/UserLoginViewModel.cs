using System.ComponentModel.DataAnnotations;

namespace ExercicioWebAPI.DTOs.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} [e inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Senha { get; set; }
    }
}
