using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Repository;
using Proyecto_Marathon2024.Services;

namespace Proyecto_Marathon2024.Controllers
{
    public class ColorController : ControllerBase
    {
        private readonly ColorRepository _Color;

        public ColorController(ColorRepository ColorRepository)
        {
            _Color= ColorRepository;
        }
        [HttpGet("GetListaColor")]
        public async Task<IActionResult> GetLista_Color()
        {
            var respuesta = await _Color.GetListaColor();
            return Ok(respuesta);
        }

    }
}