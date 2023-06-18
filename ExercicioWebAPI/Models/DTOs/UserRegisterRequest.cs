using System.ComponentModel.DataAnnotations;

namespace ExercicioWebAPI.Models.DTOs
{
    public class UserRegisterRequest
    {
        [Required(ErrorMessage = "O campo {6} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {6} é inválido")]

        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo{6} deve ter entre{2} e {1} caracteres", MinimumLength = 6)]

        public string Senha { get; set; }

        [Compare(nameof(Senha), ErrorMessage = "As senhas devem ser iguais")]

        public string SenhaConfirmacao { get; set; }

    }
}
