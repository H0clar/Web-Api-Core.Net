using System.ComponentModel.DataAnnotations;

namespace Web_Api_Core.DTOs
{
    public class DetallesPedidoDTO
    {
        [Required(ErrorMessage = "El pedido_id es obligatorio.")]
        public int PedidoId { get; set; }

        [Required(ErrorMessage = "El producto_id es obligatorio.")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un valor positivo.")]
        public int Cantidad { get; set; }
    }
}
