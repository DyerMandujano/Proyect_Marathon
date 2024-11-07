using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Repository;

namespace Proyecto_Marathon2024.Controllers
{
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepository _Cliente;

        public ClienteController(ClienteRepository ClienteRepository)
        {
            _Cliente = ClienteRepository;
        }

        [HttpGet("GetListaCliente")]
        public async Task<IActionResult> GetListaCliente()
        {
            var respuesta = await _Cliente.GetListaCliente();
            return Ok(respuesta);
        }

        [HttpGet("GetClienteID/{dni}")]
        public async Task<IActionResult> GetClienteDni(string dni)
        {
            var respuesta = await _Cliente.GetClienteDni(dni);
            if (respuesta != null)
            {
                return Ok(respuesta);
            }
            else
            {
                return NotFound($"Cliente con ID {dni} no encontrado.");
            }
        }

        [HttpPost("InsertCliente")]
        public async Task<IActionResult> InsertarCliente([FromBody] Cliente clie)
        {
            var resultado = await _Cliente.InsertCliente(clie);
            return Ok(resultado);
        }

        [HttpPost("UpdateCliente/{dni}")]
        public async Task<IActionResult> UpdateCliente(string dni, [FromBody] Up_Cliente clie)
        {
            var resultado = await _Cliente.UpdateClienteDni(dni, clie);
            return Ok(resultado);
        }

        [HttpPost("Delete_Cliente/{dni}")]
        public async Task<IActionResult> DesactivarClienteDNI(string dni)
        {
            var resultado = await _Cliente.DesactivarClienteDNI(dni);
            return Ok(resultado);
        }
    }
}

