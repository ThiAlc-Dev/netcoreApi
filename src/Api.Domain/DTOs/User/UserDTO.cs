using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs.User
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Campo nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome de ter no máxio {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é um campo obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato inválido.")]
        [StringLength(150, ErrorMessage = "Email deve ter no máximo {1} caractere.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é um campo obrigatório.")]
        [StringLength(10, ErrorMessage = "Deve ter entre 5 e 10 caracteres", MinimumLength = 5)]
        public string Password { get; set; }
    }
}