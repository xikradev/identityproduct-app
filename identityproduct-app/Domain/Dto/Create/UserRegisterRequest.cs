using System.ComponentModel.DataAnnotations;

namespace identityproduct_app.Domain.Dto.Create
{
    public class UserRegisterRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage ="O campo {0} é inválido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength =6)]
        public string Password { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Compare(nameof(Password), ErrorMessage = "As senhas devem ser iguais.")]
        public string ConfirmPassword { get; set; }
    }
}
