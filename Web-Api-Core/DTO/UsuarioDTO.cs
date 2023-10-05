using System.ComponentModel.DataAnnotations;

namespace Web_Api_Core.DTOs
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(255, ErrorMessage = "El nombre debe tener máximo 255 caracteres.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        [MaxLength(255, ErrorMessage = "El email debe tener máximo 255 caracteres.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string? Contraseña { get; set; }
    }
}