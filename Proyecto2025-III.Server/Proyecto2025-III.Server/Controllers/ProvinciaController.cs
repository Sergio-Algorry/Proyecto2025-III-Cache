using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2025_III.BD.Datos;
using Proyecto2025_III.BD.Datos.Entity;
using Proyecto2025_III.Repositorio.Repositorios;
using Proyecto2025_III.Shared.DTO;
using Proyecto2025_III.Shared.ENUM;

namespace Proyecto2025_III.Server.Controllers
{
    [ApiController]
    [Route("api/provincia")]
    public class ProvinciaController : ControllerBase
    {
        private readonly IProvinciaRepositorio repositorio;

        public ProvinciaController(IProvinciaRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet] //api/Provincia
        public async Task<ActionResult<List<Provincia>>> GetFull()
        {
            var lista = await repositorio.Select();
            if (lista == null)
            {
                return NotFound("No se encontro elementos de la lista, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("Lista sin registros.");
            }

            return Ok(lista);
        }

        //[HttpGet("listapais")] //api/listapais
        //public async Task<ActionResult<List<ProvinciaListadoDTO>>> ListaProvincia()
        //{
        //    var lista = await repositorio.SelectListaProvincia();
        //    if (lista == null)
        //    {
        //        return NotFound("No se encontro elementos de la lista, VERIFICAR.");
        //    }
        //    if (lista.Count == 0)
        //    {
        //        return Ok("Lista sin registros.");
        //    }

        //    return Ok(lista);
        //}

        [HttpGet("{id:int}")]  //api/Provincia/5
        public async Task<ActionResult<Provincia>> GetById(int id)
        {
            var entidad = await repositorio.SelectById(id);
            if (entidad is null)
            {
                return NotFound($"No existe el registro con id: {id}.");
            }

            return Ok(entidad);
        }

        [HttpGet("SelectByIGM_Id/{cod}")] // api/Provincia/GetByAlpha3Code/ARG 
        public async Task<ActionResult<Provincia>> SelectByIGM_Id(string cod)
        {
            var entidad = await repositorio.SelectByIGM_Id(cod);
            if (entidad is null)
            {
                return NotFound($"No existe registro con el codigo: {cod}.");
            }

            return Ok(entidad);
        }

        [HttpGet("SelectByIGM_IdResumen/{cod}")] // api/Provincia/GetByCodIndec/ARG
        public async Task<ActionResult<ProvinciaResumenDTO>> SelectByIGM_IdResumen(string cod)
        {
            var entidad = await repositorio.ResumenProvincia(cod);
            if (entidad is null)
            {
                return NotFound($"No existe registro con el codigo: {cod}.");
            }

            return Ok(entidad);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(ProvinciaCrearDTO DTO)
        {
            try
            {                
                Provincia entidad = new Provincia
                {
                    PaisId = DTO.PaisId,
                    TipoProvinciaId = DTO.TipoProvinciaId,
                    IGM_Id = DTO.IGM_Id,
                    NombreProvincia = DTO.NombreProvincia,
                    EstadoRegistro = EnumEstadoRegistro.activo
                };
                var id = await repositorio.Insert(entidad);
                return Ok(entidad.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el registro: {e.Message}");
            }
        }

        [HttpPut("{id:int}")]  // api/Provincia/6
        public async Task<ActionResult> Put(int id, Provincia DTO)
        {
            var flag = await repositorio.Update(id, DTO);
            if (!flag)
            {
                return BadRequest("Datos no validos o el registro no existe.");
            }
            return Ok($"Registro con el id: {id} actualizado correctamente.");
        }

        [HttpDelete("{id:int}")]  // api/Provincia/6
        public async Task<ActionResult> Delete(int id)
        {
            var flag = await repositorio.Delete(id);
            if (!flag)
            {
                return NotFound($"No existe el registro con el id: {id} o ya fue eliminado.");
            }

            return Ok($"Registro con el id: {id} eliminado correctamente.");
        }
    }
}
