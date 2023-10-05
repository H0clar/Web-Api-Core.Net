using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Api_Core.DTOs
{
    public class PedidoDTO
    {
        public int UsuarioId { get; set; } // ID del usuario que realiza el pedido

        // FechaPedido se establecerá automáticamente al crear el pedido
        public DateTime FechaPedido { get; set; }

        // DetallesPedidos contiene los detalles de los productos en el pedido
        public List<DetallePedidoDTO> DetallesPedidos { get; set; }
    }

    public class DetallePedidoDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
