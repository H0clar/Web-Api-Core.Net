using Web_Api_Core.Models;

namespace Web_Api_Core.Responses
{
    public class UsuarioResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public Usuario Data { get; set; }
    }

    public class UsuariosResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public List<Usuario> Data { get; set; }
    }
}
