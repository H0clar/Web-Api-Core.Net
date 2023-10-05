using Web_Api_Core.Models;

namespace Web_Api_Core.Responses
{
    public class CategoriaResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public Categoria Data { get; set; }
    }

    public class CategoriasResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public List<Categoria> Data { get; set; }
    }
}
