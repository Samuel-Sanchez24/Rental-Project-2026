using System.ComponentModel.DataAnnotations;

namespace Rental_Project_2026.Web.DTOs.Account
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El correo electronico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electronico no es valido.")]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password, ErrorMessage = "La contraseña no es valida.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }

    }
}
