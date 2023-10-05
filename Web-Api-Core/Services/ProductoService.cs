using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web_Api_Core.DTOs;
using Web_Api_Core.Models;
using Web_Api_Core.Responses;

namespace Web_Api_Core.Services
{
    public class ProductoService
    {
        private readonly ev2Context _context;

        public ProductoService(ev2Context context)
        {
            _context = context;
        }

        public async Task<ProductoResponse> ObtenerProductoPorId(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);

                if (producto == null)
                {
                    return new ProductoResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "El producto no existe."
                    };
                }

                var productoDTO = new ProductoDTO
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    CategoriaId = producto.CategoriaId
                };

                return new ProductoResponse
                {
                    Data = productoDTO,
                    Status = true,
                    Code = 200,
                    Message = "Producto obtenido exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new ProductoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ProductosResponse> ListarProductos()
        {
            try
            {
                var productos = await _context.Productos.ToListAsync();
                var productosDTO = new List<ProductoDTO>();

                foreach (var producto in productos)
                {
                    productosDTO.Add(new ProductoDTO
                    {
                        Id = producto.Id,
                        Nombre = producto.Nombre,
                        Descripcion = producto.Descripcion,
                        Precio = producto.Precio,
                        Stock = producto.Stock,
                        CategoriaId = producto.CategoriaId
                    });
                }

                return new ProductosResponse
                {
                    Data = productosDTO,
                    Status = true,
                    Code = 200,
                    Message = "Productos obtenidos exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new ProductosResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ProductoResponse> CrearProducto(ProductoDTO productoDTO)
        {
            try
            {
                var productoExistente = await _context.Productos
                    .FirstOrDefaultAsync(p => p.Nombre == productoDTO.Nombre);

                if (productoExistente != null)
                {
                    return new ProductoResponse
                    {
                        Status = false,
                        Code = 400,
                        Message = "Ya existe un producto con ese nombre."
                    };
                }

                var producto = new Producto
                {
                    Nombre = productoDTO.Nombre,
                    Descripcion = productoDTO.Descripcion,
                    Precio = productoDTO.Precio,
                    Stock = productoDTO.Stock,
                    CategoriaId = productoDTO.CategoriaId
                };

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                return new ProductoResponse
                {
                    Data = productoDTO,
                    Status = true,
                    Code = 201,
                    Message = "Producto creado exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new ProductoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ProductoResponse> ActualizarProducto(int id, ProductoDTO productoDTO)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);

                if (producto == null)
                {
                    return new ProductoResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "El producto no existe."
                    };
                }

                var productoExistente = await _context.Productos
                    .FirstOrDefaultAsync(p => p.Nombre == productoDTO.Nombre && p.Id != id);

                if (productoExistente != null)
                {
                    return new ProductoResponse
                    {
                        Status = false,
                        Code = 400,
                        Message = "Ya existe un producto con ese nombre."
                    };
                }

                producto.Nombre = productoDTO.Nombre;
                producto.Descripcion = productoDTO.Descripcion;
                producto.Precio = productoDTO.Precio;
                producto.Stock = productoDTO.Stock;
                producto.CategoriaId = productoDTO.CategoriaId;

                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new ProductoResponse
                {
                    Data = productoDTO,
                    Status = true,
                    Code = 200,
                    Message = "Producto actualizado exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new ProductoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ProductoResponse> EliminarProducto(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);

                if (producto == null)
                {
                    return new ProductoResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "El producto no existe."
                    };
                }

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();

                var productoDTO = new ProductoDTO
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    CategoriaId = producto.CategoriaId
                };

                return new ProductoResponse
                {
                    Data = productoDTO,
                    Status = true,
                    Code = 200,
                    Message = "Producto eliminado exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new ProductoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }
    }
}
