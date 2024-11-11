using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Repository;

namespace Proyecto_Marathon2024.Controllers
{
    public class MembresiaController : ControllerBase
    {
        private readonly MembresiaRepository _Membresia;

        public MembresiaController(MembresiaRepository MembresiaRepository)
        {
            _Membresia = MembresiaRepository;
        }

        [HttpGet("GetListaMembresia")]
        public async Task<IActionResult> GetListaMembresia()
        {
            var respuesta = await _Membresia.GetListaMembresia();
            return Ok(respuesta);
        }
    }
}