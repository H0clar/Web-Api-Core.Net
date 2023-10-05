//touch

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using Web_Api_Core.DTOs;
using Web_Api_Core.Responses;
using Web_Api_Core.Services;

namespace Web_Api_Core.Controllers
{
    [Route("api/v1/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponse>> ObtenerUsuarioPorId(int id)
        {
            try
            {
                var response = await _usuarioService.ObtenerUsuarioPorId(id);
                if (response.Status)
                    return Ok(response);
                else
                    return NotFound(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new UsuarioResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<UsuariosResponse>> ListarUsuarios()
        {
            try
            {
                var response = await _usuarioService.ListarUsuarios();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new UsuarioResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioResponse>> CrearUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                var response = await _usuarioService.CrearUsuario(usuarioDTO);
                if (response.Status)
                    return CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id = response.Data.Id }, response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new UsuarioResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioResponse>> ActualizarUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                var response = await _usuarioService.ActualizarUsuario(id, usuarioDTO);
                if (response.Status)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new UsuarioResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error interno al actualizar el usuario: " + ex.Message,
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioResponse>> EliminarUsuario(int id)
        {
            try
            {
                var response = await _usuarioService.EliminarUsuario(id);
                if (response.Status)
                    return Ok(response);
                else
                    return NotFound(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new UsuarioResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }
    }
}
