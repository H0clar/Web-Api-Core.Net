//esteban

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web_Api_Core.DTOs;
using Web_Api_Core.Responses;
using Web_Api_Core.Services;

namespace Web_Api_Core.Controllers
{
    [Route("api/v1/productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _productoService;

        public ProductoController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoResponse>> ObtenerProductoPorId(int id)
        {
            try
            {
                var response = await _productoService.ObtenerProductoPorId(id);
                if (response.Status)
                    return Ok(response);
                else
                    return NotFound(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProductoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<ProductosResponse>> ListarProductos()
        {
            try
            {
                var response = await _productoService.ListarProductos();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProductoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductoResponse>> CrearProducto([FromBody] ProductoDTO productoDTO)
        {
            try
            {
                var response = await _productoService.CrearProducto(productoDTO);
                if (response.Status)
                    return CreatedAtAction(nameof(ObtenerProductoPorId), new { id = response.Data.Id }, response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProductoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductoResponse>> ActualizarProducto(int id, [FromBody] ProductoDTO productoDTO)
        {
            try
            {
                var response = await _productoService.ActualizarProducto(id, productoDTO);
                if (response.Status)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProductoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error interno al actualizar el producto: " + ex.Message,
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductoResponse>> EliminarProducto(int id)
        {
            try
            {
                var response = await _productoService.EliminarProducto(id);
                if (response.Status)
                    return Ok(response);
                else
                    return NotFound(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProductoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }
    }
}
