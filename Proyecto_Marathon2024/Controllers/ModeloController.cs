using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Repository;
using Proyecto_Marathon2024.Services;

namespace Proyecto_Marathon2024.Controllers
{
    public class ModeloController : ControllerBase
    {
        private readonly ModeloRepository _Modelo;

        public ModeloController(ModeloRepository ModeloRepository)
        {
            _Modelo= ModeloRepository;
        }
        [HttpGet("GetListaModelo")]
        public async Task<IActionResult> GetLista_Modelo()
        {
            var respuesta = await _Modelo.GetListaModelo();
            return Ok(respuesta);
        }

    }
}