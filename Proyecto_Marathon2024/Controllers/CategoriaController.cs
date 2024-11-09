using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Repository;
using Proyecto_Marathon2024.Services;

namespace Proyecto_Marathon2024.Controllers
{
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaRepository _Categoria;

        public CategoriaController(CategoriaRepository CategoriaRepository)
        {
            _Categoria = CategoriaRepository;
        }
        [HttpGet("GetListaCategoria")]
        public async Task<IActionResult> GetLista_Categoria()
        {
            var respuesta = await _Categoria.GetListaCategoria();
            return Ok(respuesta);
        }

    }
}