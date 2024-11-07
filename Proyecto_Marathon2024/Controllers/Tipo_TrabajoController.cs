using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Repository;

namespace Proyecto_Marathon2024.Controllers
{
    public class Tipo_TrabajoController : ControllerBase
    {
        private readonly Tipo_TrabajoRepository _tipo_Trabajo;
        public Tipo_TrabajoController(Tipo_TrabajoRepository tipo_TrabajoRepository)
        {
            _tipo_Trabajo = tipo_TrabajoRepository;
        }

        [HttpGet("GetListaTipo_Trabajo")]
        public async Task<IActionResult> GetListaTipo_Trabajo()
        {
            var respuesta = await _tipo_Trabajo.GetListaTp_Trabajo();
            return Ok(respuesta);
        }
    }


}
