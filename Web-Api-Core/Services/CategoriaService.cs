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
    public class CategoriaService
    {
        private readonly ev2Context _context;

        public CategoriaService(ev2Context context)
        {
            _context = context;
        }

        public async Task<CategoriaResponse> CrearCategoria(CategoriaDTO categoriaDTO)
        {
            try
            {
                var categoriaExistente = await _context.Categorias
                    .FirstOrDefaultAsync(c => c.Nombre == categoriaDTO.Nombre);

                if (categoriaExistente != null)
                {
                    return new CategoriaResponse
                    {
                        Status = false,
                        Code = 400,
                        Message = "La categoría ya existe."
                    };
                }

                Categoria categoria = new()
                {
                    Nombre = categoriaDTO.Nombre
                };

                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();

                return new CategoriaResponse
                {
                    Data = categoria,
                    Status = true,
                    Code = 200,
                    Message = "Categoría creada exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new CategoriaResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }

        public async Task<CategoriasResponse> ListarCategorias()
        {
            try
            {
                var categorias = await _context.Categorias.ToListAsync();

                return new CategoriasResponse
                {
                    Data = categorias,
                    Status = true,
                    Code = 200,
                    Message = "Categorías obtenidas exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new CategoriasResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }

        public async Task<CategoriaResponse> ObtenerCategoriaPorId(int id)
        {
            try
            {
                var categoria = await _context.Categorias.FindAsync(id);

                if (categoria == null)
                {
                    return new CategoriaResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "La categoría no existe."
                    };
                }

                return new CategoriaResponse
                {
                    Data = categoria,
                    Status = true,
                    Code = 200,
                    Message = "Categoría obtenida exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new CategoriaResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }

        public async Task<CategoriaResponse> ActualizarCategoria(int id, CategoriaDTO categoriaDTO)
        {
            try
            {
                var categoria = await _context.Categorias.FindAsync(id);

                if (categoria == null)
                {
                    return new CategoriaResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "La categoría no existe."
                    };
                }

                var categoriaExistente = await _context.Categorias
                    .FirstOrDefaultAsync(c => c.Nombre == categoriaDTO.Nombre && c.Id != id);

                if (categoriaExistente != null)
                {
                    return new CategoriaResponse
                    {
                        Status = false,
                        Code = 400,
                        Message = "Ya existe una categoría con ese nombre."
                    };
                }

                categoria.Nombre = categoriaDTO.Nombre;

                _context.Entry(categoria).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new CategoriaResponse
                {
                    Data = categoria,
                    Status = true,
                    Code = 200,
                    Message = "Categoría actualizada exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new CategoriaResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }

        public async Task<CategoriaResponse> EliminarCategoria(int id)
        {
            try
            {
                var categoria = await _context.Categorias.FindAsync(id);

                if (categoria == null)
                {
                    return new CategoriaResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "La categoría no existe."
                    };
                }

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();

                return new CategoriaResponse
                {
                    Data = categoria,
                    Status = true,
                    Code = 200,
                    Message = "Categoría eliminada exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new CategoriaResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }
    }
}
