using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api_Core.DTOs;
using Web_Api_Core.Models;
using Web_Api_Core.Responses;

namespace Web_Api_Core.Services
{
    public class PedidoService
    {
        private readonly ev2Context _context;

        public PedidoService(ev2Context context)
        {
            _context = context;
        }

        public async Task<PedidoResponse> ObtenerPedidoPorId(int id)
        {
            try
            {
                var pedido = await _context.Pedidos.FindAsync(id);

                if (pedido != null)
                {
                    // Aquí puedes mapear el modelo Pedido a un DTO de respuesta si es necesario
                    var pedidoResponse = new PedidoResponse
                    {
                        Status = true,
                        Code = 200, // Código 200 para indicar éxito
                        Message = "Pedido encontrado",
                        Data = pedido
                    };
                    return pedidoResponse;
                }
                else
                {
                    return new PedidoResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "Pedido no encontrado",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new PedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error interno: " + ex.Message,
                    Data = null
                };
            }
        }

        public async Task<PedidosResponse> ListarPedidos()
        {
            try
            {
                var pedidos = _context.Pedidos.ToList();

                // Aquí puedes mapear la lista de modelos Pedido a una lista de DTOs de respuesta si es necesario
                var pedidosResponse = new PedidosResponse
                {
                    Status = true,
                    Code = 200, // Código 200 para indicar éxito
                    Message = "Lista de pedidos obtenida correctamente",
                    Data = pedidos
                };

                return pedidosResponse;
            }
            catch (Exception ex)
            {
                return new PedidosResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error interno: " + ex.Message,
                    Data = null
                };
            }
        }

        public async Task<PedidoResponse> CrearPedido(PedidoDTO pedidoDTO)
        {
            try
            {
                // Aquí puedes realizar la lógica para crear un nuevo pedido en la base de datos

                // Por ejemplo:
                var nuevoPedido = new Pedido
                {
                    UsuarioId = pedidoDTO.UsuarioId,
                    FechaPedido = DateTime.Now
                    // Otras propiedades del pedido
                };

                _context.Pedidos.Add(nuevoPedido);
                await _context.SaveChangesAsync();

                return new PedidoResponse
                {
                    Status = true,
                    Code = 201, // Código 201 para indicar que se creó el recurso
                    Message = "Pedido creado correctamente",
                    Data = nuevoPedido
                };
            }
            catch (Exception ex)
            {
                return new PedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error interno al crear el pedido: " + ex.Message,
                    Data = null
                };
            }
        }

        public async Task<PedidoResponse> ActualizarPedido(int id, PedidoDTO pedidoDTO)
        {
            try
            {
                var pedidoExistente = await _context.Pedidos.FindAsync(id);

                if (pedidoExistente == null)
                {
                    return new PedidoResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "Pedido no encontrado",
                        Data = null
                    };
                }

                // Actualiza las propiedades del pedidoExistente según pedidoDTO
                pedidoExistente.UsuarioId = pedidoDTO.UsuarioId;
                // Actualiza otras propiedades si es necesario

                _context.Pedidos.Update(pedidoExistente);
                await _context.SaveChangesAsync();

                return new PedidoResponse
                {
                    Status = true,
                    Code = 200, // Código 200 para indicar éxito
                    Message = "Pedido actualizado correctamente",
                    Data = pedidoExistente
                };
            }
            catch (Exception ex)
            {
                return new PedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error interno al actualizar el pedido: " + ex.Message,
                    Data = null
                };
            }
        }

        public async Task<PedidoResponse> EliminarPedido(int id)
        {
            try
            {
                var pedido = await _context.Pedidos.FindAsync(id);

                if (pedido == null)
                {
                    return new PedidoResponse
                    {
                        Status = false,
                        Code = 404,
                        Message = "Pedido no encontrado",
                        Data = null
                    };
                }

                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();

                return new PedidoResponse
                {
                    Status = true,
                    Code = 200, // Código 200 para indicar éxito
                    Message = "Pedido eliminado correctamente",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new PedidoResponse
                {
                    Status = false,
                    Code = 500,
                    Message = "Error interno al eliminar el pedido: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
