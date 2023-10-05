//julio

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web_Api_Core.DTOs;
using Web_Api_Core.Models;
using Web_Api_Core.Responses;
using Web_Api_Core.Services;

namespace Web_Api_Core.Controllers
{
    [Route("api/v1/pedidos")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _pedidoService;

        public PedidoController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoResponse>> ObtenerPedidoPorId(int id)
        {
            try
            {
                var response = await _pedidoService.ObtenerPedidoPorId(id);
                if (response.Status)
                    return Ok(response);
                else
                    return NotFound(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<PedidoResponse>> ListarPedidos()
        {
            try
            {
                var response = await _pedidoService.ListarPedidos();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<PedidoResponse>> CrearPedido([FromBody] PedidoDTO pedidoDTO)
        {
            try
            {
                var response = await _pedidoService.CrearPedido(pedidoDTO);
                if (response.Status)
                    return CreatedAtAction(nameof(ObtenerPedidoPorId), new { id = response.Data.Id }, response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PedidoResponse>> ActualizarPedido(int id, [FromBody] PedidoDTO pedidoDTO)
        {
            try
            {
                var response = await _pedidoService.ActualizarPedido(id, pedidoDTO);
                if (response.Status)
                    return Ok(response);
                else
                    return StatusCode(400, response); // Cambiado a 400 en caso de error en la solicitud
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error interno al actualizar el pedido: " + ex.Message,
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PedidoResponse>> EliminarPedido(int id)
        {
            try
            {
                var response = await _pedidoService.EliminarPedido(id);
                if (response.Status)
                    return Ok(response);
                else
                    return NotFound(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }
    }
}
