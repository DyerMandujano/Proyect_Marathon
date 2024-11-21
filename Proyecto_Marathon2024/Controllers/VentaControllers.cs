using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Repository;
using Proyecto_Marathon2024.Services;

namespace Proyecto_Marathon2024.Controllers
{
        public class VentaController : ControllerBase
        {
            private readonly VentaRepository _VentaRepository;

            public VentaController(VentaRepository ventaRepository)
            {
                _VentaRepository = ventaRepository;
            }

            // Insertar venta
            [HttpPost("InsertVenta")]
            public async Task<IActionResult> InsertVenta([FromBody] Venta venta)
            {
                if (venta == null) return BadRequest("Datos de venta inválidos.");

                var resultado = await _VentaRepository.InsertVenta(
                    venta.Cod_Tipo_Compro,
                    venta.Dni_Cliente,
                    venta.Cod_Tp_Pago,
                    venta.Tipo_Envio,
                    venta.Direccion_Envio
                );

                return Ok(resultado);
            }

            // Obtener lista de ventas
            [HttpGet("GetListaVenta")]
            public async Task<IActionResult> GetListaVenta()
            {
                var lista = await _VentaRepository.GetListaVenta();
                return Ok(lista);
            }

            // Obtener venta por ID
            [HttpGet("GetVentaPorID/{numDocumento}/{codTipoCompro}")]
            public async Task<IActionResult> GetVentaPorID(int numDocumento, int codTipoCompro)
            {
                var venta = await _VentaRepository.GetVentaPorID(numDocumento, codTipoCompro);
                if (venta != null)
                {
                    return Ok(venta);
                }
                return NotFound($"Venta con documento {numDocumento} y comprobante {codTipoCompro} no encontrada.");
            }

            // Actualizar venta
            [HttpPut("UpdateVenta/{numDocumento}/{codTipoCompro}")]
            public async Task<IActionResult> UpdateVenta(int numDocumento, int codTipoCompro, [FromBody] Venta venta)
            {
                if (venta == null) return BadRequest("Datos de venta inválidos.");

                var resultado = await _VentaRepository.UpdateVenta(
                    numDocumento,
                    codTipoCompro,
                    venta.Cod_Tipo_Compro,
                    venta.Dni_Cliente,
                    venta.Cod_Tp_Pago,
                    venta.Tipo_Envio,
                    venta.Direccion_Envio
                );

                return Ok(resultado);
            }
        }
    
}
