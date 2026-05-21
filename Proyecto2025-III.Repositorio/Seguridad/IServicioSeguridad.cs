using Proyecto2025_III.Shared.DTO;
using Proyecto2025_III.Shared.ENUM;

namespace Proyecto2025_III.Repositorio.Seguridad;

public interface IServicioSeguridad
{
    Task<ResultadoOperacionSeguridad> HacerAdmin(UsuarioDTO entidad);
    Task<ResultadoOperacionSeguridad> RemoverAdmin(string email);
    Task<List<UsuarioDTO>> ObtenerUsuarios();
}
