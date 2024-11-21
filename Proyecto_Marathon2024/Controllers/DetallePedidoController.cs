using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Repository;

namespace Proyecto_Marathon2024.Controllers
{
    public class DetallePedidoController : ControllerBase
    {
        private readonly DetallePedidoRepository _detallePedidoRepository;

        public DetallePedidoController(DetallePedidoRepository detallePedidoRepository)
        {
            _detallePedidoRepository = detallePedidoRepository;
        }

        // Insertar detalle de pedido
        [HttpPost("InsertDetallePedido")]
        public async Task<IActionResult> InsertDetallePedido([FromQuery] int codPedido, [FromQuery] int codProd, [FromQuery] int cantidad, [FromQuery] decimal precioVenta)
        {
            if (cantidad <= 0 || precioVenta <= 0)
                return BadRequest("Cantidad y precio de venta deben ser mayores a cero.");

            var resultado = await _detallePedidoRepository.InsertDetallePedido(codPedido, codProd, cantidad, precioVenta);
            return Ok(resultado);
        }

        // Obtener lista de detalles de pedidos
        [HttpGet("GetListaDetallesPedido")]
        public async Task<IActionResult> GetListaDetallesPedido()
        {
            var listaDetalles = await _detallePedidoRepository.GetListaDetallesPedido();
            return Ok(listaDetalles);
        }

        // Obtener detalle de pedido por ID
        [HttpGet("GetDetallePedidoPorID/{codDetPed}")]
        public async Task<IActionResult> GetDetallePedidoPorID(int codDetPed)
        {
            var detallePedido = await _detallePedidoRepository.GetDetallePedidoPorID(codDetPed);
            if (detallePedido != null)
            {
                return Ok(detallePedido);
            }
            return NotFound($"Detalle de pedido con código {codDetPed} no encontrado.");
        }

        // Actualizar detalle de pedido
        [HttpPut("UpdateDetallePedido/{codDetPed}")]
        public async Task<IActionResult> UpdateDetallePedido(int codDetPed, [FromQuery] int codPedido, [FromQuery] int codProd, [FromQuery] int cantidad, [FromQuery] decimal precioVenta)
        {
            if (cantidad <= 0 || precioVenta <= 0)
                return BadRequest("Cantidad y precio de venta deben ser mayores a cero.");

            var resultado = await _detallePedidoRepository.UpdateDetallePedido(codDetPed, codPedido, codProd, cantidad, precioVenta);
            return Ok(resultado);
        }

    }
}
