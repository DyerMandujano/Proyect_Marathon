using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Repository;
using Proyecto_Marathon2024.Services;
using System.Text;

namespace Proyecto_Marathon2024.Controllers
{
    public class Perfil_PersonalController:ControllerBase
    {
        //private readonly IPerfil_Personal _perfil_Personal;
        private readonly Perfil_PersonalRepository _perfil_Personal;

        //public Perfil_PersonalController(IPerfil_Personal perfil_PersonalRepository)
        //{
        //    _perfil_Personal = perfil_PersonalRepository;
        //}

        public Perfil_PersonalController(Perfil_PersonalRepository perfil_PersonalRepository)
        {
            _perfil_Personal = perfil_PersonalRepository;
        }

        [HttpGet("GetListaPerfil_Personal")]
        public async Task<IActionResult> GetListaPerfil_Personal()
        {
            var respuesta= await _perfil_Personal.GetListaPerfil_Personal();
            return Ok(respuesta);
        }

        [HttpGet("GetPerfil_PersoID/{id}")]
        public async Task<IActionResult> GetPfPerso_porID(int id)
        {
            var respuesta = await _perfil_Personal.GetPerfilPerso_PorID(id);
            if(respuesta != null)
            {
                return Ok(respuesta);
            }else
            {
                return NotFound($"Perfil con ID {id} no encontrado.");
            }
        }

        [HttpPost("InsertPerfil_Personal")]

        public async Task<IActionResult> InsertarPerfilPersonal([FromBody]Perfil_Personal pf_perso)
        {
            var resultado = await _perfil_Personal.InsertPerfilPerso(pf_perso);
            return Ok(resultado); 
        }

        [HttpPost("UpdatePerfil_Personal/{id}")]

        public async Task<IActionResult> UpdatePerfilPersonal(int id, [FromBody]Perfil_Personal pf_perso)
        {
            var resultado = await _perfil_Personal.UpdatePerfilPerso_porID(id,pf_perso);
            return Ok(resultado); 
        }

        [HttpPost("DeletePerfil_Personal/{id}")]

        public async Task<IActionResult> DeletePerfilPerso_porID(int id)
        {
            var resultado = await _perfil_Personal.DeletePerfilPerso_porID(id);
            return Ok(resultado); 
        }

    }
}
