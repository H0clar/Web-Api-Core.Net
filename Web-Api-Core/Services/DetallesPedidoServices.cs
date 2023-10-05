using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api_Core.DTOs;
using Web_Api_Core.Models;

namespace Web_Api_Core.Services
{
    public class DetallesPedidoServices
    {
        private readonly ev2Context _context;

        public DetallesPedidoServices(ev2Context context)
        {
            _context = context;
        }

        public async Task<DetallesPedido> ObtenerDetallesPedidoPorId(int id)
        {
            return await _context.DetallesPedidos.FindAsync(id);
        }

        public async Task<List<DetallesPedido>> ListarDetallesPedidos()
        {
            return await _context.DetallesPedidos.ToListAsync();
        }

        public async Task<DetallesPedido> CrearDetallesPedido(DetallesPedidoDTO detallesPedidoDTO)
        {
            var detallesPedido = new DetallesPedido
            {
                PedidoId = detallesPedidoDTO.PedidoId,
                ProductoId = detallesPedidoDTO.ProductoId,
                Cantidad = detallesPedidoDTO.Cantidad
            };

            _context.DetallesPedidos.Add(detallesPedido);
            await _context.SaveChangesAsync();

            return detallesPedido;
        }

        public async Task<DetallesPedido> ActualizarDetallesPedido(int id, DetallesPedidoDTO detallesPedidoDTO)
        {
            var detallesPedidoExistente = await _context.DetallesPedidos.FindAsync(id);

            if (detallesPedidoExistente == null)
            {
                return null;
            }

            detallesPedidoExistente.PedidoId = detallesPedidoDTO.PedidoId;
            detallesPedidoExistente.ProductoId = detallesPedidoDTO.ProductoId;
            detallesPedidoExistente.Cantidad = detallesPedidoDTO.Cantidad;

            _context.DetallesPedidos.Update(detallesPedidoExistente);
            await _context.SaveChangesAsync();

            return detallesPedidoExistente;
        }

        public async Task<bool> EliminarDetallesPedido(int id)
        {
            var detallesPedido = await _context.DetallesPedidos.FindAsync(id);

            if (detallesPedido == null)
            {
                return false;
            }

            _context.DetallesPedidos.Remove(detallesPedido);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
