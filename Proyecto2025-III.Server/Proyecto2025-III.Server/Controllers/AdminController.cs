using Microsoft.AspNetCore.Mvc;
using Proyecto2025_III.Repositorio.Seguridad;
using Proyecto2025_III.Shared.DTO;
using Proyecto2025_III.Shared.ENUM;

namespace Proyecto2025_III.Server.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IServicioSeguridad repositorio;

        public AdminController(IServicioSeguridad repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet("obtenerusuarios")] //api/
        public async Task<ActionResult<List<UsuarioDTO>>> ObtenerUsuarios()
        {
            var lista = await repositorio.ObtenerUsuarios(); ;
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

        [HttpPost("haceradmin")]
        public async Task<ActionResult<ResultadoOperacionSeguridad>> HacerAdmin(UsuarioDTO usuario)
        {
            try
            {
                var resultado = await repositorio.HacerAdmin(usuario);

                switch (resultado)
                {
                    case ResultadoOperacionSeguridad.Exitoso:
                        return Ok(resultado);
                        break;
                    case ResultadoOperacionSeguridad.Fallido:
                        return BadRequest("Error al crear el registro, VERIFICAR.");
                    case ResultadoOperacionSeguridad.NoEncontrado:
                        return NotFound($"No se encontro el usuario con el email: {usuario.Email}.");
                    case ResultadoOperacionSeguridad.SinPermiso:
                        return BadRequest("No tienes permiso para realizar esta acción.");
                    default:
                        return BadRequest("Resultado desconocido, VERIFICAR.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el registro: {e.Message}");
            }
        }

        [HttpDelete("removeradmin/{email}")]
        public async Task<ActionResult> RemoverAdmin(string email)
        {
            var resultado = await repositorio.RemoverAdmin(email);
            switch (resultado)
            {
                case ResultadoOperacionSeguridad.Exitoso:
                    return Ok();
                case ResultadoOperacionSeguridad.Fallido:
                    return BadRequest("Error al eliminar el registro, VERIFICAR.");
                case ResultadoOperacionSeguridad.NoEncontrado:
                    return NotFound($"No se encontro el usuario con el email: {email}.");
                case ResultadoOperacionSeguridad.SinPermiso:
                    return BadRequest("No tienes permiso para realizar esta acción.");
                default:
                    return BadRequest("Resultado desconocido, VERIFICAR.");
            }
        }
    }
}
