using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Repository;
using Proyecto_Marathon2024.Services;

namespace Proyecto_Marathon2024.Controllers
{
    public class MarcaController : ControllerBase
    {
        private readonly MarcaRepository _Marca;

        public MarcaController(MarcaRepository MarcaRepository)
        {
            _Marca= MarcaRepository;
        }
        [HttpGet("GetListaMarca")]
        public async Task<IActionResult> GetLista_Marca()
        {
            var respuesta = await _Marca.GetListaMarca();
            return Ok(respuesta);
        }

    }
}