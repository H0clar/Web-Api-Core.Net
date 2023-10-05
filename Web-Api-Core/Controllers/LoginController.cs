//touch
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web_Api_Core.DTOs;
using Web_Api_Core.Models;
using Web_Api_Core.Responses;
using Web_Api_Core.Services;

namespace Web_Api_Core.Controllers
{
    [Route("api/v1/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        //endpoint get para iniciar sesión.

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> IniciarSesion([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var usuario = await _loginService.ObtenerUsuarioPorEmailYContraseña(loginDTO.Email, loginDTO.Contraseña);


                if (usuario == null)
                {
                    return Unauthorized(new LoginResponse
                    {
                        Status = false,
                        Code = 401,
                        Message = "Credenciales inválidas"
                    });
                }

                return Ok(new LoginResponse
                {
                    Status = true,
                    Code = 200,
                    Message = "Inicio de sesión exitoso"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LoginResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }
    }
}
