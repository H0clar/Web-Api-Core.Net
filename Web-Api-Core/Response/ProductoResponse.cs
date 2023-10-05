using System.Collections.Generic;
using Web_Api_Core.DTOs;

namespace Web_Api_Core.Responses
{
    public class ProductoResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public ProductoDTO Data { get; set; }
    }

    public class ProductosResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public List<ProductoDTO> Data { get; set; }
    }
}
