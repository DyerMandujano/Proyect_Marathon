﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Repository;
using Proyecto_Marathon2024.Services;

namespace Proyecto_Marathon2024.Controllers
{
    public class ProductoController : ControllerBase
    {
        private readonly ProductoRepository _Producto;

        public ProductoController(ProductoRepository ProductoRepository)
        {
            _Producto = ProductoRepository;
        }
        [HttpGet("GetListaProducto")]
        public async Task<IActionResult> GetLista_Producto()
        {
            var respuesta = await _Producto.GetListaProducto();
            return Ok(respuesta);
        }

        [HttpGet("GetProductoID/{id}")]
        public async Task<IActionResult> GetProducto_porID(int id)
        {
            var respuesta = await _Producto.GetProducto_PorID(id);
            if (respuesta != null)
            {
                return Ok(respuesta);
            }
            else
            {
                return NotFound($"Producto con ID {id} no encontrado.");
            }
        }

        [HttpGet("GetListaProductoPerso")]
        public async Task<IActionResult> GetLista_ProductoPerso()
        {
            var respuesta = await _Producto.GetListaProductoPerso();
            return Ok(respuesta);
        }
        [HttpGet("GetListaProductoInic")]
        public async Task<IActionResult> GetLista_ProductoInic()
        {
            var respuesta = await _Producto.GetListaProductoInic();
            return Ok(respuesta);
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

