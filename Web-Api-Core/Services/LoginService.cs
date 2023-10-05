// LoginService.cs

using System;
using System.Threading.Tasks;
using Web_Api_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Web_Api_Core.Services
{
    public class LoginService
    {
        private readonly ev2Context _context;

        public LoginService(ev2Context context)
        {
            _context = context;
        }

        public async Task<Usuario> ObtenerUsuarioPorEmailYContraseña(string email, string contraseña)
        {
            try
            {
                // Buscar un usuario por email y contraseña en la base de datos
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Contraseña == contraseña);

                return usuario;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones en caso de errores
                throw new Exception("Error en el servicio de inicio de sesión: " + ex.Message);
            }
        }
    }
}
