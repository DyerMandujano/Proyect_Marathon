using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Repository;
using Proyecto_Marathon2024.Services;

namespace Proyecto_Marathon2024.Controllers
{
    public class TallaController : ControllerBase
    {
        private readonly TallaRepository _Talla;

        public TallaController(TallaRepository TallaRepository)
        {
            _Talla= TallaRepository;
        }
        [HttpGet("GetListaTalla")]
        public async Task<IActionResult> GetLista_Talla()
        {
            var respuesta = await _Talla.GetListaTalla();
            return Ok(respuesta);
        }

    }
}