using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Proyecto2025_III.BD.Datos;
using Proyecto2025_III.BD.Datos.Entity;
using Proyecto2025_III.Repositorio.Repositorios;
using Proyecto2025_III.Shared.Constantes;

namespace Proyecto2025_III.Server.Controllers
{
    [ApiController]
    [Route("api/TipoProvincia")]
    public class TipoProvinciaController : ControllerBase
    {
        private readonly ITipoProvinciaRepositorio repositorio;
        private readonly IOutputCacheStore outputCacheStore;

        private const string cacheKey = "TipoProvinciasCache";

        public TipoProvinciaController(ITipoProvinciaRepositorio repositorio,
                                       IOutputCacheStore outputCacheStore )
        {
            this.repositorio = repositorio;
            this.outputCacheStore = outputCacheStore;
        }

        [HttpGet] //api/TipoProvincia
        [AllowAnonymous]
        [OutputCache(Tags = new[] { cacheKey})]
        public async Task<ActionResult<List<TipoProvincia>>> Get()
        {
            var tipoProvincias = await repositorio.Select();
            //var tipoProvincias = await context.TipoProvincias.ToListAsync();
            if (tipoProvincias == null)
            {
                return NotFound("No se encontraron tipos de provincia, VERIFICAR.");
            }
            if (tipoProvincias.Count == 0)
            {
                return Ok("No existe tipos de provincia en este momento.");
            }
            var encabezadoCache = $"public,max-age={ConstantesGlobales.DuracionCacheEnSegundos}";
            Response.Headers["Cache-Control"] = encabezadoCache;
            return Ok(tipoProvincias);
        }

        [HttpGet("{id:int}")]  //api/TipoProvincia/5
        public async Task<ActionResult<TipoProvincia>> Get(int id)
        {
            var tipoProvincia = await repositorio.SelectById(id);
            //var tipoProvincia = await context.TipoProvincias.FirstOrDefaultAsync(x => x.Id == id);
            if (tipoProvincia is null)
            {
                return NotFound($"No existe el tipo de provincia con el id: {id}.");
            }

            return Ok(tipoProvincia);
        }

        [HttpGet("bycod/{cod}")]  //api/TipoProvincia/PRU
        public async Task<ActionResult<TipoProvincia>> Get(string cod)
        {
            var entidad = repositorio.SelectByCod(cod);
            if (entidad is null)
            {
                return NotFound($"No existe el registro con el codigo: {cod}.");
            }

            return Ok(entidad);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(TipoProvincia DTO)
        {
            try
            {     
                await repositorio.Insert(DTO);
                //await context.TipoProvincias.AddAsync(DTO);
                //await context.SaveChangesAsync();
                await outputCacheStore.EvictByTagAsync(cacheKey, default);
                return Ok(DTO.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el tipo de provincia: {e.Message}");
            }
        }

        [HttpPut("{id:int}")]  // api/TipoProvincia/6
        public async Task<ActionResult> Put(int id, TipoProvincia DTO)
        {
            //if (id != DTO.Id)
            //{
            //    return BadRequest("Datos no validos.");
            //}
            //var existe = await repositorio.Existe(id);
            //var existe = await context.TipoProvincias.AnyAsync(x => x.Id == id);
            //if (!existe)
            //{
            //    return NotFound($"No existe el tipo de provincia con el id: {id}.");
            //}
            //context.Update(DTO);
            //await context.SaveChangesAsync();
            var resultado = await repositorio.Update(id, DTO);
            if (!resultado)
            {
                return BadRequest("Datos no validos o el tipo de provincia no existe.");
            }
            await outputCacheStore.EvictByTagAsync(cacheKey, default);
            return Ok($"Tipo de provincia con el id: {id} actualizado correctamente.");
        }

        [HttpDelete("{id:int}")]  // api/TipoProvincia/6
        public async Task<ActionResult> Delete(int id)
        {
            //var existe = await context.TipoProvincias.AnyAsync(x => x.Id == id);
            //if (existe == false)
            //{
            //    return NotFound($"No existe el tipo de provincia con el id: {id}.");
            //}
            //var tipoProvincia = await context.TipoProvincias.FirstOrDefaultAsync(x => x.Id == id);
            //if (tipoProvincia is null)
            //{
            //    return NotFound($"No existe el tipo de provincia con el id: {id}.");
            //}
            //context.TipoProvincias.Remove(tipoProvincia);
            //await context.SaveChangesAsync();
            var flag = await repositorio.Delete(id);
            if (!flag)
            {
                return NotFound($"No existe el registro con el id: {id} o ya fue eliminado.");
            }
            await outputCacheStore.EvictByTagAsync(cacheKey, default);
            return Ok($"Registro con el id: {id} eliminado correctamente.");
        }
    }
}
