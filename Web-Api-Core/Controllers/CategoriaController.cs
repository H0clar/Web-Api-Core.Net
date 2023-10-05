//julio
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web_Api_Core.DTOs;
using Web_Api_Core.Responses;
using Web_Api_Core.Services;

namespace Web_Api_Core.Controllers
{
    [Route("api/v1/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // endpoint para obtener una categoria por ID.

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaResponse>> ObtenerCategoriaPorId(int id)
        {
            try
            {
                var response = await _categoriaService.ObtenerCategoriaPorId(id);
                if (response.Status)
                    return Ok(response);
                else
                    return NotFound(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CategoriaResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }
        // endpoint para listar todas las categorias.
        [HttpGet]
        public async Task<ActionResult<CategoriasResponse>> ListarCategorias()
        {
            try
            {
                var response = await _categoriaService.ListarCategorias();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CategoriaResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }
        //endpoint para crear una categoria.
        [HttpPost]
        public async Task<ActionResult<CategoriaResponse>> CrearCategoria([FromBody] CategoriaDTO categoriaDTO)
        {
            try
            {
                var response = await _categoriaService.CrearCategoria(categoriaDTO);
                if (response.Status)
                    return CreatedAtAction(nameof(ObtenerCategoriaPorId), new { id = response.Data.Id }, response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CategoriaResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }

        //endpoint para actualizar una categoria.
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaResponse>> ActualizarCategoria(int id, [FromBody] CategoriaDTO categoriaDTO)
        {
            try
            {
                var response = await _categoriaService.ActualizarCategoria(id, categoriaDTO);
                if (response.Status)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CategoriaResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error interno al actualizar la categoría: " + ex.Message,
                    Data = null
                });
            }
        }

        //endpoint para eliminar una categoria.
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaResponse>> EliminarCategoria(int id)
        {
            try
            {
                var response = await _categoriaService.EliminarCategoria(id);
                if (response.Status)
                    return Ok(response);
                else if (response.Code == 404)
                    return NotFound(response);
                else if (response.Code == 400)
                {
                    return BadRequest(new CategoriaResponse
                    {
                        Status = false,
                        Code = 400,
                        Message = "No se va a poder mi loco",
                        Data = null
                    });
                }
                else
                    return StatusCode(500, new CategoriaResponse
                    {
                        Status = false,
                        Code = 500,
                        Message = "Error: no se va a poder mi loco "
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CategoriaResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                });
            }
        }


    }
}
