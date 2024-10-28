using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Repository;
using Proyecto_Marathon2024.Services;

namespace Proyecto_Marathon2024.Controllers
{

    public class PersonalController : ControllerBase
    {
        private readonly PersonalRepository _Personal;

        public PersonalController(PersonalRepository PersonalRepository)
        {
            _Personal = PersonalRepository;
        }

        [HttpGet("GetListaPersonal")]
        public async Task<IActionResult> GetListaPerfil_Personal()
        {
            var respuesta = await _Personal.GetListaPersonal();
            return Ok(respuesta);
        }

        [HttpGet("GetPersonalID/{dni}")]
        public async Task<IActionResult> GetPersonal_porDni(string dni)
        {
            var respuesta = await _Personal.GetPersonal_PorDni(dni);
            if (respuesta != null)
            {
                return Ok(respuesta);
            }
            else
            {
                return NotFound($"Personal con ID {dni} no encontrado.");
            }
        }

        [HttpPost("InsertPersonal")]
        public async Task<IActionResult> InsertarPersonal([FromBody] Personal perso)
        {
            var resultado = await _Personal.InsertPersonal(perso);
            return Ok(resultado);
        }

        [HttpPost("UpdatePersonal/{dni}")]
        public async Task<IActionResult> UpdatePersonal(string dni, [FromBody] Personal perso)
        {
            var resultado = await _Personal.UpdatePersonal_porDni(dni, perso);
            return Ok(resultado);
        }

        [HttpPost("Delete_Personal/{dni}")]
        public async Task<IActionResult> DeletePersonal_porDni(string dni)
        {
            var resultado = await _Personal.DeletePersonal_porDni(dni);
            return Ok(resultado);
        }
    }
}
