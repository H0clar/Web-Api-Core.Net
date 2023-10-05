// ProductoDTO.cs
using System.ComponentModel.DataAnnotations;

namespace Web_Api_Core.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; } // Agrega la propiedad Id

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(255, ErrorMessage = "El nombre debe tener máximo 255 caracteres.")]
        public string? Nombre { get; set; }

        [MaxLength(1000, ErrorMessage = "La descripción debe tener máximo 1000 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
        public int Precio { get; set; } // Cambia el tipo a int

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un valor no negativo.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El ID de categoría es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de categoría debe ser un valor positivo.")]
        public int CategoriaId { get; set; }
    }
}