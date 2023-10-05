//esteban
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
    [Route("api/v1/detalles-pedido")]
    [ApiController]
    public class DetallesPedidoController : ControllerBase
    {
        private readonly DetallesPedidoServices _detallesPedidoService;

        public DetallesPedidoController(DetallesPedidoServices detallesPedidoService)
        {
            _detallesPedidoService = detallesPedidoService;
        }

        // endpoint get {id} para obtener detalles de un pedido por su ID.

        [HttpGet("{id}")]
        public async Task<ActionResult<DetallesPedidoResponse>> ObtenerDetallesPedidoPorId(int id)
        {
            try
            {
                var detallesPedido = await _detallesPedidoService.ObtenerDetallesPedidoPorId(id);
                if (detallesPedido != null)
                {
                    var response = new DetallesPedidoResponse
                    {
                        Status = true,
                        Code = 200,
                        Message = "Detalles de pedido encontrados",
                        Data = new List<DetallesPedido>
                        {
                            new DetallesPedido
                            {
                                Id = detallesPedido.Id,
                                PedidoId = detallesPedido.PedidoId,
                                ProductoId = detallesPedido.ProductoId,
                                Cantidad = detallesPedido.Cantidad
                            }
                        }
                    };
                    return Ok(response);
                }
                else
                {
                    return NotFound(new DetallesPedidoResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "Detalles de pedido no encontrados",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DetallesPedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message,
                    Data = null
                });
            }
        }
        //endpoint get para listar todos los detalles de pedidos.

        [HttpGet]
        public async Task<ActionResult<DetallesPedidoResponse>> ListarDetallesPedidos()
        {
            try
            {
                var detallesPedidos = await _detallesPedidoService.ListarDetallesPedidos();
                var response = new DetallesPedidoResponse
                {
                    Status = true,
                    Code = 200,
                    Message = "Lista de detalles de pedido obtenida correctamente",
                    Data = detallesPedidos.Select(dp => new DetallesPedido
                    {
                        Id = dp.Id,
                        PedidoId = dp.PedidoId,
                        ProductoId = dp.ProductoId,
                        Cantidad = dp.Cantidad
                    }).ToList()
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DetallesPedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message,
                    Data = null
                });
            }
        }

        //endpoint post para crear un detalle de pedido.

        [HttpPost]
        public async Task<ActionResult<DetallesPedidoResponse>> CrearDetallesPedido([FromBody] DetallesPedidoDTO detallesPedidoDTO)
        {
            try
            {
                var detallesPedido = await _detallesPedidoService.CrearDetallesPedido(detallesPedidoDTO);
                if (detallesPedido != null)
                {
                    var response = new DetallesPedidoResponse
                    {
                        Status = true,
                        Code = 201,
                        Message = "Detalles de pedido creado correctamente",
                        Data = new List<DetallesPedido>
                {
                    new DetallesPedido
                    {
                        Id = detallesPedido.Id,
                        PedidoId = detallesPedido.PedidoId,
                        ProductoId = detallesPedido.ProductoId,
                        Cantidad = detallesPedido.Cantidad
                    }
                }
                    };
                    return CreatedAtAction(nameof(ObtenerDetallesPedidoPorId), new { id = detallesPedido.Id }, response);
                }
                else
                {
                    return BadRequest(new DetallesPedidoResponse
                    {
                        Status = false,
                        Code = 400,
                        Message = "No se pudo crear el detalle de pedido",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DetallesPedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message,
                    Data = null
                });
            }
        }

        //endpoint put para actualizar un detalle de pedido.


        [HttpPut("{id}")]
        public async Task<ActionResult<DetallesPedidoResponse>> ActualizarDetallesPedido(int id, [FromBody] DetallesPedidoDTO detallesPedidoDTO)
        {
            try
            {
                var detallesPedidoActualizado = await _detallesPedidoService.ActualizarDetallesPedido(id, detallesPedidoDTO);
                if (detallesPedidoActualizado != null)
                {
                    var response = new DetallesPedidoResponse
                    {
                        Status = true,
                        Code = 200,
                        Message = "Detalles de pedido actualizados correctamente",
                        Data = new List<DetallesPedido>
                        {
                            new DetallesPedido
                            {
                                Id = detallesPedidoActualizado.Id,
                                PedidoId = detallesPedidoActualizado.PedidoId,
                                ProductoId = detallesPedidoActualizado.ProductoId,
                                Cantidad = detallesPedidoActualizado.Cantidad
                            }
                        }
                    };
                    return Ok(response);
                }
                else
                {
                    return NotFound(new DetallesPedidoResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "Detalles de pedido no encontrados",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DetallesPedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error interno al actualizar los detalles del pedido: " + ex.Message,
                    Data = null
                });
            }
        }


        //endpoint delete para eliminar un detalle de pedido con {id}.

        [HttpDelete("{id}")]
        public async Task<ActionResult<DetallesPedidoResponse>> EliminarDetallesPedido(int id)
        {
            try
            {
                var eliminado = await _detallesPedidoService.EliminarDetallesPedido(id);
                if (eliminado)
                {
                    return Ok(new DetallesPedidoResponse
                    {
                        Status = true,
                        Code = 200,
                        Message = "Detalles de pedido eliminados correctamente",
                        Data = null
                    });
                }
                else
                {
                    return NotFound(new DetallesPedidoResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "Detalles de pedido no encontrados",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DetallesPedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message,
                    Data = null
                });
            }
        }
    }
}
