using System.ComponentModel.DataAnnotations;

namespace EShop.Identity.Dtos;

public class RegisterUserDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatorio")]
    [EmailAddress(ErrorMessage = "O campo {0 está em formato invalido}")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatorio")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "As senhas não conferem")]
    public string ConfirmPassword { get; set; } = string.Empty;
}