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
            _Categoria= CategoriaRepository;
        }
        [HttpGet("GetListaCategoria")]
        public async Task<IActionResult> GetListaPerfil_Personal()
        {
            var respuesta = await _Categoria.GetListaCategoria();
            return Ok(respuesta);
        }

        [HttpGet("GetCategoriaID/{id}")]
        public async Task<IActionResult> GetCategoria_porID(int id)
        {
            var respuesta = await _Categoria.GetCategoria_PorID(id);
            if (respuesta != null)
            {
                return Ok(respuesta);
            }
            else
            {
                return NotFound($"Producto con ID {id} no encontrado.");
            }
        }

        [HttpPost("InsertProducto")]
        public async Task<IActionResult> InsertarProducto([FromBody] Producto prod)
        {
            var resultado = await _Producto.SP_InsertProducto(prod);
            return Ok(resultado);
        }

        [HttpPost("UpdateProducto/{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] Producto prod)
        {
            var resultado = await _Producto.UpdateProducto_porID(id, prod);
            return Ok(resultado);
        }

        [HttpPost("Delete_Producto/{id}")]
        public async Task<IActionResult> DeleteProducto_porID(int id)
        {
            var resultado = await _Producto.DeleteProducto_porID(id);
            return Ok(resultado);
        }
    }
}