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
    [Route("api/pais")]
    public class PaisController : ControllerBase
    {
        private readonly IPaisRepositorio repositorio;

        public PaisController(IPaisRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet] //api/Pais
        public async Task<ActionResult<List<Pais>>> GetFull()
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

        [HttpGet("listapais")] //api/Pais/listapais
        public async Task<ActionResult<List<PaisListadoDTO>>> ListaPais()
        {
            var lista = await repositorio.SelectListaPais();
            if (lista == null)
            {
                return NotFound("No se encontro elementos de la lista, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return NotFound("Lista sin registros.");
            }

            return Ok(lista);
        }

        [HttpGet("{id:int}")]  //api/Pais/5
        public async Task<ActionResult<Pais>> GetById(int id)
        {
            var entidad = await repositorio.SelectById(id);
            if (entidad is null)
            {
                return NotFound($"No existe el registro con id: {id}.");
            }

            return Ok(entidad);
        }

        [HttpGet("GetByAlpha3Code/{cod}")] // api/Pais/GetByAlpha3Code/ARG 
        public async Task<ActionResult<Pais>> GetByAlpha3Code(string cod)
        {
            var entidad = await repositorio.SelectByAlpha3Code(cod);
            if (entidad is null)
            {
                return NotFound($"No existe registro con el codigo: {cod}.");
            }

            return Ok(entidad);
        }

        [HttpGet("GetByCodIndec/{cod}")] // api/Pais/GetByCodIndec/ARG
        public async Task<ActionResult<Pais>> GetByCodIndec(string cod)
        {
            var entidad = await repositorio.SelectByCodIndec(cod);
            if (entidad is null)
            {
                return NotFound($"No existe registro con el codigo: {cod}.");
            }

            return Ok(entidad);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(PaisDTO DTO)
        {
            try
            {                
                Pais entidad = new Pais
                {
                    NombrePais = DTO.NombrePais,
                    CodIndec = DTO.CodIndec,
                    CodLlamada = DTO.CodLlamada,
                    Alpha3Code = DTO.Alpha3Code,
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

        [HttpPut("{id:int}")]  // api/Pais/6
        public async Task<ActionResult> Put(int id, Pais DTO)
        {
            var flag = await repositorio.Update(id, DTO);
            if (!flag)
            {
                return BadRequest("Datos no validos o el registro no existe.");
            }
            return Ok($"Registro con el id: {id} actualizado correctamente.");
        }

        [HttpDelete("{id:int}")]  // api/Pais/6
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
