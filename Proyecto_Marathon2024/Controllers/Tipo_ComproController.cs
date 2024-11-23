using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Repository;

namespace Proyecto_Marathon2024.Controllers
{

    public class Tipo_ComproController : ControllerBase
    {
        private readonly Tipo_ComproRespository _tipo_compro;
        public Tipo_ComproController(Tipo_ComproRespository tipo_comproRespository)
        {
            _tipo_compro = tipo_comproRespository;
        }

        [HttpGet("GetListaTipo_Compro")]
        public async Task<IActionResult> GetListaPerfil_Personal()
        {
            var respuesta = await _tipo_compro.GetListaTipo_Compro();
            return Ok(respuesta);
        }

    }
}
