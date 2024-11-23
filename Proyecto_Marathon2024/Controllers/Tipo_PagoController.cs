using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Repository;

namespace Proyecto_Marathon2024.Controllers
{

    public class Tipo_PagoController : ControllerBase
    {
        private readonly Tipo_PagoRespository _tipo_Pago;
        public Tipo_PagoController(Tipo_PagoRespository tipo_PagoRespository)
        {
            _tipo_Pago = tipo_PagoRespository;
        }

        [HttpGet("GetListaTipo_Pago")]
        public async Task<IActionResult> GetListaPerfil_Personal()
        {
            var respuesta = await _tipo_Pago.GetListaTipo_Pago();
            return Ok(respuesta);
        }
    }
}
