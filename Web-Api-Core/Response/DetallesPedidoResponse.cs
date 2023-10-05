using Web_Api_Core.DTOs;
using Web_Api_Core.Models;
using Web_Api_Core.Responses;

public class DetallesPedidoResponse
{
    public bool Status { get; set; }
    public int Code { get; set; }
    public string Message { get; set; }
    public List<DetallesPedido> Data { get; set; } // Cambia Data a List<DetallesPedidoData>
}
