using System.Collections.Generic;
using Web_Api_Core.Models;

namespace Web_Api_Core.Responses
{
    public class PedidoResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public Pedido Data { get; set; }
    }

    public class PedidosResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public List<Pedido> Data { get; set; }
    }
}
