using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Repository;

namespace Proyecto_Marathon2024.Controllers
{
    public class PedidoController : ControllerBase
    {
        private readonly PedidoRepository _pedidoRepository;

        public PedidoController(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        // Insertar pedido
        [HttpPost("InsertPedido")]
        public async Task<IActionResult> InsertPedido([FromQuery] int codTipoCompro, [FromQuery] int codTpPago, [FromQuery] string dniCliente)
        {
            if (string.IsNullOrEmpty(dniCliente))
                return BadRequest("DNI del cliente es requerido.");

            var resultado = await _pedidoRepository.InsertPedido(codTipoCompro, codTpPago, dniCliente);
            return Ok(resultado);
        }

        // Obtener lista de pedidos
        [HttpGet("GetListaPedidos")]
        public async Task<IActionResult> GetListaPedidos()
        {
            var listaPedidos = await _pedidoRepository.GetListaPedidos();
            return Ok(listaPedidos);
        }

        // Obtener pedido por ID
        [HttpGet("GetPedidoPorID/{codPedido}")]
        public async Task<IActionResult> GetPedidoPorID(int codPedido)
        {
            var pedido = await _pedidoRepository.GetPedidoPorID(codPedido);
            if (pedido != null)
            {
                return Ok(pedido);
            }
            return NotFound($"Pedido con código {codPedido} no encontrado.");
        }

        // Actualizar pedido
        [HttpPut("UpdatePedido/{codPedido}")]
        public async Task<IActionResult> UpdatePedido(int codPedido, [FromQuery] int codTipoCompro, [FromQuery] int codTpPago, [FromQuery] string dniCliente, [FromQuery] DateTime fechaPedido)
        {
            if (string.IsNullOrEmpty(dniCliente))
                return BadRequest("DNI del cliente es requerido.");

            var resultado = await _pedidoRepository.UpdatePedido(codPedido, codTipoCompro, codTpPago, dniCliente, fechaPedido);
            return Ok(resultado);
        }
    }
}
