using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Repository;

namespace Proyecto_Marathon2024.Controllers
{
    public class LocalMTController : ControllerBase
    {
        private readonly LocalMTRepository _localMT;
        public LocalMTController(LocalMTRepository localMTRepository)
        {
            _localMT = localMTRepository;
        }

        [HttpGet("GetListaLocalMT")]
        public async Task<IActionResult> GetListaLocal()
        {
            var respuesta = await _localMT.GetListaLocal();
            return Ok(respuesta);
        }
    }
}
