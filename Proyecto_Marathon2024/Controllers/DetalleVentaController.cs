using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Repository;

namespace Proyecto_Marathon2024.Controllers
{

        public class DetalleVentaController : ControllerBase
        {
            private readonly DetalleVentaRepository _detalleVentaRepository;

            public DetalleVentaController(DetalleVentaRepository detalleVentaRepository)
            {
                _detalleVentaRepository = detalleVentaRepository;
            }

            // Insertar detalle de venta
            [HttpPost("InsertDetalleVenta")]
            public async Task<IActionResult> InsertDetalleVenta([FromBody] Detalle_Venta detalleVenta, [FromQuery] string dniCliente)
            {
                if (detalleVenta == null || string.IsNullOrEmpty(dniCliente))
                    return BadRequest("Datos de detalle de venta o DNI del cliente inválidos.");

                var resultado = await _detalleVentaRepository.InsertDetalleVenta(detalleVenta, dniCliente);
                return Ok(resultado);
            }

            // Obtener lista de detalles de venta
            [HttpGet("GetListaDetalleVenta")]
            public async Task<IActionResult> GetListaDetalleVenta()
            {
                var lista = await _detalleVentaRepository.GetListaDetalleVenta();
                return Ok(lista);
            }

            // Obtener detalle de venta por ID
            [HttpGet("GetDetalleVentaPorID/{codDetalleVenta}/{numDocumento}/{codTipoCompro}")]
            public async Task<IActionResult> GetDetalleVentaPorID(int codDetalleVenta, int numDocumento, int codTipoCompro)
            {
                var detalleVenta = await _detalleVentaRepository.GetDetalleVentaPorID(codDetalleVenta, numDocumento, codTipoCompro);
                if (detalleVenta != null)
                {
                    return Ok(detalleVenta);
                }
                return NotFound($"Detalle de venta con código {codDetalleVenta}, documento {numDocumento}, y comprobante {codTipoCompro} no encontrado.");
            }

            // Actualizar detalle de venta
            [HttpPut("UpdateDetalleVenta/{codDetalleVenta}")]
            public async Task<IActionResult> UpdateDetalleVenta(int codDetalleVenta, [FromBody] Detalle_Venta detalleVenta, [FromQuery] string dniCliente)
            {
                if (detalleVenta == null || string.IsNullOrEmpty(dniCliente))
                    return BadRequest("Datos de detalle de venta o DNI del cliente inválidos.");

                var resultado = await _detalleVentaRepository.UpdateDetalleVenta(codDetalleVenta, detalleVenta, dniCliente);
                return Ok(resultado);
            }
        }
    
}
