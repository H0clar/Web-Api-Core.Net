//touch


using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Web_Api_Core.DTOs;
using Web_Api_Core.Models;
using Web_Api_Core.Responses;

namespace Web_Api_Core.Services
{
    public class UsuarioService
    {
        private readonly ev2Context _context;

        public UsuarioService(ev2Context context)
        {
            _context = context;
        }

        public async Task<UsuarioResponse> CrearUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                // Verificar si ya existe un usuario con el mismo email
                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == usuarioDTO.Email);

                if (usuarioExistente != null)
                {
                    return new UsuarioResponse
                    {
                        Status = false,
                        Code = 400,
                        Message = "El usuario ya existe."
                    };
                }

                Usuario usuario = new()
                {
                    Nombre = usuarioDTO.Nombre,
                    Email = usuarioDTO.Email,
                    Contraseña = usuarioDTO.Contraseña
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return new UsuarioResponse
                {
                    Data = usuario,
                    Status = true,
                    Code = 200,
                    Message = "Usuario creado exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new UsuarioResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }

        public async Task<UsuariosResponse> ListarUsuarios()
        {
            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();

                return new UsuariosResponse
                {
                    Data = usuarios,
                    Status = true,
                    Code = 200,
                    Message = "Usuarios obtenidos exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new UsuariosResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }

        public async Task<UsuarioResponse> ObtenerUsuarioPorId(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    return new UsuarioResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "El usuario no existe."
                    };
                }

                return new UsuarioResponse
                {
                    Data = usuario,
                    Status = true,
                    Code = 200,
                    Message = "Usuario obtenido exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new UsuarioResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }

        public async Task<UsuarioResponse> ActualizarUsuario(int id, UsuarioDTO usuarioDTO)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    return new UsuarioResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "El usuario no existe."
                    };
                }

                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == usuarioDTO.Email && u.Id != id);

                if (usuarioExistente != null)
                {
                    return new UsuarioResponse
                    {
                        Status = false,
                        Code = 400,
                        Message = "Ya existe un usuario con ese email."
                    };
                }

                usuario.Nombre = usuarioDTO.Nombre;
                usuario.Email = usuarioDTO.Email;
                usuario.Contraseña = usuarioDTO.Contraseña;

                _context.Entry(usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new UsuarioResponse
                {
                    Data = usuario,
                    Status = true,
                    Code = 200,
                    Message = "Usuario actualizado exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new UsuarioResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }

        public async Task<UsuarioResponse> EliminarUsuario(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    return new UsuarioResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "El usuario no existe."
                    };
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                return new UsuarioResponse
                {
                    Data = usuario,
                    Status = true,
                    Code = 200,
                    Message = "Usuario eliminado exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new UsuarioResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }
    }
}
