using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Repository;

namespace Proyecto_Marathon2024.Controllers
{
    public class Pedido_VentaController : ControllerBase
    {
        private readonly Pedido_VentaRepository _pedido_Venta;

        public Pedido_VentaController(Pedido_VentaRepository pedido_VentaRepository)
        {
            _pedido_Venta = pedido_VentaRepository;
        }


        [HttpGet("GetListaVentaPerso")]
        public async Task<IActionResult> GetListaCliente()
        {
            var respuesta = await _pedido_Venta.GetListaVentaPerso();
            return Ok(respuesta);
        }

        [HttpPost("InsertPedido_Venta")]
        public async Task<IActionResult> InsertarPedido_Venta([FromBody] Pedido_Venta ped_venta)
        {
            var resultado = await _pedido_Venta.InsertPed_Venta(ped_venta);
            return Ok(resultado);
        }

        [HttpPost("InsertDetPedido_Venta")]
        public async Task<IActionResult> InsertarDet_Pedido_Venta([FromBody] DetallePedido_Venta det_ped_venta)
        {
            var resultado = await _pedido_Venta.InsertDetallePed_Venta(det_ped_venta);
            return Ok(resultado);
        }

    }
}
