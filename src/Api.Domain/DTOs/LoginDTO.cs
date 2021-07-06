using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email é um campo obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato inválido.")]
        [StringLength(150, ErrorMessage = "Email deve ter no máximo {1} caractere.")]
        public string Email { get; set; }
    }
}